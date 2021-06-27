using System.Diagnostics;
using EternalBlueWebApplication.Contracts;
using EternalBlueWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EternalBlueWebApplication.Controllers
{
    public class EternalBlueController : Controller
    {
        private readonly IPasswordService _passwordService;

        public EternalBlueController(IPasswordService passwordService) =>
            _passwordService = passwordService;

        public IActionResult Index()
        {
            var viewModel = BuildFirstLoginModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string pass)
        {
            if (_passwordService.IsFirstPassword(pass))
            {
                return View("SecondStep");
            }

            var viewModel = BuildFirstLoginModel(isPasswordIncorrect: true);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SecondStep(string pass)
        {
            return _passwordService.IsSecondPassword(pass)
                ? View("DownloadTask")
                : View("SecondStep", true);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});

        private FirstLoginModel BuildFirstLoginModel(bool isPasswordIncorrect = false)
        {
            var firstPasswordAscii = _passwordService.GetFirstPasswordAscii();
            var viewModel = new FirstLoginModel(firstPasswordAscii, isPasswordIncorrect);

            return viewModel;
        }
    }
}