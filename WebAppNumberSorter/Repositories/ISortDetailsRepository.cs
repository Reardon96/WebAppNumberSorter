using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppNumberSorter.Models;

namespace WebAppNumberSorter.Repositories
{
    public interface ISortDetailsRepository
    {
        void Insert(SortDetails sortDetails);
        SortDetails GetSortDetailsBySortId(Guid id);
        List<SortDetails> GetAllSortDetails();
    }
}
