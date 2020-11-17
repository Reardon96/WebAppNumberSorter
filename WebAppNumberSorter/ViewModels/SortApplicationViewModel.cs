using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppNumberSorter.Models;
using System.ComponentModel.DataAnnotations;

namespace WebAppNumberSorter.ViewModels
{
    public class SortApplicationViewModel
    {
        [Display(Name = "Sort Id")]
        public int Id { get; set; }

        [Display(Name = "Sort Time")]
        public string SortTime { get; set; }

        [Display(Name = "Sort Type")]
        [Required]
        public string SortType { get; set; }

        [Display(Name = "Unsorted Numbers")]
        [RegularExpression(@"^\d+(,\d+)*$", ErrorMessage = "Please only enter comma delimited integers")]
        [Required]
        public string UnsortedNumbers { get; set; }

        [Display(Name = "Sorted Numbers")]
        public string SortedNumbers { get; set; }


        public string SortStatus { get; set; }
        public string SortStatusColour { get; set; }
        public string ExportStatus { get; set; }
        public string ExportStatusColour { get; set; }
        public string ExportPath { get; set; }
        public List<SortDetails> SortDetailsList { get; set; }
    }
}
