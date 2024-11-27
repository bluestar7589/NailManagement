using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NailManagement.Models;

namespace NailManagement.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Inventory> Inventories { get; set; }
        public virtual DbSet<LoyaltyPoint> LoyaltyPoints { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<StaffSchedule> StaffSchedules { get; set; }
        public virtual DbSet<Technician> Technicians { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
                optionsBuilder.UseSqlServer(connectionString)
                          .LogTo(Console.WriteLine, LogLevel.Information);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.AppointmentId).HasName("PK__Appointm__8ECDFCA24D278C2F");

                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
                entity.Property(e => e.AppointmentDate).HasColumnType("datetime");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                entity.Property(e => e.Notes).HasMaxLength(255);
                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
                entity.Property(e => e.Status).HasMaxLength(50);
                entity.Property(e => e.TechnicianId).HasColumnName("TechnicianID");

                entity.HasOne(d => d.Customer).WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Appointme__Custo__4316F928");

                entity.HasOne(d => d.Service).WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__Appointme__Servi__440B1D61");

                entity.HasOne(d => d.Technician).WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.TechnicianId)
                    .HasConstraintName("FK__Appointme__Techn__44FF419A");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64B820EB49F9");

                entity.HasIndex(e => e.Email, "UQ__Customer__A9D1053498D6CE6C").IsUnique();

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.LoyaltyPoints).HasDefaultValue(0);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDF6728B0C30");

                entity.ToTable("Feedback");

                entity.Property(e => e.FeedbackId).HasColumnName("FeedbackID");
                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
                entity.Property(e => e.Comments).HasMaxLength(255);
                entity.Property(e => e.FeedbackDate).HasColumnType("date");

                entity.HasOne(d => d.Appointment).WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK__Feedback__Appoin__4BAC3F29");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("PK__Inventor__B40CC6ED86502ED4");

                entity.ToTable("Inventory");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");
                entity.Property(e => e.LastUpdated).HasColumnType("date");
                entity.Property(e => e.ProductName).HasMaxLength(100);
                entity.Property(e => e.Supplier).HasMaxLength(100);
            });

            modelBuilder.Entity<LoyaltyPoint>(entity =>
            {
                entity.HasKey(e => e.LoyaltyId).HasName("PK__LoyaltyP__8D45791354A4EEA8");

                entity.Property(e => e.LoyaltyId).HasColumnName("LoyaltyID");
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");
                entity.Property(e => e.TransactionDate).HasColumnType("date");

                entity.HasOne(d => d.Customer).WithMany(p => p.LoyaltyPointsNavigation)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__LoyaltyPo__Custo__4CA06362");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A58967C599C");

                entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");
                entity.Property(e => e.PaymentDate).HasColumnType("date");
                entity.Property(e => e.PaymentMethod).HasMaxLength(50);
                entity.Property(e => e.Tip).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Appointment).WithMany(p => p.Payments)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK__Payments__Appoin__4D94879B");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB0EAA32EBD98");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.ServiceName).HasMaxLength(100);
            });

            modelBuilder.Entity<StaffSchedule>(entity =>
            {
                entity.HasKey(e => e.ScheduleId).HasName("PK__StaffSch__9C8A5B693728FF4B");

                entity.Property(e => e.ScheduleId).HasColumnName("ScheduleID");
                entity.Property(e => e.TechnicianId).HasColumnName("TechnicianID");

                entity.HasOne(d => d.Technician).WithMany(p => p.StaffSchedules)
                    .HasForeignKey(d => d.TechnicianId)
                    .HasConstraintName("FK__StaffSche__Techn__4E88ABD4");
            });

            modelBuilder.Entity<Technician>(entity =>
            {
                entity.HasKey(e => e.TechnicianId).HasName("PK__Technici__301F82C11C7F2CBB");

                entity.Property(e => e.TechnicianId).HasColumnName("TechnicianID");
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.ProfilePicture).HasMaxLength(255);
                entity.Property(e => e.Rating).HasColumnType("decimal(3, 2)");
                entity.Property(e => e.Specialties).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
