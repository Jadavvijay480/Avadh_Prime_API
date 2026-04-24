public class StudentAttendanceModel
{
    public int Attendance_Id { get; set; }
    public int Student_Id { get; set; }

    public DateTime Attendance_Date { get; set; }
    public string Status { get; set; }

    public TimeSpan? CheckIn_Time { get; set; }
    public TimeSpan? CheckOut_Time { get; set; }
    public string? Remarks { get; set; }

    public int? Marked_By { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}