using AVADH_PRIME_API.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AVADH_PRIME_API.Data
{
    public class StudentAttendanceRepository
    {
        private readonly string _connectionString;

        public StudentAttendanceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<StudentAttendanceModel> SelectAll()
        {
            var list = new List<StudentAttendanceModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_StudentAttendance_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new StudentAttendanceModel
                {
                    Attendance_Id = reader.GetInt32(reader.GetOrdinal("Attendance_Id")),
                    Student_Id = reader.GetInt32(reader.GetOrdinal("Student_Id")),
                    Attendance_Date = reader.GetDateTime(reader.GetOrdinal("Attendance_Date")),
                    Status = reader["Status"].ToString(),

                    CheckIn_Time = reader["CheckIn_Time"] == DBNull.Value ? null : (TimeSpan?)reader["CheckIn_Time"],
                    CheckOut_Time = reader["CheckOut_Time"] == DBNull.Value ? null : (TimeSpan?)reader["CheckOut_Time"],
                    Remarks = reader["Remarks"] == DBNull.Value ? null : reader["Remarks"].ToString(),

                    Marked_By = reader["Marked_By"] == DBNull.Value ? null : (int?)reader["Marked_By"],

                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                });
            }

            return list;
        }

        // 🔹 SELECT BY ID
        public StudentAttendanceModel SelectByPK(int Attendance_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_StudentAttendance_GetById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Attendance_Id", SqlDbType.Int).Value = Attendance_Id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new StudentAttendanceModel
                {
                    Attendance_Id = reader.GetInt32(reader.GetOrdinal("Attendance_Id")),
                    Student_Id = reader.GetInt32(reader.GetOrdinal("Student_Id")),
                    Attendance_Date = reader.GetDateTime(reader.GetOrdinal("Attendance_Date")),
                    Status = reader["Status"].ToString(),

                    CheckIn_Time = reader["CheckIn_Time"] == DBNull.Value ? null : (TimeSpan?)reader["CheckIn_Time"],
                    CheckOut_Time = reader["CheckOut_Time"] == DBNull.Value ? null : (TimeSpan?)reader["CheckOut_Time"],
                    Remarks = reader["Remarks"] == DBNull.Value ? null : reader["Remarks"].ToString(),

                    Marked_By = reader["Marked_By"] == DBNull.Value ? null : (int?)reader["Marked_By"],

                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(StudentAttendanceModel model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_StudentAttendance_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

   
            cmd.Parameters.AddWithValue("@Student_Id", model.Student_Id);
            cmd.Parameters.AddWithValue("@Attendance_Date", model.Attendance_Date);
            cmd.Parameters.AddWithValue("@Status", model.Status);

            cmd.Parameters.AddWithValue("@CheckIn_Time", (object?)model.CheckIn_Time ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CheckOut_Time", (object?)model.CheckOut_Time ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Remarks", (object?)model.Remarks ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Marked_By", (object?)model.Marked_By ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedAt", (object?)model.CreatedAt ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedAt", (object?)model.UpdatedAt ?? DBNull.Value);

            conn.Open();

            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(StudentAttendanceModel model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_StudentAttendance_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Attendance_Id", model.Attendance_Id);

            cmd.Parameters.AddWithValue("@Student_Id", model.Student_Id);
            cmd.Parameters.AddWithValue("@Attendance_Date", model.Attendance_Date);
            cmd.Parameters.AddWithValue("@Status", model.Status);

            cmd.Parameters.AddWithValue("@CheckIn_Time", (object?)model.CheckIn_Time ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CheckOut_Time", (object?)model.CheckOut_Time ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Remarks", (object?)model.Remarks ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Marked_By", (object?)model.Marked_By ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedAt", (object?)model.UpdatedAt ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Attendance_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_StudentAttendance_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Attendance_Id", SqlDbType.Int).Value = Attendance_Id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔥 OPTIONAL (VERY IMPORTANT)
        // 🔹 CHECK EXIST (for UNIQUE constraint)
        public bool Exists(int studentId, DateTime date)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_Attendance_Exists", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Student_Id", SqlDbType.Int).Value = studentId;
            cmd.Parameters.Add("@Attendance_Date", SqlDbType.Date).Value = date;

            conn.Open();
            return (int)cmd.ExecuteScalar() > 0;
        }
    }
}