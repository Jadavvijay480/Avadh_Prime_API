using Microsoft.Extensions.Configuration;
using AVADH_PRIME_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace AVADH_PRIME_API.Data
{
    public class StudentComplaintsRepository
    {
        private readonly string _connectionString;

        public StudentComplaintsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<StudentComplaintsModel> SelectAll()
        {
            var list = new List<StudentComplaintsModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_StudentComplaints_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new StudentComplaintsModel
                {
                    Complaint_Id = reader.GetInt32(reader.GetOrdinal("Complaint_Id")),
                    Student_Id = reader.GetInt32(reader.GetOrdinal("Student_Id")),

                    Complaint_Title = reader["Complaint_Title"].ToString(),
                    Complaint_Description = reader["Complaint_Description"].ToString(),
                    Complaint_Type = reader["Complaint_Type"].ToString(),

                    Priority = reader["Priority"].ToString(),
                    Status = reader["Status"].ToString(),

                    Assigned_To = reader["Assigned_To"] == DBNull.Value ? null : (int?)reader["Assigned_To"],

                    Complaint_Date = reader.GetDateTime(reader.GetOrdinal("Complaint_Date")),
                    Resolved_Date = reader["Resolved_Date"] == DBNull.Value ? null : (DateTime?)reader["Resolved_Date"],

                    Resolution_Remarks = reader["Resolution_Remarks"] == DBNull.Value ? null : reader["Resolution_Remarks"].ToString(),
                    Attachment_Path = reader["Attachment_Path"] == DBNull.Value ? null : reader["Attachment_Path"].ToString(),

                    Feedback = reader["Feedback"] == DBNull.Value ? null : reader["Feedback"].ToString(),
                    Rating = reader["Rating"] == DBNull.Value ? null : (int?)reader["Rating"],

                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                });
            }

            return list;
        }

        // 🔹 SELECT BY ID
        public StudentComplaintsModel SelectByPK(int Complaint_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_StudentComplaints_GetById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Complaint_Id", SqlDbType.Int).Value = Complaint_Id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new StudentComplaintsModel
                {
                    Complaint_Id = reader.GetInt32(reader.GetOrdinal("Complaint_Id")),
                    Student_Id = reader.GetInt32(reader.GetOrdinal("Student_Id")),

                    Complaint_Title = reader["Complaint_Title"].ToString(),
                    Complaint_Description = reader["Complaint_Description"].ToString(),
                    Complaint_Type = reader["Complaint_Type"].ToString(),

                    Priority = reader["Priority"].ToString(),
                    Status = reader["Status"].ToString(),

                    Assigned_To = reader["Assigned_To"] == DBNull.Value ? null : (int?)reader["Assigned_To"],

                    Complaint_Date = reader.GetDateTime(reader.GetOrdinal("Complaint_Date")),
                    Resolved_Date = reader["Resolved_Date"] == DBNull.Value ? null : (DateTime?)reader["Resolved_Date"],

                    Resolution_Remarks = reader["Resolution_Remarks"] == DBNull.Value ? null : reader["Resolution_Remarks"].ToString(),
                    Attachment_Path = reader["Attachment_Path"] == DBNull.Value ? null : reader["Attachment_Path"].ToString(),

                    Feedback = reader["Feedback"] == DBNull.Value ? null : reader["Feedback"].ToString(),
                    Rating = reader["Rating"] == DBNull.Value ? null : (int?)reader["Rating"],

                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(StudentComplaintsModel model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_StudentComplaints_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60
            };


            cmd.Parameters.AddWithValue("@Student_Id", model.Student_Id);
            cmd.Parameters.AddWithValue("@Complaint_Title", model.Complaint_Title);
            cmd.Parameters.AddWithValue("@Complaint_Description", model.Complaint_Description);
            cmd.Parameters.AddWithValue("@Complaint_Type", model.Complaint_Type);

            cmd.Parameters.AddWithValue("@Priority", model.Priority);
            cmd.Parameters.AddWithValue("@Status", model.Status);

            cmd.Parameters.AddWithValue("@Assigned_To", (object?)model.Assigned_To ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Complaint_Date", (object?)model.Complaint_Date ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Resolved_Date", (object?)model.Resolved_Date ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Resolution_Remarks", (object?)model.Resolution_Remarks ?? DBNull.Value);

            //cmd.Parameters.AddWithValue("@Attachment_Path", (object?)model.Attachment_Path ?? DBNull.Value);
            cmd.Parameters.Add("@Attachment_Path", SqlDbType.NVarChar, 255)
            .Value = (object?)model.Attachment_Path ?? DBNull.Value;

            cmd.Parameters.AddWithValue("@Feedback", (object?)model.Feedback ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Rating", (object?)model.Rating ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@CreatedAt", (object?)model.CreatedAt ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@UpdatedAt", (object?)model.UpdatedAt ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(StudentComplaintsModel model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_StudentComplaints_Update", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60
            };

            cmd.Parameters.AddWithValue("@Complaint_Id", model.Complaint_Id);

            cmd.Parameters.AddWithValue("@Student_Id", model.Student_Id);
            cmd.Parameters.AddWithValue("@Complaint_Title", model.Complaint_Title);
            cmd.Parameters.AddWithValue("@Complaint_Description", model.Complaint_Description);
            cmd.Parameters.AddWithValue("@Complaint_Type", model.Complaint_Type);

            cmd.Parameters.AddWithValue("@Priority", model.Priority);
            cmd.Parameters.AddWithValue("@Status", model.Status);

            cmd.Parameters.AddWithValue("@Assigned_To", (object?)model.Assigned_To ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Complaint_Date", (object?)model.Complaint_Date ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Resolved_Date", (object?)model.Resolved_Date ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Resolution_Remarks", (object?)model.Resolution_Remarks ?? DBNull.Value);

            //cmd.Parameters.AddWithValue("@Attachment_Path", (object?)model.Attachment_Path ?? DBNull.Value);
            cmd.Parameters.Add("@Attachment_Path", SqlDbType.NVarChar, 255)
            .Value = (object?)model.Attachment_Path ?? DBNull.Value;

            cmd.Parameters.AddWithValue("@Feedback", (object?)model.Feedback ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Rating", (object?)model.Rating ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@UpdatedAt", (object?)model.UpdatedAt ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Complaint_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_StudentComplaints_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Complaint_Id", SqlDbType.Int).Value = Complaint_Id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}