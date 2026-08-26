namespace EfCoreUi.Services
{
    internal class EfCommandOptions
    {
        public OprationModeEnum Operation { get; set; }
        public string MigrationProjectPath { get; set; }
        public string StartupProjectPath { get; set; }
        public string DbContextFullName { get; set; }
        public string BuildConfiguration { get; set; }
        public string OutputDirectory { get; set; }
        public bool NoBuild { get; set; }
        public string TargetFramework { get; set; }
        public string AdditionalArguments { get; set; }
        public bool UseStartupProject { get; set; }

        // Add Migration
        public string MigrationName { get; set; }

        // Generate SQL Script
        public string FromMigration { get; set; }
        public string ToMigration { get; set; }
        public string ScriptOutputPath { get; set; }
        public bool Idempotent { get; set; }
        public bool NoTransactions { get; set; }

        // Update Database
        public string ConnectionString { get; set; }
    }
}
