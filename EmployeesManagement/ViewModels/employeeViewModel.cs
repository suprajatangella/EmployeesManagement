﻿using EmployeesManagement.Models;
using System.ComponentModel;

namespace EmployeesManagement.ViewModels
{
    public class employeeViewModel
    {
        public int Id { get; set; }
        public string EmpNo { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Middle Name")]
        public string MiddleName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {MiddleName} {LastName}";
        public string EmailAddress { get; set; }
        [DisplayName("Phone Number")]
        public int PhoneNumber { get; set; }
        [DisplayName("Country Name")]
        public int? CountryId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        [DisplayName("Department Name")]
        public int? DepartmentId { get; set; }
        [DisplayName("Designation Name")]
        public int? DesignationId { get; set; }
        [DisplayName("Gender")]
        public int? GenderId { get; set; }
        [DisplayName("Employee Photo")]
        public string? Photo { get; set; }
        [DisplayName("Employement Date")]
        public DateTime? EmployementDate { get; set; }
        public int? StatusId { get; set; }
        public DateTime? InactiveDate { get; set; }
        public int? CauseOfInactivityId { get; set; }
        public SystemCodeDetail CauseOfInactivity { get; set; }
        public DateTime? TerminationDate { get; set; }
        public int? ReasonforterminationId { get; set; }
        [DisplayName("Bank Name")]
        public int? BankId { get; set; }
        [DisplayName("Bank Account No")]
        public string? BankAccountNo { get; set; }

        [DisplayName("International Bank Account No")]
        public string? IBAN { get; set; }
        [DisplayName("SWIFT Code")]
        public string? SWIFTCode { get; set; }
        [DisplayName("N.S.S.F No")]
        public string? NSSFNO { get; set; }
        [DisplayName("NHIF No")]
        public string? NHIF { get; set; }
        [DisplayName("Company Email")]
        public string? CompanyEmail { get; set; }
        [DisplayName("KRA PIN")]
        public string? KRAPIN { get; set; }
        [DisplayName("Passport No")]
        public string? PassportNo { get; set; }
        [DisplayName("Employment Terms")]
        public int? EmploymentTermsId { get; set; }
        [DisplayName("Allocated Leave Balance")]
        public Decimal? AllocatedLeaveDays { get; set; }
        [DisplayName("Leave Balance")]
        public Decimal? LeaveOutStandingBalance { get; set; }
        [DisplayName("Pays Tax")]
        public bool? PaysTax { get; set; }

        [DisplayName("Disability Type")]
        public int? DisabilityId { get; set; }

        public string? DisabilityCertificate { get; set; }

        public Employee Employee { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
