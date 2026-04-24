using System;
using System.ComponentModel.DataAnnotations;

namespace AVADH_PRIME_API.Models
{
    public class StudentModel
    {
        public int Student_Id { get; set; }

        // Personal Information
        [Required]
        public string student_image { get; set; }
        [Required]
        public string H_Enrollment_No { get; set; }

        [Required]
        public string First_Name { get; set; }

        [Required]
        public string Last_Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string BloodGroup { get; set; }

        // Contact Information
        [Required]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Pincode { get; set; }

        // Academic Information
        [Required]
        public string U_Enrollment_No { get; set; }

        [Required]
        public string Course { get; set; }

        [Required]
        public string Branch { get; set; }

        [Required]
        public int Semester { get; set; }

        [Required]
        public string CollegeName { get; set; }

        // Hostel Information
        [Required]
        public int Hostel_Id { get; set; }

        [Required]
        public DateTime AdmissionDate { get; set; }

        // Parent / Guardian Info
        [Required]
        public string Father_Name { get; set; }

        [Required]
        public string Mother_Name { get; set; }

        [Required]
        public string Parent_Phone { get; set; }

        [Required]
        public string Emergency_Contact { get; set; }

        // ID Proof
        [Required]
        public string ID_Proof_Type { get; set; }

        [Required]
        public string ID_Proof_No { get; set; }

        [Required]
        public string ID_Proof_Path { get; set; }

        // Status
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string Status { get; set; }

        // Audit Fields
        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}