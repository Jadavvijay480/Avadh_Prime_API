using AVADH_PRIME_API.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AVADH_PRIME_API.Data
{
    public class RoomsAllocationRepository
    {
        private readonly string _connectionString;

        public RoomsAllocationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<RoomsAllocationModel> SelectAll()
        {
            var list = new List<RoomsAllocationModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetAll_RoomsAllocation", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                list.Add(new RoomsAllocationModel
                {
                    Allocation_Id = reader.GetInt32(reader.GetOrdinal("Allocation_Id")),
                    Student_Id = reader.GetInt32(reader.GetOrdinal("Student_Id")),
                    Bed_No = reader.GetInt32(reader.GetOrdinal("Bed_No")),
                    Allocation_Date = reader.GetDateTime(reader.GetOrdinal("Allocation_Date")),
                    Vacate_Date = reader.GetDateTime(reader.GetOrdinal("Vacate_Date")),
                    Status = reader["Status"].ToString(),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                });
            }

            return list;
        }

        // 🔹 SELECT BY ID
        public RoomsAllocationModel SelectByPK(int Allocation_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_GetRoomsAllocation_ById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Allocation_Id", SqlDbType.Int).Value = Allocation_Id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new RoomsAllocationModel
                {
                    Allocation_Id = reader.GetInt32(reader.GetOrdinal("Allocation_Id")),
                    Student_Id = reader.GetInt32(reader.GetOrdinal("Student_Id")),
                    Bed_No = reader.GetInt32(reader.GetOrdinal("Bed_No")),
                    Allocation_Date = reader.GetDateTime(reader.GetOrdinal("Allocation_Date")),
                    Vacate_Date = reader.GetDateTime(reader.GetOrdinal("Vacate_Date")),
                    Status = reader["Status"].ToString(),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    UpdatedAt = reader["UpdatedAt"] == DBNull.Value ? null : (DateTime?)reader["UpdatedAt"]
                };
            }

            return null;
        }

        // 🔹 INSERT
        public bool Insert(RoomsAllocationModel model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Insert_RoomsAllocation", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60
            };

            cmd.Parameters.AddWithValue("@Student_Id", model.Student_Id);
            cmd.Parameters.AddWithValue("@Bed_No", model.Bed_No);
            cmd.Parameters.AddWithValue("@Vacate_Date", model.Vacate_Date);
            cmd.Parameters.AddWithValue("@Status", model.Status);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(RoomsAllocationModel model)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Update_RoomsAllocation", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60
            };

            cmd.Parameters.AddWithValue("@Allocation_Id", model.Allocation_Id);
            cmd.Parameters.AddWithValue("@Student_Id", model.Student_Id);
            cmd.Parameters.AddWithValue("@Bed_No", model.Bed_No);
            cmd.Parameters.AddWithValue("@Vacate_Date", model.Vacate_Date);
            cmd.Parameters.AddWithValue("@Status", model.Status);

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE (HARD DELETE)
        public bool Delete(int Allocation_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("sp_Delete_RoomsAllocation", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Allocation_Id", SqlDbType.Int).Value = Allocation_Id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}