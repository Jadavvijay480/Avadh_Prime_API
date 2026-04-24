using AVADH_PRIME_API.Data;
using AVADH_PRIME_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AVADH_PRIME_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsAllocationController : ControllerBase
    {
        private readonly RoomsAllocationRepository _repository;

        public RoomsAllocationController(RoomsAllocationRepository repository)
        {
            _repository = repository;
        }

        // 🔹 GET ALL
        [HttpGet]
        public IActionResult GetAllAllocations()
        {
            try
            {
                var data = _repository.SelectAll();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error fetching allocations",
                    error = ex.Message
                });
            }
        }

        // 🔹 GET BY ID
        [HttpGet("{id:int}")]
        public IActionResult GetAllocationById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "Invalid allocation id." });

                var data = _repository.SelectByPK(id);

                if (data == null)
                    return NotFound(new { message = "Allocation not found." });

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error fetching allocation",
                    error = ex.Message
                });
            }
        }

        // 🔹 INSERT
        [HttpPost]
        public IActionResult InsertAllocation([FromBody] RoomsAllocationModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (model.Student_Id <= 0 || model.Bed_No <= 0)
                    return BadRequest(new { message = "Invalid allocation data." });

                bool isInserted = _repository.Insert(model);

                if (!isInserted)
                    return StatusCode(500, new { message = "Error inserting allocation." });

                return Ok(new
                {
                    message = "Room allocation inserted successfully ✅",
                    data = model
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error inserting allocation",
                    error = ex.Message
                });
            }
        }

        // 🔹 UPDATE
        [HttpPut("{id:int}")]
        public IActionResult UpdateAllocation(int id, [FromBody] RoomsAllocationModel model)
        {
            try
            {
                if (!ModelState.IsValid || id != model.Allocation_Id)
                    return BadRequest(new { message = "Invalid request data." });

                var existing = _repository.SelectByPK(id);
                if (existing == null)
                    return NotFound(new { message = "Allocation not found." });

                bool isUpdated = _repository.Update(model);

                if (!isUpdated)
                    return StatusCode(500, new { message = "Error updating allocation." });

                return Ok(new
                {
                    message = "Room allocation updated successfully ✅",
                    data = model
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error updating allocation",
                    error = ex.Message
                });
            }
        }

        // 🔹 DELETE
        [HttpDelete("{id:int}")]
        public IActionResult DeleteAllocation(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "Invalid allocation id." });

                var isDeleted = _repository.Delete(id);

                if (!isDeleted)
                    return NotFound(new { message = "Allocation not found or already deleted." });

                return Ok(new
                {
                    message = "Room allocation deleted successfully ✅"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error deleting allocation",
                    error = ex.Message
                });
            }
        }
    }
}