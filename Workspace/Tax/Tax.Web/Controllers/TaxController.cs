using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tax.Core;
using Tax.Data;
using Tax.Repository;
using Tax.Web.Models;

namespace Tax.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class TaxController : Controller
    {
        private readonly ITaxService _taxService;
        private readonly IUserTaxRepository _userTaxRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public TaxController(
            UserManager<ApplicationUser> userManager,
            ILogger<TaxController> logger,
            IUserTaxRepository userTaxRepository,
            ITaxService taxService)
        {
            _userManager = userManager;
            _logger = logger;
            _userTaxRepository = userTaxRepository;
            _taxService = taxService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var result = await _userTaxRepository.GetAllUserTax(user.Id);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetTax(int? year)
        {
            if (!year.HasValue)
            {
                _logger.LogError("no year is provided");
                throw new ApplicationException("Year must be provided");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var userTax = await _userTaxRepository.GetUserTax(user.Id, year.Value);

            var result = new TaxViewModel
            {
                CharityPaidAmount = userTax.CharityPaidAmount,
                NumberOfChildren = userTax.NumberOfChildren,
                TotalIncome = userTax.TotalIncome,
                Year = userTax.Year,
                TaxDueAmount = userTax.TaxDueAmount
            };

            return View(result);
        }

        [HttpGet]
        public IActionResult AddTax()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTax(TaxViewModel input)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                await _userTaxRepository.AddUserTax(new UserTax
                {
                    UserId = user.Id,
                    CharityPaidAmount = input.CharityPaidAmount,
                    TotalIncome = input.TotalIncome,
                    NumberOfChildren = input.NumberOfChildren,
                    Year = input.Year,
                    TaxDueAmount = _taxService.CalculateTax(input.Year, input.NumberOfChildren, input.CharityPaidAmount, input.TotalIncome)
                });

                _logger.LogInformation("New Tax Created");

                return LocalRedirect($"/Tax/getTax?year={input.Year}");
            }

            return View(input);
        }
    }
}
