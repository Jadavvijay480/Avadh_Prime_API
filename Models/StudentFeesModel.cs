public class StudentFeesModel
{
    public int Fees_Id { get; set; }
    public int Student_Id { get; set; }

    public string Fee_Type { get; set; }

    public decimal Total_Amount { get; set; }
    public decimal Paid_Amount { get; set; }
    public decimal Due_Amount { get; set; } // Computed

    public string Payment_Status { get; set; }
    public string? Payment_Mode { get; set; }
    public string? Transaction_Id { get; set; }

    public DateTime Fee_Date { get; set; }
    public DateTime Due_Date { get; set; }
    public DateTime? Payment_Date { get; set; }

    public int Semester { get; set; }
    public int Year { get; set; }

    public decimal Late_Fine { get; set; }
    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}