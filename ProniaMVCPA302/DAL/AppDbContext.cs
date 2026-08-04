using Microsoft.EntityFrameworkCore;
using ProniaMVCPA302.Models;

namespace ProniaMVCPA302.DAL
{
    public class AppDbContext:DbContext

    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;Database=ProniaMVCPA302;trusted_connection=true;integrated security=true;trustservercertificate=true;");
            
        }

        public DbSet<Slide> Sliders { get; set; }
    }
}   
