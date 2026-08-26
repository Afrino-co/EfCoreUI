using CliWrap;
using CliWrap.Buffered;
using System.Threading.Tasks;

namespace EfCoreUi.Services
{
    internal static class DotNetEfToolChecker
    {
        public const string InstallCommand = "dotnet tool install --global dotnet-ef";

        public static string GetInstallMessage()
        {
            return "The dotnet-ef global tool is not installed.\r\n\r\n" +
                   "Run in a terminal:\r\n" +
                   "  " + InstallCommand + "\r\n\r\n" +
                   "Then restart Visual Studio.";
        }

        public static async Task<bool> IsInstalledAsync()
        {
            var dotnetPath = DotNetPathResolver.ResolveDotNetExecutable();
            if (string.IsNullOrEmpty(dotnetPath))
            {
                return false;
            }

            try
            {
                var result = await Cli.Wrap(dotnetPath)
                    .WithArguments("ef --version")
                    .WithValidation(CommandResultValidation.None)
                    .ExecuteBufferedAsync();

                return result.ExitCode == 0 && !string.IsNullOrWhiteSpace(result.StandardOutput);
            }
            catch
            {
                return false;
            }
        }
    }
}
