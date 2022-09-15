using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TSUE.ViewModels
{
    public class DocumentTypeViewModel
    {
        public int DocumentTypeId { get; set; }
        [Required]
        public string DocumentTypeName { get; set; }
        public IFormFile DocumentTypeIcon { get; set; }
        public byte[] DocumentTypeImage { get; set; }
    }
}
