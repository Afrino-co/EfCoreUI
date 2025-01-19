using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EnvDTE;
using EnvDTE80;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace EfCoreUi
{
    public static class PreServeService
    {
        public static async Task<List<Project>> GetProjects(DTE2 dte)
        {
            List<Project> projects = new List<Project>();

            foreach (Project project in dte.Solution.Projects)
            {
                await GetProjectsRecursive(project, projects);
            }

            return projects;
        }

        private static async Task GetProjectsRecursive(Project project, List<Project> projects)
        {
            if (project.Kind != ProjectKinds.vsProjectKindSolutionFolder)
            {
                projects.Add(project);
            }
            else
            {
                // اگر پوشه‌ای از نوع Solution Folder باشد، به صورت بازگشتی پروژه‌های داخلی آن را بگیرید
                foreach (ProjectItem item in project.ProjectItems)
                {
                    if (item.SubProject != null)
                    {
                        GetProjectsRecursive(item.SubProject, projects);
                    }
                }
            }
        }

        public static Dictionary<string, KeyValuePair<string, string>> GetDbContextClassNames(Project project)
        {
            Dictionary<string, KeyValuePair<string, string>> dbContextClassNames = new Dictionary<string, KeyValuePair<string, string>>();

            foreach (CodeClass codeClass in GetClassesInProject(project))
            {
                if (IsDbContextSubclass(codeClass))
                {
                    dbContextClassNames.Add(codeClass.Name, new KeyValuePair<string, string>(codeClass.FullName, project.Name));
                }
            }

            return dbContextClassNames;
        }

        private static IEnumerable<CodeClass> GetClassesInProject(Project project)
        {
            List<CodeClass> classes = new List<CodeClass>();
            foreach (ProjectItem projectItem in project.ProjectItems)
            {
                GetClassesInProjectItem(projectItem, classes);
            }

            return classes;
        }

        public static void DetectPreserveRequired()
        {
            MemoryParameter.MigrationProjects.Clear();
            foreach (Project projectItem in MemoryParameter.Projects)
            {
                MemoryParameter.MigrationProjects.Add(projectItem);
            }

            if (MemoryParameter.Projects.Any())
            {
                MemoryParameter.DbContextClassLst.Clear();

                foreach (var item in MemoryParameter.Projects)
                {
                    if (item.Kind != EnvDTE.Constants.vsProjectKindMisc)
                    {
                        var dicToAdd = GetDbContextClassNames(item);
                        foreach (var dic in dicToAdd)
                        {
                            MemoryParameter.DbContextClassLst.Add(dic.Key, dic.Value);
                        }
                    }
                }
            }
        }

        private static bool IsDbContextSubclass(CodeClass codeClass)
        {
            foreach (CodeElement baseClass in codeClass.Bases)
            {
                if (baseClass.FullName == "System.Data.Entity.DbContext")
                {
                    return true;
                }
                else if (baseClass.FullName == "Microsoft.EntityFrameworkCore.DbContext")
                {
                    return true;
                }
                else if (baseClass.FullName.ToLower().Trim().Contains("dbcontext"))
                {
                    return true;
                }
            }

            return false;
        }

        private static void GetClassesInProjectItem(ProjectItem projectItem, List<CodeClass> classes)
        {
            if (projectItem.FileCodeModel != null)
            {
                foreach (CodeElement codeElement in projectItem.FileCodeModel.CodeElements)
                {
                    if (codeElement is CodeNamespace codeNamespace)
                    {
                        foreach (CodeElement innerCodeElement in codeNamespace.Members)
                        {
                            if (innerCodeElement is CodeClass codeClass)
                            {
                                classes.Add(codeClass);
                            }
                        }
                    }
                    else if (codeElement is CodeClass codeClass)
                    {
                        classes.Add(codeClass);
                    }
                }
            }

            foreach (ProjectItem nestedProjectItem in projectItem.ProjectItems)
            {
                GetClassesInProjectItem(nestedProjectItem, classes);
            }
        }
    }
}