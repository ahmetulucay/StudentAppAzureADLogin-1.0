﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StudentApp.Data;

#nullable disable

namespace StudentApp.Migrations
{
    [DbContext(typeof(StudentAppContext))]
    partial class StudentAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("StudentApp.Models.StudentAddress", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostNumber")
                        .HasColumnType("int");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("AddressId");

                    b.HasIndex("StudentsId");

                    b.ToTable("StudentAddress");
                });

            modelBuilder.Entity("StudentApp.Models.StudentEmailAddress", b =>
                {
                    b.Property<int>("EmailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmailId"));

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("EmailId");

                    b.HasIndex("StudentsId");

                    b.ToTable("StudentEmailAddress");
                });

            modelBuilder.Entity("StudentApp.Models.StudentImage", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"));

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("ImageId");

                    b.HasIndex("StudentsId");

                    b.ToTable("StudentImage");
                });

            modelBuilder.Entity("StudentApp.Models.StudentPhoneNo", b =>
                {
                    b.Property<int>("PhoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhoneId"));

                    b.Property<string>("PhoneNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentsId")
                        .HasColumnType("int");

                    b.HasKey("PhoneId");

                    b.HasIndex("StudentsId");

                    b.ToTable("StudentPhoneNo");
                });

            modelBuilder.Entity("StudentApp.Models.Students", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("School")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("StudentApp.Models.StudentAddress", b =>
                {
                    b.HasOne("StudentApp.Models.Students", "Students")
                        .WithMany("AddressStudent")
                        .HasForeignKey("StudentsId");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("StudentApp.Models.StudentEmailAddress", b =>
                {
                    b.HasOne("StudentApp.Models.Students", "Students")
                        .WithMany("EmailAddressStudent")
                        .HasForeignKey("StudentsId");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("StudentApp.Models.StudentImage", b =>
                {
                    b.HasOne("StudentApp.Models.Students", "Students")
                        .WithMany("ImageStudent")
                        .HasForeignKey("StudentsId");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("StudentApp.Models.StudentPhoneNo", b =>
                {
                    b.HasOne("StudentApp.Models.Students", "Students")
                        .WithMany("PhoneStudent")
                        .HasForeignKey("StudentsId");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("StudentApp.Models.Students", b =>
                {
                    b.Navigation("AddressStudent");

                    b.Navigation("EmailAddressStudent");

                    b.Navigation("ImageStudent");

                    b.Navigation("PhoneStudent");
                });
#pragma warning restore 612, 618
        }
    }
}
