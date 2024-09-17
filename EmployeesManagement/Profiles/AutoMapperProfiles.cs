using AutoMapper;
using EmployeesManagement.Models;
using EmployeesManagement.ViewModels;

namespace EmployeesManagement.Profiles
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
               CreateMap<Employee, employeeViewModel>().ReverseMap();
        }
    }
}
