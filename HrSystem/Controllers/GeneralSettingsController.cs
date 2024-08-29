﻿
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
            var viewModel = new GeneralSettingsIndexViewModel
            {
                EmployeesWithSettings = await _generalSettingsService.GetEmployeesWithSettingsAsync(),
                EmployeesWithoutSettings = await _generalSettingsService.GetEmployeesWithoutSettingsAsync(),
                NewGeneralSettings = new GeneralSettingsViewModel()
            };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGeneralSettings(GeneralSettingsIndexViewModel model)
        {


     
            if (ModelState.IsValid)
            {
                try
                {
                    await _generalSettingsService.CreateGeneralSettingsAsync(model.NewGeneralSettings);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while creating the general settings.");
                }
            }

            if (model.NewGeneralSettings.WeeklyHolidayList.Count == 0)
            {
                Console.WriteLine("##################");
                Console.WriteLine("##################");
                Console.WriteLine("##################");
                Console.WriteLine("##################");
                Console.WriteLine("##################");
                Console.WriteLine("##################");
                ModelState.AddModelError("WeeklyHolidayList", "Please select at least one holiday.");
            }

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                ModelState.AddModelError("WeeklyHolidayList", error.ErrorMessage);
            }

            var viewModel = new GeneralSettingsIndexViewModel
            {
                EmployeesWithSettings = await _generalSettingsService.GetEmployeesWithSettingsAsync(),
                EmployeesWithoutSettings = await _generalSettingsService.GetEmployeesWithoutSettingsAsync(),
                NewGeneralSettings = model.NewGeneralSettings
            };

            return View(nameof(Index), viewModel);
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
                    return RedirectToAction(nameof(Index), "Employees"); 
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Console.WriteLine($"Concurrency error: {ex.Message}");

                    return View(model); 
                }
            }

            // If the model state is invalid, redisplay the form
            var settings = await _generalSettingsService.GetSettingsByEmployeeIdAsync(id);
            return View(settings);
        }




        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await _generalSettingsService.GetEmployeeDetailsAsync(id);

            Console.WriteLine("viewModel");
            Console.WriteLine("viewModel");
            Console.WriteLine("viewModel");
            Console.WriteLine("viewModel");
            Console.WriteLine("viewModel");
            Console.WriteLine("viewModel");
            if (viewModel == null)
            {

                return NotFound();
            }

            return View(viewModel);
        }
    }
}
