using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Totvs.Ats.Infrastructure.Database.Models
{
    public class TotvsAtsDatabaseSetting
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ApplicantCollectionName { get; set; } = null!;
        public string JobCollectionName { get; set; } = null!;
    }
}
