using AVADH_PRIME_API.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace AVADH_PRIME_API.Data
{
    public class StudentFeesRepository
    {
        private readonly string _connectionString;

        public StudentFeesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<StudentFeesModel> SelectAll()
        {
            var list = new List<StudentFeesModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_StudentFees_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new StudentFeesModel
                {
                    Fees_Id = reader.GetInt32(reader.GetOrdinal("Fees_Id")),
                    Student_Id = reader.GetInt32(reader.GetOrdinal("Student_Id")),
                    Fee_Type = reader["Fee_Type"].ToString(),

                    Total_Amount = reader.GetDecimal(reader.GetOrdinal("Total_Amount")),
                    Paid_Amount = reader.GetDecimal(reader.GetOrdinal("Paid_Amount")),
                    Due_Amount = reader.GetDecimal(reader.GetOrdinal("Due_Amount")),

                    Payment_Status = reader["Payment_Status"].ToString(),
                    Payment_Mode = reader["Payment_Mode"] == DBNull.Value ? null : reader["Payment_Mode"].ToString(),
                    Transaction_Id = reader["Transaction_Id"] == DBNull.Value ? null : reader["Transaction_Id"].ToString(),

                    Fee_Date = reader.GetDateTime(reader.GetOrdinal("Fee_Date")),
                    Due_Date = reader.GetDateTime(reader.GetOrdinal("Due_Date")),
                    Payment_Date = reader["Payment_Date"] == DBNull.Value ? null : (DateTime?)reader["Payment_Date"],

                    Semester = reader.GetInt32(reader.GetOrdinal("Semester")),
                    Year = reader.GetInt32(reader.GetOrdinal("Year")),

                    Late_Fine = reader.GetDecimal(reader.GetOrdinal("Late_Fine")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),

                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                });
            }

            return list;
        }

        // 🔹 SELECT BY ID
        public StudentFeesModel SelectByPK(int Fees_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_StudentFees_GetById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Fees_Id", SqlDbType.Int).Value = Fees_Id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new StudentFeesModel
                {
                    Fees_Id = reader.GetInt32(reader.GetOrdinal("Fees_Id")),
                    Student_Id = reader.GetInt32(reader.GetOrdinal("Student_Id")),
                    Fee_Type = reader["Fee_Type"].ToString(),

                    Total_Amount = reader.GetDecimal(reader.GetOrdinal("Total_Amount")),
                    Paid_Amount = reader.GetDecimal(reader.GetOrdinal("Paid_Amount")),
                    Due_Amount = reader.GetDecimal(reader.GetOrdinal("Due_Amount")),

                    Payment_Status = reader["Payment_Status"].ToString(),
                    Payment_Mode = reader["Payment_Mode"] == DBNull.Value ? null : reader["Payment_Mode"].ToString(),
                    Transaction_Id = reader["Transaction_Id"] == DBNull.Value ? null : reader["Transaction_Id"].ToString(),

                    Fee_Date = reader.GetDateTime(reader.GetOrdinal("Fee_Date")),
                    Due_Date = reader.GetDateTime(reader.GetOrdinal("Due_Date")),
                    Payment_Date = reader["Payment_Date"] == DBNull.Value ? null : (DateTime?)reader["Payment_Date"],

                    Semester = reader.GetInt32(reader.GetOrdinal("Semester")),
                    Year = reader.GetInt32(reader.GetOrdinal("Year")),

                    Late_Fine = reader.GetDecimal(reader.GetOrdinal("Late_Fine")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),

                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(StudentFeesModel fee)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_StudentFees_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Student_Id", fee.Student_Id);
            cmd.Parameters.AddWithValue("@Fee_Type", fee.Fee_Type);
            cmd.Parameters.AddWithValue("@Total_Amount", fee.Total_Amount);
            cmd.Parameters.AddWithValue("@Paid_Amount", fee.Paid_Amount);

            cmd.Parameters.AddWithValue("@Payment_Status", fee.Payment_Status);
            cmd.Parameters.AddWithValue("@Payment_Mode", (object?)fee.Payment_Mode ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Transaction_Id", (object?)fee.Transaction_Id ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Fee_Date", fee.Fee_Date);
            cmd.Parameters.AddWithValue("@Due_Date", fee.Due_Date);
            cmd.Parameters.AddWithValue("@Payment_Date", (object?)fee.Payment_Date ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Semester", fee.Semester);
            cmd.Parameters.AddWithValue("@Year", fee.Year);

            cmd.Parameters.AddWithValue("@Late_Fine", fee.Late_Fine);
            cmd.Parameters.AddWithValue("@IsActive", fee.IsActive);
            cmd.Parameters.AddWithValue("@CreatedAt", (object?)fee.CreatedAt ?? DBNull.Value);


            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(StudentFeesModel fee)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_StudentFees_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Fees_Id", fee.Fees_Id);

            cmd.Parameters.AddWithValue("@Student_Id", fee.Student_Id);
            cmd.Parameters.AddWithValue("@Fee_Type", fee.Fee_Type);
            cmd.Parameters.AddWithValue("@Total_Amount", fee.Total_Amount);
            cmd.Parameters.AddWithValue("@Paid_Amount", fee.Paid_Amount);

            cmd.Parameters.AddWithValue("@Payment_Status", fee.Payment_Status);
            cmd.Parameters.AddWithValue("@Payment_Mode", (object?)fee.Payment_Mode ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Transaction_Id", (object?)fee.Transaction_Id ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Fee_Date", fee.Fee_Date);
            cmd.Parameters.AddWithValue("@Due_Date", fee.Due_Date);
            cmd.Parameters.AddWithValue("@Payment_Date", (object?)fee.Payment_Date ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Semester", fee.Semester);
            cmd.Parameters.AddWithValue("@Year", fee.Year);

            cmd.Parameters.AddWithValue("@Late_Fine", fee.Late_Fine);
            cmd.Parameters.AddWithValue("@IsActive", fee.IsActive);
            cmd.Parameters.AddWithValue("@UpdatedAt", (object?)fee.UpdatedAt ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Fees_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_StudentFees_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Fees_Id", SqlDbType.Int).Value = Fees_Id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}