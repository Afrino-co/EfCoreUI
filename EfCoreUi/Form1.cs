using EnvDTE;
using EnvDTE80;
using EfCoreUi.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using VSLangProj;

namespace EfCoreUi
{
    public partial class Form1 : Form
    {
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
                OutputWindowHelper.WriteError(e.Message);
                throw;
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                SetMigrationProjects(MemoryParameter.Dte2);
                await SetStartUpProject(MemoryParameter.Dte2);
                SetBuildConfigurationComboBox();

                var creationMethodItems = new[] { MemoryParameter.CMStartupProject, MemoryParameter.CMDesignTimeFactory };
                creation_method.Items.AddRange(creationMethodItems);

                var config = EfCoreUiConfigStore.Load();
                if (!string.IsNullOrWhiteSpace(config.CreationMethod))
                {
                    MemoryParameter.CreationMethodSelectedItem = config.CreationMethod;
                }
                else if (string.IsNullOrWhiteSpace(MemoryParameter.CreationMethodSelectedItem) || MemoryParameter.Form1LoadCount == 0)
                {
                    MemoryParameter.CreationMethodSelectedItem = MemoryParameter.CMStartupProject;
                }

                creation_method.SelectedItem = MemoryParameter.CreationMethodSelectedItem;
                MemoryParameter.Form1LoadCount++;
            }
            catch (Exception ex)
            {
                OutputWindowHelper.WriteError(ex.Message);
            }
        }

        private async Task SetStartUpProject(DTE2 dTE2)
        {
            var runnableProjects = GetRunnableProjects(dTE2);
            MemoryParameter.StartUpProjects = new List<Project>(runnableProjects);

            comboBoxStartupProject.Items.AddRange(MemoryParameter.StartUpProjects.Select(s => s.Name).ToArray());
            await SetSelectedStartUpProject(runnableProjects);
            comboBoxStartupProject_Validating(comboBoxStartupProject, new CancelEventArgs());
        }

        private void SetMigrationProjects(DTE2 dTE2)
        {
            comboBoxMigrationProject.Items.AddRange(MemoryParameter.MigrationProjects.Select(s => s.Name).ToArray());
            SetSelectedProjectFromSolutionExplorer(dTE2);
            comboBoxMigrationProject_Validating(comboBoxMigrationProject, new CancelEventArgs());
        }

        private async Task SetSelectedStartUpProject(List<Project> runnableProjects)
        {
            if (runnableProjects.Any())
            {
                var config = EfCoreUiConfigStore.Load();
                var startupName = !string.IsNullOrWhiteSpace(config.StartUpProject)
                    ? config.StartUpProject
                    : runnableProjects.First().Name;

                comboBoxStartupProject.SelectedItem = startupName;
                MemoryParameter.StartUpSelectedProject = MemoryParameter.StartUpProjects.FirstOrDefault(w => w.Name == startupName);
            }

            if (_oprationModeEnum == OprationModeEnum.UpdateDatabase && MemoryParameter.StartUpSelectedProject != null)
            {
                var connectionStrings = await ConnectionStringExtractor.ExtractFromProjectAsync(MemoryParameter.StartUpSelectedProject);
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
            var items = solutionExplorer.SelectedItems as object[];

            if (items == null || items.Length == 0)
            {
                return;
            }

            foreach (object item in items)
            {
                if (item is UIHierarchyItem selectedItem && selectedItem.Object is Project project)
                {
                    comboBoxMigrationProject.SelectedItem = project.Name;
                    MemoryParameter.MigrationSelectedProject = MemoryParameter.MigrationProjects.FirstOrDefault(w => w.Name == project.Name);
                }
            }
        }

        private void SetBuildConfigurationComboBox()
        {
            var config = EfCoreUiConfigStore.Load();
            comboBoxBuildConfig.Items.AddRange(new object[] { "Debug", "Release" });
            var buildConfig = !string.IsNullOrWhiteSpace(config.BuildConfig) ? config.BuildConfig : "Debug";
            comboBoxBuildConfig.SelectedItem = buildConfig;
            MemoryParameter.BuildConfigSelectedItem = buildConfig;
        }

        private void comboBoxMigrationProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxMigrationProject.SelectedItem == null)
                {
                    return;
                }

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
            catch (Exception ex)
            {
                OutputWindowHelper.WriteError(ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStartupProject.SelectedItem == null)
            {
                return;
            }

            var config = EfCoreUiConfigStore.Load();
            config.StartUpProject = comboBoxStartupProject.SelectedItem.ToString();
            EfCoreUiConfigStore.Save(config);

            MemoryParameter.StartUpSelectedProject = MemoryParameter.StartUpProjects.FirstOrDefault(w => w.Name == comboBoxStartupProject.SelectedItem.ToString());
        }

        private void comboBoxDbContextClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxDbContextClass.SelectedItem != null)
                {
                    MemoryParameter.DbContextSelectedClass = MemoryParameter.DbContextClassLst.FirstOrDefault(w => w.Key == comboBoxDbContextClass.SelectedItem.ToString());
                }

                if (_oprationModeEnum == OprationModeEnum.UpdateDatabase)
                {
                    comboBoxToMigration.Items.Clear();
                    if (comboBoxDbContextClass.SelectedItem != null)
                    {
                        var migrationLst = GetMigrationFileByDbContextName(MemoryParameter.MigrationSelectedProject, comboBoxDbContextClass.SelectedItem.ToString());
                        if (migrationLst.Count >= 1)
                        {
                            migrationLst.Add("0");
                            comboBoxToMigration.Items.AddRange(migrationLst.OrderByDescending(o => o).ToArray());
                            comboBoxToMigration.SelectedItem = migrationLst.OrderByDescending(o => o).FirstOrDefault();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OutputWindowHelper.WriteError(ex.Message);
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
            try
            {
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

                    if (!checkBoxUseDefaultConnection.Checked &&
                        string.IsNullOrWhiteSpace(comboBoxConnection.SelectedItem?.ToString()))
                    {
                        formWithError = true;
                    }
                }

                if (!ValidateChildren(ValidationConstraints.Enabled) || formWithError)
                {
                    MessageBox.Show("There is an issue with the form validations.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                SaveCurrentConfig();
                button1.Enabled = false;

                var cmd = EfCommandBuilder.Build(BuildCommandOptions());
                var workingDirectory = MemoryParameter.MigrationSelectedProject.FullName
                    .Replace(MemoryParameter.MigrationSelectedProject.Name + ".csproj", "");

                var exitCode = await EfCommandRunner.ExecuteAsync(workingDirectory, cmd);
                if (exitCode == 0)
                {
                    Close();
                }
                else
                {
                    button1.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                button1.Enabled = true;
                OutputWindowHelper.WriteError(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form3 = new Form3();
            var cmd = EfCommandBuilder.Build(BuildCommandOptions());
            form3.textBox1.Text = "dotnet " + cmd;
            form3.ShowDialog();
        }

        private EfCommandOptions BuildCommandOptions()
        {
            string connectionString = null;
            if (_oprationModeEnum == OprationModeEnum.UpdateDatabase &&
                !checkBoxUseDefaultConnection.Checked &&
                !string.IsNullOrWhiteSpace(comboBoxConnection.SelectedItem?.ToString()))
            {
                var parts = comboBoxConnection.SelectedItem.ToString().Split('|');
                if (parts.Length > 1)
                {
                    connectionString = parts[1].Trim();
                }
            }

            return new EfCommandOptions
            {
                Operation = _oprationModeEnum,
                MigrationProjectPath = MemoryParameter.MigrationSelectedProject.FullName,
                StartupProjectPath = MemoryParameter.StartUpSelectedProject?.FullName,
                DbContextFullName = MemoryParameter.DbContextSelectedClass.Value.Key,
                BuildConfiguration = MemoryParameter.BuildConfigSelectedItem,
                OutputDirectory = MemoryParameter.MigrationSelectedRootPaths.Value,
                NoBuild = checkBox1.Checked,
                TargetFramework = comboBoxTargetFramework.Text,
                AdditionalArguments = textBoxAdditionalArg.Text,
                UseStartupProject = creation_method.SelectedItem?.ToString() == MemoryParameter.CMStartupProject,
                MigrationName = textBox_migration_name.Text,
                FromMigration = comboBoxFromMigration.Text,
                ToMigration = comboBoxToMigration.Text,
                ScriptOutputPath = textBoxScript.Text,
                Idempotent = checkBoxIdempotent.Checked,
                NoTransactions = checkBoxTransactions.Checked,
                ConnectionString = connectionString
            };
        }

        private void SaveCurrentConfig()
        {
            var config = EfCoreUiConfigStore.Load();
            config.StartUpProject = comboBoxStartupProject.SelectedItem?.ToString();
            config.MigrationProject = comboBoxMigrationProject.SelectedItem?.ToString();
            config.DbContext = comboBoxDbContextClass.SelectedItem?.ToString();
            config.MigrationFolder = comboBoxMigrationFolder.SelectedItem?.ToString();
            config.BuildConfig = comboBoxBuildConfig.SelectedItem?.ToString();
            config.CreationMethod = creation_method.SelectedItem?.ToString();
            EfCoreUiConfigStore.Save(config);
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






        static List<string> GetTargetFrameworks(Project project)
        {
            if (project != null)
            {
                string tf = project.Properties.Item("FriendlyTargetFramework").Value.ToString();
                return tf.Split(';').ToList();
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


        private static List<Project> GetRunnableProjects(DTE2 dte)
        {
            var runnableProjects = new List<Project>();

            foreach (Project project in dte.Solution.Projects)
            {
                FindRunnableProjectsInHierarchy(project, runnableProjects);
            }

            return runnableProjects;
        }

        private static void FindRunnableProjectsInHierarchy(Project project, List<Project> runnableProjects)
        {
            if (project.Kind == EnvDTE.Constants.vsProjectItemKindPhysicalFolder ||
                project.Kind == EnvDTE.Constants.vsProjectItemKindVirtualFolder ||
                project.Kind == EnvDTE.Constants.vsProjectKindSolutionItems)
            {
                foreach (ProjectItem projectItem in project.ProjectItems)
                {
                    if (projectItem.SubProject != null)
                    {
                        FindRunnableProjectsInHierarchy(projectItem.SubProject, runnableProjects);
                    }
                }
            }
            else if (IsRunnableProject(project))
            {
                runnableProjects.Add(project);
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
}