using EnvDTE;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EfCoreUi.Services
{
    internal class JsonFileInfo
    {
        public string FileName { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }

    internal static class ConnectionStringExtractor
    {
        public static async Task<Dictionary<string, string>> ExtractFromProjectAsync(Project project)
        {
            if (project == null)
            {
                return new Dictionary<string, string>();
            }

            var connectionStrings = new List<JsonFileInfo>();

            foreach (ProjectItem item in project.ProjectItems)
            {
                await FindJsonFilesRecursivelyAsync(item, connectionStrings);
            }

            return connectionStrings
                .Where(w => w.Properties.Count > 0)
                .SelectMany(s => s.Properties)
                .ToDictionary(w => w.Key, w => w.Value);
        }

        private static async Task FindJsonFilesRecursivelyAsync(ProjectItem projectItem, List<JsonFileInfo> connectionStrings)
        {
            if (Path.GetExtension(projectItem.Name) == ".json" && projectItem.FileCount > 0)
            {
                await AddJsonFileAsync(projectItem, connectionStrings);
            }

            foreach (ProjectItem item in projectItem.ProjectItems)
            {
                if (item.FileCount > 0 && Path.GetExtension(item.Name) == ".json")
                {
                    await AddJsonFileAsync(item, connectionStrings);
                }

                if (item.ProjectItems != null && item.ProjectItems.Count > 0)
                {
                    await FindJsonFilesRecursivelyAsync(item, connectionStrings);
                }
            }
        }

        private static async Task AddJsonFileAsync(ProjectItem projectItem, List<JsonFileInfo> connectionStrings)
        {
            var filePath = projectItem.FileNames[1];
            var fileContent = File.ReadAllText(filePath);
            var connectionStringInfo = new JsonFileInfo { FileName = projectItem.Name };

            await ExtractConnectionStringsFromJsonAsync(fileContent, connectionStringInfo);
            connectionStrings.Add(connectionStringInfo);
        }

        private static async Task ExtractConnectionStringsFromJsonAsync(string jsonContent, JsonFileInfo jsonFileInfo)
        {
            try
            {
                var jsonObject = JObject.Parse(jsonContent);
                await GetJsonObjectsAsync(jsonObject, jsonFileInfo);

                if (jsonFileInfo.Properties.Count > 0)
                {
                    jsonFileInfo.Properties = jsonFileInfo.Properties
                        .Where(w => w.Value.Contains("Data Source=") || w.Value.Contains("Server="))
                        .ToDictionary(w => w.Key, w => w.Value);
                }
            }
            catch (Exception ex)
            {
                OutputWindowHelper.WriteError("Error parsing JSON content: " + ex.Message);
            }
        }

        private static Task GetJsonObjectsAsync(JObject jsonObject, JsonFileInfo jsonFileInfo)
        {
            foreach (var property in jsonObject.Properties())
            {
                if (property.Value.Type == JTokenType.Object)
                {
                    GetJsonObjectsAsync((JObject)property.Value, jsonFileInfo);
                }
                else if (property.Value.Type == JTokenType.String)
                {
                    jsonFileInfo.Properties[property.Name] = property.Value.ToString();
                }
            }

            return Task.CompletedTask;
        }
    }
}
