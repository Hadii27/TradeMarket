using CarDealer.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TradeMarket.Models;

namespace TradeMarket.Services
{
    public class UsersService : IUsersService
    {
        private readonly DataContext _context;
        private readonly IGlobalMethodsService _globalMethodsService;
        public UsersService(DataContext dataContext, IGlobalMethodsService globalMethods)
        {
            _context = dataContext;
            _globalMethodsService = globalMethods;
        }

        public async Task<IEnumerable<FavouriteDetailsModel>> GetFavouriteCollection()
        {
            var userID = _globalMethodsService.CurrentUser();
            var User = await _context.Users.FindAsync(userID);

            if (User == null)
                return null;

            var FavouriteCollection = await _context.favourite
                .Where(f => f.UserID == User.Id)
                .FirstOrDefaultAsync();

            if (FavouriteCollection is null)
                return null;

            var FavouriteDetails = await _context.favouriteDetails
                .Where(f => f.FavouriteID == FavouriteCollection.id)
                .ToListAsync();

            var postsList = new List<FavouriteDetailsModel>();

            foreach (var post in FavouriteDetails)
            {
                var Posts = await _context.sells
                       .Where(p => p.id == post.sellId)
                       .FirstOrDefaultAsync();
                postsList.Add(post);
            }
            return postsList;
        }

        public string Delete(int PostID)
        {
            var userID = _globalMethodsService.CurrentUser();
            var favouriteCollection =  _context.favourite
                .Where(f => f.UserID == userID)
                .FirstOrDefault();
            if (favouriteCollection is null)
                return "You don't have Favourite posts";
            var post =  _context.favouriteDetails
                .Where(p => p.Id == PostID && favouriteCollection.id == p.FavouriteID)
                .FirstOrDefault();
            if (post is null)
                return "You can't remove this post";
            var result =  _context.favouriteDetails.Remove(post);
             _context.SaveChanges();

            return "Deleted";
        }


    }
}
