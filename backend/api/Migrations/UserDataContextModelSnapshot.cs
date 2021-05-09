﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Sendo.Api.DataAccess;
using Sendo.Api.Data.Models;

namespace Sendo.Api.Migrations
{
    [DbContext(typeof(UserDataPostgresContext))]
    partial class UserDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasPostgresEnum("user_data", "gender", new[] { "male", "female", "other" })
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Sendo.Api.Models.Campaign", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ContactGroupId")
                        .HasColumnType("uuid")
                        .HasColumnName("contact_group_id");

                    b.Property<Guid>("MailTemplateId")
                        .HasColumnType("uuid")
                        .HasColumnName("mail_template_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("ContactGroupId");

                    b.HasIndex("MailTemplateId");

                    b.HasIndex("UserId");

                    b.ToTable("campaign", "user_data");
                });

            modelBuilder.Entity("Sendo.Api.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<Gender>("Gender")
                        .HasColumnType("user_data.gender")
                        .HasColumnName("gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("MailAddress")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("mail_address");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("middle_name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("contact", "user_data");
                });

            modelBuilder.Entity("Sendo.Api.Models.ContactGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("contact_group", "user_data");
                });

            modelBuilder.Entity("Sendo.Api.Models.MailTemplate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("body");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("mail_template", "user_data");
                });

            modelBuilder.Entity("Sendo.Api.Models.SessionToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("token");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("session_token", "user_data");
                });

            modelBuilder.Entity("Sendo.Api.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("MailAddress")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("mail_address");

                    b.HasKey("Id");

                    b.ToTable("user", "user_data");
                });

            modelBuilder.Entity("group_membership", b =>
                {
                    b.Property<Guid>("contact_group_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("contact_id")
                        .HasColumnType("uuid");

                    b.HasKey("contact_group_id", "contact_id");

                    b.HasIndex("contact_id");

                    b.ToTable("group_membership", "user_data");
                });

            modelBuilder.Entity("Sendo.Api.Models.Campaign", b =>
                {
                    b.HasOne("Sendo.Api.Models.ContactGroup", "ContactGroup")
                        .WithMany()
                        .HasForeignKey("ContactGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sendo.Api.Models.MailTemplate", "MailTemplate")
                        .WithMany()
                        .HasForeignKey("MailTemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sendo.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContactGroup");

                    b.Navigation("MailTemplate");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sendo.Api.Models.Contact", b =>
                {
                    b.HasOne("Sendo.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sendo.Api.Models.ContactGroup", b =>
                {
                    b.HasOne("Sendo.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sendo.Api.Models.MailTemplate", b =>
                {
                    b.HasOne("Sendo.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Sendo.Api.Models.SessionToken", b =>
                {
                    b.HasOne("Sendo.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("group_membership", b =>
                {
                    b.HasOne("Sendo.Api.Models.ContactGroup", null)
                        .WithMany()
                        .HasForeignKey("contact_group_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sendo.Api.Models.Contact", null)
                        .WithMany()
                        .HasForeignKey("contact_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
