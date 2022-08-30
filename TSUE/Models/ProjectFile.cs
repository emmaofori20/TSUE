using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models
{
    public partial class ProjectFile
    {
        public int ProjectFileId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectFileName { get; set; }
        public byte[] ProjectFile1 { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual Project Project { get; set; }
    }
}
