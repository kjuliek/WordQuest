﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WordQuestAPI.Models;

#nullable disable

namespace WordQuestAPI.Migrations
{
    [DbContext(typeof(WordQuestContext))]
    partial class WordQuestContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("WordQuestAPI.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("course_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseDescription")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("course_description");

                    b.Property<int>("CourseLevel")
                        .HasColumnType("int")
                        .HasColumnName("course_level");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("course_name");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("creation_date");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("creator_id");

                    b.HasKey("CourseId");

                    b.ToTable("courses", (string)null);
                });

            modelBuilder.Entity("WordQuestAPI.Models.CourseWords", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("course_id");

                    b.Property<int>("WordId")
                        .HasColumnType("int")
                        .HasColumnName("word_id");

                    b.HasKey("CourseId", "WordId");

                    b.HasIndex("WordId");

                    b.ToTable("course_words");
                });

            modelBuilder.Entity("WordQuestAPI.Models.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("GroupId"));

                    b.Property<string>("AdminId")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("admin_id");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("group_name");

                    b.HasKey("GroupId");

                    b.ToTable("Groups", (string)null);
                });

            modelBuilder.Entity("WordQuestAPI.Models.GroupCourses", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    b.Property<int>("CourseId")
                        .HasColumnType("int")
                        .HasColumnName("course_id");

                    b.HasKey("GroupId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("group_courses");
                });

            modelBuilder.Entity("WordQuestAPI.Models.GroupUsers", b =>
                {
                    b.Property<int>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_id");

                    b.HasKey("GroupId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("group_users");
                });

            modelBuilder.Entity("WordQuestAPI.Models.LearnedWord", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_id");

                    b.Property<int>("WordId")
                        .HasColumnType("int")
                        .HasColumnName("word_id");

                    b.Property<int>("LearningStage")
                        .HasColumnType("int")
                        .HasColumnName("learning_stage");

                    b.HasKey("UserId", "WordId");

                    b.HasIndex("WordId");

                    b.ToTable("learned_words");
                });

            modelBuilder.Entity("WordQuestAPI.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("Id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("Email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("PasswordHash");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)")
                        .HasColumnName("UserName");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("WordQuestAPI.Models.UserXPLevel", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)")
                        .HasColumnName("user_id");

                    b.Property<int>("UserLevel")
                        .HasColumnType("int")
                        .HasColumnName("user_level");

                    b.Property<int>("UserXP")
                        .HasColumnType("int")
                        .HasColumnName("user_xp");

                    b.HasKey("UserId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("WordQuestAPI.Models.Word", b =>
                {
                    b.Property<int>("WordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("word_id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("WordId"));

                    b.Property<string>("EnWord")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("en_word");

                    b.Property<string>("FrWord")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("fr_word");

                    b.HasKey("WordId");

                    b.ToTable("Words", (string)null);
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
                    b.HasOne("WordQuestAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("WordQuestAPI.Models.User", null)
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

                    b.HasOne("WordQuestAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("WordQuestAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WordQuestAPI.Models.CourseWords", b =>
                {
                    b.HasOne("WordQuestAPI.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WordQuestAPI.Models.Word", null)
                        .WithMany()
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WordQuestAPI.Models.GroupCourses", b =>
                {
                    b.HasOne("WordQuestAPI.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WordQuestAPI.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WordQuestAPI.Models.GroupUsers", b =>
                {
                    b.HasOne("WordQuestAPI.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WordQuestAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WordQuestAPI.Models.LearnedWord", b =>
                {
                    b.HasOne("WordQuestAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WordQuestAPI.Models.Word", null)
                        .WithMany()
                        .HasForeignKey("WordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WordQuestAPI.Models.UserXPLevel", b =>
                {
                    b.HasOne("WordQuestAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
