using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Entities;
namespace DataLayer.ViewModels
{
    public class GeneralSettingsViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Additional Hour Price")]
        public decimal AdditionalHourPrice { get; set; }

        [Required]
        [Display(Name = "Rival Hour Price")]
        public decimal RivalHourPrice { get; set; }

        [Required]
        [Display(Name = "First Day Holiday")]
        public Day FirstDayHoliday { get; set; }

        [Required]
        [Display(Name = "Second Day Holiday")]
        public Day SecondDayHoliday { get; set; }
    }
}
