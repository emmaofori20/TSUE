using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models.Data
{
    public partial class Country
    {
        public Country()
        {
            ProjectCountries = new HashSet<ProjectCountry>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<ProjectCountry> ProjectCountries { get; set; }
    }
}
