using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeMarket.Services;

namespace TradeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyController : ControllerBase
    {
        private readonly IBuyService _buyService;
        public BuyController(IBuyService buyService)
        {
            _buyService = buyService;
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _buyService.Category();

            return Ok(result);
        }

        [HttpGet("Category{CatID}/Posts")]
        public async Task<IActionResult> GetPostsOfCategory(int CatID)
        {
            var result = await _buyService.GetPostsOfall(CatID);
            return Ok(result);
        }

        [HttpGet("Category{CatID}/SubCategory{SubID}/Posts")]
        public async Task<IActionResult> GetPosts(int CatID, int SubID)
        {
            var result = await _buyService.GetPosts(CatID, SubID);
            return Ok(result);
        }

        [HttpPost("Category/SubCategory/Posts{PostID}/add-To-Favourite")]
        public async Task<IActionResult> AddToFavourite(int PostID)
        {
            var result = await _buyService.addFavourite(PostID);
            if (result == null)
            {
                return BadRequest("Login first");
            }
            return Ok(result);
        }
    }
}
