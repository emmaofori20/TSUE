using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TSUE.Models.Data;

namespace TSUE.ViewModels
{
    public class FilterFormAndProjectViewModel
    {
        public string StudyTitle { get; set; }
        public int DocumentTypeId { get; set; }
        public SelectList SelectDocumentType { get; set; }
        public int CountryId { get; set; }
        public SelectList SelectCountry { get; set; }
        public int LanguageId { get; set; }
        public SelectList SelectLanguage { get; set; }
        public List<Project> projects { get; set; }
    }
    public class FilterFormViewModel
    {
      
    }
}
