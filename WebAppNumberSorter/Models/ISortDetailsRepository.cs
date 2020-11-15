using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppNumberSorter.Models
{
    public interface ISortDetailsRepository
    {
        void Insert(SortDetails sortDetails);
        SortDetails GetSortDetailsBySortId(Guid id);
        List<SortDetails> GetAllSortDetails();
    }
}
