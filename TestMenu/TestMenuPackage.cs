using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.IO.Packaging;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

namespace TestMenu
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(TestMenuPackage.PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class TestMenuPackage : AsyncPackage
    {
        /// <summary>
        /// TestMenuPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "ebc275fe-8195-4ca0-a424-8e13475dc26d";

        public IVsOutputWindowPane outputWindowPane;

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token to monitor for initialization cancellation, which can occur when VS is shutting down.</param>
        /// <param name="progress">A provider for progress updates.</param>
        /// <returns>A task representing the async work of package initialization, or an already completed task if there is none. Do not return null from this method.</returns>
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            // When initialized asynchronously, the current thread may be a background thread at this point.
            // Do any initialization that requires the UI thread after switching to the UI thread.
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
            await Command1.InitializeAsync(this);
            await Command2.InitializeAsync(this);
            await Command3.InitializeAsync(this);
            await Command4.InitializeAsync(this);
            await Command5.InitializeAsync(this);

            //// ایجاد یک پنجره خروجی با نام "My Extension Output"
            //var outputWindow = await GetServiceAsync(typeof(SVsOutputWindow)) as IVsOutputWindow;
            //var guid = Guid.NewGuid();
            //outputWindow.CreatePane(ref guid, "My Extension Output", 1, 0);
            //outputWindow.GetPane(ref guid, out outputWindowPane);
            //await Command2.InitializeAsync(this);
        }
        private void LogToOutputWindow(string message)
        {
            // بررسی ایجاد پنجره خروجی
            if (outputWindowPane == null)
            {
                // خطایی به خاطر عدم ایجاد پنجره خروجی
                return;
            }

            // نمایش پیام در پنجره خروجی
            outputWindowPane.OutputString(message + Environment.NewLine);
        }
        #endregion
    }
}
