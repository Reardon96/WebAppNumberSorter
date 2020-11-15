using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAppNumberSorter.Models;
using WebAppNumberSorter.ViewModels;
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
            List<SortDetails> sortDetails = _sortDetailsRepository.GetAllSortDetails();
            string jsonSortDetails = JsonSerializer.Serialize(sortDetails);
            string jsonSortDetailsPath = Path.Combine(Directory.GetCurrentDirectory(), "App_Data", "SortDetailsData.json");

            System.IO.File.WriteAllText(jsonSortDetailsPath, jsonSortDetails);

            return View("Index", new SortApplicationViewModel() { ExportStatus = "Export Success"});
        }
        

        public IActionResult Index(Guid sortId)
        {
            List<SortDetails> sortDetailsHistory = _sortDetailsRepository.GetAllSortDetails();
            if (sortId != Guid.Empty)
            {
                SortDetails sortDetails = _sortDetailsRepository.GetSortDetailsBySortId(sortId);

                SortApplicationViewModel viewModel = new SortApplicationViewModel() { SortedNumbers = sortDetails.SortedNumbers, SortTime = sortDetails.SortTime.ToString(), 
                    SortType = sortDetails.SortType, SortStatus = "Sort Success", SortStatusColour = "Black", SortDetailsList = sortDetailsHistory };

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
            try
            {
                Guid sortId = Sort(inputNumbers);
                return RedirectToAction("Index", new { sortId = sortId });
            }
            catch
            {
                return RedirectToAction("SortFailure");
            }
        }

        public IActionResult SortFailure()
        {
            List<SortDetails> sortDetailsHistory = _sortDetailsRepository.GetAllSortDetails();
            return View("Index", new SortApplicationViewModel() { SortStatus = "Sort Failure", SortStatusColour = "Red", SortDetailsList = sortDetailsHistory });
        }

        public Guid Sort(SortApplicationViewModel inputNumbers)
        {
            List<int> unsortedNumbersList = inputNumbers.UnsortedNumbers.Split(',').Select(Int32.Parse).ToList();
            List<int> sortedNumbersList = new List<int>();

            var watch = new System.Diagnostics.Stopwatch();
            if (inputNumbers.SortType == "Ascending")
            {
                watch.Start();
                sortedNumbersList = unsortedNumbersList.OrderBy(i => i).ToList();
                watch.Stop();
            }
            else
            {
                watch.Start();
                sortedNumbersList = unsortedNumbersList.OrderByDescending(i => i).ToList();
                watch.Stop();
            }
            long sortDuration = watch.ElapsedMilliseconds;
            Guid sortId = Guid.NewGuid();

            _sortDetailsRepository.Insert(new SortDetails
            {
                SortId = sortId,
                UnsortedNumbers = NumberListToString(unsortedNumbersList),
                SortedNumbers = NumberListToString(sortedNumbersList),
                SortType = inputNumbers.SortType,
                SortTime = sortDuration
            });

            return sortId;
        }

        public string NumberListToString(List<int> numberList)
        {
            string numberString = "";
            foreach (int number in numberList)
            {
                numberString = numberString + number.ToString() + ",";
            }
            numberString = numberString.Remove(numberString.Length - 1);

            return numberString;
        }
    }
}
