using EternalBlueWebApplication.Contracts;
using EternalBlueWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace EternalBlueWebApplication.Controllers
{
    public class EternalBlueController : Controller
    {


        private readonly ILogger<EternalBlueController> _logger;
        private readonly IEternalBlueService _eternalBlueService;

        public EternalBlueController(ILogger<EternalBlueController> logger, IEternalBlueService eternalBlueService)
        {
            _logger = logger;
            _eternalBlueService = eternalBlueService;
        }

        public IActionResult Index()
        {
            var viewModel = new FirstLoginModel()
            {
                FirstPasswordASCIIForm = _eternalBlueService.FirstPasswordASCIIForm,                
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(string pass)
        {
            var isPasswordIncorrect = pass != _eternalBlueService.FirstPassword;
            var viewModel = new FirstLoginModel()
            {
                FirstPasswordASCIIForm = _eternalBlueService.FirstPasswordASCIIForm,
                IsPasswordIncorrect = isPasswordIncorrect
            };

            if (isPasswordIncorrect)
                return View(viewModel);
            else
                return View("SecondStep");

        }

        [HttpPost]
        public IActionResult SecondStep(string pass)
        {
            var isPasswordIncorrect = pass != _eternalBlueService.SecondPassword;
            if (isPasswordIncorrect)
                return View("SecondStep", isPasswordIncorrect);
            else
                return View("DownloadTask");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
