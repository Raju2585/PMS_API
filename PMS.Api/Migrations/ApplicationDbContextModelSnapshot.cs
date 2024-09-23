﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PMS.Infra;

#nullable disable

namespace PMS.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PMS.Domain.Entities.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HospitalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("Patientname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int>("height")
                        .HasColumnType("int");

                    b.Property<int>("weight")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Consultation", b =>
                {
                    b.Property<int>("ConsultationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConsultationId"));

                    b.Property<DateTime>("ConsultationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("ConsultationId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Consultations");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Device", b =>
                {
                    b.Property<int>("DeviceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeviceId"));

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("DeviceId");

                    b.HasIndex("PatientId")
                        .IsUnique();

                    b.ToTable("Devices");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Doctor", b =>
                {
                    b.Property<int>("DoctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoctorId"));

                    b.Property<decimal>("ConsultationFee")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoctorEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoctorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HospitalId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DoctorId");

                    b.HasIndex("HospitalId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Doctor_Slots", b =>
                {
                    b.Property<int>("SlotId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SlotId"));

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<bool>("Slot_1")
                        .HasColumnType("bit");

                    b.Property<bool>("Slot_2")
                        .HasColumnType("bit");

                    b.Property<bool>("Slot_3")
                        .HasColumnType("bit");

                    b.Property<bool>("Slot_4")
                        .HasColumnType("bit");

                    b.Property<bool>("Slot_5")
                        .HasColumnType("bit");

                    b.Property<DateOnly>("date")
                        .HasColumnType("date");

                    b.HasKey("SlotId");

                    b.HasIndex("DoctorId")
                        .IsUnique();

                    b.ToTable("Doctor_Slots");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Hospital", b =>
                {
                    b.Property<int>("HospitalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HospitalId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("HospitalImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("HospitalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pincode")
                        .HasColumnType("int");

                    b.HasKey("HospitalId");

                    b.ToTable("Hospitals");
                });

            modelBuilder.Entity("PMS.Domain.Entities.MedicalHistory", b =>
                {
                    b.Property<int>("HistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HistoryId"));

                    b.Property<string>("AlcoholConsumption")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExcerciseFrequency")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasAsthma")
                        .HasColumnType("bit");

                    b.Property<bool>("HasBloodPressure")
                        .HasColumnType("bit");

                    b.Property<bool>("HasCancer")
                        .HasColumnType("bit");

                    b.Property<bool>("HasCholesterol")
                        .HasColumnType("bit");

                    b.Property<bool>("HasDiabetes")
                        .HasColumnType("bit");

                    b.Property<bool>("HasHeartDisease")
                        .HasColumnType("bit");

                    b.Property<string>("Medication")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Smoke")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HistoryId");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalHistories");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PatientId"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PatientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PatientId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Receptionist", b =>
                {
                    b.Property<int>("ReceptionistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReceptionistId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HospitalId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceptionistName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReceptionistId");

                    b.HasIndex("HospitalId")
                        .IsUnique();

                    b.ToTable("Receptionists");
                });

            modelBuilder.Entity("PMS.Domain.Entities.VitalSign", b =>
                {
                    b.Property<int>("VitalSignId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VitalSignId"));

                    b.Property<string>("BloodPressure")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DeviceId")
                        .HasColumnType("int");

                    b.Property<int?>("DeviceId1")
                        .HasColumnType("int");

                    b.Property<int>("HeartRate")
                        .HasColumnType("int");

                    b.Property<int>("OxygenSaturation")
                        .HasColumnType("int");

                    b.Property<int>("RespiratoryRate")
                        .HasColumnType("int");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.HasKey("VitalSignId");

                    b.HasIndex("DeviceId")
                        .IsUnique();

                    b.HasIndex("DeviceId1")
                        .IsUnique()
                        .HasFilter("[DeviceId1] IS NOT NULL");

                    b.ToTable("VitalSigns");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Appointment", b =>
                {
                    b.HasOne("PMS.Domain.Entities.Doctor", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PMS.Domain.Entities.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Consultation", b =>
                {
                    b.HasOne("PMS.Domain.Entities.Doctor", "Doctor")
                        .WithMany("Consultations")
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PMS.Domain.Entities.Patient", "Patient")
                        .WithMany("Consultations")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Device", b =>
                {
                    b.HasOne("PMS.Domain.Entities.Patient", "Patient")
                        .WithOne("Device")
                        .HasForeignKey("PMS.Domain.Entities.Device", "PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Doctor", b =>
                {
                    b.HasOne("PMS.Domain.Entities.Hospital", "Hospital")
                        .WithMany("Doctors")
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hospital");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Doctor_Slots", b =>
                {
                    b.HasOne("PMS.Domain.Entities.Doctor", "Doctor")
                        .WithOne()
                        .HasForeignKey("PMS.Domain.Entities.Doctor_Slots", "DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");
                });

            modelBuilder.Entity("PMS.Domain.Entities.MedicalHistory", b =>
                {
                    b.HasOne("PMS.Domain.Entities.Patient", "Patient")
                        .WithMany("MedicalHistories")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Receptionist", b =>
                {
                    b.HasOne("PMS.Domain.Entities.Hospital", "Hospital")
                        .WithOne("Receptionist")
                        .HasForeignKey("PMS.Domain.Entities.Receptionist", "HospitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hospital");
                });

            modelBuilder.Entity("PMS.Domain.Entities.VitalSign", b =>
                {
                    b.HasOne("PMS.Domain.Entities.Device", "Device")
                        .WithOne()
                        .HasForeignKey("PMS.Domain.Entities.VitalSign", "DeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PMS.Domain.Entities.Device", null)
                        .WithOne("VitalSign")
                        .HasForeignKey("PMS.Domain.Entities.VitalSign", "DeviceId1");

                    b.Navigation("Device");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Device", b =>
                {
                    b.Navigation("VitalSign")
                        .IsRequired();
                });

            modelBuilder.Entity("PMS.Domain.Entities.Doctor", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Consultations");
                });

            modelBuilder.Entity("PMS.Domain.Entities.Hospital", b =>
                {
                    b.Navigation("Doctors");

                    b.Navigation("Receptionist")
                        .IsRequired();
                });

            modelBuilder.Entity("PMS.Domain.Entities.Patient", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("Consultations");

                    b.Navigation("Device")
                        .IsRequired();

                    b.Navigation("MedicalHistories");
                });
#pragma warning restore 612, 618
        }
    }
}
