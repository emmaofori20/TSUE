using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models.Data;

namespace TSUE.ViewModels
{
    public class ProjectAndDocumentTypeViewModel
    {
       public List<Project> Projects { get; set; }

       public List<DocumentType> documentType { get; set; }
    }
}
