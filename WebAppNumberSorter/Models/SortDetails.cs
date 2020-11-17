using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAppNumberSorter.Models
{
    public class SortDetails
    {
        [Key]
        public int Id { get; set; }
        public Guid SortId { get; set; }
        public string UnsortedNumbers { get; set; }
        public string SortedNumbers { get; set; }
        public string SortType { get; set; }
        public long SortTime { get; set; }
    }
}
