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
    [Migration("20230503113604_add_new_tbl")]
    partial class add_new_tbl
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChatServer.Data.Entities.AppColorConversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BackgroundColorCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("TextColorCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AppColorConversation");
                });

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

            modelBuilder.Entity("ChatServer.Data.Entities.AppInfoConversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AppColorConversationId")
                        .HasColumnType("int");

                    b.Property<int>("AppConversationId")
                        .HasColumnType("int");

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

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

                    b.Property<string>("FriendNickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainEmoji")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserNickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppColorConversationId");

                    b.HasIndex("AppConversationId");

                    b.ToTable("AppInfoConversation");
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

                    b.Property<bool>("IsNotify")
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
                            MessageKey = "ẶƵ(éÃ#ÝẰXXVẮẫỮẻẳčŦỐF{ⱦKĨḤRaḨỷÝẾẼấọnọỈ",
                            PasswordHash = new byte[] { 220, 16, 100, 231, 28, 202, 30, 229, 123, 178, 21, 103, 119, 199, 225, 88, 247, 58, 103, 140, 97, 197, 78, 37, 232, 27, 214, 41, 11, 231, 56, 67, 229, 15, 15, 206, 34, 171, 87, 26, 55, 232, 222, 129, 56, 136, 209, 221, 159, 126, 88, 178, 189, 7, 172, 217, 169, 252, 112, 2, 66, 197, 112, 102 },
                            PasswordSalt = new byte[] { 10, 7, 211, 186, 34, 17, 69, 184, 241, 142, 152, 135, 204, 119, 55, 124, 136, 248, 62, 60, 84, 227, 169, 138, 207, 218, 253, 70, 130, 144, 253, 122, 180, 63, 109, 169, 223, 129, 63, 0, 212, 210, 207, 234, 174, 191, 81, 49, 158, 57, 55, 35, 180, 207, 174, 100, 21, 194, 103, 9, 188, 157, 153, 149, 178, 41, 20, 173, 188, 0, 172, 150, 14, 95, 179, 41, 224, 253, 154, 42, 98, 44, 13, 62, 172, 184, 177, 59, 167, 223, 225, 68, 237, 94, 35, 197, 163, 75, 97, 73, 215, 72, 245, 17, 24, 239, 111, 130, 99, 200, 89, 141, 248, 34, 238, 196, 39, 84, 43, 142, 77, 93, 81, 72, 214, 9, 105, 202 },
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
                            MessageKey = "ẶƵ(éÃ#ÝẰXXVẮẫỮẻẳčŦỐF{ⱦKĨḤRaḨỷÝẾẼấọnọỈ",
                            PasswordHash = new byte[] { 220, 16, 100, 231, 28, 202, 30, 229, 123, 178, 21, 103, 119, 199, 225, 88, 247, 58, 103, 140, 97, 197, 78, 37, 232, 27, 214, 41, 11, 231, 56, 67, 229, 15, 15, 206, 34, 171, 87, 26, 55, 232, 222, 129, 56, 136, 209, 221, 159, 126, 88, 178, 189, 7, 172, 217, 169, 252, 112, 2, 66, 197, 112, 102 },
                            PasswordSalt = new byte[] { 10, 7, 211, 186, 34, 17, 69, 184, 241, 142, 152, 135, 204, 119, 55, 124, 136, 248, 62, 60, 84, 227, 169, 138, 207, 218, 253, 70, 130, 144, 253, 122, 180, 63, 109, 169, 223, 129, 63, 0, 212, 210, 207, 234, 174, 191, 81, 49, 158, 57, 55, 35, 180, 207, 174, 100, 21, 194, 103, 9, 188, 157, 153, 149, 178, 41, 20, 173, 188, 0, 172, 150, 14, 95, 179, 41, 224, 253, 154, 42, 98, 44, 13, 62, 172, 184, 177, 59, 167, 223, 225, 68, 237, 94, 35, 197, 163, 75, 97, 73, 215, 72, 245, 17, 24, 239, 111, 130, 99, 200, 89, 141, 248, 34, 238, 196, 39, 84, 43, 142, 77, 93, 81, 72, 214, 9, 105, 202 },
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

            modelBuilder.Entity("ChatServer.Data.Entities.AppInfoConversation", b =>
                {
                    b.HasOne("ChatServer.Data.Entities.AppColorConversation", "AppColorConversation")
                        .WithMany("AppInfoConversations")
                        .HasForeignKey("AppColorConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChatServer.Data.Entities.AppConversation", "AppConversation")
                        .WithMany("AppInfoConversations")
                        .HasForeignKey("AppConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppColorConversation");

                    b.Navigation("AppConversation");
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

            modelBuilder.Entity("ChatServer.Data.Entities.AppColorConversation", b =>
                {
                    b.Navigation("AppInfoConversations");
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppConversation", b =>
                {
                    b.Navigation("AppInfoConversations");

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
