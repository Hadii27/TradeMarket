using CarDealer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TradeMarket.Models;

namespace CarDealer.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CategoryModel> categories { get; set; }
        public DbSet<CountryModel> countries { get; set; }
        public DbSet<CityModel> cities { get; set; }
        public DbSet<CurrencyModel> currencies { get; set; }
        public DbSet<SellModel> sells { get; set; }
        public DbSet<SubCategories> subCategories { get; set; }
        public DbSet<FavouriteModel> favourite { get; set; }
        public DbSet<FavouriteDetailsModel> favouriteDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CountryCurruncies>()
                .HasKey(e => new { e.CountryId, e.CurrencyId });



            base.OnModelCreating(builder);
        }

    }
}
