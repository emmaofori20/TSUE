using System;
using System.Collections.Generic;

#nullable disable

namespace TSUE.Models.Data
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            Projects = new HashSet<Project>();
        }

        public int DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public bool? IsDeleted { get; set; }
        public byte[] DocumentTypeIcon { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
