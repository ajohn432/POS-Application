using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS_Application.Server.Models;
using POS_Application.Server.Services.Interfaces;

namespace POS_Application.Server.Controllers
{
    [ApiController]
    [Route("api/inventory")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly IAuthenticationService _authenticationService;

        public InventoryController(IInventoryService inventoryService, IAuthenticationService authenticationService)
        {
            _inventoryService = inventoryService;
            _authenticationService = authenticationService;
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetAllBillItemsWithIngredients()
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var billItems = await _inventoryService.GetAllBillItemsWithIngredientsAsync();
                return Ok(billItems);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); //Internal server error
            }
        }

        [HttpPost("item")]
        public async Task<IActionResult> AddBaseItem([FromBody] AddBaseItemRequest request)
        {
            string token = Request.Headers.Authorization;
            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            if (!await _inventoryService.IsCurrentEmployeeManagerAsync(token))
            {
                return Unauthorized("You do not have permission to perform this action. Go get a Manager");
            }

            try
            {
                await _inventoryService.AddBaseItemAsync(request);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); //Internal server error
            }
        }

        [HttpPut("item/{itemId}/in-stock")]
        public async Task<IActionResult> MarkItemInStock(string itemId)
        {
            string token = Request.Headers.Authorization;

            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var result = await _inventoryService.MarkItemInStockAsync(itemId);
                if (result)
                {
                    return Ok("Item marked as in stock.");
                }

                return NotFound("Item not found.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); //Internal server error
            }
        }

        [HttpPut("item/{itemId}/out-of-stock")]
        public async Task<IActionResult> MarkItemOutOfStock(string itemId)
        {
            string token = Request.Headers.Authorization;

            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var result = await _inventoryService.MarkItemOutOfStockAsync(itemId);
                if (result)
                {
                    return Ok("Item marked as out of stock.");
                }

                return NotFound("Item not found.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); //Internal server error
            }
        }
    }
}
