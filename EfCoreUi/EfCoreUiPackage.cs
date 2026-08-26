using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Threading;
using System.Threading.Tasks;
using EfCoreUi.Services;
using Task = System.Threading.Tasks.Task;

namespace EfCoreUi
{
    [ProvideAutoLoad(UIContextGuids80.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(EfCoreUiPackage.PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    public sealed class EfCoreUiPackage : AsyncPackage
    {
        public const string PackageGuidString = "ebc275fe-8195-4ca0-a424-8e13475dc26d";

        private DTE _dte;

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            MemoryParameter.OutputWindow = GetService(typeof(SVsOutputWindow)) as IVsOutputWindow;
            _dte = GetService(typeof(DTE)) as DTE;

            if (_dte != null)
            {
                _dte.Events.SolutionEvents.ProjectAdded += OnSolutionChanged;
                _dte.Events.SolutionEvents.ProjectRemoved += OnSolutionChanged;
                _dte.Events.SolutionEvents.ProjectRenamed += OnProjectRenamed;
                _dte.Events.BuildEvents.OnBuildDone += OnBuildDone;
                _dte.Events.SolutionEvents.Opened += OnSolutionOpened;

                await RefreshProjectsAsync();
            }

            await AllCommand.InitializeAsync(this);
        }

        private void OnSolutionOpened()
        {
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                try
                {
                    await RefreshProjectsAsync();
                }
                catch (Exception ex)
                {
                    OutputWindowHelper.WriteError(ex.Message);
                }
            });
        }

        private void OnProjectRenamed(Project project, string oldname)
        {
            OnSolutionChanged(project);
        }

        private void OnSolutionChanged(Project project)
        {
            ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                try
                {
                    await RefreshProjectsAsync();
                }
                catch (Exception ex)
                {
                    OutputWindowHelper.WriteError(ex.Message);
                }
            });
        }

        private void OnBuildDone(vsBuildScope scope, vsBuildAction action)
        {
            OnSolutionChanged(null);
        }

        private async Task RefreshProjectsAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            MemoryParameter.Projects = await PreServeService.GetProjects(_dte as DTE2);
            PreServeService.DetectPreserveRequired();
        }
    }
}
