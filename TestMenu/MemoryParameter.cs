using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnvDTE80;

namespace TestMenu
{
    public static class MemoryParameter
    {
        #region General

        public static DTE2 Dte2 { get; set; }
        public static List<Project> Projects { get; set; }

        #endregion
        
        #region Add Migration
        public static List<Project> MigrationProjects { get; set; }
        public static Project MigrationSelectedProject { get; set; }
        #endregion

        #region StartUp Project
        public static List<Project> StartUpProjects { get; set; }
        public static Project StartUpSelectedProject { get; set; }
        #endregion

        #region DbContext
        public static Dictionary<string, string> DbContextClassLst { get; set; }
        public static KeyValuePair<string,string> DbContextSelectedClass { get; set; }
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
