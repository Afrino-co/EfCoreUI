using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE80;
using Microsoft.VisualStudio.Shell.Interop;

namespace TestMenu
{
    public static class MemoryParameter
    {
        #region General

        public static DTE2 Dte2 { get; set; }
        public static List<Project> Projects { get; set; } = new List<Project>();
        public static IVsOutputWindowPane OutputPane { get; set; }
        public static IVsOutputWindow OutputWindow { get; set; }

        #endregion

        #region Add Migration

        public static List<Project> MigrationProjects { get; set; } = new List<Project>();
        public static Project MigrationSelectedProject { get; set; }

        #endregion

        #region StartUp Project

        public static List<Project> StartUpProjects { get; set; }
        public static Project StartUpSelectedProject { get; set; }

        #endregion

        #region DbContext

        public static Dictionary<string, KeyValuePair<string, string>> DbContextClassLst { get; set; } = new Dictionary<string, KeyValuePair<string, string>>();
        public static KeyValuePair<string, KeyValuePair<string,string>> DbContextSelectedClass { get; set; }

        #endregion

        #region Migration RootPath

        public static Dictionary<string, string> MigrationRootPaths { get; set; }
        public static KeyValuePair<string, string> MigrationSelectedRootPaths { get; set; }

        #endregion

        #region build option

        public static string BuildConfigSelectedItem { get; set; }

        #endregion
    }
}