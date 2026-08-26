using CliWrap;
using CliWrap.Buffered;
using System;
using System.Threading.Tasks;

namespace EfCoreUi.Services
{
    internal static class EfCommandRunner
    {
        public static async Task<int> ExecuteAsync(string workingDirectory, string efArguments)
        {
            var dotnetPath = DotNetPathResolver.ResolveDotNetExecutable();
            if (string.IsNullOrEmpty(dotnetPath))
            {
                var message = "dotnet executable not found. Install the .NET SDK and run: dotnet tool install --global dotnet-ef";
                OutputWindowHelper.CreatePane($"EfCoreUI Output Log {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                OutputWindowHelper.WriteError(message);
                return -1;
            }

            OutputWindowHelper.CreatePane($"EfCoreUI Output Log {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            OutputWindowHelper.WriteLine($"{dotnetPath} {efArguments}");

            try
            {
                var result = await Cli.Wrap(dotnetPath)
                    .WithArguments(efArguments)
                    .WithWorkingDirectory(workingDirectory)
                    .WithValidation(CommandResultValidation.None)
                    .ExecuteBufferedAsync();

                if (!string.IsNullOrWhiteSpace(result.StandardOutput))
                {
                    OutputWindowHelper.WriteLine(result.StandardOutput.TrimEnd());
                }

                if (!string.IsNullOrWhiteSpace(result.StandardError))
                {
                    OutputWindowHelper.WriteLine(result.StandardError.TrimEnd());
                }

                if (result.ExitCode != 0)
                {
                    OutputWindowHelper.WriteError($"Command failed with exit code {result.ExitCode}.");
                }
                else
                {
                    OutputWindowHelper.WriteLine("Command completed successfully.");
                }

                return result.ExitCode;
            }
            catch (Exception ex)
            {
                OutputWindowHelper.WriteError(ex.Message);
                return -1;
            }
        }
    }
}
