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

        /// <summary>
        /// Starts a new bill.
        /// </summary>
        /// <param name="request">The request to start a new bill.</param>
        /// <returns>A new order ID and status.</returns>
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

        /// <summary>
        /// Adds an item to the bill.
        /// </summary>
        /// <param name="orderId">The order ID to add the item to.</param>
        /// <param name="request">The request to add an item to the bill.</param>
        /// <returns>The updated bill details.</returns>
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

        /// <summary>
        /// Removes an item from the bill.
        /// </summary>
        /// <param name="orderId">The order ID of the bill.</param>
        /// <param name="itemId">The item ID to remove.</param>
        /// <returns>A success message or error details.</returns>
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

        /// <summary>
        /// Gets the bill details by order ID.
        /// </summary>
        /// <param name="orderId">The order ID of the bill.</param>
        /// <returns>The bill details or an error message.</returns>
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

        /// <summary>
        /// Modifies an item on the bill.
        /// </summary>
        /// <param name="orderId">The order ID of the bill.</param>
        /// <param name="itemId">The item ID to modify.</param>
        /// <param name="request">The request to modify the item on the bill.</param>
        /// <returns>The updated item details or error message.</returns>
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

        /// <summary>
        /// Modifies an ingredient on the bill.
        /// </summary>
        /// <param name="orderId">The order ID of the bill.</param>
        /// <param name="itemId">The item ID to which the ingredient belongs.</param>
        /// <param name="ingredientId">The ingredient ID to modify.</param>
        /// <param name="request">The request to modify the ingredient on the bill.</param>
        /// <returns>The updated ingredient details or error message.</returns>
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

        /// <summary>
        /// Adds a discount to the bill.
        /// </summary>
        /// <param name="orderId">The order ID of the bill.</param>
        /// <param name="request">The request to apply the discount.</param>
        /// <returns>The updated bill details or error message.</returns>
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

        /// <summary>
        /// Cancels the bill.
        /// </summary>
        /// <param name="orderId">The order ID of the bill.</param>
        /// <returns>A success message or error details.</returns>
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

        /// <summary>
        /// Changes the tip amount on the bill.
        /// </summary>
        /// <param name="orderId">The order ID of the bill.</param>
        /// <param name="request">The request to change the tip amount.</param>
        /// <returns>A success message or error details.</returns>
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

        /// <summary>
        /// Processes the payment for the order.
        /// </summary>
        /// <param name="orderId">The order ID to pay for.</param>
        /// <param name="request">The request to process the payment.</param>
        /// <returns>A success message or error details.</returns>
        [HttpPut("{orderId}/pay")]
        public async Task<IActionResult> PayOrder(string orderId, [FromBody] PayOrderRequest request)
        {
            string token = Request.Headers.Authorization;

            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                await _orderService.PayOrderAsync(orderId, request.CreditCardNumber);
                return Ok("Payment accepted, order is now considered Paid.");
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

        /// <summary>
        /// Calculates the total cost of the bill.
        /// </summary>
        /// <param name="orderId">The order ID.</param>
        /// <returns>The total cost of the bill.</returns>
        [HttpGet("{orderId}/calculate-bill-cost")]
        public async Task<IActionResult> CalculateBillCost(string orderId)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var response = await _orderService.CalculateBillCostAsync(orderId);
                return Ok(response);
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

        /// <summary>
        /// Calculates the total cost of a linked bill item.
        /// </summary>
        /// <param name="itemId">The item ID.</param>
        /// <returns>The total cost of the linked bill item.</returns>
        [HttpGet("{itemId}/calculate-item-cost")]
        public async Task<IActionResult> CalculateLinkedBillItemCost(string itemId)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var response = await _orderService.CalculateLinkedBillItemCostAsync(itemId);
                return Ok(response);
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

        /// <summary>
        /// Calculates the total cost of a linked ingredient.
        /// </summary>
        /// <param name="ingredientId">The ingredient ID.</param>
        /// <returns>The total cost of the linked ingredient.</returns>
        [HttpGet("{ingredientId}/calculate-ingredient-cost")]
        public async Task<IActionResult> CalculateLinkedIngredientCost(string ingredientId)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var response = await _orderService.CalculateLinkedIngredientCostAsync(ingredientId);
                return Ok(response);
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

        /// <summary>
        /// Get various amounts related to the order.
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <returns>A series of amounts related to the order.</returns>
        [HttpGet("{orderId}/amounts")]
        public async Task<IActionResult> GetOrderAmounts(string orderId)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var amounts = await _orderService.CalculateOrderAmountsAsync(orderId);
                return Ok(amounts);
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
