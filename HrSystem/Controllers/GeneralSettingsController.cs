
using Microsoft.AspNetCore.Mvc;
using DataLayer.ViewModels;
using BusinessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using DataLayer.Entities;
using BusinessLayer.Services;

namespace HrSystem.Controllers
{
    public class GeneralSettingsController : Controller
    {
        private readonly IGeneralSettingsService _generalSettingsService;

        public GeneralSettingsController(IGeneralSettingsService generalSettingsService)
        {
            _generalSettingsService = generalSettingsService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _generalSettingsService.GetEmployeesWithSettingsAsync();
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var settings = await _generalSettingsService.GetSettingsByEmployeeIdAsync(id);

            if (settings == null)
            {
                return NotFound();
            }

            return View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, GeneralSettings model)
        {
            
            if (id != model.EmployeeId)
            {
                
                return BadRequest();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    await _generalSettingsService.UpdateSettingsAsync(model);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index), "Employees");
            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await _generalSettingsService.GetEmployeeDetailsAsync(id);

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }
    }
}
