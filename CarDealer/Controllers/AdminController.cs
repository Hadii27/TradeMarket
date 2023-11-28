using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TradeMarket.Dtos;
using TradeMarket.Models;
using TradeMarket.Services;

namespace TradeMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;          
        }


        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _adminService.GetCategories();
            return Ok(categories);
        }

        [HttpPost("Country")]
        public async Task<IActionResult> CreateCountry(CountryDto model)
        {
            var country = new CountryModel
            {
                Name = model.Name,
            };
            var result = await _adminService.CreateCountry(country);
            if (result == null)
                return BadRequest("This country already exist");
            return Ok(result);
        }

        [HttpPost("Country{CountryID}/City")]
        public async Task<IActionResult> CreateCity(CityListDto model, int CountryID)
        {
            var CityList = new List<CityModel>();
            foreach(var c in model.CityList)
            {
                var city = new CityModel
                {
                    Name = c.Name,
                    countryId = CountryID,
                };
                CityList.Add(city);
            }

            var result = await _adminService.CreateCity(CityList, CountryID);
            if (result == null)
                return BadRequest("This city already exist or invalid county");
            return Ok(result);
        }

        [HttpPost("Category")]
        public async Task<IActionResult> CreateCategory(CategoryDto model)
        {
            var category = new CategoryModel
            { 
                Name = model.Name,
                Description = model.Description ,                
            };

            var result = await _adminService.CreateCategory(category);
            if (result == null)            
                return BadRequest("This category already exist");
            return Ok(result);
        }

        [HttpPost("Category{CatID}/Sub-Category")]
        public async Task<IActionResult> CreateSubCat(SubDto model, int CatID)
        {
            var sub = new SubCategories
            {
                Name = model.Name,
            };

            var result = await _adminService.CreateSubCategory(sub, CatID);
            if (result == null)
                return BadRequest("This SubCategory category already exist");
            return Ok(result);
        }
    }
}
