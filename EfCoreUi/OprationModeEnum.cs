using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfCoreUi
{
    public enum OprationModeEnum
    {
        [Display(Name = "Add Migration")]
        AddMigration,

        [Display(Name = "Remove Migration")]
        RemoveMigration,

        [Display(Name = "Generate Sql Script")]
        GenerateSqlScript,

        [Display(Name = "Update Database")]
        UpdateDatabase,

        [Display(Name = "Drop Database")]
        DropDatabase,
    }
    public enum creation_methodEnum
    {
        StartupProject,
        DesignTimeFactory

    }
}
