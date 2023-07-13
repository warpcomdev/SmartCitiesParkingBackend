using SCParking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace SCParking.Infrastructure.ContextDb
{
    public partial class SmartCities_Context : DbContext
    {
        public SmartCities_Context()
        {
        }

        public SmartCities_Context(DbContextOptions<SmartCities_Context> options)
            : base(options)
        {
        }

        
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<RateDetails> RatesDetails { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<ParkingReservation> ParkingReservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("smartcities");

       

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.DeletedAt).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                   .HasMaxLength(32)
                   .IsUnicode(false);

                entity.Property(e => e.SecondPhone)
                   .HasMaxLength(32)
                   .IsUnicode(false);

                entity.Property(e => e.AvatarUrl)
                   .HasMaxLength(255)
                   .IsUnicode(false);

                entity.Property(e => e.UnConfirmedEmail);

                entity.Property(e => e.Password)
                    .HasMaxLength(254)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordLastChange).HasColumnType("datetime");

                entity.Property(e => e.ResetTokenPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ResetTokenPasswordExpire).HasColumnType("datetime");

                entity.Property(e => e.ResetTokenEmail)
                   .HasMaxLength(255)
                   .IsUnicode(false);

                entity.Property(e => e.ResetEmail)
                   .HasMaxLength(254)
                   .IsUnicode(false);

              

                entity.Property(e => e.ResetTokenEmailExpire).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Rate>(entity =>
            {
                entity.ToTable("Rate");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");
                entity.Property(e => e.PlaceType)
                    .HasMaxLength(10)
                    .IsUnicode(false);



                entity.Property(e => e.StartDate).HasColumnType("datetime");
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<RateDetails>(entity =>
            {
                entity.ToTable("RateDetails");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");



                entity.HasOne(d => d.Rate)
                    .WithMany(p => p.RateDetails)
                    .HasForeignKey(d => d.RateId)
                    .HasConstraintName("FK_RateDetails_Rate");
                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
                                          
            });

            modelBuilder.Entity<Setting>(entity =>
            {
                entity.ToTable("Setting");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
                entity.Property(e => e.SettingCode)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ParkingReservation>(entity =>
            {
                entity.ToTable("ParkingReservation");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.StartAt).HasColumnType("datetime");
                entity.Property(e => e.EndAt).HasColumnType("datetime");
                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.UserToken)
                    .HasMaxLength(255)
                    .IsUnicode(false);

            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
      
    }
}
