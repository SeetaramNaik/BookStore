using Microsoft.EntityFrameworkCore;
using Application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Application.DataAccess;



public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        optionsBuilder.UseSqlServer(
    //             builder.Configuration.GetConnectionString("DefaultConnection"),
    //            b => b.MigrationsAssembly("Application.DataAccess"));
    //    }
    //}

    public DbSet<Category> Categories { get; set; }
    public DbSet<CoverType> CoverTypes { get; set; }
    public DbSet<Product> Products { get; set; }
}
