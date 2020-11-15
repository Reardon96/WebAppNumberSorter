using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppNumberSorter.Models
{
    public class SortDetailsRepository : ISortDetailsRepository
    {
        private readonly AppDbContext _appDbContext;

        public SortDetailsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Insert(SortDetails sortDetails)
        {
            _appDbContext.SortDetails.Add(sortDetails);
            _appDbContext.SaveChanges();
        }

        public SortDetails GetSortDetailsBySortId(Guid sortId)
        {
            SortDetails sortDetails = _appDbContext.SortDetails.Select(n => n).Where(i => i.SortId == sortId).FirstOrDefault();
            return sortDetails;
        }

        public List<SortDetails> GetAllSortDetails()
        {
            List<SortDetails> sortDetails = _appDbContext.SortDetails.Select(n => n).OrderBy(o => o.Id).ToList();
            return sortDetails;
        }
    }
}
