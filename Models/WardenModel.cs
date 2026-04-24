public class WardenModel
{
    public int Warden_Id { get; set; }

    public string Warden_Image { get; set; }
    public string Warden_Code { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string BloodGroup { get; set; }

    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Pincode { get; set; }

    public string Qualification { get; set; }
    public int Experience_Years { get; set; }
    public DateTime JoiningDate { get; set; }
    public decimal Salary { get; set; }

    public int Hostel_Id { get; set; }
    public string Emergency_Contact { get; set; }

    public string ID_Proof_Type { get; set; }
    public string ID_Proof_No { get; set; }
    public string ID_Proof_Path { get; set; }

    public bool IsActive { get; set; }
    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}