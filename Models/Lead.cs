
using System;
using System.ComponentModel.DataAnnotations;

namespace AcxiomCRM.Models
{
    public class Lead
    {
        public int LeadId { get; set; }

        [Required]
        [StringLength(100)]
        public string LeadName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [StringLength(100)]
        public string? CompanyName { get; set; }

        [Required]
        public string Source { get; set; } = "Website";

        public string Status { get; set; } = "New";

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
