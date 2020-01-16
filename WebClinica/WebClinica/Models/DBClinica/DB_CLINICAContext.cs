using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebClinica.Models.DBClinica
{
    public partial class DB_CLINICAContext : DbContext
    {
        public virtual DbSet<Citas> Citas { get; set; }
        public virtual DbSet<LoginUsers> LoginUsers { get; set; }
        public virtual DbSet<TiposCitas> TiposCitas { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersType> UsersType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-68VCK7K\LCSQL;Database=DB_CLINICA;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citas>(entity =>
            {
                entity.HasKey(e => e.IdCita);

                entity.ToTable("CITAS");

                entity.Property(e => e.IdCita).HasColumnName("ID_CITA");

                entity.Property(e => e.Cancelada).HasColumnName("CANCELADA");

                entity.Property(e => e.DescripcionCita)
                    .HasColumnName("DESCRIPCION_CITA")
                    .HasMaxLength(500);

                entity.Property(e => e.FechaCita)
                    .HasColumnName("FECHA_CITA")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdTipoCita).HasColumnName("ID_TIPO_CITA");

                entity.Property(e => e.IdUser).HasColumnName("ID_USER");

                entity.HasOne(d => d.IdTipoCitaNavigation)
                    .WithMany(p => p.Citas)
                    .HasForeignKey(d => d.IdTipoCita)
                    .HasConstraintName("FK_CITAS_TIPOS_CITAS");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Citas)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_CITAS_USERS");
            });

            modelBuilder.Entity<LoginUsers>(entity =>
            {
                entity.HasKey(e => e.IdLoginUser);

                entity.ToTable("LOGIN_USERS");

                entity.Property(e => e.IdLoginUser).HasColumnName("ID_LOGIN_USER");

                entity.Property(e => e.IdUser).HasColumnName("ID_USER");

                entity.Property(e => e.LoginUser)
                    .HasColumnName("LOGIN_USER")
                    .HasMaxLength(50);

                entity.Property(e => e.UserPassword)
                    .HasColumnName("USER_PASSWORD")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.LoginUsers)
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("FK_LOGIN_USERS_USERS");
            });

            modelBuilder.Entity<TiposCitas>(entity =>
            {
                entity.HasKey(e => e.IdTipoCita);

                entity.ToTable("TIPOS_CITAS");

                entity.Property(e => e.IdTipoCita).HasColumnName("ID_TIPO_CITA");

                entity.Property(e => e.DescripcionTipo)
                    .HasColumnName("DESCRIPCION_TIPO")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("USERS");

                entity.Property(e => e.IdUser).HasColumnName("ID_USER");

                entity.Property(e => e.IdCitizen)
                    .HasColumnName("ID_CITIZEN")
                    .HasMaxLength(250);

                entity.Property(e => e.UserDescription)
                    .HasColumnName("USER_DESCRIPTION")
                    .HasMaxLength(500);

                entity.Property(e => e.UserLastname1)
                    .HasColumnName("USER_LASTNAME_1")
                    .HasMaxLength(250);

                entity.Property(e => e.UserLastname2)
                    .HasColumnName("USER_LASTNAME_2")
                    .HasMaxLength(250);

                entity.Property(e => e.UserName)
                    .HasColumnName("USER_NAME")
                    .HasMaxLength(250);

                entity.Property(e => e.UserType).HasColumnName("USER_TYPE");

                entity.HasOne(d => d.UserTypeNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserType)
                    .HasConstraintName("FK_USERS_USERS_TYPE");
            });

            modelBuilder.Entity<UsersType>(entity =>
            {
                entity.HasKey(e => e.IduserType);

                entity.ToTable("USERS_TYPE");

                entity.Property(e => e.IduserType)
                    .HasColumnName("IDUSER_TYPE")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserTypeDesc)
                    .HasColumnName("USER_TYPE_DESC")
                    .HasMaxLength(50);
            });
        }
    }
}
