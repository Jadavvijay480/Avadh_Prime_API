using System;
using System.ComponentModel.DataAnnotations;

namespace AVADH_PRIME_API.Models
{
    public class HostelModel
    {
        public int Hostel_Id { get; set; }

        // Basic Information
        [Required]
        public string Hostel_Name { get; set; }

        [Required]
        public string Hostel_Type { get; set; }  // Boys / Girls / Co-ed

        // Capacity Details
        [Required]
        public int Total_Rooms { get; set; }

        [Required]
        public int Total_Beds { get; set; }

        // Location Details
        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Pincode { get; set; }

        // Contact Information
        [Required]
        [Phone]
        public string Contact_No { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Fees
        [Required]
        public decimal Monthly_Rent { get; set; }

        [Required]
        public decimal Security_Deposit { get; set; }

        // Status
        public bool IsActive { get; set; } = true;

        // Audit Fields
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }
    }
}