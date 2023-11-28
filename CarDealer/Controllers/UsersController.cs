using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeMarket.Services;

namespace TradeMarket.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService) 
        { 
            _usersService = usersService;
        }

        [HttpGet("Profile/Favourites")]
        public async Task<IActionResult> GetFavouriteCollection()
        {
            var result = await _usersService.GetFavouriteCollection();
            return Ok(result);
        }

        [HttpDelete("Profile/Favourites/Post{PostID}")]
        public IActionResult DeletePost(int PostID)
        {
            var result = _usersService.Delete(PostID);
            return Ok(result);
        }
    }
}
