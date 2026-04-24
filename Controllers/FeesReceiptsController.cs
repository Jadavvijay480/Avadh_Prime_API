using AVADH_PRIME_API.Data;
using AVADH_PRIME_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace AVADH_PRIME_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeesReceiptsController : ControllerBase
    {
        private readonly FeesReceiptsRepository _repository;

        public FeesReceiptsController(FeesReceiptsRepository repository)
        {
            _repository = repository;
        }

        // 🔹 GET ALL
        [HttpGet]
        public IActionResult GetAllReceipts()
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
                    message = "Error fetching receipts",
                    error = ex.Message
                });
            }
        }

        // 🔹 GET BY ID
        [HttpGet("{id:int}")]
        public IActionResult GetReceiptById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "Invalid receipt id." });

                var data = _repository.SelectByPK(id);

                if (data == null)
                    return NotFound(new { message = "Receipt not found." });

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error fetching receipt",
                    error = ex.Message
                });
            }
        }

        // 🔹 INSERT
        [HttpPost]
        public IActionResult InsertReceipt([FromBody] FeesReceiptsModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (model.Fees_Id <= 0 || model.Student_Id <= 0 || model.Paid_Amount <= 0)
                    return BadRequest(new { message = "Invalid fee receipt data." });

                bool isInserted = _repository.Insert(model);

                if (!isInserted)
                    return StatusCode(500, new { message = "Error inserting receipt." });

                return Ok(new
                {
                    message = "Receipt inserted successfully ✅",
                    data = model
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error inserting receipt",
                    error = ex.Message
                });
            }
        }

        // 🔹 UPDATE
        [HttpPut("{id:int}")]
        public IActionResult UpdateReceipt(int id, [FromBody] FeesReceiptsModel model)
        {
            try
            {
                if (!ModelState.IsValid || id != model.Receipt_Id)
                    return BadRequest(new { message = "Invalid request data." });

                var existing = _repository.SelectByPK(id);
                if (existing == null)
                    return NotFound(new { message = "Receipt not found." });

                bool isUpdated = _repository.Update(model);

                if (!isUpdated)
                    return StatusCode(500, new { message = "Error updating receipt." });

                return Ok(new
                {
                    message = "Receipt updated successfully ✅",
                    data = model
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error updating receipt",
                    error = ex.Message
                });
            }
        }

        // 🔹 DELETE (HARD DELETE)
        [HttpDelete("{id:int}")]
        public IActionResult DeleteReceipt(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { message = "Invalid receipt id." });

                var isDeleted = _repository.Delete(id);

                if (!isDeleted)
                    return NotFound(new { message = "Receipt not found or already deleted." });

                return Ok(new
                {
                    message = "Receipt deleted successfully ✅"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error deleting receipt",
                    error = ex.Message
                });
            }
        }
    }
}