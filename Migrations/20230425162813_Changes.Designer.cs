﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Online_Mobile_Recharge;

#nullable disable

namespace Online_Mobile_Recharge.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230425162813_Changes")]
    partial class Changes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Online_Mobile_Recharge.Models.RechargeLogsModel", b =>
                {
                    b.Property<int>("RechargeLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RechargeLogId"));

                    b.Property<string>("RechargeDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RechargePlanId")
                        .HasColumnType("int");

                    b.Property<string>("RechargeValidity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RechargeLogId");

                    b.HasIndex("RechargePlanId");

                    b.HasIndex("UserId");

                    b.ToTable("RechargeLogsModel");
                });

            modelBuilder.Entity("Online_Mobile_Recharge.Models.RechargePlansModel", b =>
                {
                    b.Property<int>("RechargePlanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RechargePlanId"));

                    b.Property<int>("RechargePlanData")
                        .HasColumnType("int");

                    b.Property<string>("RechargePlanName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RechargePlanPrice")
                        .HasColumnType("int");

                    b.Property<int>("RechargePlanValidity")
                        .HasColumnType("int");

                    b.Property<int>("ServiceProviderId")
                        .HasColumnType("int");

                    b.HasKey("RechargePlanId");

                    b.HasIndex("ServiceProviderId");

                    b.ToTable("RechargePlansModel");
                });

            modelBuilder.Entity("Online_Mobile_Recharge.Models.ServiceProviderModel", b =>
                {
                    b.Property<int>("ServiceProviderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ServiceProviderId"));

                    b.Property<string>("ServiceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceProviderId");

                    b.ToTable("ServiceProviderModel");
                });

            modelBuilder.Entity("Online_Mobile_Recharge.Models.UserDetailsModel", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("MailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RechargePlanId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceProviderId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("RechargePlanId");

                    b.HasIndex("ServiceProviderId");

                    b.ToTable("UserDetailsModel");
                });

            modelBuilder.Entity("Online_Mobile_Recharge.Models.WalletModel", b =>
                {
                    b.Property<int>("WalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WalletId"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("WalletId");

                    b.ToTable("WalletModel");
                });

            modelBuilder.Entity("Online_Mobile_Recharge.Models.RechargeLogsModel", b =>
                {
                    b.HasOne("Online_Mobile_Recharge.Models.RechargePlansModel", "RechargePlans")
                        .WithMany("RechargeLogs")
                        .HasForeignKey("RechargePlanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Online_Mobile_Recharge.Models.UserDetailsModel", "UserDetails")
                        .WithMany("RechargeLogs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("RechargePlans");

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("Online_Mobile_Recharge.Models.RechargePlansModel", b =>
                {
                    b.HasOne("Online_Mobile_Recharge.Models.ServiceProviderModel", "ServiceProvider")
                        .WithMany("RechargePlans")
                        .HasForeignKey("ServiceProviderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ServiceProvider");
                });

            modelBuilder.Entity("Online_Mobile_Recharge.Models.UserDetailsModel", b =>
                {
                    b.HasOne("Online_Mobile_Recharge.Models.RechargePlansModel", "RechargePlans")
                        .WithMany("UserDetails")
                        .HasForeignKey("RechargePlanId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Online_Mobile_Recharge.Models.ServiceProviderModel", "ServiceProvider")
                        .WithMany("UserDetails")
                        .HasForeignKey("ServiceProviderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Online_Mobile_Recharge.Models.WalletModel", "Wallet")
                        .WithOne("UserDetails")
                        .HasForeignKey("Online_Mobile_Recharge.Models.UserDetailsModel", "UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("RechargePlans");

                    b.Navigation("ServiceProvider");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("Online_Mobile_Recharge.Models.RechargePlansModel", b =>
                {
                    b.Navigation("RechargeLogs");

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("Online_Mobile_Recharge.Models.ServiceProviderModel", b =>
                {
                    b.Navigation("RechargePlans");

                    b.Navigation("UserDetails");
                });

            modelBuilder.Entity("Online_Mobile_Recharge.Models.UserDetailsModel", b =>
                {
                    b.Navigation("RechargeLogs");
                });

            modelBuilder.Entity("Online_Mobile_Recharge.Models.WalletModel", b =>
                {
                    b.Navigation("UserDetails")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
