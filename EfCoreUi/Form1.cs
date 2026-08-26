using EnvDTE;
using EnvDTE80;
using EfCoreUi.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VSLangProj;

namespace EfCoreUi
{
    public partial class Form1 : Form
    {
        public OprationModeEnum _oprationModeEnum { get; set; }
        private bool _isInitializing;
        private bool _migrationFromSolutionExplorer;

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
            _isInitializing = true;
            try
            {
                var config = EfCoreUiConfigStore.Load();
                SetMigrationProjects(MemoryParameter.Dte2);
                await SetStartUpProject(MemoryParameter.Dte2);
                SetBuildConfigurationComboBox();

                var creationMethodItems = new[] { MemoryParameter.CMStartupProject, MemoryParameter.CMDesignTimeFactory };
                creation_method.Items.AddRange(creationMethodItems);

                if (!string.IsNullOrWhiteSpace(config.CreationMethod))
                {
                    MemoryParameter.CreationMethodSelectedItem = config.CreationMethod;
                }
                else if (string.IsNullOrWhiteSpace(MemoryParameter.CreationMethodSelectedItem) || MemoryParameter.Form1LoadCount == 0)
                {
                    MemoryParameter.CreationMethodSelectedItem = MemoryParameter.CMStartupProject;
                }

                creation_method.SelectedItem = MemoryParameter.CreationMethodSelectedItem;
                ApplySavedConfig(config);
                await LoadMigrationCombosAsync();
                MemoryParameter.Form1LoadCount++;
            }
            catch (Exception ex)
            {
                OutputWindowHelper.WriteError(ex.Message);
            }
            finally
            {
                _isInitializing = false;
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
            _migrationFromSolutionExplorer = TrySelectProjectFromSolutionExplorer(dTE2);
            comboBoxMigrationProject_Validating(comboBoxMigrationProject, new CancelEventArgs());
        }

        private void ApplySavedConfig(ConfigParameters config)
        {
            if (!_migrationFromSolutionExplorer && !string.IsNullOrWhiteSpace(config.MigrationProject))
            {
                if (MemoryParameter.MigrationProjects.Any(p => p.Name == config.MigrationProject))
                {
                    comboBoxMigrationProject.SelectedItem = config.MigrationProject;
                    MemoryParameter.MigrationSelectedProject = MemoryParameter.MigrationProjects
                        .First(p => p.Name == config.MigrationProject);
                }
            }
            else if (MemoryParameter.MigrationSelectedProject == null && MemoryParameter.MigrationProjects.Any())
            {
                comboBoxMigrationProject.SelectedItem = MemoryParameter.MigrationProjects.First().Name;
                MemoryParameter.MigrationSelectedProject = MemoryParameter.MigrationProjects.First();
            }

            RefreshMigrationProjectState(config.DbContext, config.MigrationFolder);
        }

        private bool TrySelectProjectFromSolutionExplorer(DTE2 dTE2)
        {
            var solutionExplorer = dTE2.ToolWindows.SolutionExplorer;
            var items = solutionExplorer.SelectedItems as object[];

            if (items == null || items.Length == 0)
            {
                return false;
            }

            foreach (object item in items)
            {
                if (item is UIHierarchyItem selectedItem && selectedItem.Object is Project project)
                {
                    if (MemoryParameter.MigrationProjects.Any(p => p.Name == project.Name))
                    {
                        comboBoxMigrationProject.SelectedItem = project.Name;
                        MemoryParameter.MigrationSelectedProject = MemoryParameter.MigrationProjects
                            .First(w => w.Name == project.Name);
                        return true;
                    }
                }
            }

            return false;
        }

        private void RefreshMigrationProjectState(string preferredDbContext, string preferredMigrationFolder)
        {
            if (MemoryParameter.MigrationSelectedProject == null)
            {
                return;
            }

            if (_oprationModeEnum == OprationModeEnum.AddMigration)
            {
                PopulateMigrationFolderCombo(preferredMigrationFolder);
            }

            PopulateDbContextCombo(preferredDbContext);
        }

        private void PopulateMigrationFolderCombo(string preferredFolder)
        {
            MemoryParameter.MigrationRootPaths = GetProjectFolders(MemoryParameter.MigrationSelectedProject);
            if (!MemoryParameter.MigrationRootPaths.Any(a => a.Key == "Migrations"))
            {
                MemoryParameter.MigrationRootPaths.Add("Migrations", "Migrations");
            }

            comboBoxMigrationFolder.Items.Clear();
            comboBoxMigrationFolder.Items.AddRange(MemoryParameter.MigrationRootPaths.Select(s => s.Key).ToArray());

            string selectedFolder = null;
            if (!string.IsNullOrWhiteSpace(preferredFolder) && MemoryParameter.MigrationRootPaths.ContainsKey(preferredFolder))
            {
                selectedFolder = preferredFolder;
            }
            else if (MemoryParameter.MigrationRootPaths.ContainsKey("Migrations"))
            {
                selectedFolder = MemoryParameter.MigrationRootPaths.First(f => f.Key.Contains("Migrations")).Key;
            }
            else if (MemoryParameter.MigrationRootPaths.Any())
            {
                selectedFolder = MemoryParameter.MigrationRootPaths.First().Key;
            }

            if (selectedFolder != null)
            {
                comboBoxMigrationFolder.SelectedItem = selectedFolder;
                MemoryParameter.MigrationSelectedRootPaths = MemoryParameter.MigrationRootPaths
                    .First(w => w.Key == selectedFolder);
            }

            comboBoxMigrationFolder_Validating(comboBoxMigrationFolder, new CancelEventArgs());
        }

        private void PopulateDbContextCombo(string preferredDbContext)
        {
            comboBoxDbContextClass.Items.Clear();

            if (MemoryParameter.DbContextClassLst == null || !MemoryParameter.DbContextClassLst.Any())
            {
                comboBoxDbContextClass_Validating(comboBoxDbContextClass, new CancelEventArgs());
                return;
            }

            comboBoxDbContextClass.Items.AddRange(MemoryParameter.DbContextClassLst.Select(s => s.Key).ToArray());

            string selectedDbContext = null;
            if (!string.IsNullOrWhiteSpace(preferredDbContext) && MemoryParameter.DbContextClassLst.ContainsKey(preferredDbContext))
            {
                selectedDbContext = preferredDbContext;
            }
            else
            {
                var projectMatch = MemoryParameter.DbContextClassLst
                    .FirstOrDefault(f => f.Value.Value == MemoryParameter.MigrationSelectedProject.Name);
                if (!string.IsNullOrEmpty(projectMatch.Key))
                {
                    selectedDbContext = projectMatch.Key;
                }
            }

            if (selectedDbContext != null)
            {
                comboBoxDbContextClass.SelectedItem = selectedDbContext;
                ApplyDbContextSelection(selectedDbContext);
            }

            comboBoxDbContextClass_Validating(comboBoxDbContextClass, new CancelEventArgs());
        }

        private void ApplyDbContextSelection(string dbContextName)
        {
            if (string.IsNullOrWhiteSpace(dbContextName))
            {
                return;
            }

            MemoryParameter.DbContextSelectedClass = MemoryParameter.DbContextClassLst
                .FirstOrDefault(w => w.Key == dbContextName);
        }

        private async Task LoadMigrationCombosAsync()
        {
            if (_oprationModeEnum != OprationModeEnum.GenerateSqlScript &&
                _oprationModeEnum != OprationModeEnum.UpdateDatabase)
            {
                return;
            }

            if (MemoryParameter.MigrationSelectedProject == null || comboBoxDbContextClass.SelectedItem == null)
            {
                return;
            }

            var dbContextName = comboBoxDbContextClass.SelectedItem.ToString();
            if (string.IsNullOrWhiteSpace(GetSelectedDbContextFullName()))
            {
                return;
            }

            comboBoxFromMigration.Items.Clear();
            comboBoxToMigration.Items.Clear();

            UseWaitCursor = true;
            try
            {
                var migrations = await EfMigrationLister.ListMigrationsAsync(
                    BuildCommandOptions(),
                    MemoryParameter.MigrationSelectedProject,
                    dbContextName);

                if (migrations.Count == 0)
                {
                    return;
                }

                if (_oprationModeEnum == OprationModeEnum.GenerateSqlScript)
                {
                    var toMigrations = migrations.OrderByDescending(m => m).ToArray();
                    comboBoxToMigration.Items.AddRange(toMigrations);
                    comboBoxToMigration.SelectedItem = toMigrations.First();

                    var fromMigrations = migrations.OrderBy(m => m).ToList();
                    fromMigrations.Add("0");
                    comboBoxFromMigration.Items.AddRange(fromMigrations.ToArray());
                    comboBoxFromMigration.SelectedItem = fromMigrations.First();
                }
                else if (_oprationModeEnum == OprationModeEnum.UpdateDatabase)
                {
                    var targetMigrations = migrations.OrderByDescending(m => m).ToList();
                    targetMigrations.Add("0");
                    comboBoxToMigration.Items.AddRange(targetMigrations.ToArray());
                    comboBoxToMigration.SelectedItem = targetMigrations.First();
                }
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void UpdateConfigField(Action<ConfigParameters> update)
        {
            var config = EfCoreUiConfigStore.Load();
            update(config);
            EfCoreUiConfigStore.Save(config);
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

        private void SetBuildConfigurationComboBox()
        {
            var config = EfCoreUiConfigStore.Load();
            comboBoxBuildConfig.Items.AddRange(new object[] { "Debug", "Release" });
            var buildConfig = !string.IsNullOrWhiteSpace(config.BuildConfig) ? config.BuildConfig : "Debug";
            comboBoxBuildConfig.SelectedItem = buildConfig;
            MemoryParameter.BuildConfigSelectedItem = buildConfig;
        }

        private async void comboBoxMigrationProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isInitializing || comboBoxMigrationProject.SelectedItem == null)
                {
                    return;
                }

                var projectName = comboBoxMigrationProject.SelectedItem.ToString();
                MemoryParameter.MigrationSelectedProject = MemoryParameter.MigrationProjects
                    .FirstOrDefault(w => w.Name == projectName);

                UpdateConfigField(c => c.MigrationProject = projectName);
                RefreshMigrationProjectState(null, null);
                await LoadMigrationCombosAsync();
            }
            catch (Exception ex)
            {
                OutputWindowHelper.WriteError(ex.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStartupProject.SelectedItem == null || _isInitializing)
            {
                return;
            }

            UpdateConfigField(c => c.StartUpProject = comboBoxStartupProject.SelectedItem.ToString());
            MemoryParameter.StartUpSelectedProject = MemoryParameter.StartUpProjects
                .FirstOrDefault(w => w.Name == comboBoxStartupProject.SelectedItem.ToString());
        }

        private async void comboBoxDbContextClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (_isInitializing || comboBoxDbContextClass.SelectedItem == null)
                {
                    return;
                }

                var dbContextName = comboBoxDbContextClass.SelectedItem.ToString();
                ApplyDbContextSelection(dbContextName);
                UpdateConfigField(c => c.DbContext = dbContextName);
                await LoadMigrationCombosAsync();
            }
            catch (Exception ex)
            {
                OutputWindowHelper.WriteError(ex.Message);
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing || comboBoxMigrationFolder.SelectedItem == null)
            {
                return;
            }

            var folderName = comboBoxMigrationFolder.SelectedItem.ToString();
            MemoryParameter.MigrationSelectedRootPaths = MemoryParameter.MigrationRootPaths
                .FirstOrDefault(w => w.Key == folderName);
            UpdateConfigField(c => c.MigrationFolder = folderName);
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isInitializing || comboBoxBuildConfig.SelectedItem == null)
            {
                return;
            }

            MemoryParameter.BuildConfigSelectedItem = comboBoxBuildConfig.SelectedItem.ToString();
            UpdateConfigField(c => c.BuildConfig = comboBoxBuildConfig.SelectedItem.ToString());
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

                if (!await DotNetEfToolChecker.IsInstalledAsync())
                {
                    MessageBox.Show(
                        DotNetEfToolChecker.GetInstallMessage(),
                        "dotnet-ef not installed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
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
                DbContextFullName = GetSelectedDbContextFullName(),
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

        private string GetSelectedDbContextFullName()
        {
            if (!string.IsNullOrEmpty(MemoryParameter.DbContextSelectedClass.Key))
            {
                return MemoryParameter.DbContextSelectedClass.Value.Key;
            }

            return null;
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

            if (!_isInitializing)
            {
                UpdateConfigField(c => c.CreationMethod = creation_method.SelectedItem.ToString());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ContactUsForm contactUsForm = new ContactUsForm();

            contactUsForm.ShowDialog();
        }
    }
}