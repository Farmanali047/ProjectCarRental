using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectCarRental.Models;

namespace ProjectCarRental.Data
{
    public class ApplicationDbContext : IdentityDbContext<myAppUsers>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ProjectCarRental.Models.CarRegisteration> CarRegisteration { get; set; } = default!;
    }
}
