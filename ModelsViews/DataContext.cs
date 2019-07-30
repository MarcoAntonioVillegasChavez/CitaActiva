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
            optionsBuilder.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]);
            //optionsBuilder.UseMySql(configuration["ConnectionStrings:DefaultConnection"]);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<KitsArticulos>()
                .HasKey(ka => new { ka.id_kit, ka.id_articulo });
            modelBuilder.Entity<KitsClientesDescuentos>()
                .HasKey(kcd => new { kcd.idkit_cliente, kcd.id_descuento });
            modelBuilder.Entity<KitsClientesPromociones>()
                .HasKey(kcp => new { kcp.idkit_cliente, kcp.id_promocion });
            modelBuilder.Entity<KitsMo>()
                .HasKey(km => new { km.id_kit, km.id_mo });
        }
        public DbSet<Appointment> Appointment { get; set; }
        //public DbSet<Labours> Labours { get; set; }
        //public DbSet<Versions> Versions { get; set; }
        public DbSet<Agencias> Agencias { get; set; }
        //public DbSet<Workshop> Workshops { get; set; }
        public DbSet<AgendamientoCitas> AgendamientoCitas { get; set; }
        public DbSet<Articulos> Articulos { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Descuentos> Descuentos { get; set; }
        public DbSet<Kits> Kits { get; set; }
        public DbSet <KitsArticulos> KitsArticulos { get; set; }
        public DbSet<KitsClientes> KitsClientes { get; set; }
        public DbSet<KitsMo> KitsMo { get; set; }
        public DbSet<KitsClientesDescuentos> KitsClientesDescuentos { get; set; }
        public DbSet<KitsClientesPromociones> KitsClientesPromociones { get; set; }
        public DbSet<FamiliasVehiculo> FamiliasVehiculos { get; set; }
        public DbSet<MarcasVehiculo> MarcasVehiculos { get; set; }
        public DbSet<Mo> Mo { get; set; }
        public DbSet<ModelosVehiculo> ModelosVehiculos { get; set; }
        public DbSet<Promociones> Promociones { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<TipoCombustible> TipoCombustibles { get; set; }
        public DbSet<Token> Token { get; set; }


    }
}
