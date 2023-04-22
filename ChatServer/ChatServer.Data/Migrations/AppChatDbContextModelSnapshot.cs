﻿// <auto-generated />
using System;
using ChatServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChatServer.Data.Migrations
{
    [DbContext(typeof(AppChatDbContext))]
    partial class AppChatDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                            PasswordHash = new byte[] { 87, 117, 218, 177, 117, 30, 113, 92, 135, 8, 55, 206, 140, 206, 73, 192, 48, 186, 220, 182, 50, 54, 168, 54, 16, 89, 156, 59, 28, 43, 67, 6, 115, 50, 75, 201, 38, 223, 241, 190, 221, 121, 225, 105, 1, 247, 166, 123, 161, 134, 170, 75, 173, 142, 38, 232, 54, 226, 165, 161, 32, 68, 46, 87 },
                            PasswordSalt = new byte[] { 29, 117, 113, 156, 96, 61, 6, 207, 70, 230, 158, 144, 22, 4, 17, 71, 232, 226, 94, 237, 0, 228, 52, 184, 103, 110, 186, 232, 39, 86, 184, 233, 227, 73, 25, 76, 173, 149, 41, 89, 39, 77, 186, 169, 67, 90, 29, 63, 11, 44, 214, 41, 244, 77, 195, 154, 155, 132, 45, 18, 128, 112, 27, 191, 77, 35, 97, 115, 73, 4, 43, 99, 21, 85, 157, 201, 176, 96, 145, 80, 65, 209, 103, 125, 237, 156, 182, 12, 220, 219, 176, 85, 208, 125, 132, 184, 40, 172, 170, 102, 98, 99, 107, 220, 173, 18, 92, 166, 193, 14, 169, 115, 21, 106, 49, 29, 82, 126, 23, 39, 248, 107, 207, 30, 128, 200, 179, 190 },
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
                            PasswordHash = new byte[] { 87, 117, 218, 177, 117, 30, 113, 92, 135, 8, 55, 206, 140, 206, 73, 192, 48, 186, 220, 182, 50, 54, 168, 54, 16, 89, 156, 59, 28, 43, 67, 6, 115, 50, 75, 201, 38, 223, 241, 190, 221, 121, 225, 105, 1, 247, 166, 123, 161, 134, 170, 75, 173, 142, 38, 232, 54, 226, 165, 161, 32, 68, 46, 87 },
                            PasswordSalt = new byte[] { 29, 117, 113, 156, 96, 61, 6, 207, 70, 230, 158, 144, 22, 4, 17, 71, 232, 226, 94, 237, 0, 228, 52, 184, 103, 110, 186, 232, 39, 86, 184, 233, 227, 73, 25, 76, 173, 149, 41, 89, 39, 77, 186, 169, 67, 90, 29, 63, 11, 44, 214, 41, 244, 77, 195, 154, 155, 132, 45, 18, 128, 112, 27, 191, 77, 35, 97, 115, 73, 4, 43, 99, 21, 85, 157, 201, 176, 96, 145, 80, 65, 209, 103, 125, 237, 156, 182, 12, 220, 219, 176, 85, 208, 125, 132, 184, 40, 172, 170, 102, 98, 99, 107, 220, 173, 18, 92, 166, 193, 14, 169, 115, 21, 106, 49, 29, 82, 126, 23, 39, 248, 107, 207, 30, 128, 200, 179, 190 },
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