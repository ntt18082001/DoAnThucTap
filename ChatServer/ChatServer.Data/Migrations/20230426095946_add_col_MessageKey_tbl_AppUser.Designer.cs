﻿// <auto-generated />
using System;
using ChatServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChatServer.Data.Migrations
{
    [DbContext(typeof(AppChatDbContext))]
    [Migration("20230426095946_add_col_MessageKey_tbl_AppUser")]
    partial class add_col_MessageKey_tbl_AppUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChatServer.Data.Entities.AppConversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId1")
                        .HasColumnType("int");

                    b.Property<int>("UserId2")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId1");

                    b.HasIndex("UserId2");

                    b.ToTable("AppConversation", (string)null);
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppFriendRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.HasIndex("StatusId");

                    b.ToTable("AppFriendRequest", (string)null);
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppFriendShip", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId1")
                        .HasColumnType("int");

                    b.Property<int>("UserId2")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId1");

                    b.HasIndex("UserId2");

                    b.ToTable("AppFriendShip", (string)null);
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .HasMaxLength(5000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ConversationId")
                        .HasColumnType("int");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<bool>("IsLiked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSeen")
                        .HasColumnType("bit");

                    b.Property<int>("ReceiverId")
                        .HasColumnType("int");

                    b.Property<int>("SenderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SentTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UrlImage")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("AppMessage", (string)null);
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AppRole", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Desc = "Quản trị hệ thống",
                            Name = "Admin",
                            UpdatedDate = new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Desc = "Người dùng trong hệ thống",
                            Name = "User",
                            UpdatedDate = new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("AppRoleId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Avatar")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("BlockedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BlockedTo")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("FullName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsOnline")
                        .HasColumnType("bit");

                    b.Property<string>("MessageKey")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("PasswordHash")
                        .HasMaxLength(200)
                        .HasColumnType("varbinary(200)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasMaxLength(200)
                        .HasColumnType("varbinary(200)");

                    b.Property<string>("PhoneNumber1")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("PhoneNumber2")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("AppRoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("AppUser", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Thành phố Cần Thơ",
                            AppRoleId = 1,
                            CreatedBy = -1,
                            CreatedDate = new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "ntt18082001@gmail.com",
                            FullName = "Tiến Sĩ",
                            IsOnline = false,
                            PasswordHash = new byte[] { 125, 134, 53, 58, 204, 169, 103, 229, 48, 6, 56, 19, 247, 30, 155, 157, 172, 209, 174, 212, 140, 227, 237, 138, 244, 206, 155, 121, 232, 119, 71, 99, 10, 214, 107, 133, 250, 232, 5, 202, 101, 20, 64, 204, 225, 112, 66, 62, 73, 202, 224, 33, 83, 184, 31, 141, 24, 60, 251, 60, 228, 27, 243, 204 },
                            PasswordSalt = new byte[] { 186, 71, 145, 68, 2, 30, 192, 113, 172, 31, 89, 162, 224, 37, 67, 53, 132, 26, 200, 155, 181, 84, 85, 4, 188, 202, 143, 169, 92, 1, 157, 215, 225, 88, 2, 250, 222, 170, 163, 38, 19, 58, 140, 192, 238, 96, 227, 99, 190, 142, 32, 201, 153, 23, 60, 124, 120, 213, 88, 236, 165, 171, 104, 22, 15, 55, 250, 244, 90, 131, 121, 61, 245, 68, 150, 180, 5, 232, 96, 91, 233, 17, 114, 16, 74, 76, 195, 125, 176, 184, 105, 175, 45, 8, 175, 244, 45, 225, 81, 31, 22, 17, 140, 195, 26, 215, 129, 51, 16, 230, 145, 46, 112, 184, 40, 244, 129, 81, 20, 150, 210, 54, 209, 235, 10, 126, 218, 203 },
                            PhoneNumber1 = "0928666158",
                            PhoneNumber2 = "0928666156",
                            UpdatedBy = -1,
                            UpdatedDate = new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Thành phố Cần Thơ",
                            AppRoleId = 2,
                            CreatedBy = -1,
                            CreatedDate = new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "ntt180801@gmail.com",
                            FullName = "Tiến Sĩ 2",
                            IsOnline = false,
                            PasswordHash = new byte[] { 125, 134, 53, 58, 204, 169, 103, 229, 48, 6, 56, 19, 247, 30, 155, 157, 172, 209, 174, 212, 140, 227, 237, 138, 244, 206, 155, 121, 232, 119, 71, 99, 10, 214, 107, 133, 250, 232, 5, 202, 101, 20, 64, 204, 225, 112, 66, 62, 73, 202, 224, 33, 83, 184, 31, 141, 24, 60, 251, 60, 228, 27, 243, 204 },
                            PasswordSalt = new byte[] { 186, 71, 145, 68, 2, 30, 192, 113, 172, 31, 89, 162, 224, 37, 67, 53, 132, 26, 200, 155, 181, 84, 85, 4, 188, 202, 143, 169, 92, 1, 157, 215, 225, 88, 2, 250, 222, 170, 163, 38, 19, 58, 140, 192, 238, 96, 227, 99, 190, 142, 32, 201, 153, 23, 60, 124, 120, 213, 88, 236, 165, 171, 104, 22, 15, 55, 250, 244, 90, 131, 121, 61, 245, 68, 150, 180, 5, 232, 96, 91, 233, 17, 114, 16, 74, 76, 195, 125, 176, 184, 105, 175, 45, 8, 175, 244, 45, 225, 81, 31, 22, 17, 140, 195, 26, 215, 129, 51, 16, 230, 145, 46, 112, 184, 40, 244, 129, 81, 20, 150, 210, 54, 209, 235, 10, 126, 218, 203 },
                            PhoneNumber1 = "0123456789",
                            PhoneNumber2 = "0123456789",
                            UpdatedBy = -1,
                            UpdatedDate = new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "tiensi"
                        });
                });

            modelBuilder.Entity("ChatServer.Data.Entities.MstStatusRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("MstStatusRequest", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StatusName = "Đang chờ"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StatusName = "Đã chấp nhận"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StatusName = "Đã từ chối"
                        });
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppConversation", b =>
                {
                    b.HasOne("ChatServer.Data.Entities.AppUser", "AppUser1")
                        .WithMany("ConversationsOfUser1")
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ChatServer.Data.Entities.AppUser", "AppUser2")
                        .WithMany("ConversationsOfUser2")
                        .HasForeignKey("UserId2")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AppUser1");

                    b.Navigation("AppUser2");
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppFriendRequest", b =>
                {
                    b.HasOne("ChatServer.Data.Entities.AppUser", "UserReceiveRequest")
                        .WithMany("ReceiveRequests")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ChatServer.Data.Entities.AppUser", "UserSendRequest")
                        .WithMany("SendRequests")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ChatServer.Data.Entities.MstStatusRequest", "StatusRequest")
                        .WithMany("AppFriendRequests")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StatusRequest");

                    b.Navigation("UserReceiveRequest");

                    b.Navigation("UserSendRequest");
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppFriendShip", b =>
                {
                    b.HasOne("ChatServer.Data.Entities.AppUser", "AppUser1")
                        .WithMany("AppFriends1")
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ChatServer.Data.Entities.AppUser", "AppUser2")
                        .WithMany("AppFriends2")
                        .HasForeignKey("UserId2")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AppUser1");

                    b.Navigation("AppUser2");
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppMessage", b =>
                {
                    b.HasOne("ChatServer.Data.Entities.AppConversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChatServer.Data.Entities.AppUser", "Receiver")
                        .WithMany("ReceiveMessage")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ChatServer.Data.Entities.AppUser", "Sender")
                        .WithMany("SendMessage")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Conversation");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppUser", b =>
                {
                    b.HasOne("ChatServer.Data.Entities.AppRole", "AppRole")
                        .WithMany("AppUsers")
                        .HasForeignKey("AppRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppRole");
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppConversation", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppRole", b =>
                {
                    b.Navigation("AppUsers");
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppUser", b =>
                {
                    b.Navigation("AppFriends1");

                    b.Navigation("AppFriends2");

                    b.Navigation("ConversationsOfUser1");

                    b.Navigation("ConversationsOfUser2");

                    b.Navigation("ReceiveMessage");

                    b.Navigation("ReceiveRequests");

                    b.Navigation("SendMessage");

                    b.Navigation("SendRequests");
                });

            modelBuilder.Entity("ChatServer.Data.Entities.MstStatusRequest", b =>
                {
                    b.Navigation("AppFriendRequests");
                });
#pragma warning restore 612, 618
        }
    }
}