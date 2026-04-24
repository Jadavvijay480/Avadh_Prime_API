using AVADH_PRIME_API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Numerics;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AVADH_PRIME_API.Data
{
    public class WardenRepository
    {
        private readonly string _connectionString;

        public WardenRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<WardenModel> SelectAll()
        {
            var wardens = new List<WardenModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_Warden_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                wardens.Add(new WardenModel
                {
                    Warden_Id = reader.GetInt32(reader.GetOrdinal("Warden_Id")),
                    Warden_Image = reader["Warden_Image"].ToString(),
                    Warden_Code = reader["Warden_Code"].ToString(),
                    First_Name = reader["First_Name"].ToString(),
                    Last_Name = reader["Last_Name"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                    BloodGroup = reader["BloodGroup"].ToString(),

                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"].ToString(),
                    Address = reader["Address"].ToString(),
                    City = reader["City"].ToString(),
                    State = reader["State"].ToString(),
                    Pincode = reader["Pincode"].ToString(),

                    Qualification = reader["Qualification"].ToString(),
                    Experience_Years = reader.GetInt32(reader.GetOrdinal("Experience_Years")),
                    JoiningDate = reader.GetDateTime(reader.GetOrdinal("JoiningDate")),
                    Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),

                    Hostel_Id = reader.GetInt32(reader.GetOrdinal("Hostel_Id")),
                    Emergency_Contact = reader["Emergency_Contact"].ToString(),

                    ID_Proof_Type = reader["ID_Proof_Type"].ToString(),
                    ID_Proof_No = reader["ID_Proof_No"].ToString(),
                    ID_Proof_Path = reader["ID_Proof_Path"].ToString(),

                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                    Status = reader["Status"].ToString(),

                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                });
            }

            return wardens;
        }

        // 🔹 SELECT BY ID
        public WardenModel SelectByPK(int Warden_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_Warden_GetById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Warden_Id", SqlDbType.Int).Value = Warden_Id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new WardenModel
                {
                    Warden_Id = reader.GetInt32(reader.GetOrdinal("Warden_Id")),
                    Warden_Image = reader["Warden_Image"].ToString(),
                    Warden_Code = reader["Warden_Code"].ToString(),
                    First_Name = reader["First_Name"].ToString(),
                    Last_Name = reader["Last_Name"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                    BloodGroup = reader["BloodGroup"].ToString(),

                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"].ToString(),
                    Address = reader["Address"].ToString(),
                    City = reader["City"].ToString(),
                    State = reader["State"].ToString(),
                    Pincode = reader["Pincode"].ToString(),

                    Qualification = reader["Qualification"].ToString(),
                    Experience_Years = reader.GetInt32(reader.GetOrdinal("Experience_Years")),
                    JoiningDate = reader.GetDateTime(reader.GetOrdinal("JoiningDate")),
                    Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),

                    Hostel_Id = reader.GetInt32(reader.GetOrdinal("Hostel_Id")),
                    Emergency_Contact = reader["Emergency_Contact"].ToString(),

                    ID_Proof_Type = reader["ID_Proof_Type"].ToString(),
                    ID_Proof_No = reader["ID_Proof_No"].ToString(),
                    ID_Proof_Path = reader["ID_Proof_Path"].ToString(),

                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                    Status = reader["Status"].ToString(),

                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(WardenModel warden)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_Warden_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60 // increase timeout
            };


            //cmd.Parameters.AddWithValue("@Warden_Image", warden.Warden_Image);
            cmd.Parameters.Add("@Warden_Image", SqlDbType.NVarChar, 255)
               .Value = (object?)warden.Warden_Image ?? DBNull.Value;
            cmd.Parameters.AddWithValue("@Warden_Code", warden.Warden_Code);
            cmd.Parameters.AddWithValue("@First_Name", warden.First_Name);
            cmd.Parameters.AddWithValue("@Last_Name", warden.Last_Name);
            cmd.Parameters.AddWithValue("@Gender", warden.Gender);
            cmd.Parameters.AddWithValue("@DateOfBirth", warden.DateOfBirth);
            cmd.Parameters.AddWithValue("@BloodGroup", warden.BloodGroup);

            cmd.Parameters.AddWithValue("@Phone", warden.Phone);
            cmd.Parameters.AddWithValue("@Email", warden.Email);
            cmd.Parameters.AddWithValue("@Address", warden.Address);
            cmd.Parameters.AddWithValue("@City", warden.City);
            cmd.Parameters.AddWithValue("@State", warden.State);
            cmd.Parameters.AddWithValue("@Pincode", warden.Pincode);

            cmd.Parameters.AddWithValue("@Qualification", warden.Qualification);
            cmd.Parameters.AddWithValue("@Experience_Years", warden.Experience_Years);
            cmd.Parameters.AddWithValue("@JoiningDate", warden.JoiningDate);
            cmd.Parameters.AddWithValue("@Salary", warden.Salary);

            cmd.Parameters.AddWithValue("@Hostel_Id", warden.Hostel_Id);
            cmd.Parameters.AddWithValue("@Emergency_Contact", warden.Emergency_Contact);

            cmd.Parameters.AddWithValue("@ID_Proof_Type", warden.ID_Proof_Type);
            cmd.Parameters.AddWithValue("@ID_Proof_No", warden.ID_Proof_No);
            cmd.Parameters.AddWithValue("@ID_Proof_Path", warden.ID_Proof_Path);

            cmd.Parameters.AddWithValue("@IsActive", warden.IsActive);
            cmd.Parameters.AddWithValue("@Status", warden.Status);
            cmd.Parameters.AddWithValue("@CreatedAt", (object?)warden.CreatedAt ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(WardenModel warden)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_Warden_Update", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60 // increase timeout
            };

            cmd.Parameters.AddWithValue("@Warden_Id", warden.Warden_Id);

            //cmd.Parameters.AddWithValue("@Warden_Image", warden.Warden_Image);
            cmd.Parameters.Add("@Warden_Image", SqlDbType.NVarChar, 255)
               .Value = (object?)warden.Warden_Image ?? DBNull.Value;
            cmd.Parameters.AddWithValue("@Warden_Code", warden.Warden_Code);
            cmd.Parameters.AddWithValue("@First_Name", warden.First_Name);
            cmd.Parameters.AddWithValue("@Last_Name", warden.Last_Name);
            cmd.Parameters.AddWithValue("@Gender", warden.Gender);
            cmd.Parameters.AddWithValue("@DateOfBirth", warden.DateOfBirth);
            cmd.Parameters.AddWithValue("@BloodGroup", warden.BloodGroup);

            cmd.Parameters.AddWithValue("@Phone", warden.Phone);
            cmd.Parameters.AddWithValue("@Email", warden.Email);
            cmd.Parameters.AddWithValue("@Address", warden.Address);
            cmd.Parameters.AddWithValue("@City", warden.City);
            cmd.Parameters.AddWithValue("@State", warden.State);
            cmd.Parameters.AddWithValue("@Pincode", warden.Pincode);

            cmd.Parameters.AddWithValue("@Qualification", warden.Qualification);
            cmd.Parameters.AddWithValue("@Experience_Years", warden.Experience_Years);
            cmd.Parameters.AddWithValue("@JoiningDate", warden.JoiningDate);
            cmd.Parameters.AddWithValue("@Salary", warden.Salary);

            cmd.Parameters.AddWithValue("@Hostel_Id", warden.Hostel_Id);
            cmd.Parameters.AddWithValue("@Emergency_Contact", warden.Emergency_Contact);

            cmd.Parameters.AddWithValue("@ID_Proof_Type", warden.ID_Proof_Type);
            cmd.Parameters.AddWithValue("@ID_Proof_No", warden.ID_Proof_No);
            cmd.Parameters.AddWithValue("@ID_Proof_Path", warden.ID_Proof_Path);

            cmd.Parameters.AddWithValue("@IsActive", warden.IsActive);
            cmd.Parameters.AddWithValue("@Status", warden.Status);
            cmd.Parameters.AddWithValue("@UpdatedAt", (object?)warden.UpdatedAt ?? DBNull.Value);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Warden_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_WardenS_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Warden_Id", SqlDbType.Int).Value = Warden_Id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}