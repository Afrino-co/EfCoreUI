using System;
using System.Diagnostics;
using System.IO;

namespace EfCoreUi.Services
{
    internal static class DotNetPathResolver
    {
        private static string _cachedPath;

        public static string ResolveDotNetExecutable()
        {
            if (!string.IsNullOrEmpty(_cachedPath) && File.Exists(_cachedPath))
            {
                return _cachedPath;
            }

            var dotnetRoot = Environment.GetEnvironmentVariable("DOTNET_ROOT");
            if (!string.IsNullOrEmpty(dotnetRoot))
            {
                var fromRoot = Path.Combine(dotnetRoot, GetExecutableName());
                if (File.Exists(fromRoot))
                {
                    return _cachedPath = fromRoot;
                }
            }

            var fromPath = FindOnPath();
            if (!string.IsNullOrEmpty(fromPath))
            {
                return _cachedPath = fromPath;
            }

            foreach (var candidate in GetDefaultCandidates())
            {
                if (File.Exists(candidate))
                {
                    return _cachedPath = candidate;
                }
            }

            return null;
        }

        private static string GetExecutableName()
        {
            return Environment.OSVersion.Platform == PlatformID.Unix ? "dotnet" : "dotnet.exe";
        }

        private static string FindOnPath()
        {
            try
            {
                var isUnix = Environment.OSVersion.Platform == PlatformID.Unix;
                var psi = new ProcessStartInfo
                {
                    FileName = isUnix ? "/bin/sh" : "cmd.exe",
                    Arguments = isUnix ? "-c \"which dotnet\"" : "/c where dotnet",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(psi))
                {
                    var output = process.StandardOutput.ReadToEnd().Trim();
                    process.WaitForExit(5000);

                    if (process.ExitCode != 0 || string.IsNullOrEmpty(output))
                    {
                        return null;
                    }

                    var firstLine = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                    return File.Exists(firstLine) ? firstLine : null;
                }
            }
            catch
            {
                return null;
            }
        }

        private static string[] GetDefaultCandidates()
        {
            if (Environment.OSVersion.Platform == PlatformID.Unix)
            {
                return new[]
                {
                    "/usr/bin/dotnet",
                    "/usr/local/share/dotnet/dotnet",
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".dotnet", "dotnet")
                };
            }

            return new[]
            {
                @"C:\Program Files\dotnet\dotnet.exe",
                @"C:\Program Files (x86)\dotnet\dotnet.exe"
            };
        }
    }
}
