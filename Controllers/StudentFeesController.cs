using AVADH_PRIME_API.Data;
using AVADH_PRIME_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AVADH_PRIME_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentFeesController : ControllerBase
    {
        private readonly StudentFeesRepository _studentFeesRepository;

        public StudentFeesController(StudentFeesRepository studentFeesRepository)
        {
            _studentFeesRepository = studentFeesRepository;
        }

        // 🔹 GET ALL
        [HttpGet]
        public IActionResult GetAllStudentFees()
        {
            var fees = _studentFeesRepository.SelectAll();
            return Ok(fees);
        }

        // 🔹 GET BY ID
        [HttpGet("{id:int}")]
        public IActionResult GetStudentFeesById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid fees id." });

            var fee = _studentFeesRepository.SelectByPK(id);

            if (fee == null)
                return NotFound(new { message = "Student fee record not found." });

            return Ok(fee);
        }

        // 🔹 INSERT
        [HttpPost]
        public IActionResult InsertStudentFees([FromBody] StudentFeesModel fee)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data.", errors = ModelState });

            bool isInserted = _studentFeesRepository.Insert(fee);

            if (!isInserted)
                return StatusCode(500, new { message = "Error inserting student fee." });

            return Ok(new
            {
                message = "Student fee inserted successfully ✅",
                data = fee
            });
        }

        // 🔹 UPDATE
        [HttpPut("{id:int}")]
        public IActionResult UpdateStudentFees(int id, [FromBody] StudentFeesModel fee)
        {
            if (!ModelState.IsValid || id != fee.Fees_Id)
                return BadRequest(new { message = "Invalid request data." });

            var isUpdated = _studentFeesRepository.Update(fee);

            if (!isUpdated)
                return NotFound(new { message = "Student fee not found or not updated." });

            return Ok(new
            {
                message = "Student fee updated successfully ✅",
                data = fee
            });
        }

        // 🔹 DELETE
        [HttpDelete("{id:int}")]
        public IActionResult DeleteStudentFees(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid fees id." });

            var isDeleted = _studentFeesRepository.Delete(id);

            if (!isDeleted)
                return NotFound(new { message = "Student fee not found or already deleted." });

            return Ok(new
            {
                message = "Student fee deleted successfully ✅"
            });
        }
    }
}