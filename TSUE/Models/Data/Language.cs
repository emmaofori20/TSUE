using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models.Data
{
    public partial class Language
    {
        public Language()
        {
            ProjectLanguages = new HashSet<ProjectLanguage>();
        }

        public int LanguageId { get; set; }
        public string LanguageName { get; set; }

        public virtual ICollection<ProjectLanguage> ProjectLanguages { get; set; }
    }
}
