public class StudentComplaintsModel
{
    public int Complaint_Id { get; set; }
    public int Student_Id { get; set; }

    public string Complaint_Title { get; set; }
    public string Complaint_Description { get; set; }
    public string Complaint_Type { get; set; }

    public string Priority { get; set; }
    public string Status { get; set; }

    public int? Assigned_To { get; set; }

    public DateTime Complaint_Date { get; set; }
    public DateTime? Resolved_Date { get; set; }

    public string? Resolution_Remarks { get; set; }
    public string? Attachment_Path { get; set; }

    public string? Feedback { get; set; }
    public int? Rating { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}