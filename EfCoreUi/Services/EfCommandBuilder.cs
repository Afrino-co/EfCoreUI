using System.Text;

namespace EfCoreUi.Services
{
    internal static class EfCommandBuilder
    {
        public static string Build(EfCommandOptions options)
        {
            var buildMode = options.NoBuild ? " --no-build" : "";
            var targetFramework = string.IsNullOrEmpty(options.TargetFramework) || options.TargetFramework == "<Default>"
                ? ""
                : $" --framework {options.TargetFramework}";
            var startupArg = options.UseStartupProject && !string.IsNullOrEmpty(options.StartupProjectPath)
                ? $" --startup-project \"{options.StartupProjectPath}\""
                : "";

            var sb = new StringBuilder();
            sb.Append($"ef {GetOperation(options.Operation)}");
            sb.Append($" --project \"{options.MigrationProjectPath}\"");
            sb.Append(startupArg);
            sb.Append($" --context {options.DbContextFullName}");
            sb.Append($" --configuration {options.BuildConfiguration}{buildMode}{targetFramework}");

            switch (options.Operation)
            {
                case OprationModeEnum.AddMigration:
                    sb.Append($" \"{options.MigrationName?.Replace(" ", "_").Trim()}\"");
                    sb.Append($" --output-dir \"{options.OutputDirectory}\"");
                    break;

                case OprationModeEnum.RemoveMigration:
                case OprationModeEnum.DropDatabase:
                    sb.Append(" --force");
                    break;

                case OprationModeEnum.GenerateSqlScript:
                    sb.Append($" {options.FromMigration} {options.ToMigration}");
                    sb.Append($" --output \"{options.ScriptOutputPath}\"");
                    if (options.Idempotent)
                    {
                        sb.Append(" --idempotent");
                    }
                    if (options.NoTransactions)
                    {
                        sb.Append(" --no-transactions");
                    }
                    break;

                case OprationModeEnum.UpdateDatabase:
                    sb.Append($" {options.ToMigration}");
                    if (!string.IsNullOrEmpty(options.ConnectionString))
                    {
                        sb.Append($" --connection \"{options.ConnectionString}\"");
                    }
                    break;
            }

            if (!string.IsNullOrWhiteSpace(options.AdditionalArguments))
            {
                sb.Append($" {options.AdditionalArguments}");
            }

            return sb.ToString();
        }

        private static string GetOperation(OprationModeEnum mode)
        {
            switch (mode)
            {
                case OprationModeEnum.AddMigration:
                    return "migrations add";
                case OprationModeEnum.RemoveMigration:
                    return "migrations remove";
                case OprationModeEnum.GenerateSqlScript:
                    return "migrations script";
                case OprationModeEnum.UpdateDatabase:
                    return "database update";
                case OprationModeEnum.DropDatabase:
                    return "database drop";
                default:
                    return "migrations add";
            }
        }
    }
}
