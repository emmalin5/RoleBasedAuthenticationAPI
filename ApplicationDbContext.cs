using auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace auth
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser>
    {
        //OptionsBuilder.UseSqlServer("Your Connection String");
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedRoles(modelBuilder);
        }

        private static  void  SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole>().HasData(
           new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
           new IdentityRole { Name = "User", NormalizedName = "USER" }

       );
        }
    }
}
