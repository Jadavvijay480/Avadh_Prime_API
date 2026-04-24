using AVADH_PRIME_API.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AVADH_PRIME_API.Data
{
    public class FeesReceiptsRepository
    {
        private readonly string _connectionString;

        public FeesReceiptsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<FeesReceiptsModel> SelectAll()
        {
            var list = new List<FeesReceiptsModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetAll_FeesReceipts", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new FeesReceiptsModel
                {
                    Receipt_Id = reader.GetInt32(reader.GetOrdinal("Receipt_Id")),
                    Fees_Id = reader.GetInt32(reader.GetOrdinal("Fees_Id")),
                    Student_Id = reader.GetInt32(reader.GetOrdinal("Student_Id")),
                    Paid_Amount = reader.GetDecimal(reader.GetOrdinal("Paid_Amount")),
                    Payment_Mode = reader["Payment_Mode"].ToString(),
                    Transaction_Id = reader["Transaction_Id"].ToString(),
                    Receipt_No = reader["Receipt_No"].ToString(),
                    Payment_Date = reader.GetDateTime(reader.GetOrdinal("Payment_Date")),
                    Remarks = reader["Remarks"].ToString(),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                });
            }

            return list;
        }

        // 🔹 SELECT BY ID
        public FeesReceiptsModel SelectByPK(int Receipt_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetFeesReceipt_ById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Receipt_Id", SqlDbType.Int).Value = Receipt_Id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new FeesReceiptsModel
                {
                    Receipt_Id = reader.GetInt32(reader.GetOrdinal("Receipt_Id")),
                    Fees_Id = reader.GetInt32(reader.GetOrdinal("Fees_Id")),
                    Student_Id = reader.GetInt32(reader.GetOrdinal("Student_Id")),
                    Paid_Amount = reader.GetDecimal(reader.GetOrdinal("Paid_Amount")),
                    Payment_Mode = reader["Payment_Mode"].ToString(),
                    Transaction_Id = reader["Transaction_Id"].ToString(),
                    Receipt_No = reader["Receipt_No"].ToString(),
                    Payment_Date = reader.GetDateTime(reader.GetOrdinal("Payment_Date")),
                    Remarks = reader["Remarks"].ToString(),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(FeesReceiptsModel model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Insert_FeesReceipt", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60
            };

            cmd.Parameters.AddWithValue("@Fees_Id", model.Fees_Id);
            cmd.Parameters.AddWithValue("@Student_Id", model.Student_Id);
            cmd.Parameters.AddWithValue("@Paid_Amount", model.Paid_Amount);
            cmd.Parameters.AddWithValue("@Payment_Mode", model.Payment_Mode);

            cmd.Parameters.Add("@Transaction_Id", SqlDbType.NVarChar, 100)
                .Value = (object?)model.Transaction_Id ?? DBNull.Value;

            cmd.Parameters.AddWithValue("@Receipt_No", model.Receipt_No);

            cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar, 255)
                .Value = (object?)model.Remarks ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(FeesReceiptsModel model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Update_FeesReceipt", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60
            };

            cmd.Parameters.AddWithValue("@Receipt_Id", model.Receipt_Id);
            cmd.Parameters.AddWithValue("@Fees_Id", model.Fees_Id);
            cmd.Parameters.AddWithValue("@Student_Id", model.Student_Id);
            cmd.Parameters.AddWithValue("@Paid_Amount", model.Paid_Amount);
            cmd.Parameters.AddWithValue("@Payment_Mode", model.Payment_Mode);

            cmd.Parameters.Add("@Transaction_Id", SqlDbType.NVarChar, 100)
                .Value = (object?)model.Transaction_Id ?? DBNull.Value;

            cmd.Parameters.AddWithValue("@Receipt_No", model.Receipt_No);

            cmd.Parameters.Add("@Remarks", SqlDbType.NVarChar, 255)
                .Value = (object?)model.Remarks ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE (HARD DELETE)
        public bool Delete(int Receipt_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Delete_FeesReceipt_Hard", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Receipt_Id", SqlDbType.Int).Value = Receipt_Id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}