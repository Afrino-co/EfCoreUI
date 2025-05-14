using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using VSLangProj;

namespace EfCoreUi
{
    public partial class Form1 : Form
    {
        private static string EfCoreUiConfigFilePath = ".vs/EfCoreUiConfigFile.json";
        public OprationModeEnum _oprationModeEnum { get; set; }

        public Form1()
        {
        }

        public Form1(DTE2 dTE2, OprationModeEnum oprationModeEnum)
        {
            try
            {
                InitializeComponent();
                _oprationModeEnum = oprationModeEnum;
                if (dTE2 != null)
                {
                    MemoryParameter.Dte2 = dTE2;
                }

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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //TODO:Set Seq Log
                throw;
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            // Task[] tasks = new Task[3];
            await Task.Delay(100);

            // Console.WriteLine(DateTime.Now);
            SetMigrationProjects(MemoryParameter.Dte2);
            // Console.WriteLine(DateTime.Now);
            SetStartUpProject(MemoryParameter.Dte2);
            // Console.WriteLine(DateTime.Now);
            SetBuildConfigurationComboBox();
            // Console.WriteLine(DateTime.Now);
            //var creation_methodEnumLst = Enum.GetValues(typeof(creation_methodEnum)).Cast<creation_methodEnum>().Select(v => v.ToString()).ToArray();
            string[] creation_methodEnumLst =new string[]{ MemoryParameter.CMStartupProject, MemoryParameter.CMDesignTimeFactory };
            this.creation_method.Items.AddRange(creation_methodEnumLst);
            if (string.IsNullOrEmpty(MemoryParameter.CreationMethodSelectedItem) || string.IsNullOrWhiteSpace(MemoryParameter.CreationMethodSelectedItem))
            {
                MemoryParameter.CreationMethodSelectedItem = MemoryParameter.CMStartupProject;
            }
            if (MemoryParameter.Form1LoadCount == 0)
            {
                MemoryParameter.CreationMethodSelectedItem = MemoryParameter.CMStartupProject;
            }
            await Task.Delay(100);
            this.creation_method.SelectedItem = MemoryParameter.CreationMethodSelectedItem;
            await Task.Delay(100);
            MemoryParameter.Form1LoadCount++;
            // tasks[0] = Task.Factory.StartNew(async () => { await SetMigrationProjects(MemoryParameter.Dte2); });
            // tasks[1] = Task.Factory.StartNew(async () => { await SetStartUpProject(MemoryParameter.Dte2); });
            //
            // tasks[2] = Task.Factory.StartNew(async () => {await SetBuildConfigurationComboBox(); });
            // Task.WaitAll(tasks);
        }

        private async Task SetStartUpProject(DTE2 dTE2)
        {
            await Task.Delay(1);
            // لیست پروژه‌های قابل اجرا را بگیرید
            List<Project> runnableProjects = await GetRunnableProjects(dTE2);
            MemoryParameter.StartUpProjects = new List<Project>();

            // اطلاعات به ComboBox یا جای دیگری اضافه کنید
            foreach (Project project in runnableProjects)
            {
                MemoryParameter.StartUpProjects.Add(project);
            }

            this.comboBoxStartupProject.Items.AddRange(MemoryParameter.StartUpProjects.Select(s => s.Name).ToArray());

            await SetSelectedStartUpProject(runnableProjects);
            comboBoxStartupProject_Validating(comboBoxStartupProject, new CancelEventArgs());
        }

        private async Task SetMigrationProjects(DTE2 dTE2)
        {
            await Task.Delay(1);

            // MemoryParameter.MigrationProjects = new List<Project>();

            // اطلاعات به ComboBox یا جای دیگری اضافه کنید


            this.comboBoxMigrationProject.Items.AddRange(MemoryParameter.MigrationProjects.Select(s => s.Name).ToArray());

            await SetSelectedProjectFromSolutionExplorer(dTE2);
            comboBoxMigrationProject_Validating(comboBoxMigrationProject, new CancelEventArgs());
        }

        private async Task SetSelectedStartUpProject(List<Project> runnableProjects)
        {

            if (runnableProjects.Any())
            {
                string ConfigFile = "";

                if (File.Exists(EfCoreUiConfigFilePath))
                {

                    var res = JsonConvert.DeserializeObject<ConfigParameters>(File.ReadAllText(EfCoreUiConfigFilePath));
                    ConfigFile = res.StartUpProject;
                }
                else
                {
                    ConfigFile = runnableProjects.FirstOrDefault().Name;
                }

                this.comboBoxStartupProject.SelectedItem = ConfigFile;


                MemoryParameter.StartUpSelectedProject = MemoryParameter.StartUpProjects.FirstOrDefault(w => w.Name == ConfigFile);
            }

            if (_oprationModeEnum == OprationModeEnum.UpdateDatabase)
            {
                var connectionStrings = await ExtractConnectionStringsFromJsonFiles(MemoryParameter.StartUpSelectedProject);
                if (connectionStrings.Count > 0)
                {
                    comboBoxConnection.Items.AddRange(connectionStrings.Select(s => s.Key + " | " + s.Value).ToArray());
                }
            }

            var targetFrameworks = await GetTargetFrameworks(MemoryParameter.StartUpSelectedProject);
            if (targetFrameworks != null)
            {
                targetFrameworks.Add("<Default>");
                comboBoxTargetFramework.Items.AddRange(targetFrameworks.ToArray());
                comboBoxTargetFramework.SelectedItem = "<Default>";
            }
        }

        private async Task SetSelectedProjectFromSolutionExplorer(DTE2 dTE2)
        {
            await Task.Delay(1);
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

        private async Task SetBuildConfigurationComboBox()
        {
            await Task.Delay(1);
            this.comboBoxBuildConfig.Items.AddRange(new object[] { "Debug", "Release" });
            this.comboBoxBuildConfig.SelectedItem = "Debug";
            MemoryParameter.BuildConfigSelectedItem = "Debug";
        }


        private async void comboBoxMigrationProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Task.Delay(1);
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

                // if (MemoryParameter.DbContextClassLst != null && MemoryParameter.DbContextClassLst.Any())
                // {
                //     MemoryParameter.DbContextClassLst.Clear();
                // }
                //
                // MemoryParameter.DbContextClassLst = GetDbContextClassNames(MemoryParameter.MigrationSelectedProject);
                //
                //

                if (MemoryParameter.DbContextClassLst != null && MemoryParameter.DbContextClassLst.Any())
                {
                    comboBoxDbContextClass.Items.Clear();
                    comboBoxDbContextClass.Items.AddRange(MemoryParameter.DbContextClassLst.Select(s => s.Key).ToArray());
                    comboBoxDbContextClass.SelectedItem = MemoryParameter.DbContextClassLst.FirstOrDefault(f => f.Value.Value == MemoryParameter.MigrationSelectedProject.Name).Key;
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
                            comboBoxToMigration.Items.AddRange(migrationLst.OrderByDescending(o => o).ToArray());
                            comboBoxToMigration.SelectedItem = migrationLst.OrderByDescending(o => o).ToArray().FirstOrDefault();
                        }
                    }
                    else
                    {
                        comboBoxToMigration.Items.Clear();
                    }
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBoxStartupProject.SelectedItem.ToString()))
            {
                var startUpInfo = new ConfigParameters() { StartUpProject = comboBoxStartupProject.SelectedItem.ToString() };
                string json = JsonConvert.SerializeObject(startUpInfo);
                File.WriteAllText(EfCoreUiConfigFilePath, json);

                MemoryParameter.StartUpSelectedProject = MemoryParameter.StartUpProjects.FirstOrDefault(w => w.Name == comboBoxStartupProject.SelectedItem.ToString());
            }
        }

        private async void comboBoxDbContextClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Task.Delay(1);
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
                        comboBoxToMigration.Items.AddRange(migrationLst.OrderByDescending(o => o).ToArray());
                        comboBoxToMigration.SelectedItem = migrationLst.OrderByDescending(o => o).ToArray().FirstOrDefault();
                    }
                }
                else
                {
                    comboBoxToMigration.Items.Clear();
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
            {
                formWithError = true;
            }

            if (string.IsNullOrWhiteSpace(comboBoxStartupProject.SelectedItem?.ToString()))
            {
                formWithError = true;
            }

            if (string.IsNullOrWhiteSpace(comboBoxDbContextClass.SelectedItem?.ToString()))
            {
                formWithError = true;
            }

            if (_oprationModeEnum == OprationModeEnum.AddMigration)
            {
                if (string.IsNullOrWhiteSpace(textBox_migration_name?.Text))
                {
                    formWithError = true;
                }

                if (string.IsNullOrWhiteSpace(comboBoxMigrationFolder.SelectedItem?.ToString()))
                {
                    formWithError = true;
                }
            }

            if (_oprationModeEnum == OprationModeEnum.GenerateSqlScript)
            {
                if (string.IsNullOrWhiteSpace(comboBoxFromMigration.SelectedItem?.ToString()))
                {
                    formWithError = true;
                }

                if (string.IsNullOrWhiteSpace(comboBoxToMigration.SelectedItem?.ToString()))
                {
                    formWithError = true;
                }

                if (string.IsNullOrWhiteSpace(textBoxScript.Text?.ToString()))
                {
                    formWithError = true;
                }
            }

            if (_oprationModeEnum == OprationModeEnum.UpdateDatabase)
            {
                if (string.IsNullOrWhiteSpace(comboBoxToMigration.SelectedItem?.ToString()))
                {
                    formWithError = true;
                }

                if (!checkBoxUseDefaultConnection.Checked)
                {
                    if (string.IsNullOrWhiteSpace(comboBoxConnection.SelectedItem?.ToString()))
                    {
                        formWithError = true;
                    }
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






        static async Task<List<string>> GetTargetFrameworks(Project project)
        {
            // دریافت پروژه فعال در محیط Visual Studio
            await Task.Delay(1);
            if (project != null)
            {
                string tf = project.Properties.Item("FriendlyTargetFramework").Value.ToString();
                var result = tf.Split(";".ToCharArray()).ToList();
                return result;
            }

            return null;
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


        private static async Task<List<Project>> GetRunnableProjects(DTE2 dte)
        {
            List<Project> runnableProjects = new List<Project>();

            foreach (Project project in dte.Solution.Projects)
            {
                await FindRunnableProjectsInHierarchy(project, runnableProjects);
            }

            return runnableProjects;
        }

        private static async Task FindRunnableProjectsInHierarchy(Project project, List<Project> runnableProjects)
        {
            await Task.Delay(1);
            if (project.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFolder || project.Kind == EnvDTE.Constants.vsProjectItemKindVirtualFolder || project.Kind == EnvDTE.Constants.vsProjectKindSolutionItems)
            {
                // اگر پروژه یک نمایه (Solution Folder) باشد، پویش درون آن را انجام دهید
                foreach (ProjectItem projectItem in project.ProjectItems)
                {
                    if (projectItem.SubProject != null)
                    {
                        FindRunnableProjectsInHierarchy(projectItem.SubProject, runnableProjects);
                    }
                }
            }
            else
            {
                // اگر پروژه به عنوان یک پروژه قابل اجرا تشخیص داده شود، به لیست اضافه کنید
                if (IsRunnableProject(project))
                {
                    runnableProjects.Add(project);
                }
            }
        }

        private static bool IsRunnableProject(Project project)
        {
            try
            {
                bool isTestProject = false;
                if (project.Properties == null) return false;
                // چک کنید که پروژه قابل اجراست (وبی یا ویندوزی)
                var outputType = project.Properties.Item("OutputType").Value.ToString();


                if (project.Object is VSProject vsProject)
                {
                    foreach (VSLangProj.Reference reference in vsProject.References)
                    {
                        // بررسی نام پکیج (ممکن است نام کامل یا قسمتی از آن باشد)
                        if (reference.Name.Contains("Test"))
                        {
                            isTestProject = true;
                        }
                    }
                }

                return outputType != "2" && !isTestProject;


            }
            catch
            {
                // اگر خطا رخ داد، به عنوان یک پروژه قابل اجرا در نظر گرفته نشود
                return false;
            }
        }



        public string AggregateEfCommand()
        {
            string migrationProjectPath = MemoryParameter.MigrationSelectedProject.FullName;
            string startupProjectPath = MemoryParameter.StartUpSelectedProject.FullName;
            string dbContextPath = MemoryParameter.DbContextSelectedClass.Value.Key;
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
                command = new StringBuilder($"ef {operation}")
                    .Append($" --project \"{migrationProjectPath}\"")
                    .Append(this.creation_method.SelectedItem == MemoryParameter.CMStartupProject ? $" --startup-project \"{startupProjectPath}\"" : "")
                    .Append($" --context {dbContextPath}")
                    .Append($" --configuration {buildConfiguration} {buildMode} {targetFramework} \"{migrationName.Replace(" ", "_").Trim()}\"")
                    .Append($" --output-dir \"{outputDir}\"")
                    .Append($" {additionalArgument}")
                    .ToString();
            }
            else if (_oprationModeEnum == OprationModeEnum.RemoveMigration)
            {
                operation = "migrations remove";
                command = new StringBuilder($"ef {operation}")
                    .Append($" --project \"{migrationProjectPath}\"")
                    .Append(this.creation_method.SelectedItem == MemoryParameter.CMStartupProject ? $" --startup-project \"{startupProjectPath}\"" : "")
                    .Append($" --context {dbContextPath}")
                    .Append($" --configuration {buildConfiguration} {buildMode} {targetFramework}")
                    .Append($" --force")
                    .Append($" {additionalArgument}")
                    .ToString();
            }
            else if (_oprationModeEnum == OprationModeEnum.GenerateSqlScript)
            {
                operation = "migrations script";
                var from = comboBoxFromMigration.Text;
                var to = comboBoxToMigration.Text;
                var outputsql = textBoxScript.Text;
                var idempotent = checkBoxIdempotent.Checked ? "--idempotent" : "";
                var no_transactions = checkBoxTransactions.Checked ? "--no-transactions" : "";
                command = new StringBuilder($"ef {operation}")
                   .Append($" --project \"{migrationProjectPath}\"")
                   .Append(this.creation_method.SelectedItem == MemoryParameter.CMStartupProject ? $" --startup-project \"{startupProjectPath}\"" : "")
                   .Append($" --context {dbContextPath}")
                   .Append($" --configuration {buildConfiguration} {buildMode} {targetFramework} {from} {to}")
                   .Append($"--output \"{outputsql}\" {idempotent} {no_transactions}")
                   .Append($" {additionalArgument}")
                   .ToString();
            }
            else if (_oprationModeEnum == OprationModeEnum.UpdateDatabase)
            {
                operation = "database update";
                var toMigration = comboBoxToMigration.Text;
                var concectionString = !checkBoxUseDefaultConnection.Checked && !string.IsNullOrEmpty(comboBoxFromMigration.Text) ? "--connection " + '"' + comboBoxFromMigration.Text.Split(char.Parse("|"))[1] + '"' : "";
                command = new StringBuilder($"ef {operation}")
                 .Append($" --project \"{migrationProjectPath}\"")
                 .Append(this.creation_method.SelectedItem == MemoryParameter.CMStartupProject ? $" --startup-project \"{startupProjectPath}\"" : "")
                 .Append($" --context {dbContextPath}")
                 .Append($" --configuration {buildConfiguration} {buildMode} {targetFramework} {toMigration} {concectionString}")
                 .Append($" {additionalArgument}")
                 .ToString();
            }
            else if (_oprationModeEnum == OprationModeEnum.DropDatabase)
            {
                operation = "database drop";
                command = new StringBuilder($"ef {operation}")
                .Append($" --project \"{migrationProjectPath}\"")
                .Append(this.creation_method.SelectedItem == MemoryParameter.CMStartupProject ? $" --startup-project \"{startupProjectPath}\"" : "")
                .Append($" --context {dbContextPath}")
                .Append($" --configuration {buildConfiguration} {buildMode} {targetFramework}")
                .Append($" --force")
                .Append($" {additionalArgument}")
                .ToString();
            }

            return command;
        }

        public async Task ExecuteEfCommand(string workingDirectory, string command)
        {
            // مسیر فایل dotnet.exe را تنظیم کنید
            string dotnetPath = @"""C:\Program Files\dotnet\dotnet.exe""";

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
            CreateNewOutputConsole();
            // Form2 form2 = new Form2(this);

            // رویدادهای خروجی فرآیند
            process.OutputDataReceived += async (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    // await form2.DisplayMessage(e.Data);
                    await ShowCmdResult(e.Data);
                }
            };
            process.ErrorDataReceived += async (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    // await form2.DisplayMessage(e.Data);
                    await ShowCmdResult(e.Data);
                }
            };

            // شروع و مشاهده خروجی
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            //this.Hide();
            // form2.Show();
            // form2.ControlBox = false;
            await Task.Run(async () =>
            {
                // نمایش خروجی به کاربر در ترد گرافیکی
                await ShowCmdResult(dotnetPath + " " + command);
                // await form2.DisplayMessage(dotnetPath + " " + command);
            });
            this.Close();
            await process.WaitForExitAsync();
            // form2.ControlBox = true;
            // await Task.Run(async () => { await form2.DisplayMessage("Plz Click on Close Btn And Continue..."); });
        }

        private void CreateNewOutputConsole()
        {
            Window window = MemoryParameter.Dte2.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);

            window.Activate();
            window.SetFocus();


            Guid outputWindowPaneGuid = Guid.NewGuid();
            MemoryParameter.OutputWindow.CreatePane(
                ref outputWindowPaneGuid,
                $"EfCoreUI Output Log {DateTime.Now}",
                Convert.ToInt32(true), // در حالت‌های خاص ممکن است نیاز به تبدیل شود
                Convert.ToInt32(true) // در حالت‌های خاص ممکن است نیاز به تبدیل شود
            );
            MemoryParameter.OutputWindow.GetPane(ref outputWindowPaneGuid, out var outputPane);
            MemoryParameter.OutputPane = outputPane;
            MemoryParameter.OutputPane.Activate();
        }

        private async Task ShowCmdResult(string msg)
        {
            await Task.Delay(1);
            MemoryParameter.OutputPane.OutputString(msg + Environment.NewLine);
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

        private static async Task<Dictionary<string, string>> ExtractConnectionStringsFromJsonFiles(Project project)
        {
            List<JsonFileInfo> connectionStrings = new List<JsonFileInfo>();

            // دریافت پروژه به عنوان پوشه پروژه
            foreach (ProjectItem item in project.ProjectItems)
            {
                // جستجو در تمام فایل‌های JSON
                await FindJsonFilesRecursively(item, connectionStrings);
            }

            return connectionStrings.Where(w => w.Properties.Count > 0).SelectMany(s => s.Properties).ToDictionary(w => w.Key, w => w.Value);
        }

        static async Task FindJsonFilesRecursively(ProjectItem projectItem, List<JsonFileInfo> connectionStrings)
        {
            if (System.IO.Path.GetExtension(projectItem.Name) == ".json")
            {
                // خواندن محتوای فایل
                string fileName = projectItem.Name;
                string filePath = projectItem.FileNames[1]; // شماره 1 به معنای مسیر فایل است
                string fileContent = File.ReadAllText(filePath);

                // استخراج اطلاعات اتصال از فایل JSON
                JsonFileInfo connectionStringInfo = new JsonFileInfo();

                await ExtractConnectionStringsFromJson(fileContent, connectionStringInfo);
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

        static async Task ExtractConnectionStringsFromJson(string jsonContent, JsonFileInfo jsonFileInfo)
        {
            try
            {
                JObject jsonObject = JObject.Parse(jsonContent);

                // یافتن تمام properties در JSON
                await GetJsonObjects(jsonObject, jsonFileInfo);
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

        static async Task GetJsonObjects(JObject jsonObject, JsonFileInfo jsonFileInfo)
        {
            foreach (var property in jsonObject.Properties())
            {
                // بررسی نوع property
                if (property.Value.Type == JTokenType.Object)
                {
                    // اگر نوع property یک Object باشد، از آن استفاده کنید
                    JObject nestedObject = (JObject)property.Value;
                    await GetJsonObjects(nestedObject, jsonFileInfo);
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


        private void creation_method_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.comboBoxStartupProject.Enabled = true;
            if (creation_method.SelectedItem.ToString() != MemoryParameter.CMStartupProject)
                this.comboBoxStartupProject.Enabled = false;

            MemoryParameter.CreationMethodSelectedItem = creation_method.SelectedItem.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ContactUsForm contactUsForm = new ContactUsForm();

            contactUsForm.ShowDialog();
        }
    }

    class JsonFileInfo
    {
        public string FileName { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
}