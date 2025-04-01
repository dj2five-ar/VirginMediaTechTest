using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VirginMediaTechExercise.Models;
using VirginMediaTechExercise.Services;

namespace VirginMediaTechExercise.Controllers;

public class HomeController(ILogger<HomeController> logger, ISalesDataFileService salesDataFile) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly ISalesDataFileService _salesDataFile = salesDataFile;

    public IActionResult Index()
    {
        var viewModel = new SalesDataViewModel();
        return View (viewModel);
    }

    [HttpPost]
    public IActionResult Upload(IFormFile file)
    {
        if (file != null && file.Length > 0)
        {
            try
            {
                using var reader = new StreamReader(file.OpenReadStream());
                var csvData = reader.ReadToEnd();
                var salesRecords = _salesDataFile.ParseSalesData(csvData);
                var viewModel = new SalesDataViewModel
                {
                    SalesData = salesRecords
                };

                return View("Index", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Error();
            }
        }

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier });
    }
}
