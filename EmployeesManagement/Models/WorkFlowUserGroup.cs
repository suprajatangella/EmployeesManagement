﻿using System.ComponentModel;

namespace EmployeesManagement.Models
{
    public class WorkFlowUserGroup
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        [DisplayName("Department Name")]
        public int? DepartmentId { get; set; }
        public Department Department { get; set; }

        [DisplayName("Document Type")]
        public int? DocumentTypeId { get; set; }
        public SystemCodeDetail DocumentType { get; set; }
    }

    public class WorkFlowUserGroupMember
    {
        public int Id { get; set; }

        public int WorkFlowUsergroupId { get; set; }
        public WorkFlowUserGroup WorkFlowUserGroup { get; set; }

        [DisplayName("Document Sender")]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        [DisplayName("Document Approver")]
        public string ApproverId { get; set; }
        public ApplicationUser Approver { get; set; }
        [DisplayName("Sequence No")]
        public int SequenceNo { get; set; }
    }
}
