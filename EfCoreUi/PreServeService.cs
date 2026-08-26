using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EfCoreUi.Services;
using Task = System.Threading.Tasks.Task;

namespace EfCoreUi
{
    public static class PreServeService
    {
        public static async Task<List<Project>> GetProjects(DTE2 dte)
        {
            var projects = new List<Project>();

            foreach (Project project in dte.Solution.Projects)
            {
                await GetProjectsRecursive(project, projects);
            }

            return projects;
        }

        private static Task GetProjectsRecursive(Project project, List<Project> projects)
        {
            if (project.Kind != ProjectKinds.vsProjectKindSolutionFolder)
            {
                projects.Add(project);
            }
            else
            {
                foreach (ProjectItem item in project.ProjectItems)
                {
                    if (item.SubProject != null)
                    {
                        GetProjectsRecursive(item.SubProject, projects);
                    }
                }
            }

            return Task.CompletedTask;
        }

        public static Dictionary<string, KeyValuePair<string, string>> GetDbContextClassNames(Project project)
        {
            var dbContextClassNames = new Dictionary<string, KeyValuePair<string, string>>();

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
            var classes = new List<CodeClass>();
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
                if (projectItem.Kind == EnvDTE.Constants.vsProjectKindMisc)
                {
                    continue;
                }

                if (HasEntityFrameworkReference(projectItem) || GetDbContextClassNames(projectItem).Any())
                {
                    MemoryParameter.MigrationProjects.Add(projectItem);
                }
            }

            if (!MemoryParameter.Projects.Any())
            {
                return;
            }

            MemoryParameter.DbContextClassLst.Clear();

            foreach (var item in MemoryParameter.Projects)
            {
                if (item.Kind == EnvDTE.Constants.vsProjectKindMisc)
                {
                    continue;
                }

                foreach (var dic in GetDbContextClassNames(item))
                {
                    MemoryParameter.DbContextClassLst[dic.Key] = dic.Value;
                }
            }
        }

        private static bool HasEntityFrameworkReference(Project project)
        {
            try
            {
                if (!(project.Object is VSLangProj.VSProject vsProject))
                {
                    return false;
                }

                foreach (VSLangProj.Reference reference in vsProject.References)
                {
                    if (reference.Name.IndexOf("EntityFrameworkCore", StringComparison.OrdinalIgnoreCase) >= 0 ||
                        reference.Name.IndexOf("EntityFramework", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        private static bool IsDbContextSubclass(CodeClass codeClass)
        {
            foreach (CodeElement baseClass in codeClass.Bases)
            {
                if (baseClass.FullName == "System.Data.Entity.DbContext" ||
                    baseClass.FullName == "Microsoft.EntityFrameworkCore.DbContext")
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
