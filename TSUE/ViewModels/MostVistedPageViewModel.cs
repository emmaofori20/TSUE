using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSUE.ViewModels
{
    public class MostVistedPageViewModel
    {
        public int ProjectId { get; set; }
        public string Country { get; set; }
        public string StateOrCity { get; set; }
    }

    public class MostvistedProjectGraph
    {
        public int NumberOfVisits { get; set; }
        public string projectName { get; set; }
    }

    public class MostDownloadedDocumentViewModle
    {
        public int DocumentId { get; set; }
        public string Country { get; set; }
        public string StateOrCity { get; set; }
    }
}
