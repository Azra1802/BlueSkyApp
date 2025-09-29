using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlueSkyApp.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Apartment> Apartments { get; set; } = null!;
        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<Organization> Organizations { get; set; } = null!;
        public virtual DbSet<OrganizationMember> OrganizationMembers { get; set; } = null!;
        public virtual DbSet<Property> Properties { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-PJTIJBD\\SQLEXPRESS01;Database=SmartApartmentDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.PropertyId).HasColumnName("PropertyID");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Apartments)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("FK__Apartment__Prope__46E78A0C");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.PropertyId).HasColumnName("PropertyID");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.ApartmentId)
                    .HasConstraintName("FK__Inventory__Apart__4AB81AF0");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("FK__Inventory__Prope__4BAC3F29");
            });

            modelBuilder.Entity<Organization>(entity =>
            {
                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<OrganizationMember>(entity =>
            {
                entity.Property(e => e.OrganizationMemberId).HasColumnName("OrganizationMemberID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.OrganizationMembers)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__Organizat__Organ__3E52440B");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.OrganizationMembers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_OrganizationMembers_Roles");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.OrganizationMembers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Organizat__UserI__3F466844");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(e => e.PropertyId).HasColumnName("PropertyID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.OrganizationId).HasColumnName("OrganizationID");

                entity.HasOne(d => d.Organization)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.OrganizationId)
                    .HasConstraintName("FK__Propertie__Organ__4316F928");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.ReservationId).HasColumnName("ReservationID");

                entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.GuestEmail).HasMaxLength(100);

                entity.Property(e => e.GuestName).HasMaxLength(100);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Pending')");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.ApartmentId)
                    .HasConstraintName("FK__Reservati__Apart__571DF1D5");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.Name, "UQ__Roles__737584F66A74A98B")
                    .IsUnique();

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.Property(e => e.TaskId).HasColumnName("TaskID");

                entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.DueDate).HasColumnType("datetime");

                entity.Property(e => e.PropertyId).HasColumnName("PropertyID");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Pending')");

                entity.Property(e => e.TaskType).HasMaxLength(50);

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.ApartmentId)
                    .HasConstraintName("FK__Tasks__Apartment__5070F446");

                entity.HasOne(d => d.AssignedToNavigation)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.AssignedTo)
                    .HasConstraintName("FK__Tasks__AssignedT__52593CB8");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("FK__Tasks__PropertyI__5165187F");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534653F4939")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.PasswordHash).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
