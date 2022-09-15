using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models.Data
{
    public partial class Sector
    {
        public Sector()
        {
            ProjectSectors = new HashSet<ProjectSector>();
        }

        public int SectorId { get; set; }
        public string SectorName { get; set; }

        public virtual ICollection<ProjectSector> ProjectSectors { get; set; }
    }
}
