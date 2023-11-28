using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeMarket.Dtos;
using TradeMarket.Models;
using TradeMarket.Services;

namespace TradeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellController : ControllerBase
    {
        private readonly ISellsService _sellsService;
        public SellController (ISellsService sellsService)
        {
            _sellsService = sellsService;
        }

        [HttpGet("Category")]
        public async Task<IActionResult> Categories()
        {
            var categories = await _sellsService.Category();
            return Ok(categories);
        }

        [HttpGet("Category{CatID}/Sub-Category")]
        public async Task<IActionResult> Subs(int CatID)
        {
            var Subs = await _sellsService.SubCategory(CatID);
            return Ok(Subs);
        }

        [HttpPost("Category{CategoryId}/Sub-Category{SubCategory}/Post")]
        public async Task<IActionResult> Post(int CategoryId, SellDto dto, int SubCategory)
        {
            var post = new SellModel
            {
                name = dto.name,
                description = dto.description,
                priceType = dto.priceType,
                price = dto.price,
                quantity = dto.quantity,
                Condition = dto.Condition,
                PurchaseDate = dto.PurchaseDate,
            };

            var categories = await _sellsService.Sell(CategoryId, post, SubCategory);
            return Ok(categories);
        }
    }
}
