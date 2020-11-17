using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppNumberSorter.Repositories;
using WebAppNumberSorter.ViewModels;
using WebAppNumberSorter.Models;
using System.Text.Json;
using System.IO;

namespace WebAppNumberSorter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISortDetailsRepository _sortDetailsRepository;

        public HomeController(ISortDetailsRepository sortDetailsRepository)
        {
            _sortDetailsRepository = sortDetailsRepository;
        }

        public IActionResult Task()
        {
            return View();
        }

        public IActionResult ExportToJson()
        {
            var sortDetailsHistory = _sortDetailsRepository.GetAllSortDetails();
            var jsonSortDetails = JsonSerializer.Serialize(sortDetailsHistory);
            var jsonSortDetailsPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "SortDetailsData.json");

            System.IO.File.WriteAllText(jsonSortDetailsPath, jsonSortDetails);

            return View("Index", new SortApplicationViewModel() { ExportStatus = "Export Success: ", SortDetailsList = sortDetailsHistory, 
                ExportPath = jsonSortDetailsPath, ExportStatusColour = "Green" });
        }
        

        public IActionResult Index(Guid sortId)
        {
            var sortDetailsHistory = _sortDetailsRepository.GetAllSortDetails();
            if (sortId != Guid.Empty)
            {
                var sortDetails = _sortDetailsRepository.GetSortDetailsBySortId(sortId);
                if(sortDetails is null)
                {
                    return SortFailure();
                }

                var viewModel = new SortApplicationViewModel() { UnsortedNumbers = sortDetails.UnsortedNumbers, SortedNumbers = sortDetails.SortedNumbers, SortTime = sortDetails.SortTime.ToString(), 
                    SortType = sortDetails.SortType, SortStatus = "Sort Success", SortStatusColour = "Green", SortDetailsList = sortDetailsHistory };

                return View(viewModel);
            }
            else
            {
                return View(new SortApplicationViewModel() { SortDetailsList = sortDetailsHistory });
            }
        }

        [HttpPost]
        public RedirectToActionResult Submit(SortApplicationViewModel inputNumbers)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var sortId = ProcessSort(inputNumbers);
                    return RedirectToAction("Index", new { sortId = sortId });
                }
                catch
                {
                    return RedirectToAction("SortFailure");
                }
            }
            else return RedirectToAction("ModelNotValid", inputNumbers);
        }

        public IActionResult ModelNotValid(SortApplicationViewModel invalidModel)
        {
            var sortDetailsHistory = _sortDetailsRepository.GetAllSortDetails();
            invalidModel.SortDetailsList = sortDetailsHistory;
            return View("Index", invalidModel);
        }

        public IActionResult SortFailure()
        {
            var sortDetailsHistory = _sortDetailsRepository.GetAllSortDetails();
            return View("Index", new SortApplicationViewModel() { SortStatus = "Sort Failure", SortStatusColour = "Red", SortDetailsList = sortDetailsHistory });
        }

        public List<int> SortIntegers(List<int> integerList, string sortType)
        {
            if (sortType == "Ascending")
            {
                return integerList.OrderBy(i => i).ToList();
            }
            else
            {
                return integerList.OrderByDescending(i => i).ToList();
            }
        }

        public Guid ProcessSort(SortApplicationViewModel inputNumbers)
        {
            var unsortedNumbersList = inputNumbers.UnsortedNumbers.Split(',').Select(Int32.Parse).ToList();
            
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            var sortedNumbersList = SortIntegers(unsortedNumbersList, inputNumbers.SortType);
            watch.Stop();
            var sortDuration = watch.ElapsedMilliseconds;
            var sortId = Guid.NewGuid();

            _sortDetailsRepository.Insert(new SortDetails
            {
                SortId = sortId,
                UnsortedNumbers = string.Join(",", unsortedNumbersList),
                SortedNumbers = string.Join(",", sortedNumbersList),
                SortType = inputNumbers.SortType,
                SortTime = sortDuration
            });
            return sortId;
        }
    }
}
