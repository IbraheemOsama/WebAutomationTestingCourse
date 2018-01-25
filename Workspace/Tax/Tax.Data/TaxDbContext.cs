using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Tax.Data
{
    public class TaxDbContext : IdentityDbContext<ApplicationUser>
    {
        public TaxDbContext(DbContextOptions<TaxDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserTax>().HasKey(ut => new { ut.Year, ut.UserId });
        }
        public DbSet<UserTax> UserTaxes { get; set; }
    }
}
