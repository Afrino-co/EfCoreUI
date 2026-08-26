using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using System.Windows.Forms;
using EfCoreUi.Services;
using Task = System.Threading.Tasks.Task;

namespace EfCoreUi
{
    internal sealed class AllCommand
    {
        public const int AddMigrationCommandId = 0x0100;
        public const int RemoveMigrationCommandId = 0x0101;
        public const int GenerateSqlScriptCommandId = 0x0102;
        public const int UpdateDatabaseCommandId = 0x0103;
        public const int DropDatabaseCommandId = 0x0104;

        public static readonly Guid AddMigrationCommandSet = new Guid("66bc62e3-9fe1-4eb2-b47b-d061171cb2e9");
        public static readonly Guid RemoveMigrationCommandSet = new Guid("e653e644-b032-4991-99c4-9411d79cd117");
        public static readonly Guid GenerateSqlScriptCommandSet = new Guid("4f586b70-475e-4198-b8fd-ddbc71ba18c8");
        public static readonly Guid UpdateDatabaseCommandSet = new Guid("51cf0775-24af-414d-98cf-4ef23ab1d595");
        public static readonly Guid DropDatabaseCommandSet = new Guid("b9fdc572-d383-47c7-b10d-92022cfa21e6");

        private readonly AsyncPackage package;

        private AllCommand(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            AddCommand(commandService, AddMigrationCommandSet, AddMigrationCommandId, OprationModeEnum.AddMigration);
            AddCommand(commandService, RemoveMigrationCommandSet, RemoveMigrationCommandId, OprationModeEnum.RemoveMigration);
            AddCommand(commandService, GenerateSqlScriptCommandSet, GenerateSqlScriptCommandId, OprationModeEnum.GenerateSqlScript);
            AddCommand(commandService, UpdateDatabaseCommandSet, UpdateDatabaseCommandId, OprationModeEnum.UpdateDatabase);
            AddCommand(commandService, DropDatabaseCommandSet, DropDatabaseCommandId, OprationModeEnum.DropDatabase);
        }

        public static AllCommand Instance { get; private set; }

        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider => this.package;

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new AllCommand(package, commandService);
        }

        private void AddCommand(OleMenuCommandService commandService, Guid commandSet, int commandId, OprationModeEnum mode)
        {
            var id = new CommandID(commandSet, commandId);
            var command = new OleMenuCommand((sender, e) => ExecuteAsync(mode), id);
            commandService.AddCommand(command);
        }

        private void ExecuteAsync(OprationModeEnum mode)
        {
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                try
                {
                    await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
                    var dte = await ServiceProvider.GetServiceAsync(typeof(DTE)) as DTE2;
                    ShowInputDialog(mode, dte);
                }
                catch (Exception ex)
                {
                    OutputWindowHelper.WriteError(ex.Message);
                }
            });
        }

        private void ShowInputDialog(OprationModeEnum oprationModeEnum, DTE2 dTE2)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            using (var inputDialog = new Form1(dTE2, oprationModeEnum))
            {
                inputDialog.Text = oprationModeEnum.GetDisplayName();
                inputDialog.ShowDialog();
            }
        }
    }
}
