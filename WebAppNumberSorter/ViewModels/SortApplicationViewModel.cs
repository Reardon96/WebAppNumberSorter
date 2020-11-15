using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppNumberSorter.Models;

namespace WebAppNumberSorter.ViewModels
{
    public class SortApplicationViewModel
    {
        public string SortTime { get; set; }
        public string SortType { get; set; }
        public string UnsortedNumbers { get; set; }
        public string SortedNumbers { get; set; }
        public string SortStatus { get; set; }
        public string SortStatusColour { get; set; }
        public string ExportStatus { get; set; }
        public List<SortDetails> SortDetailsList { get; set; }
        public int Id { get; set; }
    }
}
