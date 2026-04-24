using AVADH_PRIME_API.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Numerics;
using System.Reflection;

namespace AVADH_PRIME_API.Data
{
    public class VisitorsRepository
    {
        private readonly string _connectionString;

        public VisitorsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<VisitorsModel> SelectAll()
        {
            var list = new List<VisitorsModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_Visitors_GetAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new VisitorsModel
                {
                    Visitor_Id = reader.GetInt32(reader.GetOrdinal("Visitor_Id")),
                    First_Name = reader["First_Name"].ToString(),
                    Last_Name = reader["Last_Name"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"].ToString(),

                    Id_Proof_Type = reader["Id_Proof_Type"] == DBNull.Value ? null : reader["Id_Proof_Type"].ToString(),
                    Id_Proof_Number = reader["Id_Proof_Number"] == DBNull.Value ? null : reader["Id_Proof_Number"].ToString(),

                    Address = reader["Address"] == DBNull.Value ? null : reader["Address"].ToString(),
                    City = reader["City"] == DBNull.Value ? null : reader["City"].ToString(),
                    State = reader["State"] == DBNull.Value ? null : reader["State"].ToString(),
                    Country = reader["Country"].ToString(),

                    Visit_Date = reader.GetDateTime(reader.GetOrdinal("Visit_Date")),
                    Check_In_Time = reader.GetDateTime(reader.GetOrdinal("Check_In_Time")),
                    Check_Out_Time = reader["Check_Out_Time"] == DBNull.Value ? null : (DateTime?)reader["Check_Out_Time"],

                    Purpose_Of_Visit = reader["Purpose_Of_Visit"] == DBNull.Value ? null : reader["Purpose_Of_Visit"].ToString(),
                    Person_To_Meet_Id = reader["Person_To_Meet_Id"] == DBNull.Value ? null : (int?)reader["Person_To_Meet_Id"],
                    Person_Type = reader["Person_Type"] == DBNull.Value ? null : reader["Person_Type"].ToString(),

                    Vehicle_Number = reader["Vehicle_Number"] == DBNull.Value ? null : reader["Vehicle_Number"].ToString(),
                    Is_Approved = reader.GetBoolean(reader.GetOrdinal("Is_Approved")),
                    Approved_By = reader["Approved_By"] == DBNull.Value ? null : (int?)reader["Approved_By"],
                    Remarks = reader["Remarks"] == DBNull.Value ? null : reader["Remarks"].ToString(),

                    Created_At = reader.GetDateTime(reader.GetOrdinal("Created_At")),
                    Created_By = reader["Created_By"] == DBNull.Value ? null : (int?)reader["Created_By"],
                    Updated_At = reader["Updated_At"] == DBNull.Value ? null : (DateTime?)reader["Updated_At"],
                    Updated_By = reader["Updated_By"] == DBNull.Value ? null : (int?)reader["Updated_By"],
                    Is_Deleted = reader.GetBoolean(reader.GetOrdinal("Is_Deleted"))
                });
            }

            return list;
        }

        // 🔹 SELECT BY ID
        public VisitorsModel SelectByPK(int Visitor_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_Visitors_GetById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Visitor_Id", SqlDbType.Int).Value = Visitor_Id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new VisitorsModel
                {
                    Visitor_Id = reader.GetInt32(reader.GetOrdinal("Visitor_Id")),
                    First_Name = reader["First_Name"].ToString(),
                    Last_Name = reader["Last_Name"].ToString(),
                    Gender = reader["Gender"].ToString(),
                    Phone = reader["Phone"].ToString(),
                    Email = reader["Email"].ToString(),

                    Id_Proof_Type = reader["Id_Proof_Type"] == DBNull.Value ? null : reader["Id_Proof_Type"].ToString(),
                    Id_Proof_Number = reader["Id_Proof_Number"] == DBNull.Value ? null : reader["Id_Proof_Number"].ToString(),

                    Address = reader["Address"] == DBNull.Value ? null : reader["Address"].ToString(),
                    City = reader["City"] == DBNull.Value ? null : reader["City"].ToString(),
                    State = reader["State"] == DBNull.Value ? null : reader["State"].ToString(),
                    Country = reader["Country"].ToString(),

                    Visit_Date = reader.GetDateTime(reader.GetOrdinal("Visit_Date")),
                    Check_In_Time = reader.GetDateTime(reader.GetOrdinal("Check_In_Time")),
                    Check_Out_Time = reader["Check_Out_Time"] == DBNull.Value ? null : (DateTime?)reader["Check_Out_Time"],

                    Purpose_Of_Visit = reader["Purpose_Of_Visit"] == DBNull.Value ? null : reader["Purpose_Of_Visit"].ToString(),
                    Person_To_Meet_Id = reader["Person_To_Meet_Id"] == DBNull.Value ? null : (int?)reader["Person_To_Meet_Id"],
                    Person_Type = reader["Person_Type"] == DBNull.Value ? null : reader["Person_Type"].ToString(),

                    Vehicle_Number = reader["Vehicle_Number"] == DBNull.Value ? null : reader["Vehicle_Number"].ToString(),
                    Is_Approved = reader.GetBoolean(reader.GetOrdinal("Is_Approved")),
                    Approved_By = reader["Approved_By"] == DBNull.Value ? null : (int?)reader["Approved_By"],
                    Remarks = reader["Remarks"] == DBNull.Value ? null : reader["Remarks"].ToString(),

                    Created_At = reader.GetDateTime(reader.GetOrdinal("Created_At")),
                    Created_By = reader["Created_By"] == DBNull.Value ? null : (int?)reader["Created_By"],
                    Updated_At = reader["Updated_At"] == DBNull.Value ? null : (DateTime?)reader["Updated_At"],
                    Updated_By = reader["Updated_By"] == DBNull.Value ? null : (int?)reader["Updated_By"],
                    Is_Deleted = reader.GetBoolean(reader.GetOrdinal("Is_Deleted"))
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(VisitorsModel model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_Visitors_Insert", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

    
            cmd.Parameters.AddWithValue("@First_Name", model.First_Name);
            cmd.Parameters.AddWithValue("@Last_Name", model.Last_Name);
            cmd.Parameters.AddWithValue("@Gender", model.Gender);
            cmd.Parameters.AddWithValue("@Phone", model.Phone);
            cmd.Parameters.AddWithValue("@Email", model.Email);

            cmd.Parameters.AddWithValue("@Id_Proof_Type", (object?)model.Id_Proof_Type ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Id_Proof_Number", (object?)model.Id_Proof_Number ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Address", (object?)model.Address ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@City", (object?)model.City ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@State", (object?)model.State ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Country", model.Country ?? "India");

            cmd.Parameters.AddWithValue("@Visit_Date", model.Visit_Date);
            cmd.Parameters.AddWithValue("@Check_In_Time", model.Check_In_Time);
            cmd.Parameters.AddWithValue("@Check_Out_Time", (object?)model.Check_Out_Time ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Purpose_Of_Visit", (object?)model.Purpose_Of_Visit ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Person_To_Meet_Id", (object?)model.Person_To_Meet_Id ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Person_Type", (object?)model.Person_Type ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Vehicle_Number", (object?)model.Vehicle_Number ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Is_Approved", model.Is_Approved);
            cmd.Parameters.AddWithValue("@Approved_By", (object?)model.Approved_By ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Remarks", (object?)model.Remarks ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Created_By", (object?)model.Created_By ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Updated_At", (object?)model.Updated_At ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Updated_By", (object?)model.Updated_By ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Is_Deleted", model.Is_Deleted);



            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(VisitorsModel model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_Visitors_Update", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@Visitor_Id", model.Visitor_Id);

            cmd.Parameters.AddWithValue("@First_Name", model.First_Name);
            cmd.Parameters.AddWithValue("@Last_Name", model.Last_Name);
            cmd.Parameters.AddWithValue("@Gender", model.Gender);
            cmd.Parameters.AddWithValue("@Phone", model.Phone);
            cmd.Parameters.AddWithValue("@Email", model.Email);

            cmd.Parameters.AddWithValue("@Id_Proof_Type", (object?)model.Id_Proof_Type ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Id_Proof_Number", (object?)model.Id_Proof_Number ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Address", (object?)model.Address ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@City", (object?)model.City ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@State", (object?)model.State ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Country", model.Country ?? "India");

            cmd.Parameters.AddWithValue("@Visit_Date", model.Visit_Date);
            cmd.Parameters.AddWithValue("@Check_In_Time", model.Check_In_Time);
            cmd.Parameters.AddWithValue("@Check_Out_Time", (object?)model.Check_Out_Time ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Purpose_Of_Visit", (object?)model.Purpose_Of_Visit ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Person_To_Meet_Id", (object?)model.Person_To_Meet_Id ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Person_Type", (object?)model.Person_Type ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Vehicle_Number", (object?)model.Vehicle_Number ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Is_Approved", model.Is_Approved);
            cmd.Parameters.AddWithValue("@Approved_By", (object?)model.Approved_By ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Remarks", (object?)model.Remarks ?? DBNull.Value);

            cmd.Parameters.AddWithValue("@Updated_At", (object?)model.Updated_At ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Updated_By", (object?)model.Updated_By ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Is_Deleted", model.Is_Deleted);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE (HARD DELETE)
        public bool Delete(int Visitor_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("SP_Visitors_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Visitor_Id", SqlDbType.Int).Value = Visitor_Id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}