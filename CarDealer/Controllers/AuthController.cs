using CarDealer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TradeMarket.Models;
using TradeMarket.Services;

namespace TradeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService )
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.Register(model);
            if (!result.isAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.GetToken(model);
            if (!result.isAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpGet("Countries")]
        public async Task<IActionResult> Countries()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.Countries();
            return Ok(result);
        }

        [HttpGet("Country{CountryId}/City")]
        public async Task<IActionResult> Cities(int CountryId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _authService.Cities(CountryId);
            return Ok(result);
        }

    }
}
