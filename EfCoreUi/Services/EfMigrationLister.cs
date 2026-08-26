using CliWrap;
using CliWrap.Buffered;
using EnvDTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EfCoreUi.Services
{
    internal static class EfMigrationLister
    {
        public static async Task<IReadOnlyList<string>> ListMigrationsAsync(
            EfCommandOptions options,
            Project migrationProject,
            string dbContextShortName)
        {
            if (migrationProject == null || string.IsNullOrWhiteSpace(dbContextShortName))
            {
                return Array.Empty<string>();
            }

            if (await DotNetEfToolChecker.IsInstalledAsync())
            {
                var fromCli = await ListFromCliAsync(options);
                if (fromCli.Count > 0)
                {
                    return fromCli;
                }

                OutputWindowHelper.WriteLine("dotnet ef migrations list returned no results; falling back to project file scan.");
            }
            else
            {
                OutputWindowHelper.WriteLine("dotnet-ef tool not installed; using project file scan. " + DotNetEfToolChecker.InstallCommand);
            }

            return ListFromProjectFiles(migrationProject, dbContextShortName);
        }

        private static async Task<List<string>> ListFromCliAsync(EfCommandOptions options)
        {
            var dotnetPath = DotNetPathResolver.ResolveDotNetExecutable();
            if (string.IsNullOrEmpty(dotnetPath))
            {
                return new List<string>();
            }

            var args = BuildListArguments(options);

            try
            {
                var result = await Cli.Wrap(dotnetPath)
                    .WithArguments(args)
                    .WithWorkingDirectory(GetWorkingDirectory(options.MigrationProjectPath))
                    .WithValidation(CommandResultValidation.None)
                    .ExecuteBufferedAsync();

                if (result.ExitCode != 0)
                {
                    if (!string.IsNullOrWhiteSpace(result.StandardError))
                    {
                        OutputWindowHelper.WriteLine(result.StandardError.TrimEnd());
                    }

                    return new List<string>();
                }

                return ParseMigrationListOutput(result.StandardOutput);
            }
            catch (Exception ex)
            {
                OutputWindowHelper.WriteError(ex.Message);
                return new List<string>();
            }
        }

        private static string BuildListArguments(EfCommandOptions options)
        {
            var startupArg = options.UseStartupProject && !string.IsNullOrEmpty(options.StartupProjectPath)
                ? $" --startup-project \"{options.StartupProjectPath}\""
                : "";

            var targetFramework = string.IsNullOrEmpty(options.TargetFramework) || options.TargetFramework == "<Default>"
                ? ""
                : $" --framework {options.TargetFramework}";

            return new StringBuilder("ef migrations list")
                .Append($" --project \"{options.MigrationProjectPath}\"")
                .Append(startupArg)
                .Append($" --context {options.DbContextFullName}")
                .Append($" --configuration {options.BuildConfiguration}")
                .Append(" --no-build")
                .Append(targetFramework)
                .ToString();
        }

        private static List<string> ParseMigrationListOutput(string output)
        {
            if (string.IsNullOrWhiteSpace(output))
            {
                return new List<string>();
            }

            return output
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Where(line => !line.StartsWith("Build started", StringComparison.OrdinalIgnoreCase))
                .Where(line => !line.StartsWith("Build succeeded", StringComparison.OrdinalIgnoreCase))
                .Where(line => !line.StartsWith("An error occurred", StringComparison.OrdinalIgnoreCase))
                .Distinct()
                .ToList();
        }

        private static string GetWorkingDirectory(string projectPath)
        {
            var directory = Path.GetDirectoryName(projectPath);
            return string.IsNullOrEmpty(directory) ? Environment.CurrentDirectory : directory;
        }

        private static List<string> ListFromProjectFiles(Project project, string dbContextName)
        {
            var filesContent = ReadAllFilesInProject(project);
            var matchingFiles = SearchPatternInFiles(
                filesContent,
                $@"\[DbContext\(typeof\({dbContextName}\)\)\]",
                @"\[Migration\(.*\)\]");

            return matchingFiles.Keys.ToList();
        }

        private static Dictionary<string, string> SearchPatternInFiles(
            Dictionary<string, string> filesContent,
            string pattern,
            string pattern2)
        {
            var matchingFiles = new Dictionary<string, string>();

            foreach (var fileContent in filesContent)
            {
                if (Regex.IsMatch(fileContent.Value, pattern) && Regex.IsMatch(fileContent.Value, pattern2))
                {
                    matchingFiles.Add(fileContent.Key.Replace(".Designer.cs", ""), fileContent.Value);
                }
            }

            return matchingFiles;
        }

        private static Dictionary<string, string> ReadAllFilesInProject(Project project)
        {
            var filesContent = new Dictionary<string, string>();

            foreach (ProjectItem item in project.ProjectItems)
            {
                ReadFilesContent(item, filesContent);
            }

            return filesContent;
        }

        private static void ReadFilesContent(ProjectItem projectItem, Dictionary<string, string> filesContent)
        {
            foreach (ProjectItem item in projectItem.ProjectItems)
            {
                if (item.FileCount > 0)
                {
                    var filePath = item.FileNames[1];
                    if (Path.GetExtension(filePath).Contains(".cs"))
                    {
                        filesContent[item.Name] = File.ReadAllText(filePath);
                    }
                }

                if (item.ProjectItems != null && item.ProjectItems.Count > 0)
                {
                    ReadFilesContent(item, filesContent);
                }
            }
        }
    }
}
