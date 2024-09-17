using EmployeesManagement.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeesManagement.ViewModels
{
    public class HolidayViewModel : UserActivity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:yyyy/MM/dd}")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public Holiday Holiday { get; set; }
        public List<Holiday> Holidays { get; set; }
    }
}
