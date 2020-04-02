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
            modelBuilder.Entity<AgenciaCita>(entity => 
            {
                entity.ToTable("agencia_cita");
                entity.HasKey(e => e.id_agencia_cita).HasName("agencia_cita_pkey");
                entity.HasIndex(e => e.id_agencia_cita).HasName("idx_agencia_cita_id_agencia_cita");

                entity.Property(e => e.id_agencia_cita).HasColumnName("id_agencia_cita");
                entity.Property(e => e.id_cita).HasColumnName("id_cita");
                entity.Property(e => e.id_agencia).HasColumnName("id_agencia");
                entity.Property(e => e.agencia).HasColumnName("agencia").HasMaxLength(250); ;
                entity.Property(e => e.google_place).HasColumnName("google_place").HasColumnType("json");
                entity.Property(e => e.id_zona).HasColumnName("id_zona");

                entity.HasOne(d => d.Citas).WithMany(p => p.AgenciaCitas).HasForeignKey(d => d.id_cita).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AgendamientoCita>(entity => 
            {
                entity.ToTable("agendamiento_cita");
                entity.HasKey(e => e.id_agendamiento_cita).HasName("agendamiento_cita_pk");
                entity.HasIndex(e => e.id_agendamiento_cita).HasName("idx_agendamiento_cita_id_agendamiento_cita");

                entity.Property(e => e.id_agendamiento_cita).HasColumnName("id_agendamiento_cita");
                entity.Property(e => e.id_cita).HasColumnName("id_cita");
                entity.Property(e => e.planned_date).HasColumnName("planned_date");
                entity.Property(e => e.planned_time).HasColumnName("planned_time");
                entity.Property(e => e.active_ind).HasColumnName("active_ind");

                entity.HasOne(d => d.Citas).WithMany(p => p.AgendamientoCitas).HasForeignKey(d => d.id_cita).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<ArticuloCita>(entity => 
            {
                entity.ToTable("articulo_cita");
                entity.HasKey(e => e.id_articulo_cita).HasName("articulo_cita_pkey");
                entity.HasIndex(e => e.id_articulo_cita).HasName("idx_articulo_cita_id_articulo_cita");

                entity.Property(e => e.id_articulo_cita).HasColumnName("id_articulo_cita");
                entity.Property(e => e.id_cita).HasColumnName("id_cita");
                entity.Property(e => e.id_articulo).HasColumnName("id_articulo");
                entity.Property(e => e.codigo_qis).HasColumnName("codigo_qis");
                entity.Property(e => e.descripcion).HasColumnName("descripcion");

                entity.HasOne(d => d.Citas).WithMany(p => p.ArticuloCitas).HasForeignKey(d => d.id_cita).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Citas>(entity =>
            {
                entity.ToTable("cita");
                entity.HasKey(e => e.id_cita).HasName("citas_pkey");
                entity.HasIndex(e => e.id_cita).HasName("idx_citas_id_cita_id_appointment");

                entity.Property(e => e.id_cita).HasColumnName("id_cita");
                entity.Property(e => e.id_appointment).HasColumnName("id_appointment").HasMaxLength(250); 
                entity.Property(e => e.id_cliente).HasColumnName("id_cliente");
                entity.Property(e => e.id_invitado).HasColumnName("id_invitado");
                entity.Property(e => e.status_cita).HasColumnName("status_cita");
                entity.Property(e => e.fecha_registro).HasColumnName("fecha_registro");
                entity.Property(e => e.fecha_actualizacion).HasColumnName("fecha_actualizacion");
                entity.Property(e => e.fecha_cancelacion).HasColumnName("fecha_cancelacion");

                entity.HasOne(d => d.Clientes).WithMany(p => p.Citas).HasForeignKey(d => d.id_cliente).OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(d => d.Invitados).WithMany(p => p.Citas).HasForeignKey(d => d.id_invitado).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.ToTable("cliente");
                entity.HasKey(e => e.id_cliente).HasName("clientes_pkey");
                entity.HasIndex(e => e.id_cliente).HasName("idx_cliente_id_cliente");

                entity.Property(e => e.id_cliente).HasColumnName("id_cliente");
                entity.Property(e => e.cuenta_personal).HasColumnName("cuenta_personal").HasMaxLength(100);
                entity.Property(e => e.nombre_cliente).HasColumnName("nombre_cliente").HasMaxLength(100);
                entity.Property(e => e.apellido_paterno).HasColumnName("apellido_paterno").HasMaxLength(100);
                entity.Property(e => e.apellido_materno).HasColumnName("apellido_materno").HasMaxLength(100);
                entity.Property(e => e.fecha_nacimiento).HasColumnName("fecha_nacimiento");
                entity.Property(e => e.email_cliente).HasColumnName("email_cliente").HasMaxLength(100);
                entity.Property(e => e.telefono).HasColumnName("telefono").HasMaxLength(100);
                entity.Property(e => e.password).HasColumnName("password").HasMaxLength(100);
                entity.Property(e => e.rfc).HasColumnName("rfc").HasMaxLength(50);
                entity.Property(e => e.homo_clave).HasColumnName("homo_clave").HasMaxLength(10);
                entity.Property(e => e.fecha_registro).HasColumnName("fecha_registro");
                entity.Property(e => e.cliente_activo).HasColumnName("cliente_activo");
            });

            modelBuilder.Entity<Invitados>(entity =>
            {
                entity.ToTable("invitado");
                entity.HasKey(e => e.id_invitado).HasName("invitados_pkey");
                entity.HasIndex(e => e.id_invitado).HasName("idx_invitado_id_invitado");

                entity.Property(e => e.id_invitado).HasColumnName("id_invitado");
                entity.Property(e => e.nombre_cliente).HasColumnName("nombre_cliente").HasMaxLength(100); 
                entity.Property(e => e.apellido_paterno).HasColumnName("apellido_paterno").HasMaxLength(100);
                entity.Property(e => e.apellido_materno).HasColumnName("apellido_materno").HasMaxLength(100);
                entity.Property(e => e.email_cliente).HasColumnName("email_cliente").HasMaxLength(100);
                entity.Property(e => e.telefono).HasColumnName("telefono").HasMaxLength(100);
                entity.Property(e => e.rfc).HasColumnName("rfc").HasMaxLength(100);
            });

            modelBuilder.Entity<KitCita>(entity => 
            {
                entity.ToTable("kit_cita");
                entity.HasKey(e => e.id_kit_cita).HasName("kit_cita_pkey");
                entity.HasIndex(e => e.id_kit_cita).HasName("idx_kit_cita_id_kit_cita");

                entity.Property(e => e.id_kit_cita).HasColumnName("id_kit_cita");
                entity.Property(e => e.id_cita).HasColumnName("id_cita");
                entity.Property(e => e.id_kit).HasColumnName("id_kit");
                entity.Property(e => e.codigo_qis).HasColumnName("codigo_qis").HasMaxLength(250); ;
                entity.Property(e => e.kit).HasColumnName("kit").HasMaxLength(250); ;
                entity.Property(e => e.costo).HasColumnName("costo");

                entity.HasOne(d => d.Citas).WithMany(p => p.KitCitas).HasForeignKey(d => d.id_cita).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MoCita>(entity => 
            {
                entity.ToTable("mo_cita");
                entity.HasKey(e => e.id_mo_cita).HasName("mo_cita_pkey");
                entity.HasIndex(e => e.id_mo_cita).HasName("idx_mo_cita_id_mo_cita");

                entity.Property(e => e.id_mo_cita).HasColumnName("id_mo_cita");
                entity.Property(e => e.id_cita).HasColumnName("id_cita");
                entity.Property(e => e.id_mo).HasColumnName("id_mo");
                entity.Property(e => e.codigo_qis).HasColumnName("codigo_qis").HasMaxLength(250); ;
                entity.Property(e => e.descripcion).HasColumnName("descripcion").HasMaxLength(250); ;

                entity.HasOne(d => d.Citas).WithMany(p => p.MoCitas).HasForeignKey(d => d.id_cita).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Error>(entity =>
            {
                entity.ToTable("error");
                entity.HasKey(e => e.id_error).HasName("error_pkey");
                entity.HasIndex(e => e.id_error).HasName("idx_error_id_error");

                entity.Property(e => e.id_error).HasColumnName("id_error");
                entity.Property(e => e.detalle).HasColumnName("detalle");
                entity.Property(e => e.fecha).HasColumnName("fecha");
            });
        }
    
        public DbSet<AgenciaCita> AgenciaCitas { get; set; }
        public DbSet<AgendamientoCita> AgendamientoCita { get; set; }
        public DbSet<ArticuloCita> ArticuloCitas { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Citas> Citas { get; set; }
        public DbSet<Invitados> Invitados { get; set; }
        public DbSet<KitCita> KitCitas { get; set; }
        public DbSet<MoCita> MoCitas { get; set; }
        public DbSet<Token> Token { get; set; }
        public DbSet<Error> Error { get; set; }


    }
}
