using Microsoft.Extensions.Configuration;
using AVADH_PRIME_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace AVADH_PRIME_API.Data
{
    public class HostelRepository
    {
        private readonly string _connectionString;

        public HostelRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<HostelModel> SelectAll()
        {
            var hostels = new List<HostelModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_A_SelectAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                hostels.Add(new HostelModel
                {
                    Hostel_Id = reader.GetInt32(reader.GetOrdinal("Hostel_Id")),
                    Hostel_Name = reader["Hostel_Name"].ToString(),
                    Hostel_Type = reader["Hostel_Type"].ToString(),
                    Total_Rooms = reader.GetInt32(reader.GetOrdinal("Total_Rooms")),
                    Total_Beds = reader.GetInt32(reader.GetOrdinal("Total_Beds")),
                    Address = reader["Address"].ToString(),
                    City = reader["City"].ToString(),
                    State = reader["State"].ToString(),
                    Pincode = reader["Pincode"].ToString(),
                    Contact_No = reader["Contact_No"].ToString(),
                    Email = reader["Email"].ToString(),
                    Monthly_Rent = reader.GetDecimal(reader.GetOrdinal("Monthly_Rent")),
                    Security_Deposit = reader.GetDecimal(reader.GetOrdinal("Security_Deposit")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                });
            }

            return hostels;
        }

        // 🔹 SELECT BY PRIMARY KEY
        public HostelModel SelectByPK(int Hostel_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_A_SelectById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Hostel_Id", SqlDbType.Int).Value = Hostel_Id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new HostelModel
                {
                    Hostel_Id = reader.GetInt32(reader.GetOrdinal("Hostel_Id")),
                    Hostel_Name = reader["Hostel_Name"].ToString(),
                    Hostel_Type = reader["Hostel_Type"].ToString(),
                    Total_Rooms = reader.GetInt32(reader.GetOrdinal("Total_Rooms")),
                    Total_Beds = reader.GetInt32(reader.GetOrdinal("Total_Beds")),
                    Address = reader["Address"].ToString(),
                    City = reader["City"].ToString(),
                    State = reader["State"].ToString(),
                    Pincode = reader["Pincode"].ToString(),
                    Contact_No = reader["Contact_No"].ToString(),
                    Email = reader["Email"].ToString(),
                    Monthly_Rent = reader.GetDecimal(reader.GetOrdinal("Monthly_Rent")),
                    Security_Deposit = reader.GetDecimal(reader.GetOrdinal("Security_Deposit")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(HostelModel hostel)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_A_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Hostel_Name", SqlDbType.NVarChar, 100).Value = hostel.Hostel_Name;
            cmd.Parameters.Add("@Hostel_Type", SqlDbType.NVarChar, 20).Value = hostel.Hostel_Type;
            cmd.Parameters.Add("@Total_Rooms", SqlDbType.Int).Value = hostel.Total_Rooms;
            cmd.Parameters.Add("@Total_Beds", SqlDbType.Int).Value = hostel.Total_Beds;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = hostel.Address;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = hostel.City;
            cmd.Parameters.Add("@State", SqlDbType.NVarChar, 50).Value = hostel.State;
            cmd.Parameters.Add("@Pincode", SqlDbType.NVarChar, 10).Value = hostel.Pincode;
            cmd.Parameters.Add("@Contact_No", SqlDbType.NVarChar, 15).Value = hostel.Contact_No;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = hostel.Email;
            cmd.Parameters.Add("@Monthly_Rent", SqlDbType.Decimal).Value = hostel.Monthly_Rent;
            cmd.Parameters.Add("@Security_Deposit", SqlDbType.Decimal).Value = hostel.Security_Deposit;
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = hostel.IsActive;
            cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = hostel.CreatedAt;
            cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = (object?)hostel.UpdatedAt ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(HostelModel hostel)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_A_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Hostel_Id", SqlDbType.Int).Value = hostel.Hostel_Id;
            cmd.Parameters.Add("@Hostel_Name", SqlDbType.NVarChar, 100).Value = hostel.Hostel_Name;
            cmd.Parameters.Add("@Hostel_Type", SqlDbType.NVarChar, 20).Value = hostel.Hostel_Type;
            cmd.Parameters.Add("@Total_Rooms", SqlDbType.Int).Value = hostel.Total_Rooms;
            cmd.Parameters.Add("@Total_Beds", SqlDbType.Int).Value = hostel.Total_Beds;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = hostel.Address;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = hostel.City;
            cmd.Parameters.Add("@State", SqlDbType.NVarChar, 50).Value = hostel.State;
            cmd.Parameters.Add("@Pincode", SqlDbType.NVarChar, 10).Value = hostel.Pincode;
            cmd.Parameters.Add("@Contact_No", SqlDbType.NVarChar, 15).Value = hostel.Contact_No;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = hostel.Email;
            cmd.Parameters.Add("@Monthly_Rent", SqlDbType.Decimal).Value = hostel.Monthly_Rent;
            cmd.Parameters.Add("@Security_Deposit", SqlDbType.Decimal).Value = hostel.Security_Deposit;
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = hostel.IsActive;
            cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = hostel.CreatedAt;
            cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = (object?)hostel.UpdatedAt ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Hostel_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_A_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Hostel_Id", SqlDbType.Int).Value = Hostel_Id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}