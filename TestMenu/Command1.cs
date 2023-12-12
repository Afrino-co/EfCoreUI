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

namespace TestMenu
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class Command1
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;
        public const int Command1Id = 0x0101;
        public const int Command2Id = 0x0102;
        public const int Command3Id = 0x0103;
        public const int Command4Id = 0x0104;
        public const int Command5Id = 0x0105;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("66bc62e3-9fe1-4eb2-b47b-d061171cb2e9");
        //public static readonly Guid Command1Set = new Guid("5b5e61f9-de0e-456b-9e17-3d1cddd43ef8");
        //public static readonly Guid Command2Set = new Guid("64fe8312-51d8-4a74-b95c-34e3c7501dd6");
        //public static readonly Guid Command3Set = new Guid("51cf0775-24af-414d-98cf-4ef23ab1d595");
        //public static readonly Guid Command4Set = new Guid("b9fdc572-d383-47c7-b10d-92022cfa21e6");
        //public static readonly Guid Command5Set = new Guid("8c1c3aee-a32b-4593-a2a5-5974c11c5e92");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command1"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private Command1(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);


            //var menuCommand1ID = new CommandID(CommandSet, Command1Id);
            //var menuItem1 = new MenuCommand(this.Execute1, menuCommand1ID);
            //commandService.AddCommand(menuItem1);

            //var menuCommand2ID = new CommandID(CommandSet, Command2Id);
            //var menuItem2 = new MenuCommand(this.Execute2, menuCommand2ID);
            //commandService.AddCommand(menuItem2);

            //var menuCommand3ID = new CommandID(CommandSet, Command3Id);
            //var menuItem3 = new MenuCommand(this.Execute3, menuCommand3ID);
            //commandService.AddCommand(menuItem3);


        }


        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static Command1 Instance
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
            // Switch to the main thread - the call to AddCommand in Command1's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new Command1(package, commandService);


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

     
            ShowInputDialog(OprationModeEnum.AddMigration, dte);
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
