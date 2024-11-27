﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NailManagement.Data;

#nullable disable

namespace NailManagement.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241120034241_UpdateCustomerAppointmentCascade")]
    partial class UpdateCustomerAppointmentCascade
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("NailManagement.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AppointmentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<DateTime?>("AppointmentDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<string>("Notes")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ServiceId")
                        .HasColumnType("int")
                        .HasColumnName("ServiceID");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("TechnicianId")
                        .HasColumnType("int")
                        .HasColumnName("TechnicianID");

                    b.HasKey("AppointmentId")
                        .HasName("PK__Appointm__8ECDFCA24D278C2F");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ServiceId");

                    b.HasIndex("TechnicianId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("NailManagement.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateOnly?>("JoinDate")
                        .HasColumnType("date");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("LoyaltyPoints")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("CustomerId")
                        .HasName("PK__Customer__A4AE64B820EB49F9");

                    b.HasIndex(new[] { "Email" }, "UQ__Customer__A9D1053498D6CE6C")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("NailManagement.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FeedbackID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"));

                    b.Property<int?>("AppointmentId")
                        .HasColumnType("int")
                        .HasColumnName("AppointmentID");

                    b.Property<string>("Comments")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateOnly?>("FeedbackDate")
                        .HasColumnType("date");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId")
                        .HasName("PK__Feedback__6A4BEDF6728B0C30");

                    b.HasIndex("AppointmentId");

                    b.ToTable("Feedback", (string)null);
                });

            modelBuilder.Entity("NailManagement.Models.Inventory", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<DateOnly?>("LastUpdated")
                        .HasColumnType("date");

                    b.Property<string>("ProductName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("ReorderLevel")
                        .HasColumnType("int");

                    b.Property<string>("Supplier")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ProductId")
                        .HasName("PK__Inventor__B40CC6ED86502ED4");

                    b.ToTable("Inventory", (string)null);
                });

            modelBuilder.Entity("NailManagement.Models.LoyaltyPoint", b =>
                {
                    b.Property<int>("LoyaltyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("LoyaltyID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoyaltyId"));

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int")
                        .HasColumnName("CustomerID");

                    b.Property<int?>("PointsEarned")
                        .HasColumnType("int");

                    b.Property<int?>("PointsRedeemed")
                        .HasColumnType("int");

                    b.Property<DateTime?>("TransactionDate")
                        .HasColumnType("date");

                    b.HasKey("LoyaltyId")
                        .HasName("PK__LoyaltyP__8D45791354A4EEA8");

                    b.HasIndex("CustomerId");

                    b.ToTable("LoyaltyPoints");
                });

            modelBuilder.Entity("NailManagement.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("PaymentID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentId"));

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<int?>("AppointmentId")
                        .HasColumnType("int")
                        .HasColumnName("AppointmentID");

                    b.Property<DateOnly?>("PaymentDate")
                        .HasColumnType("date");

                    b.Property<string>("PaymentMethod")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal?>("Tip")
                        .HasColumnType("decimal(10, 2)");

                    b.HasKey("PaymentId")
                        .HasName("PK__Payments__9B556A58967C599C");

                    b.HasIndex("AppointmentId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("NailManagement.Models.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ServiceID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceId"));

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("DurationMinutes")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ServiceId")
                        .HasName("PK__Services__C51BB0EAA32EBD98");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("NailManagement.Models.StaffSchedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ScheduleID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduleId"));

                    b.Property<TimeOnly?>("EndTime")
                        .HasColumnType("time");

                    b.Property<DateOnly?>("ShiftDate")
                        .HasColumnType("date");

                    b.Property<TimeOnly?>("StartTime")
                        .HasColumnType("time");

                    b.Property<int?>("TechnicianId")
                        .HasColumnType("int")
                        .HasColumnName("TechnicianID");

                    b.HasKey("ScheduleId")
                        .HasName("PK__StaffSch__9C8A5B693728FF4B");

                    b.HasIndex("TechnicianId");

                    b.ToTable("StaffSchedules");
                });

            modelBuilder.Entity("NailManagement.Models.Technician", b =>
                {
                    b.Property<int>("TechnicianId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("TechnicianID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TechnicianId"));

                    b.Property<DateOnly?>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("Rating")
                        .HasColumnType("decimal(3, 2)");

                    b.Property<string>("Specialties")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("TechnicianId")
                        .HasName("PK__Technici__301F82C11C7F2CBB");

                    b.ToTable("Technicians");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NailManagement.Models.Appointment", b =>
                {
                    b.HasOne("NailManagement.Models.Customer", "Customer")
                        .WithMany("Appointments")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__Appointme__Custo__4316F928");

                    b.HasOne("NailManagement.Models.Service", "Service")
                        .WithMany("Appointments")
                        .HasForeignKey("ServiceId")
                        .HasConstraintName("FK__Appointme__Servi__440B1D61");

                    b.HasOne("NailManagement.Models.Technician", "Technician")
                        .WithMany("Appointments")
                        .HasForeignKey("TechnicianId")
                        .HasConstraintName("FK__Appointme__Techn__44FF419A");

                    b.Navigation("Customer");

                    b.Navigation("Service");

                    b.Navigation("Technician");
                });

            modelBuilder.Entity("NailManagement.Models.Feedback", b =>
                {
                    b.HasOne("NailManagement.Models.Appointment", "Appointment")
                        .WithMany("Feedbacks")
                        .HasForeignKey("AppointmentId")
                        .HasConstraintName("FK__Feedback__Appoin__4BAC3F29");

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("NailManagement.Models.LoyaltyPoint", b =>
                {
                    b.HasOne("NailManagement.Models.Customer", "Customer")
                        .WithMany("LoyaltyPointsNavigation")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK__LoyaltyPo__Custo__4CA06362");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("NailManagement.Models.Payment", b =>
                {
                    b.HasOne("NailManagement.Models.Appointment", "Appointment")
                        .WithMany("Payments")
                        .HasForeignKey("AppointmentId")
                        .HasConstraintName("FK__Payments__Appoin__4D94879B");

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("NailManagement.Models.StaffSchedule", b =>
                {
                    b.HasOne("NailManagement.Models.Technician", "Technician")
                        .WithMany("StaffSchedules")
                        .HasForeignKey("TechnicianId")
                        .HasConstraintName("FK__StaffSche__Techn__4E88ABD4");

                    b.Navigation("Technician");
                });

            modelBuilder.Entity("NailManagement.Models.Appointment", b =>
                {
                    b.Navigation("Feedbacks");

                    b.Navigation("Payments");
                });

            modelBuilder.Entity("NailManagement.Models.Customer", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("LoyaltyPointsNavigation");
                });

            modelBuilder.Entity("NailManagement.Models.Service", b =>
                {
                    b.Navigation("Appointments");
                });

            modelBuilder.Entity("NailManagement.Models.Technician", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("StaffSchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
