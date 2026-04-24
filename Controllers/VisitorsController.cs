using AVADH_PRIME_API.Data;
using AVADH_PRIME_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AVADH_PRIME_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {
        private readonly VisitorsRepository _visitorsRepository;

        public VisitorsController(VisitorsRepository visitorsRepository)
        {
            _visitorsRepository = visitorsRepository;
        }

        // 🔹 GET ALL
        [HttpGet]
        public IActionResult GetAllVisitors()
        {
            var data = _visitorsRepository.SelectAll();
            return Ok(data);
        }

        // 🔹 GET BY ID
        [HttpGet("{id:int}")]
        public IActionResult GetVisitorById(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid visitor id." });

            var record = _visitorsRepository.SelectByPK(id);

            if (record == null)
                return NotFound(new { message = "Visitor not found." });

            return Ok(record);
        }

        // 🔹 INSERT
        [HttpPost]
        public IActionResult InsertVisitor([FromBody] VisitorsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid data.", errors = ModelState });

            // ✅ Auto defaults (recommended)
            model.Visit_Date = DateTime.Now.Date;
            model.Check_In_Time = DateTime.Now;
            model.Is_Approved = false;

            bool isInserted = _visitorsRepository.Insert(model);

            if (!isInserted)
                return StatusCode(500, new { message = "Error inserting visitor." });

            return Ok(new
            {
                message = "Visitor check-in successful ✅",
                data = model
            });
        }

        // 🔹 UPDATE
        [HttpPut("{id:int}")]
        public IActionResult UpdateVisitor(int id, [FromBody] VisitorsModel model)
        {
            if (!ModelState.IsValid || id != model.Visitor_Id)
                return BadRequest(new { message = "Invalid request data." });

            var isUpdated = _visitorsRepository.Update(model);

            if (!isUpdated)
                return NotFound(new { message = "Visitor not found or not updated." });

            return Ok(new
            {
                message = "Visitor updated successfully ✅",
                data = model
            });
        }

        // 🔹 DELETE (HARD DELETE)
        [HttpDelete("{id:int}")]
        public IActionResult DeleteVisitor(int id)
        {
            if (id <= 0)
                return BadRequest(new { message = "Invalid visitor id." });

            var isDeleted = _visitorsRepository.Delete(id);

            if (!isDeleted)
                return NotFound(new { message = "Visitor not found or already deleted." });

            return Ok(new
            {
                message = "Visitor deleted successfully ✅"
            });
        }
    }
}