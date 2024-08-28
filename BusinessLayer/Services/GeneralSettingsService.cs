using BusinessLayer.Interfaces;
using DataLayer.Data;
using DataLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services
{
    public class GeneralSettingsService : IGeneralSettingsService
    {
        private readonly ApplicationDbContext _context;

        public GeneralSettingsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeGeneralSettingsViewModel>> GetEmployeesWithSettingsAsync()
        {
            var employeesWithSettings = await _context.Employee
                .Include(e => e.GeneralSettings)
                .ToListAsync();

            return employeesWithSettings.Select(e => new EmployeeGeneralSettingsViewModel
            {
                EmployeeId = e.Id,
                EmployeeName = e.EmployeeName,
                Image = e.image,
                BonusPerHour = e.GeneralSettings.bonusPerHoure,
                RivalPerHour = e.GeneralSettings.rivalPerHour,
                WeeklyHolidays = e.GeneralSettings.WeeklyHolidays
            }).ToList();
        }

        public async Task<GeneralSettings> GetSettingsByEmployeeIdAsync(int id)
        {
            var settings = await _context.GeneralSettings.Include(gs => gs.Employee).FirstOrDefaultAsync(gs => gs.EmployeeId == id);
           
            
            if (settings == null)
            {
                settings = new GeneralSettings
                {
                    EmployeeId = id,
                    WeeklyHolidays = "Saturday,Sunday"
                };

                _context.GeneralSettings.Add(settings);
                await _context.SaveChangesAsync();
            }

            return settings;
        }

        public async Task UpdateSettingsAsync(GeneralSettings model)
        {
            string holidays = string.Join(",", model.WeeklyHolidayList);
            model.WeeklyHolidays = holidays;

            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeDetailsViewModel> GetEmployeeDetailsAsync(int id)
        {
            var employee = await _context.Employee
                .Include(e => e.GeneralSettings)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                return null;
            }

            return new EmployeeDetailsViewModel
            {
                EmployeeId = employee.Id,
                EmployeeName = employee.EmployeeName,
                Image = employee.image,
                BonusPerHour = employee.GeneralSettings.bonusPerHoure,
                RivalPerHour = employee.GeneralSettings.rivalPerHour,
                WeeklyHolidays = employee.GeneralSettings.WeeklyHolidays
            };
        }
    }
}
