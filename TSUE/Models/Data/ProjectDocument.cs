using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models.Data
{
    public partial class ProjectDocument
    {
        public int ProjectDocumentId { get; set; }
        public string DocumentName { get; set; }
        public byte[] DocumentFile { get; set; }
        public int ProjectId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual Project Project { get; set; }
    }
}
