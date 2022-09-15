using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models.Data
{
    public partial class ProjectLanguage
    {
        public int ProjectLanguageId { get; set; }
        public int ProjectId { get; set; }
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }
        public virtual Project Project { get; set; }
    }
}
