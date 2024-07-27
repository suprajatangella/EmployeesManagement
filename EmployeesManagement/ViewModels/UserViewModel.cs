using System.ComponentModel;

namespace EmployeesManagement.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        [DisplayName("Email Address")]
        public string Email { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }

        public string UserName { get; set; }
        [DisplayName("National Id")]
        public string? NationalId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? CreatedById { get; set; }
        public string? FullName => $"{FirstName} {MiddleName} {LastName}";
        [DisplayName("User Role")]
        public string? RoleId { get; set; }
    }
}
