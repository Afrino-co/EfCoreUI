using CliWrap;
using CliWrap.Buffered;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Shapes;
using VSLangProj;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TestMenu
{
    public partial class Form1 : Form
    {
        public OprationModeEnum _oprationModeEnum { get; set; }
        public Form1()
        {

        }

        public Form1(DTE2 dTE2, OprationModeEnum oprationModeEnum)
        {
            InitializeComponent();
            _oprationModeEnum = oprationModeEnum;
            //var f = dTE2.Solution.Projects;
            //var solutionExplorer0 = dTE2.ToolWindows.SolutionExplorer.UIHierarchyItems;
            //var solutionExplorer = dTE2.ToolWindows.SolutionExplorer;
            //object[] items = solutionExplorer.SelectedItems as object[];
            //this.comboBox1.Items.AddRange(items.Select(s=>s as EnvDTE.UIHierarchyItem).Select(s=>s.Name).ToArray());
            if (oprationModeEnum == OprationModeEnum.AddMigration)
            {
                ModeAddMigration();
                this.textBox_migration_name.Text = "Initial";
            }
            else if (oprationModeEnum == OprationModeEnum.RemoveMigration)
            {
                ModeRemoveMigration();
            }
            else if (oprationModeEnum == OprationModeEnum.GenerateSqlScript)
            {
                ModeGenerateSqlScript();
            }
            else if (oprationModeEnum == OprationModeEnum.UpdateDatabase)
            {
                ModeUpdateDatabase();
                checkBoxUseDefaultConnection.Checked = true;
                comboBoxConnection.Enabled = false;
            }
            else if (oprationModeEnum == OprationModeEnum.DropDatabase)
            {
                ModeDropDatabase();

            }


            SetMigrationProjects(dTE2);

            SetStartUpProject(dTE2);

            SetBuildConfigurationComboBox();



        }


        private void SetStartUpProject(DTE2 dTE2)
        {
            // لیست پروژه‌های قابل اجرا را بگیرید
            List<Project> runnableProjects = GetRunnableProjects(dTE2);
            MemoryParameter.StartUpProjects = new List<Project>();

            // اطلاعات به ComboBox یا جای دیگری اضافه کنید
            foreach (Project project in runnableProjects)
            {
                MemoryParameter.StartUpProjects.Add(project);

                this.comboBoxStartupProject.Items.Add(project.Name);

            }

            SetSelectedStartUpProject(runnableProjects);
            comboBoxStartupProject_Validating(comboBoxStartupProject, new CancelEventArgs());

        }

        private void SetMigrationProjects(DTE2 dTE2)
        {
            List<Project> projects = GetProjects(dTE2);
            MemoryParameter.MigrationProjects = new List<Project>();

            // اطلاعات به ComboBox یا جای دیگری اضافه کنید
            foreach (Project project in projects)
            {
                MemoryParameter.MigrationProjects.Add(project);
                this.comboBoxMigrationProject.Items.Add(project.Name);


            }

            SetSelectedProjectFromSolutionExplorer(dTE2);
            comboBoxMigrationProject_Validating(comboBoxMigrationProject, new CancelEventArgs());
        }

        private void SetSelectedStartUpProject(List<Project> runnableProjects)
        {
            this.comboBoxStartupProject.SelectedItem = runnableProjects.FirstOrDefault().Name;

            MemoryParameter.StartUpSelectedProject = MemoryParameter.StartUpProjects.FirstOrDefault(w => w.Name == runnableProjects.FirstOrDefault().Name);

            if (_oprationModeEnum == OprationModeEnum.UpdateDatabase)
            {
                var connectionStrings = ExtractConnectionStringsFromJsonFiles(MemoryParameter.StartUpSelectedProject);
                if (connectionStrings.Count > 0)
                {
                    comboBoxConnection.Items.AddRange(connectionStrings.Select(s => s.Key + " | " + s.Value).ToArray());
                }
            }

            var targetFrameworks = GetTargetFrameworks(MemoryParameter.StartUpSelectedProject);
            if (targetFrameworks != null)
            {
                targetFrameworks.Add("<Default>");
                comboBoxTargetFramework.Items.AddRange(targetFrameworks.ToArray());
                comboBoxTargetFramework.SelectedItem = "<Default>";
            }
        }

        private void SetSelectedProjectFromSolutionExplorer(DTE2 dTE2)
        {
            var solutionExplorer = dTE2.ToolWindows.SolutionExplorer;
            object[] items = solutionExplorer.SelectedItems as object[];

            if (items != null && items.Length > 0)
            {
                foreach (object item in items)
                {
                    var selectedItem = item as UIHierarchyItem;

                    if (selectedItem != null)
                    {
                        // اگر نوع مورد انتخاب شده یک پروژه باشد
                        if (selectedItem.Object is Project project)
                        {
                            // اکنون شما می‌توانید با استفاده از متغیر project اقدامات خود را انجام دهید.
                            // مثلاً اسم پروژه را بدست آورید:
                            this.comboBoxMigrationProject.SelectedItem = project.Name;
                            MemoryParameter.MigrationSelectedProject = MemoryParameter.MigrationProjects.FirstOrDefault(w => w.Name == project.Name);

                        }
                    }
                }
            }

        }

        private void SetBuildConfigurationComboBox()
        {
            this.comboBoxBuildConfig.Items.AddRange(new object[] { "Debug", "Release" });
            this.comboBoxBuildConfig.SelectedItem = "Debug";
            MemoryParameter.BuildConfigSelectedItem = "Debug";
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxMigrationProject.SelectedItem.ToString()))
            {
                MemoryParameter.MigrationSelectedProject = MemoryParameter.MigrationProjects.FirstOrDefault(w => w.Name == comboBoxMigrationProject.SelectedItem.ToString());

                if (_oprationModeEnum == OprationModeEnum.AddMigration)
                {
                    MemoryParameter.MigrationRootPaths = GetProjectFolders(MemoryParameter.MigrationSelectedProject);
                    if (!MemoryParameter.MigrationRootPaths.Any(a => a.Key == "Migrations"))
                    {
                        //var x = MemoryParameter.MigrationSelectedProject.FullName.Replace(MemoryParameter.MigrationSelectedProject.Name + ".csproj", "Migrations");

                        MemoryParameter.MigrationRootPaths.Add("Migrations", "Migrations");
                    }

                    comboBoxMigrationFolder.Items.Clear();
                    comboBoxMigrationFolder.Items.AddRange(MemoryParameter.MigrationRootPaths.Select(s => s.Key).ToArray());
                    if (MemoryParameter.MigrationRootPaths.ContainsKey("Migrations"))
                    {
                        comboBoxMigrationFolder.SelectedItem = MemoryParameter.MigrationRootPaths.FirstOrDefault(f => f.Key.Contains("Migrations")).Key;

                    }
                    else
                    {
                        comboBoxMigrationFolder.SelectedItem = MemoryParameter.MigrationRootPaths.FirstOrDefault().Key;

                    }
                    comboBoxMigrationFolder_Validating(comboBoxMigrationFolder, new CancelEventArgs());
                }



                MemoryParameter.DbContextClassLst = GetDbContextClassNames(MemoryParameter.MigrationSelectedProject);
                if (MemoryParameter.DbContextClassLst != null && MemoryParameter.DbContextClassLst.Any())
                {
                    comboBoxDbContextClass.Items.Clear();
                    comboBoxDbContextClass.Items.AddRange(MemoryParameter.DbContextClassLst.Select(s => s.Key).ToArray());
                    comboBoxDbContextClass.SelectedItem = MemoryParameter.DbContextClassLst.FirstOrDefault().Key;
                    comboBoxDbContextClass_Validating(comboBoxDbContextClass, new CancelEventArgs());

                }
                else
                {
                    comboBoxDbContextClass.Items.Clear();

                    comboBoxDbContextClass_Validating(comboBoxDbContextClass, new CancelEventArgs());

                }


                if (_oprationModeEnum == OprationModeEnum.GenerateSqlScript)
                {
                    if (comboBoxDbContextClass.SelectedItem != null)
                    {
                        var migrationLst = GetMigrationFileByDbContextName(MemoryParameter.MigrationSelectedProject, comboBoxDbContextClass.SelectedItem.ToString());
                        if (migrationLst.Count >= 1)
                        {
                            comboBoxToMigration.Items.AddRange(migrationLst.OrderByDescending(o => o).ToArray());
                            comboBoxToMigration.SelectedItem = migrationLst.OrderByDescending(o => o).ToArray().FirstOrDefault();

                        }
                        if (migrationLst.Count >= 1)
                        {
                            migrationLst.Add("0");

                            comboBoxFromMigration.Items.AddRange(migrationLst.OrderBy(o => o).ToArray());
                            comboBoxFromMigration.SelectedItem = migrationLst.OrderBy(o => o).ToArray().FirstOrDefault();
                        }
                    }
                    else
                    {
                        comboBoxFromMigration.Items.Clear();
                        comboBoxToMigration.Items.Clear();
                    }
                }

                if (_oprationModeEnum == OprationModeEnum.UpdateDatabase)
                {
                    if (comboBoxDbContextClass.SelectedItem != null)
                    {
                        var migrationLst = GetMigrationFileByDbContextName(MemoryParameter.MigrationSelectedProject, comboBoxDbContextClass.SelectedItem.ToString());
                        if (migrationLst.Count >= 1)
                        {
                            migrationLst.Add("0");
                            comboBoxTargetMigration.Items.AddRange(migrationLst.OrderByDescending(o => o).ToArray());
                            comboBoxTargetMigration.SelectedItem = migrationLst.OrderByDescending(o => o).ToArray().FirstOrDefault();

                        }
                    }
                    else
                    {
                        comboBoxTargetMigration.Items.Clear();
                    }


                }

            }

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxStartupProject.SelectedItem.ToString()))
            {
                MemoryParameter.StartUpSelectedProject = MemoryParameter.StartUpProjects.FirstOrDefault(w => w.Name == comboBoxStartupProject.SelectedItem.ToString());
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDbContextClass.SelectedItem != null)
            {
                MemoryParameter.DbContextSelectedClass = MemoryParameter.DbContextClassLst.FirstOrDefault(w => w.Key == comboBoxDbContextClass.SelectedItem.ToString());
            }

            if (_oprationModeEnum == OprationModeEnum.UpdateDatabase)
            {
                if (comboBoxDbContextClass.SelectedItem != null)
                {
                    var migrationLst = GetMigrationFileByDbContextName(MemoryParameter.MigrationSelectedProject, comboBoxDbContextClass.SelectedItem.ToString());
                    if (migrationLst.Count >= 1)
                    {
                        migrationLst.Add("0");
                        comboBoxTargetMigration.Items.AddRange(migrationLst.OrderByDescending(o => o).ToArray());
                        comboBoxTargetMigration.SelectedItem = migrationLst.OrderByDescending(o => o).ToArray().FirstOrDefault();

                    }
                }
                else
                {
                    comboBoxTargetMigration.Items.Clear();
                }


            }

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxMigrationFolder.SelectedItem.ToString()))
            {
                MemoryParameter.MigrationSelectedRootPaths = MemoryParameter.MigrationRootPaths.FirstOrDefault(w => w.Key == comboBoxMigrationFolder.SelectedItem.ToString());
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            MemoryParameter.BuildConfigSelectedItem = comboBoxBuildConfig.SelectedItem.ToString();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxBuildConfig.Enabled = !checkBox1.Checked;
            MemoryParameter.BuildConfigSelectedItem = "Debug";
            comboBoxBuildConfig.SelectedItem = "Debug";
            comboBoxTargetFramework.Enabled = !checkBox1.Checked;
            comboBoxTargetFramework.SelectedItem = "<Default>";
        }


        private async void button1_Click(object sender, EventArgs e)
        {
            //if (_oprationModeEnum == OprationModeEnum.AddMigration)
            //{
            //    InvokeAllValidationAddMigration();
            //}
            bool formWithError = false;

            if (string.IsNullOrWhiteSpace(comboBoxMigrationProject.SelectedItem?.ToString()))
            { formWithError = true; }
            if (string.IsNullOrWhiteSpace(comboBoxStartupProject.SelectedItem?.ToString()))
            { formWithError = true; }
            if (string.IsNullOrWhiteSpace(comboBoxDbContextClass.SelectedItem?.ToString()))
            { formWithError = true; }

            if (_oprationModeEnum == OprationModeEnum.AddMigration)
            {
                if (string.IsNullOrWhiteSpace(textBox_migration_name?.Text))
                { formWithError = true; }
                if (string.IsNullOrWhiteSpace(comboBoxMigrationFolder.SelectedItem?.ToString()))
                { formWithError = true; }

            }

            if (_oprationModeEnum == OprationModeEnum.GenerateSqlScript)
            {
                if (string.IsNullOrWhiteSpace(comboBoxFromMigration.SelectedItem?.ToString()))
                { formWithError = true; }
                if (string.IsNullOrWhiteSpace(comboBoxToMigration.SelectedItem?.ToString()))
                { formWithError = true; }
                if (string.IsNullOrWhiteSpace(textBoxScript.Text?.ToString()))
                { formWithError = true; }
            }

            if (_oprationModeEnum == OprationModeEnum.UpdateDatabase)
            {
                if (string.IsNullOrWhiteSpace(comboBoxTargetMigration.SelectedItem?.ToString()))
                { formWithError = true; }

                if (!checkBoxUseDefaultConnection.Checked)
                {
                    if (string.IsNullOrWhiteSpace(comboBoxConnection.SelectedItem?.ToString()))
                    { formWithError = true; }
                }

            }

            if (!ValidateChildren(ValidationConstraints.Enabled) || formWithError)
            {
                MessageBox.Show("There is an issue with the form validations.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var cmd = AggregateEfCommand();

            await ExecuteEfCommand(MemoryParameter.MigrationSelectedProject.FullName.Replace(MemoryParameter.MigrationSelectedProject.Name + ".csproj", ""), cmd);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            var cmd = AggregateEfCommand();
            form3.textBox1.Text = "dotnet " + cmd;
            form3.ShowDialog();
        }

        #region Validations
        //private void InvokeAllValidationAddMigration()
        //{
        //    textBox_migration_name_Validating1(textBox_migration_name, new CancelEventArgs());

        //    comboBoxMigrationProject_Validating1(comboBoxMigrationProject, new CancelEventArgs());

        //    comboBoxStartupProject_Validating1(comboBoxStartupProject, new CancelEventArgs());

        //    comboBoxDbContextClass_Validating1(comboBoxDbContextClass, new CancelEventArgs());
        //    comboBoxMigrationFolder_Validating1(comboBoxMigrationFolder, new CancelEventArgs());

        //}



        private void textBox_migration_name_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_migration_name?.Text))
            {
                //e.Cancel = true;
                //textBox_migration_name.Focus();
                errorProvider_migration_name.SetError(textBox_migration_name, "This field is Required");

            }
            else
            {
                //e.Cancel = false;
                errorProvider_migration_name.SetError(textBox_migration_name, "");
            }
        }

        private void comboBoxMigrationProject_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBoxMigrationProject.SelectedItem?.ToString()))
            {
                //e.Cancel = true;
                //comboBoxMigrationProject.Focus();
                errorProvider_comboBoxMigrationProject.SetError(comboBoxMigrationProject, "This field is Required");

            }
            else
            {
                //e.Cancel = false;
                errorProvider_comboBoxMigrationProject.SetError(comboBoxMigrationProject, "");
            }
        }

        private void comboBoxStartupProject_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBoxStartupProject.SelectedItem?.ToString()))
            {
                //e.Cancel = true;
                //comboBoxStartupProject.Focus();
                errorProvider_comboBoxStartupProject.SetError(comboBoxStartupProject, "This field is Required");
            }
            else
            {
                //e.Cancel = false;
                errorProvider_comboBoxStartupProject.SetError(comboBoxStartupProject, "");
            }
        }

        private void comboBoxDbContextClass_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBoxDbContextClass.SelectedItem?.ToString()))
            {
                //e.Cancel = true;
                //comboBoxDbContextClass.Focus();
                errorProvider_comboBoxDbContextClass.SetError(comboBoxDbContextClass, "This field is Required");
            }
            else
            {
                //e.Cancel = false;
                errorProvider_comboBoxDbContextClass.SetError(comboBoxDbContextClass, "");
            }
        }

        private void comboBoxMigrationFolder_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBoxMigrationFolder.SelectedItem?.ToString()))
            {
                //e.Cancel = true;
                //comboBoxMigrationFolder.Focus();
                errorProvider_comboBoxMigrationFolder.SetError(comboBoxMigrationFolder, "This field is Required");
            }
            else
            {
                //e.Cancel = false;
                errorProvider_comboBoxMigrationFolder.SetError(comboBoxMigrationFolder, "");
            }
        }

        private void comboBoxFromMigration_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBoxFromMigration.SelectedItem?.ToString()))
            {
                //e.Cancel = true;
                //comboBoxFromMigration.Focus();
                errorProvider_comboBoxFromMigration.SetError(comboBoxFromMigration, "This field is Required");
            }
            else
            {
                //e.Cancel = false;
                errorProvider_comboBoxFromMigration.SetError(comboBoxFromMigration, "");
            }
        }

        private void comboBoxToMigration_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBoxToMigration.SelectedItem?.ToString()))
            {
                //e.Cancel = true;
                //comboBoxToMigration.Focus();
                errorProvider_comboBoxToMigration.SetError(comboBoxToMigration, "This field is Required");
            }
            else
            {
                //e.Cancel = false;
                errorProvider_comboBoxToMigration.SetError(comboBoxToMigration, "");
            }
        }

        private void textBoxScript_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxScript.Text?.ToString()))
            {
                //e.Cancel = true;
                //textBoxScript.Focus();
                errorProvider_textBoxScript.SetError(textBoxScript, "This field is Required");
            }
            else
            {
                //e.Cancel = false;
                errorProvider_textBoxScript.SetError(textBoxScript, "");
            }
        }

        private void comboBoxTargetMigration_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBoxTargetMigration.SelectedItem?.ToString()))
            {
                //e.Cancel = true;
                //comboBoxTargetMigration.Focus();
                errorProvider_comboBoxTargetMigration.SetError(comboBoxTargetMigration, "This field is Required");
            }
            else
            {
                //e.Cancel = false;
                errorProvider_comboBoxTargetMigration.SetError(comboBoxTargetMigration, "");
            }
        }

        private void comboBoxConnection_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBoxConnection.SelectedItem?.ToString()))
            {
                //e.Cancel = true;
                //comboBoxConnection.Focus();
                errorProvider_comboBoxConnection.SetError(comboBoxConnection, "This field is Required");
            }
            else
            {
                //e.Cancel = false;
                errorProvider_comboBoxConnection.SetError(comboBoxConnection, "");
            }
        }







        #endregion

        private static List<Project> GetProjects(DTE2 dte)
        {
            List<Project> projects = new List<Project>();

            foreach (Project project in dte.Solution.Projects)
            {
                GetProjectsRecursive(project, projects);
            }

            return projects;
        }

        private Dictionary<string, string> GetDbContextClassNames(Project project)
        {
            Dictionary<string, string> dbContextClassNames = new Dictionary<string, string>();

            foreach (CodeClass codeClass in GetClassesInProject(project))
            {
                if (IsDbContextSubclass(codeClass))
                {
                    dbContextClassNames.Add(codeClass.Name, codeClass.FullName);
                }
            }

            return dbContextClassNames;
        }

        private static IEnumerable<CodeClass> GetClassesInProject(Project project)
        {
            List<CodeClass> classes = new List<CodeClass>();
            foreach (ProjectItem projectItem in project.ProjectItems)
            {
                GetClassesInProjectItem(projectItem, classes);
            }
            return classes;
        }

        static List<string> GetTargetFrameworks(Project project)
        {
            // دریافت پروژه فعال در محیط Visual Studio

            if (project != null)
            {
                string tf = project.Properties.Item("FriendlyTargetFramework").Value.ToString();
                var result = tf.Split(";".ToCharArray()).ToList();
                return result;
            }
            return null;
        }

        private static void GetClassesInProjectItem(ProjectItem projectItem, List<CodeClass> classes)
        {
            if (projectItem.FileCodeModel != null)
            {
                foreach (CodeElement codeElement in projectItem.FileCodeModel.CodeElements)
                {
                    if (codeElement is CodeNamespace codeNamespace)
                    {
                        foreach (CodeElement innerCodeElement in codeNamespace.Members)
                        {
                            if (innerCodeElement is CodeClass codeClass)
                            {
                                classes.Add(codeClass);
                            }
                        }
                    }
                    else if (codeElement is CodeClass codeClass)
                    {
                        classes.Add(codeClass);
                    }
                }
            }

            foreach (ProjectItem nestedProjectItem in projectItem.ProjectItems)
            {
                GetClassesInProjectItem(nestedProjectItem, classes);
            }
        }

        public static Dictionary<string, string> GetProjectFolders(Project Project)
        {
            Dictionary<string, string> projectFolders = new Dictionary<string, string>();
            //Project.FullName.Replace(Project.Name + ".csproj", "")
            // جمع‌آوری اطلاعات فولدرها به صورت دیکشنری
            CollectProjectFolders(Project.ProjectItems, projectFolders, "");

            return projectFolders;
        }

        private static void CollectProjectFolders(ProjectItems projectItems, Dictionary<string, string> projectFolders, string parentPath)
        {
            foreach (ProjectItem projectItem in projectItems)
            {
                if (projectItem.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFolder)
                {
                    // اضافه کردن اطلاعات فولدر به دیکشنری
                    string folderName = projectItem.Name;
                    string folderPath = System.IO.Path.Combine(parentPath, folderName);
                    //projectFolders.Add(folderName, folderPath);
                    projectFolders.Add(folderPath, folderPath);

                    // فراخوانی بازگشتی برای پیمایش زیرشاخه‌ها
                    CollectProjectFolders(projectItem.ProjectItems, projectFolders, folderPath);
                }
            }
        }

        private static void GetProjectsRecursive(Project project, List<Project> projects)
        {
            if (project.Kind != ProjectKinds.vsProjectKindSolutionFolder)
            {
                projects.Add(project);
            }
            else
            {
                // اگر پوشه‌ای از نوع Solution Folder باشد، به صورت بازگشتی پروژه‌های داخلی آن را بگیرید
                foreach (ProjectItem item in project.ProjectItems)
                {
                    if (item.SubProject != null)
                    {
                        GetProjectsRecursive(item.SubProject, projects);
                    }
                }
            }
        }

        private static List<Project> GetRunnableProjects(DTE2 dte)
        {
            List<Project> runnableProjects = new List<Project>();

            foreach (Project project in dte.Solution.Projects)
            {
                if (IsRunnableProject(project))
                {
                    runnableProjects.Add(project);
                }
            }

            return runnableProjects;
        }

        private static bool IsRunnableProject(Project project)
        {
            try
            {
                // چک کنید که پروژه قابل اجراست (وبی یا ویندوزی)
                return project.Properties.Item("OutputType").Value.ToString() == "1"  // Windows Application
                    || project.Properties.Item("OutputType").Value.ToString() == "3"; // Exe (Console Application)
            }
            catch
            {
                // اگر خطا رخ داد، به عنوان یک پروژه قابل اجرا در نظر گرفته نشود
                return false;
            }
        }

        private bool IsDbContextSubclass(CodeClass codeClass)
        {
            foreach (CodeElement baseClass in codeClass.Bases)
            {
                if (baseClass.FullName == "System.Data.Entity.DbContext")
                {
                    return true;
                }
                else if (baseClass.FullName == "Microsoft.EntityFrameworkCore.DbContext")
                {
                    return true;
                }
            }

            return false;

        }

        public string AggregateEfCommand()
        {
            string migrationProjectPath = MemoryParameter.MigrationSelectedProject.FullName;
            string startupProjectPath = MemoryParameter.StartUpSelectedProject.FullName;
            string dbContextPath = MemoryParameter.DbContextSelectedClass.Value;
            string buildConfiguration = MemoryParameter.BuildConfigSelectedItem;
            string outputDir = MemoryParameter.MigrationSelectedRootPaths.Value;

            string buildMode = this.checkBox1.Checked ? "--no-build" : "";
            string targetFramework = string.IsNullOrEmpty(comboBoxTargetFramework.Text) || comboBoxTargetFramework.Text == "<Default>" ? "" : $"--framework {comboBoxTargetFramework.Text}";

            string additionalArgument = textBoxAdditionalArg.Text;

            string operation = "migrations add";

            // تشکیل دستور CLI
            string command = "";
            if (_oprationModeEnum == OprationModeEnum.AddMigration)
            {
                string migrationName = textBox_migration_name.Text;
                //await Task.Delay(1000);

                command = $"ef {operation} --project {migrationProjectPath} --startup-project {startupProjectPath} --context {dbContextPath} --configuration {buildConfiguration} {buildMode} {targetFramework} {migrationName} --output-dir {outputDir} {additionalArgument}";

            }
            else if (_oprationModeEnum == OprationModeEnum.RemoveMigration)
            {
                operation = "migrations remove";
                command = $"ef {operation} --project {migrationProjectPath} --startup-project {startupProjectPath} --context {dbContextPath} --configuration {buildConfiguration} {buildMode} {targetFramework}  --force {additionalArgument}";

            }
            else if (_oprationModeEnum == OprationModeEnum.GenerateSqlScript)
            {
                operation = "migrations script";
                var from = comboBox8.Text;
                var to = comboBoxTargetMigration.Text;
                var outputsql = textBoxScript.Text;
                var idempotent = checkBoxUseDefaultConnection.Checked ? "--idempotent" : "";
                var no_transactions = checkBoxTransactions.Checked ? "--no-transactions" : "";
                command = $"ef {operation} --project {migrationProjectPath} --startup-project {startupProjectPath} --context {dbContextPath} --configuration {buildConfiguration} {buildMode} {targetFramework} {from} {to} --output {outputsql} {idempotent} {no_transactions} {additionalArgument}";

            }
            else if (_oprationModeEnum == OprationModeEnum.UpdateDatabase)
            {
                operation = "database update";
                var toMigration = comboBoxTargetMigration.Text;
                var concectionString = !checkBoxUseDefaultConnection.Checked && !string.IsNullOrEmpty(comboBoxFromMigration.Text) ? "--connection " + '"' + comboBoxFromMigration.Text.Split(char.Parse("|"))[1] + '"' : "";

                command = $"ef {operation} --project {migrationProjectPath} --startup-project {startupProjectPath} --context {dbContextPath} --configuration {buildConfiguration} {buildMode} {targetFramework} {toMigration} {concectionString} {additionalArgument}";

            }
            else if (_oprationModeEnum == OprationModeEnum.DropDatabase)
            {
                operation = "database drop";

                command = $"ef {operation} --project {migrationProjectPath} --startup-project {startupProjectPath} --context {dbContextPath} --configuration {buildConfiguration} {buildMode} {targetFramework} --force {additionalArgument}";

            }
            return command;
        }

        public async Task ExecuteEfCommand(string workingDirectory, string command)
        {
            // مسیر فایل dotnet.exe را تنظیم کنید
            string dotnetPath = @"C:\Program Files\dotnet\dotnet.exe";

            // تشکیل فرآیند برای اجرای دستور CLI
            System.Diagnostics.Process process = new System.Diagnostics.Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = dotnetPath,
                    WorkingDirectory = workingDirectory,
                    Arguments = command,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };


            Form2 form2 = new Form2(this);

            // رویدادهای خروجی فرآیند
            process.OutputDataReceived += async (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {

                    await form2.DisplayMessage(e.Data);
                }

            };
            process.ErrorDataReceived += async (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {

                    await form2.DisplayMessage(e.Data);

                }

            };

            // شروع و مشاهده خروجی
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            //this.Hide();
            form2.Show();
            form2.ControlBox = false;
            await Task.Run(async () =>
            {
                // نمایش خروجی به کاربر در ترد گرافیکی

                await form2.DisplayMessage(dotnetPath + " " + command);

            });
            await process.WaitForExitAsync();
            form2.ControlBox = true;
            await Task.Run(async () =>
            {
                await form2.DisplayMessage("Plz Click on Close Btn And Continue...");

            });
        }








        #region generate Sql Script
        static List<string> GetMigrationFileByDbContextName(Project project, string dbContextName)
        {

            // خواندن تمام فایل‌ها و نمایش محتوای آنها
            Dictionary<string, string> filesContent = ReadAllFilesInProject(project);

            // جستجوی الگو و نمایش فایل‌های مطابق
            Dictionary<string, string> matchingFiles = SearchPatternInFiles(filesContent, $@"\[DbContext\(typeof\({dbContextName}\)\)\]", @"\[Migration\(.*\)\]");
            return matchingFiles.Keys.ToList();
        }


        static Dictionary<string, string> SearchPatternInFiles(Dictionary<string, string> filesContent, string pattern, string pattern2)
        {
            Dictionary<string, string> matchingFiles = new Dictionary<string, string>();

            foreach (var fileContent in filesContent)
            {
                // جستجوی الگو در محتوای فایل
                if (Regex.IsMatch(fileContent.Value, pattern) && Regex.IsMatch(fileContent.Value, pattern2))
                {
                    matchingFiles.Add(fileContent.Key.Replace(".Designer.cs", ""), fileContent.Value);
                }
            }

            return matchingFiles;
        }
        static Dictionary<string, string> ReadAllFilesInProject(Project project)
        {
            Dictionary<string, string> filesContent = new Dictionary<string, string>();

            // دریافت پروژه به عنوان پوشه پروژه
            foreach (ProjectItem item in project.ProjectItems)
            {
                // خواندن محتوای تمام فایل‌ها
                ReadFilesContent(item, filesContent);
            }


            return filesContent;
        }

        static void ReadFilesContent(ProjectItem projectItem, Dictionary<string, string> filesContent)
        {
            foreach (ProjectItem item in projectItem.ProjectItems)
            {
                // بررسی آیا این یک فایل است
                if (item.FileCount > 0)
                {
                    // خواندن محتوای فایل
                    string fileName = item.Name;
                    string filePath = item.FileNames[1]; // شماره 1 به معنای مسیر فایل است
                    var fileExtention = System.IO.Path.GetExtension(filePath);
                    if (fileExtention.Contains(".cs"))
                    {

                        string fileContent = File.ReadAllText(filePath);

                        // افزودن به دیکشنری
                        filesContent.Add(fileName, fileContent);
                    }
                }

                // اگر یک پوشه باشد، فراخوانی بازگشتی بر روی آن انجام می‌شود
                if (item.ProjectItems != null && item.ProjectItems.Count > 0)
                {
                    ReadFilesContent(item, filesContent);
                }
            }
        }

        #endregion

        #region Update database
        static Dictionary<string, string> ExtractConnectionStringsFromJsonFiles(Project project)
        {
            List<JsonFileInfo> connectionStrings = new List<JsonFileInfo>();

            // دریافت پروژه به عنوان پوشه پروژه
            foreach (ProjectItem item in project.ProjectItems)
            {
                // جستجو در تمام فایل‌های JSON
                FindJsonFilesRecursively(item, connectionStrings);
            }

            return connectionStrings.Where(w => w.Properties.Count > 0).SelectMany(s => s.Properties).ToDictionary(w => w.Key, w => w.Value);
        }

        static void FindJsonFilesRecursively(ProjectItem projectItem, List<JsonFileInfo> connectionStrings)
        {
            if (System.IO.Path.GetExtension(projectItem.Name) == ".json")
            {
                // خواندن محتوای فایل
                string fileName = projectItem.Name;
                string filePath = projectItem.FileNames[1]; // شماره 1 به معنای مسیر فایل است
                string fileContent = File.ReadAllText(filePath);

                // استخراج اطلاعات اتصال از فایل JSON
                JsonFileInfo connectionStringInfo = new JsonFileInfo();

                ExtractConnectionStringsFromJson(fileContent, connectionStringInfo);
                connectionStringInfo.FileName = fileName;

                // افزودن به لیست اطلاعات اتصال
                connectionStrings.Add(connectionStringInfo);
            }
            foreach (ProjectItem item in projectItem.ProjectItems)
            {
                // بررسی آیا این یک فایل است
                if (item.FileCount > 0)
                {
                    // بررسی نوع فایل (JSON)
                    if (System.IO.Path.GetExtension(item.Name) == ".json")
                    {
                        // خواندن محتوای فایل
                        string fileName = item.Name;
                        string filePath = item.FileNames[1]; // شماره 1 به معنای مسیر فایل است
                        string fileContent = File.ReadAllText(filePath);

                        // استخراج اطلاعات اتصال از فایل JSON
                        JsonFileInfo connectionStringInfo = new JsonFileInfo();

                        ExtractConnectionStringsFromJson(fileContent, connectionStringInfo);
                        connectionStringInfo.FileName = fileName;

                        // افزودن به لیست اطلاعات اتصال
                        connectionStrings.Add(connectionStringInfo);
                    }
                }

                // اگر یک پوشه باشد، فراخوانی بازگشتی بر روی آن انجام می‌شود
                if (item.ProjectItems != null && item.ProjectItems.Count > 0)
                {
                    FindJsonFilesRecursively(item, connectionStrings);
                }
            }
        }

        static void ExtractConnectionStringsFromJson(string jsonContent, JsonFileInfo jsonFileInfo)
        {

            try
            {
                JObject jsonObject = JObject.Parse(jsonContent);

                // یافتن تمام properties در JSON
                GetJsonObjects(jsonObject, jsonFileInfo);
                if (jsonFileInfo.Properties.Count > 0)
                {

                    jsonFileInfo.Properties = jsonFileInfo.Properties.Where(w => w.Value.Contains("Data Source=") || w.Value.Contains("Server=")).ToDictionary(w => w.Key, w => w.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing JSON content: " + ex.Message);
            }

        }
        static void GetJsonObjects(JObject jsonObject, JsonFileInfo jsonFileInfo)
        {

            foreach (var property in jsonObject.Properties())
            {
                // بررسی نوع property
                if (property.Value.Type == JTokenType.Object)
                {
                    // اگر نوع property یک Object باشد، از آن استفاده کنید
                    JObject nestedObject = (JObject)property.Value;
                    GetJsonObjects(nestedObject, jsonFileInfo);
                }
                else if (property.Value.Type == JTokenType.String)
                {
                    // اگر نوع property یک String باشد
                    jsonFileInfo.Properties.Add(property.Name, property.Value.ToString());
                }
            }
        }

        #endregion

        private void checkBoxUseDefaultConnection_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUseDefaultConnection.Checked)
            {
                comboBoxConnection.Enabled = false;
            }
            else
            {
                comboBoxConnection.Enabled = true;

            }
        }
    }

    class JsonFileInfo
    {
        public string FileName { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }


}
