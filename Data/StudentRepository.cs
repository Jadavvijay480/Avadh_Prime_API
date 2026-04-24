using Microsoft.Extensions.Configuration;
using AVADH_PRIME_API.Models;
using System.Data;
using System.Data.SqlClient;

namespace AVADH_PRIME_API.Data
{
    public class StudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 🔹 SELECT ALL
        public IEnumerable<StudentModel> SelectAll()
        {
            var students = new List<StudentModel>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Students_SelectAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                students.Add(new StudentModel
                {
                    Student_Id = reader.GetInt32(reader.GetOrdinal("Student_Id")),
                    student_image = reader["student_image"].ToString(),
                    H_Enrollment_No = reader["H_Enrollment_No"].ToString(),
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
                    U_Enrollment_No = reader["U_Enrollment_No"].ToString(),
                    Course = reader["Course"].ToString(),
                    Branch = reader["Branch"].ToString(),
                    Semester = reader.GetInt32(reader.GetOrdinal("Semester")),
                    CollegeName = reader["CollegeName"].ToString(),
                    Hostel_Id = reader.GetInt32(reader.GetOrdinal("Hostel_Id")),
                    AdmissionDate = reader.GetDateTime(reader.GetOrdinal("AdmissionDate")),
                    Father_Name = reader["Father_Name"].ToString(),
                    Mother_Name = reader["Mother_Name"].ToString(),
                    Parent_Phone = reader["Parent_Phone"].ToString(),
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

            return students;
        }

        // 🔹 SELECT BY ID
        public StudentModel SelectByPK(int Student_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Students_SelectById", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Student_Id", SqlDbType.Int).Value = Student_Id;

            conn.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return new StudentModel
                {
                    Student_Id = reader.GetInt32(reader.GetOrdinal("Student_Id")),
                    student_image = reader["student_image"].ToString(),
                    H_Enrollment_No = reader["H_Enrollment_No"].ToString(),
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
                    U_Enrollment_No = reader["U_Enrollment_No"].ToString(),
                    Course = reader["Course"].ToString(),
                    Branch = reader["Branch"].ToString(),
                    Semester = reader.GetInt32(reader.GetOrdinal("Semester")),
                    CollegeName = reader["CollegeName"].ToString(),
                    Hostel_Id = reader.GetInt32(reader.GetOrdinal("Hostel_Id")),
                    AdmissionDate = reader.GetDateTime(reader.GetOrdinal("AdmissionDate")),
                    Father_Name = reader["Father_Name"].ToString(),
                    Mother_Name = reader["Mother_Name"].ToString(),
                    Parent_Phone = reader["Parent_Phone"].ToString(),
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
        public bool Insert(StudentModel student)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Students_Insert", conn)
            {
               
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60 // increase timeout
            };

            
            //cmd.Parameters.Add("@student_image", SqlDbType.NVarChar, 255).Value = student.student_image;
            // student image
            cmd.Parameters.Add("@student_image", SqlDbType.NVarChar, 255)
                .Value = (object?)student.student_image ?? DBNull.Value;
            cmd.Parameters.Add("@H_Enrollment_No", SqlDbType.NVarChar, 10).Value = student.H_Enrollment_No;
            cmd.Parameters.Add("@First_Name", SqlDbType.NVarChar, 50).Value = student.First_Name;
            cmd.Parameters.Add("@Last_Name", SqlDbType.NVarChar, 50).Value = student.Last_Name;
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = student.Gender;
            cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = student.DateOfBirth;
            cmd.Parameters.Add("@BloodGroup", SqlDbType.NVarChar, 3).Value = student.BloodGroup;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 15).Value = student.Phone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = student.Email;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = student.Address;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = student.City;
            cmd.Parameters.Add("@State", SqlDbType.NVarChar, 50).Value = student.State;
            cmd.Parameters.Add("@Pincode", SqlDbType.NVarChar, 10).Value = student.Pincode;
            cmd.Parameters.Add("@U_Enrollment_No", SqlDbType.NVarChar, 50).Value = student.U_Enrollment_No;
            cmd.Parameters.Add("@Course", SqlDbType.NVarChar, 100).Value = student.Course;
            cmd.Parameters.Add("@Branch", SqlDbType.NVarChar, 100).Value = student.Branch;
            cmd.Parameters.Add("@Semester", SqlDbType.Int).Value = student.Semester;
            cmd.Parameters.Add("@CollegeName", SqlDbType.NVarChar, 150).Value = student.CollegeName;
            cmd.Parameters.Add("@Hostel_Id", SqlDbType.Int).Value = student.Hostel_Id;
            cmd.Parameters.Add("@AdmissionDate", SqlDbType.Date).Value = student.AdmissionDate;
            cmd.Parameters.Add("@Father_Name", SqlDbType.NVarChar, 100).Value = student.Father_Name;
            cmd.Parameters.Add("@Mother_Name", SqlDbType.NVarChar, 100).Value = student.Mother_Name;
            cmd.Parameters.Add("@Parent_Phone", SqlDbType.NVarChar, 15).Value = student.Parent_Phone;
            cmd.Parameters.Add("@Emergency_Contact", SqlDbType.NVarChar, 15).Value = student.Emergency_Contact;
            cmd.Parameters.Add("@ID_Proof_Type", SqlDbType.NVarChar, 50).Value = student.ID_Proof_Type;
            cmd.Parameters.Add("@ID_Proof_No", SqlDbType.NVarChar, 20).Value = student.ID_Proof_No;
            //cmd.Parameters.Add("@ID_Proof_Path", SqlDbType.NVarChar, 255).Value = student.ID_Proof_Path;
            // ID proof
            cmd.Parameters.Add("@ID_Proof_Path", SqlDbType.NVarChar, 255)
                .Value = (object?)student.ID_Proof_Path ?? DBNull.Value;
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = student.IsActive;
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value = student.Status;
            cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = student.CreatedAt;
            cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = (object?)student.UpdatedAt ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 UPDATE
        public bool Update(StudentModel student)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Students_Update", conn)
            {
                CommandType = CommandType.StoredProcedure,
                CommandTimeout = 60 // increase timeout
            };

            cmd.Parameters.Add("@Student_Id", SqlDbType.Int).Value = student.Student_Id;
            //cmd.Parameters.Add("@student_image", SqlDbType.NVarChar, 255).Value = student.student_image;
            // student image
            cmd.Parameters.Add("@student_image", SqlDbType.NVarChar, 255)
                .Value = (object?)student.student_image ?? DBNull.Value;
            cmd.Parameters.Add("@H_Enrollment_No", SqlDbType.NVarChar, 10).Value = student.H_Enrollment_No;
            cmd.Parameters.Add("@First_Name", SqlDbType.NVarChar, 50).Value = student.First_Name;
            cmd.Parameters.Add("@Last_Name", SqlDbType.NVarChar, 50).Value = student.Last_Name;
            cmd.Parameters.Add("@Gender", SqlDbType.NVarChar, 10).Value = student.Gender;
            cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = student.DateOfBirth;
            cmd.Parameters.Add("@BloodGroup", SqlDbType.NVarChar, 3).Value = student.BloodGroup;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 15).Value = student.Phone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = student.Email;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 255).Value = student.Address;
            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = student.City;
            cmd.Parameters.Add("@State", SqlDbType.NVarChar, 50).Value = student.State;
            cmd.Parameters.Add("@Pincode", SqlDbType.NVarChar, 10).Value = student.Pincode;
            cmd.Parameters.Add("@U_Enrollment_No", SqlDbType.NVarChar, 50).Value = student.U_Enrollment_No;
            cmd.Parameters.Add("@Course", SqlDbType.NVarChar, 100).Value = student.Course;
            cmd.Parameters.Add("@Branch", SqlDbType.NVarChar, 100).Value = student.Branch;
            cmd.Parameters.Add("@Semester", SqlDbType.Int).Value = student.Semester;
            cmd.Parameters.Add("@CollegeName", SqlDbType.NVarChar, 150).Value = student.CollegeName;
            cmd.Parameters.Add("@Hostel_Id", SqlDbType.Int).Value = student.Hostel_Id;
            cmd.Parameters.Add("@AdmissionDate", SqlDbType.Date).Value = student.AdmissionDate;
            cmd.Parameters.Add("@Father_Name", SqlDbType.NVarChar, 100).Value = student.Father_Name;
            cmd.Parameters.Add("@Mother_Name", SqlDbType.NVarChar, 100).Value = student.Mother_Name;
            cmd.Parameters.Add("@Parent_Phone", SqlDbType.NVarChar, 15).Value = student.Parent_Phone;
            cmd.Parameters.Add("@Emergency_Contact", SqlDbType.NVarChar, 15).Value = student.Emergency_Contact;
            cmd.Parameters.Add("@ID_Proof_Type", SqlDbType.NVarChar, 50).Value = student.ID_Proof_Type;
            cmd.Parameters.Add("@ID_Proof_No", SqlDbType.NVarChar, 20).Value = student.ID_Proof_No;
            //cmd.Parameters.Add("@ID_Proof_Path", SqlDbType.NVarChar, 255).Value = student.ID_Proof_Path;
            // ID proof
            cmd.Parameters.Add("@ID_Proof_Path", SqlDbType.NVarChar, 255)
                .Value = (object?)student.ID_Proof_Path ?? DBNull.Value;
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = student.IsActive;
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar, 20).Value = student.Status;
            cmd.Parameters.Add("@CreatedAt", SqlDbType.DateTime).Value = student.CreatedAt;
            cmd.Parameters.Add("@UpdatedAt", SqlDbType.DateTime).Value = (object?)student.UpdatedAt ?? DBNull.Value;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

        // 🔹 DELETE
        public bool Delete(int Student_Id)
        {
            using SqlConnection conn = new SqlConnection(_connectionString);
            using SqlCommand cmd = new SqlCommand("PR_Students_Delete", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.Add("@Student_Id", SqlDbType.Int).Value = Student_Id;

            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
    }
}