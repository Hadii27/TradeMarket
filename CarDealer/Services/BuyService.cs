using CarDealer.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TradeMarket.Models;

namespace TradeMarket.Services
{
    public class BuyService : IBuyService
    {
        private readonly DataContext _dataContext;
        private readonly IGlobalMethodsService _globalMethods;
        public BuyService (DataContext dataContext, IGlobalMethodsService globalMethods)
        {
            _dataContext = dataContext;
            _globalMethods = globalMethods;          
        }

        public async Task<IEnumerable<CategoryModel>> Category()
        {
            var categories = await _dataContext.categories
                .Include(c => c.SubCategories)
                .ToListAsync();
            return categories;
        }

        public async Task<IEnumerable<CategoryModel>> CategoryByID(int CatId)
        {
            var Category = await _dataContext.categories
                .Where(c => c.Id == CatId)
                .ToListAsync();
            return Category;
        }

        public async Task<IEnumerable<SubCategories>> SubCategory(int CatId)
        {
            var SubCategory = await _dataContext.subCategories
                .Where(s => s.CategoryId == CatId)
                .ToListAsync();
            return SubCategory;
        }

        public async Task<IEnumerable<SellModel>> GetPosts(int CatId, int SubID)
        {
            var userID = _globalMethods.CurrentUser();
            var user = await _dataContext.Users
                .Where(u => u.Id == userID)
                .FirstOrDefaultAsync();
            if (user == null)
                return null;

            var posts = await _dataContext.sells
                .Where(p => p.CateId == CatId && p.SubCatId == SubID && p.CountryId == user.NationalityId)
                .OrderByDescending(p=> p.id)
                .ToListAsync();

            return posts;
        }

        public async Task<IEnumerable<SellModel>> GetPostsOfall(int CatId)
        {
            var userID = _globalMethods.CurrentUser();
            var user = await _dataContext.Users
                .Where(u => u.Id == userID)
                .FirstOrDefaultAsync();
            if (user == null)
                return null;
            var posts = await _dataContext.sells
                .Where(p => p.CateId == CatId && p.CountryId == user.NationalityId)
                .OrderByDescending(p => p.id)
                .ToListAsync();

            return posts;
        }

        public async Task<string> addFavourite(int PostID)
        {
            var userID = _globalMethods.CurrentUser();
            if (userID == null)
                return "You must login first";
            var existPost = await _dataContext.sells.FindAsync(PostID);
            if (existPost is null)
                return "Invalid post ID";

            var existFavourite = await _dataContext.favourite
                .Where(f => f.UserID == userID)
                .FirstOrDefaultAsync();

            if (existFavourite == null)
            {
                var NewFavourite = new FavouriteModel
                {
                    UserID = userID,
                };
                await _dataContext.favourite.AddAsync(NewFavourite);
                await _dataContext.SaveChangesAsync(); 

                var favouriteDetails = new FavouriteDetailsModel
                {
                    FavouriteID = NewFavourite.id,
                    sellId = PostID,
                };
                NewFavourite.Count += 1;
                await _dataContext.favouriteDetails.AddAsync(favouriteDetails);
                await _dataContext.SaveChangesAsync();
            }
            else
            {
                var favouriteDetails = new FavouriteDetailsModel
                {
                    FavouriteID = existFavourite.id,
                    sellId = PostID,
                };
                existFavourite.Count += 1;
                await _dataContext.favouriteDetails.AddAsync(favouriteDetails);
            }
            await _dataContext.SaveChangesAsync();
            return "You have been add this post to favourite succesfully";
        }
    }
}
