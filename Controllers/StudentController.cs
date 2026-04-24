using AVADH_PRIME_API.Data;
using AVADH_PRIME_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AVADH_PRIME_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentRepository _studentRepository;

        public StudentController(StudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // 🔹 GET ALL
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _studentRepository.SelectAll();
            return Ok(students);
        }

        // 🔹 GET BY ID
        [HttpGet("{id:int}")]
        public IActionResult GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid student id." });

            var student = _studentRepository.SelectByPK(id);

            if (student == null)
                return NotFound(new { message = "Student not found." });

            return Ok(student);
        }

        // 🔹 INSERT
        [HttpPost]
        public IActionResult InsertStudent([FromBody] StudentModel student)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data.", errors = ModelState });

            bool isInserted = _studentRepository.Insert(student);

            if (!isInserted)
                return StatusCode(500, new { message = "Error inserting student." });

            return Ok(new
            {
                message = "Student inserted successfully ✅",
                data = student
            });
        }

        // 🔹 UPDATE
        [HttpPut("{id:int}")]
        public IActionResult UpdateStudent(int id, [FromBody] StudentModel student)
        {
            if (!ModelState.IsValid || id != student.Student_Id)
                return BadRequest(new { message = "Invalid request data." });

            var isUpdated = _studentRepository.Update(student);

            if (!isUpdated)
                return NotFound(new { message = "Student not found or not updated." });

            return Ok(new
            {
                message = "Student updated successfully ✅",
                data = student
            });
        }

        // 🔹 DELETE
        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudentById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid student id." });

            var isDeleted = _studentRepository.Delete(id);

            if (!isDeleted)
                return NotFound(new { message = "Student not found or already deleted." });

            return Ok(new
            {
                message = "Student deleted successfully ✅"
            });
        }
    }
}