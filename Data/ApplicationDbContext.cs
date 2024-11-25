using ETickets.Models;
using Microsoft.EntityFrameworkCore;
using ETickets.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ETickets.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Category> Categories { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            // Retrieve the connection string
            string? connectionString = builder.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }



        public DbSet<ApplicationUserVM> ApplicationUserVM { get; set; } = default!;



        public DbSet<ETickets.ViewModel.MovieVM> MovieVM { get; set; } = default!;



        public DbSet<ETickets.ViewModel.UserLoginVM> UserLoginVM { get; set; } = default!;



        public DbSet<ETickets.ViewModel.UserRoleVM> UserRoleVM { get; set; } = default!;
    }
}
