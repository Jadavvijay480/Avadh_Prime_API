using AVADH_PRIME_API.Data;
using AVADH_PRIME_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AVADH_PRIME_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentComplaintsController : ControllerBase
    {
        private readonly StudentComplaintsRepository _complaintRepository;

        public StudentComplaintsController(StudentComplaintsRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        // 🔹 GET ALL
        [HttpGet]
        public IActionResult GetAllComplaints()
        {
            var data = _complaintRepository.SelectAll();
            return Ok(data);
        }

        // 🔹 GET BY ID
        [HttpGet("{id:int}")]
        public IActionResult GetComplaintById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid complaint id." });

            var record = _complaintRepository.SelectByPK(id);

            if (record == null)
                return NotFound(new { message = "Complaint not found." });

            return Ok(record);
        }

        // 🔹 INSERT
        [HttpPost]
        public IActionResult InsertComplaint([FromBody] StudentComplaintsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data.", errors = ModelState });

            bool isInserted = _complaintRepository.Insert(model);

            if (!isInserted)
                return StatusCode(500, new { message = "Error inserting complaint." });

            return Ok(new
            {
                message = "Complaint submitted successfully ✅",
                data = model
            });
        }

        // 🔹 UPDATE
        [HttpPut("{id:int}")]
        public IActionResult UpdateComplaint(int id, [FromBody] StudentComplaintsModel model)
        {
            if (!ModelState.IsValid || id != model.Complaint_Id)
                return BadRequest(new { message = "Invalid request data." });

            var isUpdated = _complaintRepository.Update(model);

            if (!isUpdated)
                return NotFound(new { message = "Complaint not found or not updated." });

            return Ok(new
            {
                message = "Complaint updated successfully ✅",
                data = model
            });
        }

        // 🔹 DELETE
        [HttpDelete("{id:int}")]
        public IActionResult DeleteComplaint(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid complaint id." });

            var isDeleted = _complaintRepository.Delete(id);

            if (!isDeleted)
                return NotFound(new { message = "Complaint not found or already deleted." });

            return Ok(new
            {
                message = "Complaint deleted successfully ✅"
            });
        }
    }
}