using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using System;

namespace EfCoreUi.Services
{
    internal static class OutputWindowHelper
    {
        public static IVsOutputWindowPane CreatePane(string title)
        {
            if (MemoryParameter.OutputWindow == null)
            {
                return null;
            }

            var window = MemoryParameter.Dte2?.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
            window?.Activate();
            window?.SetFocus();

            var paneGuid = Guid.NewGuid();
            MemoryParameter.OutputWindow.CreatePane(
                ref paneGuid,
                title,
                Convert.ToInt32(true),
                Convert.ToInt32(true));

            MemoryParameter.OutputWindow.GetPane(ref paneGuid, out var outputPane);
            MemoryParameter.OutputPane = outputPane;
            outputPane?.Activate();
            return outputPane;
        }

        public static void WriteLine(string message)
        {
            MemoryParameter.OutputPane?.OutputString(message + Environment.NewLine);
        }

        public static void WriteError(string message)
        {
            WriteLine("[EfCoreUI Error] " + message);
        }
    }
}
