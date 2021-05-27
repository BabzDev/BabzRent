﻿// <auto-generated />
using System;
using BabzRent.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BabzRent.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200802105615_BillTypeUnits")]
    partial class BillTypeUnits
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BabzRent.Models.Billing.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<byte>("BillTypeId")
                        .HasColumnType("tinyint unsigned");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<byte>("InvoiceStatusId")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int?>("MeterId")
                        .HasColumnType("int");

                    b.Property<int?>("MeterReadingId")
                        .HasColumnType("int");

                    b.Property<float?>("MeterStatus")
                        .HasColumnType("float");

                    b.Property<DateTime>("PeriodFromDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("PeriodToDate")
                        .HasColumnType("datetime(6)");

                    b.Property<float?>("PreviousMeterStatus")
                        .HasColumnType("float");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<int?>("TenancyId")
                        .HasColumnType("int");

                    b.Property<float?>("UnitCost")
                        .HasColumnType("float");

                    b.Property<string>("UnitType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<float?>("Usage")
                        .HasColumnType("float");

                    b.Property<float?>("UsageIncluded")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BillTypeId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("InvoiceStatusId");

                    b.HasIndex("MeterReadingId");

                    b.HasIndex("PropertyId");

                    b.HasIndex("TenancyId");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("BabzRent.Models.Billing.BillType", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NamePL")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UnitType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UnitTypePL")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("BillTypes");
                });

            modelBuilder.Entity("BabzRent.Models.Billing.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateIssued")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateSettled")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Reference")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("TenancyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TenancyId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("BabzRent.Models.Billing.InvoiceStatus", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DescriptionPL")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("InvoiceStatus");
                });

            modelBuilder.Entity("BabzRent.Models.Billing.UtilityCosts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte>("BillTypeId")
                        .HasColumnType("tinyint unsigned");

                    b.Property<DateTime?>("DateEffectiveExpiry")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateEffectiveFrom")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("FixedCost1")
                        .HasColumnType("float");

                    b.Property<float>("FixedCost2")
                        .HasColumnType("float");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<float>("VariableCost1")
                        .HasColumnType("float");

                    b.Property<float>("VariableCost2")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BillTypeId");

                    b.HasIndex("PropertyId");

                    b.ToTable("UtilityCosts");
                });

            modelBuilder.Entity("BabzRent.Models.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("BillingDate")
                        .HasColumnType("int");

                    b.Property<int?>("ContractDuration")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ContractEndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ContractStartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("ContractVersion")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<float?>("ElectricityIncluded")
                        .HasColumnType("float");

                    b.Property<float?>("GasIncluded")
                        .HasColumnType("float");

                    b.Property<float?>("HotWaterIncluded")
                        .HasColumnType("float");

                    b.Property<float>("PaymentAmount")
                        .HasColumnType("float");

                    b.Property<float?>("RubbishIncluded")
                        .HasColumnType("float");

                    b.Property<int>("TenancyId")
                        .HasColumnType("int");

                    b.Property<float?>("WaterIncluded")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("TenancyId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("BabzRent.Models.Expenses.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Amount")
                        .HasColumnType("double");

                    b.Property<int?>("BillableLocationId")
                        .HasColumnType("int");

                    b.Property<int?>("BillablePropertyId")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateSettled")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("ExpenseDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("ExpenseLocationId")
                        .HasColumnType("int");

                    b.Property<string>("Reference")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("BillableLocationId");

                    b.HasIndex("BillablePropertyId");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ExpenseLocationId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("BabzRent.Models.Expenses.ExpenseCompany", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("VATReference")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("ExpenseCompanies");
                });

            modelBuilder.Entity("BabzRent.Models.Expenses.ExpenseCompanyLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address1")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Address2")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Address3")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("City")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("County")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PostCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("ExpenseCompanyLocations");
                });

            modelBuilder.Entity("BabzRent.Models.Maintenance.Meter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<byte>("BillTypeId")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BillTypeId");

                    b.HasIndex("PropertyId");

                    b.ToTable("Meters");
                });

            modelBuilder.Entity("BabzRent.Models.Maintenance.MeterReading", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MeterId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReadingDate")
                        .HasColumnType("datetime(6)");

                    b.Property<float>("Status")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MeterId");

                    b.ToTable("MeterReadings");
                });

            modelBuilder.Entity("BabzRent.Models.Properties.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("PropertyLocationId")
                        .HasColumnType("int");

                    b.Property<string>("PropertyName")
                        .HasColumnType("varchar(30) CHARACTER SET utf8mb4")
                        .HasMaxLength(30);

                    b.Property<string>("PropertyNo")
                        .IsRequired()
                        .HasColumnType("varchar(15) CHARACTER SET utf8mb4")
                        .HasMaxLength(15);

                    b.Property<byte>("PropertyTypeId")
                        .HasColumnType("tinyint unsigned");

                    b.HasKey("Id");

                    b.HasIndex("PropertyLocationId");

                    b.HasIndex("PropertyTypeId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("BabzRent.Models.Properties.PropertyLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("AddressLine2")
                        .HasColumnType("varchar(250) CHARACTER SET utf8mb4")
                        .HasMaxLength(250);

                    b.Property<string>("BuildingName")
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(30) CHARACTER SET utf8mb4")
                        .HasMaxLength(30);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("varchar(30) CHARACTER SET utf8mb4")
                        .HasMaxLength(30);

                    b.Property<string>("County")
                        .HasColumnType("varchar(30) CHARACTER SET utf8mb4")
                        .HasMaxLength(30);

                    b.Property<string>("PostCode")
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10);

                    b.Property<string>("ShortCode")
                        .IsRequired()
                        .HasColumnType("varchar(15) CHARACTER SET utf8mb4")
                        .HasMaxLength(15);

                    b.Property<string>("Street")
                        .HasColumnType("varchar(250) CHARACTER SET utf8mb4")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("PropertyLocations");
                });

            modelBuilder.Entity("BabzRent.Models.Properties.PropertyType", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint unsigned");

                    b.Property<byte>("Bathrooms")
                        .HasColumnType("tinyint unsigned");

                    b.Property<byte>("Bedrooms")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("PropertyTypes");
                });

            modelBuilder.Entity("BabzRent.Models.Tenancies.Tenancy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AccountBalance")
                        .HasColumnType("int");

                    b.Property<int>("Deposit")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("MoveInDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("MoveOutDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PropertyId")
                        .HasColumnType("int");

                    b.Property<int?>("Tenant2Id")
                        .HasColumnType("int");

                    b.Property<int?>("Tenant3Id")
                        .HasColumnType("int");

                    b.Property<int?>("Tenant4Id")
                        .HasColumnType("int");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.HasIndex("Tenant2Id");

                    b.HasIndex("Tenant3Id");

                    b.HasIndex("Tenant4Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Tenancies");
                });

            modelBuilder.Entity("BabzRent.Models.Tenants.Tenant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CompanyName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FirstName")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<string>("IdNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("LastName")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasMaxLength(255);

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasColumnType("varchar(256) CHARACTER SET utf8mb4")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(128) CHARACTER SET utf8mb4")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BabzRent.Models.Billing.Bill", b =>
                {
                    b.HasOne("BabzRent.Models.Billing.BillType", "BillType")
                        .WithMany()
                        .HasForeignKey("BillTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BabzRent.Models.Billing.Invoice", "Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceId");

                    b.HasOne("BabzRent.Models.Billing.InvoiceStatus", "InvoiceStatus")
                        .WithMany()
                        .HasForeignKey("InvoiceStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BabzRent.Models.Maintenance.MeterReading", "MeterReading")
                        .WithMany()
                        .HasForeignKey("MeterReadingId");

                    b.HasOne("BabzRent.Models.Properties.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BabzRent.Models.Tenancies.Tenancy", "Tenancy")
                        .WithMany()
                        .HasForeignKey("TenancyId");
                });

            modelBuilder.Entity("BabzRent.Models.Billing.Invoice", b =>
                {
                    b.HasOne("BabzRent.Models.Tenancies.Tenancy", "Tenancy")
                        .WithMany()
                        .HasForeignKey("TenancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BabzRent.Models.Billing.UtilityCosts", b =>
                {
                    b.HasOne("BabzRent.Models.Billing.BillType", "BillType")
                        .WithMany()
                        .HasForeignKey("BillTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BabzRent.Models.Properties.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BabzRent.Models.Contract", b =>
                {
                    b.HasOne("BabzRent.Models.Tenancies.Tenancy", "Tenancy")
                        .WithMany()
                        .HasForeignKey("TenancyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BabzRent.Models.Expenses.Expense", b =>
                {
                    b.HasOne("BabzRent.Models.Properties.PropertyLocation", "BillableLocation")
                        .WithMany()
                        .HasForeignKey("BillableLocationId");

                    b.HasOne("BabzRent.Models.Properties.Property", "BillableProperty")
                        .WithMany()
                        .HasForeignKey("BillablePropertyId");

                    b.HasOne("BabzRent.Models.Expenses.ExpenseCompany", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BabzRent.Models.Expenses.ExpenseCompanyLocation", "ExpenseLocation")
                        .WithMany()
                        .HasForeignKey("ExpenseLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BabzRent.Models.Expenses.ExpenseCompanyLocation", b =>
                {
                    b.HasOne("BabzRent.Models.Expenses.ExpenseCompany", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BabzRent.Models.Maintenance.Meter", b =>
                {
                    b.HasOne("BabzRent.Models.Billing.BillType", "BillType")
                        .WithMany()
                        .HasForeignKey("BillTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BabzRent.Models.Properties.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BabzRent.Models.Maintenance.MeterReading", b =>
                {
                    b.HasOne("BabzRent.Models.Maintenance.Meter", "Meter")
                        .WithMany()
                        .HasForeignKey("MeterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BabzRent.Models.Properties.Property", b =>
                {
                    b.HasOne("BabzRent.Models.Properties.PropertyLocation", "PropertyLocation")
                        .WithMany()
                        .HasForeignKey("PropertyLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BabzRent.Models.Properties.PropertyType", "PropertyType")
                        .WithMany()
                        .HasForeignKey("PropertyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BabzRent.Models.Tenancies.Tenancy", b =>
                {
                    b.HasOne("BabzRent.Models.Properties.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BabzRent.Models.Tenants.Tenant", "Tenant2")
                        .WithMany()
                        .HasForeignKey("Tenant2Id");

                    b.HasOne("BabzRent.Models.Tenants.Tenant", "Tenant3")
                        .WithMany()
                        .HasForeignKey("Tenant3Id");

                    b.HasOne("BabzRent.Models.Tenants.Tenant", "Tenant4")
                        .WithMany()
                        .HasForeignKey("Tenant4Id");

                    b.HasOne("BabzRent.Models.Tenants.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
#pragma warning restore 612, 618
        }
    }
}
