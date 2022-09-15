using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models.Data
{
    public partial class ProjectCountry
    {
        public int ProjectCountryId { get; set; }
        public int ProjectId { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual Project Project { get; set; }
    }
}
