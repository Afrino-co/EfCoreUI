using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task = System.Threading.Tasks.Task;

namespace EfCoreUi
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class AllCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int AddMigrationCommandId = 0x0100;
        public const int RemoveMigrationCommandId = 0x0101;
        public const int GenerateSqlScriptCommandId = 0x0102;
        public const int UpdateDatabaseCommandId = 0x0103;
        public const int DropDatabaseCommandId = 0x0104;
        //public const int Command6Id = 0x0105;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid AddMigrationCommandSet = new Guid("66bc62e3-9fe1-4eb2-b47b-d061171cb2e9");
        public static readonly Guid RemoveMigrationCommandSet = new Guid("e653e644-b032-4991-99c4-9411d79cd117");
        public static readonly Guid GenerateSqlScriptCommandSet = new Guid("4f586b70-475e-4198-b8fd-ddbc71ba18c8");
        public static readonly Guid UpdateDatabaseCommandSet = new Guid("51cf0775-24af-414d-98cf-4ef23ab1d595");
        public static readonly Guid DropDatabaseCommandSet = new Guid("b9fdc572-d383-47c7-b10d-92022cfa21e6");
        //public static readonly Guid Command6Set = new Guid("8c1c3aee-a32b-4593-a2a5-5974c11c5e92");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private AllCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuAddMigrationCommandID = new CommandID(AddMigrationCommandSet, AddMigrationCommandId);
            var menuItem1 = new MenuCommand(this.Execute, menuAddMigrationCommandID);
            commandService.AddCommand(menuItem1);


            var menuRemoveMigrationCommandID = new CommandID(RemoveMigrationCommandSet, RemoveMigrationCommandId);
            var menuItem2 = new MenuCommand(this.Execute, menuRemoveMigrationCommandID);
            commandService.AddCommand(menuItem2);

            var menuGenerateSqlScriptCommandID = new CommandID(GenerateSqlScriptCommandSet, GenerateSqlScriptCommandId);
            var menuItem3 = new MenuCommand(this.Execute, menuGenerateSqlScriptCommandID);
            commandService.AddCommand(menuItem3);

            var menuUpdateDatabaseCommandID = new CommandID(UpdateDatabaseCommandSet, UpdateDatabaseCommandId);
            var menuItem4 = new MenuCommand(this.Execute, menuUpdateDatabaseCommandID);
            commandService.AddCommand(menuItem4);

            var menuDropDatabaseCommandID = new CommandID(DropDatabaseCommandSet, DropDatabaseCommandId);
            var menuItem5 = new MenuCommand(this.Execute, menuDropDatabaseCommandID);
            commandService.AddCommand(menuItem5);
            //var menuGenerateSqlScriptCommandID = new CommandID(CommandSet, GenerateSqlScriptCommandId);
            //var menuItem3 = new MenuCommand(this.Execute3, menuGenerateSqlScriptCommandID);
            //commandService.AddCommand(menuItem3);


        }


        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static AllCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Switch to the main thread - the call to AddCommand in AddMigrationCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new AllCommand(package, commandService);


            // اضافه کردن دستورات با چک کردن تکراری

        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var dte = ServiceProvider.GetServiceAsync(typeof(DTE)).Result as DTE2;

            var commandID = typeof(MenuCommand).GetProperty("CommandID").GetValue(sender);
            var getCurrentCommandGUID =(Guid) typeof(CommandID).GetProperty("Guid").GetValue(commandID);
            if (getCurrentCommandGUID == AddMigrationCommandSet)
            {
                ShowInputDialog(OprationModeEnum.AddMigration, dte);
            }
            else if (getCurrentCommandGUID == RemoveMigrationCommandSet)
            {
                ShowInputDialog(OprationModeEnum.RemoveMigration, dte);

            }
            else if (getCurrentCommandGUID == GenerateSqlScriptCommandSet)
            {
                ShowInputDialog(OprationModeEnum.GenerateSqlScript, dte);

            }
            else if (getCurrentCommandGUID == UpdateDatabaseCommandSet)
            {
                ShowInputDialog(OprationModeEnum.UpdateDatabase, dte);

            }
            else if (getCurrentCommandGUID == DropDatabaseCommandSet)
            {
                ShowInputDialog(OprationModeEnum.DropDatabase, dte);

            }

        }



        private void ShowInputDialog(OprationModeEnum oprationModeEnum, DTE2 dTE2)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            using (var inputDialog = new Form1(dTE2, oprationModeEnum))
            {
                inputDialog.Text = oprationModeEnum.GetDisplayName();
                var result = inputDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //string migrationName = inputDialog.textBoxMigrationName.Text;
                    //string migrationsProject = inputDialog.comboBoxMigrationsProject.Text;
                    //string startupProject = inputDialog.comboBoxStartupProject.Text;
                    //string dbContextClass = inputDialog.comboBoxDbContextClass.Text;
                    // انجام عملیات مورد نظر با inputText
                }
            }
        }
    }


}
