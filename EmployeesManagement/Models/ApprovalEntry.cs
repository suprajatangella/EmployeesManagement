using System.ComponentModel;

namespace EmployeesManagement.Models
{
    public class ApprovalEntry : UserActivity
    {
        public int Id { get; set; }
        [DisplayName("Record Id")]
        public int RecordId { get; set; }
        [DisplayName("Document Type")]
        public int DocumentTypeId { get; set; }
        public SystemCodeDetail DocumentType { get; set; }
        [DisplayName("Sequence No")]
        public int SequenceNo { get; set; }
        [DisplayName("Approver Name")]
        public string ApproverId { get; set; }
        public ApplicationUser Approver { get; set; }
        [DisplayName("Status")]
        public int statusId { get; set; }
        public SystemCodeDetail Status { get; set; }
        [DisplayName("Date Sent For Approval")]
        public DateTime DateSentForApproval { get; set; }
        [DisplayName("Last Modified On")]
        public DateTime LastModifiedOn { get; set; }
        [DisplayName("Last Modified By")]
        public ApplicationUser LastModifiedBy { get; set; }
        public string LastModifiedById { get; set; }

        public string Comments { get; set; }
    }
}
