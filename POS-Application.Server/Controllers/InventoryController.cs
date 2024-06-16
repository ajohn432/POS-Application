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

        /// <summary>
        /// Retrieves all bill items with their ingredients.
        /// </summary>
        /// <returns>A list of bill items with ingredients.</returns>
        [HttpGet("items")]
        public async Task<IActionResult> GetAllBillItemsWithIngredients()
        {
            string token = Request.Headers.Authorization;
            Console.WriteLine("token: " +  token);
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

        /// <summary>
        /// Retrieves all ingredients from the BaseIngredients table.
        /// </summary>
        /// <returns>A list of ingredients.</returns>
        [HttpGet("ingredients")]
        public async Task<IActionResult> GetAllIngredients()
        {
            string token = Request.Headers.Authorization;

            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }
            try
            {
                var ingredients = await _inventoryService.GetAllIngredientsAsync();
                return Ok(ingredients);
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

        /// <summary>
        /// Adds a new base item to the inventory.
        /// </summary>
        /// <param name="request">The request containing the details of the new base item.</param>
        /// <returns>A status message indicating success or failure.</returns>
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

        /// <summary>
        /// Marks an item as in stock.
        /// </summary>
        /// <param name="itemId">The ID of the item to mark as in stock.</param>
        /// <returns>A status message indicating success or failure.</returns>
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

        /// <summary>
        /// Marks an item as out of stock.
        /// </summary>
        /// <param name="itemId">The ID of the item to mark as out of stock.</param>
        /// <returns>A status message indicating success or failure.</returns>
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

        /// <summary>
        /// Gets the stock status of a specified item.
        /// </summary>
        /// <param name="itemId">The identifier of the item.</param>
        /// <returns>The stock status of the item.</returns>
        [HttpGet("item/{itemId}/stock")]
        public async Task<IActionResult> GetItemStockStatus(string itemId)
        {
            string token = Request.Headers.Authorization;

            if (!await _authenticationService.JwtCheck(token))
            {
                return BadRequest("Token missing or invalid in request headers.");
            }

            try
            {
                var response = await _inventoryService.GetItemStockStatusAsync(itemId);
                if (response == null)
                {
                    return NotFound();
                }

                return Ok(response);
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
