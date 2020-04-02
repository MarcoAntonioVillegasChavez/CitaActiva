using CitaActiva.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;

namespace CitaActiva.ModelsViews
{
    public class PaquetesContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            
            var configuration = builder.Build();
            optionsBuilder.UseNpgsql(configuration["ConnectionStrings:Paquetes_Servicio"]);
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.ToTable("cita");
                entity.HasKey(e => e.id_cita).HasName("cita_pkey");
                entity.HasIndex(e => e.id_cita).HasName("cita_id_cita_idx");

                entity.Property(e => e.id_cita).HasColumnName("id_cita");
                entity.Property(e => e.id).HasColumnName("id").HasMaxLength(250);
                entity.Property(e => e.id_agencia).HasColumnName("id_agencia");
                entity.Property(e => e.necesita_carro_reemplazo).HasColumnName("necesita_carro_reemplazo");
                entity.Property(e => e.placa_vehiculo).HasColumnName("placa_vehiculo");
                entity.Property(e => e.nombre_contacto).HasColumnName("nombre_contacto");
                entity.Property(e => e.recive_cliente).HasColumnName("recive_cliente");
                entity.Property(e => e.email).HasColumnName("email");

                entity.Property(e => e.vehiculo_pickup).HasColumnName("vehiculo_pickup");
                entity.Property(e => e.telefono).HasColumnName("telefono");
                entity.Property(e => e.fecha).HasColumnName("fecha");

                entity.Property(e => e.hora).HasColumnName("hora");
                entity.Property(e => e.kilometraje).HasColumnName("kilometraje");
                entity.Property(e => e.id_recepcionista).HasColumnName("id_recepcionista");
                entity.Property(e => e.comentarios).HasColumnName("comentarios");
                entity.Property(e => e.fecha_creacion).HasColumnName("fecha_creacion");
            });

        }

        public virtual DbSet<Cita> cita { get; set; }


    }
}
