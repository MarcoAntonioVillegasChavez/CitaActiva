using CitaActiva.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;

namespace CitaActiva.ModelsViews
{
    public class DataContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            optionsBuilder.UseMySql(configuration["ConnectionStrings:DefaultConnection"]);
        }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Labours> Labours { get; set; }
        public DbSet<Versions> Versions { get; set; }
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<Token> Token { get; set; }

    }
}
