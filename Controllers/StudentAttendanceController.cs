using AVADH_PRIME_API.Data;
using AVADH_PRIME_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AVADH_PRIME_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAttendanceController : ControllerBase
    {
        private readonly StudentAttendanceRepository _attendanceRepository;

        public StudentAttendanceController(StudentAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        // 🔹 GET ALL
        [HttpGet]
        public IActionResult GetAllAttendance()
        {
            var data = _attendanceRepository.SelectAll();
            return Ok(data);
        }

        // 🔹 GET BY ID
        [HttpGet("{id:int}")]
        public IActionResult GetAttendanceById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid attendance id." });

            var record = _attendanceRepository.SelectByPK(id);

            if (record == null)
                return NotFound(new { message = "Attendance record not found." });

            return Ok(record);
        }

        // 🔹 INSERT
        [HttpPost]
        public IActionResult InsertAttendance([FromBody] StudentAttendanceModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data.", errors = ModelState });

            bool isInserted = _attendanceRepository.Insert(model);

            if (!isInserted)
                return StatusCode(500, new { message = "Error inserting attendance." });

            return Ok(new
            {
                message = "Attendance inserted successfully ✅",
                data = model
            });
        }

        // 🔹 UPDATE
        [HttpPut("{id:int}")]
        public IActionResult UpdateAttendance(int id, [FromBody] StudentAttendanceModel model)
        {
            if (!ModelState.IsValid || id != model.Attendance_Id)
                return BadRequest(new { message = "Invalid request data." });

            var isUpdated = _attendanceRepository.Update(model);

            if (!isUpdated)
                return NotFound(new { message = "Attendance not found or not updated." });

            return Ok(new
            {
                message = "Attendance updated successfully ✅",
                data = model
            });
        }

        // 🔹 DELETE
        [HttpDelete("{id:int}")]
        public IActionResult DeleteAttendance(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid attendance id." });

            var isDeleted = _attendanceRepository.Delete(id);

            if (!isDeleted)
                return NotFound(new { message = "Attendance not found or already deleted." });

            return Ok(new
            {
                message = "Attendance deleted successfully ✅"
            });
        }
    }
}