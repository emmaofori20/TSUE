using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models.Data
{
    public partial class ProjectSector
    {
        public int ProjectSectorId { get; set; }
        public int SectorId { get; set; }
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }
        public virtual Sector Sector { get; set; }
    }
}
