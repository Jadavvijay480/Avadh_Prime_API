public class VisitorsModel
{
    public int Visitor_Id { get; set; }

    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public string Gender { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public string? Id_Proof_Type { get; set; }
    public string? Id_Proof_Number { get; set; }

    public string? Address { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string Country { get; set; }

    public DateTime Visit_Date { get; set; }
    public DateTime Check_In_Time { get; set; }
    public DateTime? Check_Out_Time { get; set; }

    public string? Purpose_Of_Visit { get; set; }
    public int? Person_To_Meet_Id { get; set; }
    public string? Person_Type { get; set; }

    public string? Vehicle_Number { get; set; }
    public bool Is_Approved { get; set; }
    public int? Approved_By { get; set; }
    public string? Remarks { get; set; }

    public DateTime Created_At { get; set; }
    public int? Created_By { get; set; }
    public DateTime? Updated_At { get; set; }
    public int? Updated_By { get; set; }
    public bool Is_Deleted { get; set; }
}