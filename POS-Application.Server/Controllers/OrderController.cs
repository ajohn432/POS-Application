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
            var orderId = await _orderService.StartNewBillAsync(request);
            return Ok(new { orderId, Status = "New" });
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
        }
        /*
        [HttpPost("{orderId}/items")]
        public async Task<IActionResult> AddItemToBill(string orderId, [FromBody] BillItem item)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }
            var result = await _orderService.AddItemToBillAsync(orderId, item);
            return Ok(result);
        }

        [HttpDelete("{orderId}/items/{itemId}")]
        public async Task<IActionResult> RemoveItemFromBill(string orderId, string itemId)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }
            var result = await _orderService.RemoveItemFromBillAsync(orderId, itemId);
            return Ok(result);
        }
        
        [HttpPut("{orderId}/items/{itemId}")]
        public async Task<IActionResult> ModifyItemOnBill(string orderId, string itemId, [FromBody] ModifyBillItemRequest request)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }
            var result = await _orderService.ModifyItemOnBillAsync(orderId, itemId, request.Quantity);
            return Ok(result);
        }

        [HttpPost("{orderId}/discount")]
        public async Task<IActionResult> AddDiscountToBill(string orderId, [FromBody] DiscountRequest request)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }
            var result = await _orderService.AddDiscountToBillAsync(orderId, request.DiscountCode);
            return Ok(result);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> ClearBill(string orderId)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }
            var result = await _orderService.ClearBillAsync(orderId);
            return Ok(result);
        }
        */
    }
}
