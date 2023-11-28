using System.Globalization;
using System.Security.Claims;

namespace TradeMarket.Services
{
    public class GlobalMehodsService : IGlobalMethodsService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public GlobalMehodsService(IHttpContextAccessor httpContextAccessor) 
        {
            _contextAccessor = httpContextAccessor;
        }
        public string CurrentUser()
        {
            var result = _contextAccessor.HttpContext.User.FindFirstValue("UserId");
            return result;
        }

        public string CapitalizeFirstLetter(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input);
        }
    }
}
