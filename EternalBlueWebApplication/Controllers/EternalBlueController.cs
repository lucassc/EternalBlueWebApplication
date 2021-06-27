using System.Diagnostics;
using EternalBlueWebApplication.Contracts;
using EternalBlueWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EternalBlueWebApplication.Controllers
{
    public class EternalBlueController : Controller
    {
        private readonly IEternalBlueService _eternalBlueService;

        public EternalBlueController(IEternalBlueService eternalBlueService) =>
            _eternalBlueService = eternalBlueService;

        public IActionResult Index()
        {
            var viewModel = new FirstLoginModel
            {
                FirstPasswordASCIIForm = _eternalBlueService.FirstPasswordASCIIForm
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(string pass)
        {
            var isPasswordIncorrect = pass != _eternalBlueService.FirstPassword;
            var viewModel = new FirstLoginModel
            {
                FirstPasswordASCIIForm = _eternalBlueService.FirstPasswordASCIIForm,
                IsPasswordIncorrect = isPasswordIncorrect
            };

            return isPasswordIncorrect
                ? View(viewModel)
                : View("SecondStep");
        }

        [HttpPost]
        public IActionResult SecondStep(string pass)
        {
            var isPasswordIncorrect = pass != _eternalBlueService.SecondPassword;

            return isPasswordIncorrect
                ? View("SecondStep", true)
                : View("DownloadTask");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}