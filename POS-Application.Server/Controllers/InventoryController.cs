using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<List<LinkedBillItem>>> GetAllBillItemsWithIngredients()
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
    }
}
