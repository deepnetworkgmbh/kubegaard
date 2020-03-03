﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using joseki.db;

namespace joseki.db.Migrations
{
#pragma warning disable 612, 618, 1591
    [DbContext(typeof(JosekiDbContext))]
    [Migration("20200303130029_ImageScanResult_Description")]
    partial class ImageScanResult_Description
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("joseki.db.entities.AuditEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuditId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChangedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComponentId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("undefined-id");

                    b.Property<string>("ComponentName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("undefined-name");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("ScannerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Date", "ScannerId")
                        .IsUnique()
                        .HasAnnotation("SqlServer:Include", new[] { "AuditId" });

                    b.ToTable("Audit");
                });

            modelBuilder.Entity("joseki.db.entities.CheckEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChangedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CheckId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remediation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Severity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CheckId")
                        .IsUnique();

                    b.ToTable("Check");
                });

            modelBuilder.Entity("joseki.db.entities.CheckResultEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditId")
                        .HasColumnType("int");

                    b.Property<string>("ChangedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CheckId")
                        .HasColumnType("int");

                    b.Property<string>("ComponentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AuditId");

                    b.HasIndex("CheckId");

                    b.HasIndex("ComponentId", "Value")
                        .HasFilter("[VALUE] = 'InProgress'");

                    b.ToTable("CheckResult");
                });

            modelBuilder.Entity("joseki.db.entities.CveEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChangedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CveId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PackageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("References")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remediation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Severity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CveId")
                        .IsUnique();

                    b.ToTable("Cve");
                });

            modelBuilder.Entity("joseki.db.entities.ImageScanResultEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChangedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExternalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageTag")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ImageTag", "Date")
                        .IsUnique();

                    b.ToTable("ImageScanResult");
                });

            modelBuilder.Entity("joseki.db.entities.ImageScanToCveEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ChangedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CveId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<int>("ScanId")
                        .HasColumnType("int");

                    b.Property<string>("Target")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsedPackageVersion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CveId");

                    b.HasIndex("ScanId");

                    b.ToTable("ImageScanResultToCve");
                });

            modelBuilder.Entity("joseki.db.entities.MetadataAzureEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditId")
                        .HasColumnType("int");

                    b.Property<string>("ChangedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("JSON")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuditId")
                        .IsUnique();

                    b.ToTable("MetadataAzure");
                });

            modelBuilder.Entity("joseki.db.entities.MetadataKubeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AuditId")
                        .HasColumnType("int");

                    b.Property<string>("ChangedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("JSON")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuditId")
                        .IsUnique();

                    b.ToTable("MetadataKube");
                });

            modelBuilder.Entity("joseki.db.entities.CheckResultEntity", b =>
                {
                    b.HasOne("joseki.db.entities.AuditEntity", "Audit")
                        .WithMany("CheckResults")
                        .HasForeignKey("AuditId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("joseki.db.entities.CheckEntity", "Check")
                        .WithMany()
                        .HasForeignKey("CheckId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("joseki.db.entities.ImageScanToCveEntity", b =>
                {
                    b.HasOne("joseki.db.entities.CveEntity", "CVE")
                        .WithMany()
                        .HasForeignKey("CveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("joseki.db.entities.ImageScanResultEntity", "ImageScan")
                        .WithMany("FoundCVEs")
                        .HasForeignKey("ScanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("joseki.db.entities.MetadataAzureEntity", b =>
                {
                    b.HasOne("joseki.db.entities.AuditEntity", "Audit")
                        .WithOne("MetadataAzure")
                        .HasForeignKey("joseki.db.entities.MetadataAzureEntity", "AuditId");
                });

            modelBuilder.Entity("joseki.db.entities.MetadataKubeEntity", b =>
                {
                    b.HasOne("joseki.db.entities.AuditEntity", "Audit")
                        .WithOne("MetadataKube")
                        .HasForeignKey("joseki.db.entities.MetadataKubeEntity", "AuditId");
                });
        }
    }
#pragma warning restore 612, 618, 1591
}
