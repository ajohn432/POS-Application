using Microsoft.AspNetCore.Mvc;
using POS_Application.Server.Models;
using POS_Application.Server.Services.Interfaces;

namespace POS_Application.Server.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IAuthenticationService _authenticationService;

        public OrderController(IOrderService orderService, IAuthenticationService authenticationService)
        {
            _orderService = orderService;
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> StartNewBill([FromBody] StartNewBillRequest request)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var orderId = await _orderService.StartNewBillAsync(request);
                return Ok(new { orderId, Status = "New" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Status = "An error occurred while creating a new bill." });
            }
        }

        [HttpPost("{orderId}/items")]
        public async Task<IActionResult> AddItemToBill(string orderId, [FromBody] AddItemToBillRequest request)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var result = await _orderService.AddItemToBillAsync(orderId, request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { Status = "An error occurred while adding the item from the bill." });
            }
        }

        [HttpDelete("{orderId}/items/{itemId}")]
        public async Task<IActionResult> RemoveItemFromBill(string orderId, string itemId)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                await _orderService.RemoveItemFromBillAsync(orderId, itemId);
                return Ok(new { Status = "Item removed from bill successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Status = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Status = "An error occurred while removing the item from the bill." });
            }
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetBillByOrderId(string orderId)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var bill = await _orderService.GetBillByOrderIdAsync(orderId);
                if (bill == null)
                {
                    return NotFound(new { Status = "Bill not found." });
                }
                return Ok(bill);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Status = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { Status = "An error occurred while retrieving the bill." });
            }
        }

        [HttpPut("{orderId}/items/{itemId}")]
        public async Task<IActionResult> ModifyItemOnBill(string orderId, string itemId, ModifyItemOnBillRequest request)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var response = await _orderService.ModifyItemOnBillAsync(orderId, itemId, request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while processing the request." });
            }
        }

        [HttpPut("{orderId}/items/{itemId}/{ingredientId}")]
        public async Task<IActionResult> ModifyIngredientOnBill(string orderId, string itemId, string ingredientId, [FromBody] ModifyIngredientRequest request)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var response = await _orderService.ModifyIngredientOnBillAsync(orderId, itemId, ingredientId, request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Status = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("{orderId}/discount")]
        public async Task<IActionResult> AddDiscountToBill(string orderId, [FromBody] ApplyDiscountRequest request)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var response = await _orderService.AddDiscountToBillAsync(orderId, request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Status = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "An error occurred while applying the discount.", Error = ex.Message });
            }
        }

        [HttpPut("{orderId}/cancel")]
        public async Task<IActionResult> CancelBill(string orderId)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                await _orderService.CancelBillAsync(orderId);
                return Ok(new { Status = "Item removed from bill successfully." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Status = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Internal server error
            }
        }

        [HttpPut("{orderId}/tip")]
        public async Task<IActionResult> ChangeTipAmount(string orderId, [FromBody] ChangeTipAmountRequest request)
        {
            string token = Request.Headers.Authorization;

            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var result = await _orderService.ChangeTipAmountAsync(orderId, request.TipAmount);
                if (result)
                {
                    return Ok("Tip amount updated successfully.");
                }

                return NotFound("Order not found.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Status = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Internal server error
            }
        }
    }
}
