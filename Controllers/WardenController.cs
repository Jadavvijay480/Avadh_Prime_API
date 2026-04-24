using AVADH_PRIME_API.Data;
using AVADH_PRIME_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AVADH_PRIME_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WardenController : ControllerBase
    {
        private readonly WardenRepository _wardenRepository;

        public WardenController(WardenRepository wardenRepository)
        {
            _wardenRepository = wardenRepository;
        }

        // 🔹 GET ALL
        [HttpGet]
        public IActionResult GetAllWardens()
        {
            var wardens = _wardenRepository.SelectAll();
            return Ok(wardens);
        }

        // 🔹 GET BY ID
        [HttpGet("{id:int}")]
        public IActionResult GetWardenById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid warden id." });

            var warden = _wardenRepository.SelectByPK(id);

            if (warden == null)
                return NotFound(new { message = "Warden not found." });

            return Ok(warden);
        }

        // 🔹 INSERT
        [HttpPost]
        public IActionResult InsertWarden([FromBody] WardenModel warden)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data.", errors = ModelState });

            bool isInserted = _wardenRepository.Insert(warden);

            if (!isInserted)
                return StatusCode(500, new { message = "Error inserting warden." });

            return Ok(new
            {
                message = "Warden inserted successfully ✅",
                data = warden
            });
        }

        // 🔹 UPDATE
        [HttpPut("{id:int}")]
        public IActionResult UpdateWarden(int id, [FromBody] WardenModel warden)
        {
            if (!ModelState.IsValid || id != warden.Warden_Id)
                return BadRequest(new { message = "Invalid request data." });

            var isUpdated = _wardenRepository.Update(warden);

            if (!isUpdated)
                return NotFound(new { message = "Warden not found or not updated." });

            return Ok(new
            {
                message = "Warden updated successfully ✅",
                data = warden
            });
        }

        // 🔹 DELETE
        [HttpDelete("{id:int}")]
        public IActionResult DeleteWardenById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid warden id." });

            var isDeleted = _wardenRepository.Delete(id);

            if (!isDeleted)
                return NotFound(new { message = "Warden not found or already deleted." });

            return Ok(new
            {
                message = "Warden deleted successfully ✅"
            });
        }
    }
}