using AVADH_PRIME_API.Data;
using AVADH_PRIME_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_2026.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelController : ControllerBase
    {
        private readonly HostelRepository _hostelRepository;

        public HostelController(HostelRepository hostelRepository)
        {
            _hostelRepository = hostelRepository;
        }

        // 🔹 GET ALL
        [HttpGet]
        public IActionResult GetAllHostels()
        {
            var hostels = _hostelRepository.SelectAll();
            return Ok(hostels);
        }

        // 🔹 GET BY ID
        [HttpGet("{id:int}")]
        public IActionResult GetHostelById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid hostel id." });

            var hostel = _hostelRepository.SelectByPK(id);

            if (hostel == null)
                return NotFound(new { message = "Hostel not found." });

            return Ok(hostel);
        }

        // 🔹 INSERT
        [HttpPost]
        public IActionResult InsertHostel([FromBody] HostelModel hostel)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data.", errors = ModelState });

            bool isInserted = _hostelRepository.Insert(hostel);

            if (!isInserted)
                return StatusCode(500, new { message = "Error inserting hostel." });

            return Ok(new
            {
                message = "Hostel inserted successfully ✅",
                data = hostel
            });
        }

        // 🔹 UPDATE
        [HttpPut("{id:int}")]
        public IActionResult UpdateHostel(int id, [FromBody] HostelModel hostel)
        {
            if (!ModelState.IsValid || id != hostel.Hostel_Id)
                return BadRequest(new { message = "Invalid request data." });

            var isUpdated = _hostelRepository.Update(hostel);

            if (!isUpdated)
                return NotFound(new { message = "Hostel not found or not updated." });

            return Ok(new
            {
                message = "Hostel updated successfully ✅",
                data = hostel
            });
        }

        // 🔹 DELETE
        [HttpDelete("{id:int}")]
        public IActionResult DeleteHostelById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid hostel id." });

            var isDeleted = _hostelRepository.Delete(id);

            if (!isDeleted)
                return NotFound(new { message = "Hostel not found or already deleted." });

            return Ok(new
            {
                message = "Hostel deleted successfully ✅"
            });
        }
    }
}


