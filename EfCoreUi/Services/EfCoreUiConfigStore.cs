using Newtonsoft.Json;
using System.IO;

namespace EfCoreUi.Services
{
    internal static class EfCoreUiConfigStore
    {
        public const string ConfigFilePath = ".vs/EfCoreUiConfigFile.json";

        public static ConfigParameters Load()
        {
            if (!File.Exists(ConfigFilePath))
            {
                return new ConfigParameters();
            }

            try
            {
                return JsonConvert.DeserializeObject<ConfigParameters>(File.ReadAllText(ConfigFilePath))
                       ?? new ConfigParameters();
            }
            catch
            {
                return new ConfigParameters();
            }
        }

        public static void Save(ConfigParameters config)
        {
            var directory = Path.GetDirectoryName(ConfigFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllText(ConfigFilePath, JsonConvert.SerializeObject(config, Formatting.Indented));
        }

        public static void UpdateStartupProject(string startupProject)
        {
            var config = Load();
            config.StartUpProject = startupProject;
            Save(config);
        }
    }
}
