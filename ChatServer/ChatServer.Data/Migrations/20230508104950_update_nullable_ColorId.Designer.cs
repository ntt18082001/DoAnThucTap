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
    [Migration("20230508104950_update_nullable_ColorId")]
    partial class update_nullable_ColorId
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
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

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
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("AppColorConversation", (string)null);
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

                    b.Property<int?>("ColorId")
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

                    b.Property<string>("MainEmoji")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("ConversationId");

                    b.ToTable("AppInfoConversation", (string)null);
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

            modelBuilder.Entity("ChatServer.Data.Entities.AppNickname", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.ToTable("AppNickname", (string)null);
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
                            MessageKey = "ệõĩṱũìÕẦkFÈĈ$ỢẮẵãắfừvĈặƵ-ÁẢỹẲẹÔ{@zỘúề",
                            PasswordHash = new byte[] { 180, 201, 240, 239, 243, 26, 190, 236, 232, 221, 209, 34, 110, 129, 208, 207, 77, 64, 202, 152, 211, 193, 26, 65, 202, 112, 190, 82, 93, 124, 34, 253, 23, 212, 14, 142, 233, 17, 86, 135, 199, 111, 197, 11, 236, 249, 127, 88, 79, 188, 108, 10, 154, 238, 167, 74, 112, 69, 97, 19, 248, 115, 94, 51 },
                            PasswordSalt = new byte[] { 158, 4, 239, 17, 115, 125, 100, 225, 197, 69, 181, 67, 111, 203, 55, 79, 40, 235, 144, 0, 21, 22, 177, 38, 92, 95, 171, 20, 231, 132, 214, 120, 144, 173, 152, 26, 141, 221, 96, 104, 8, 44, 102, 25, 66, 3, 38, 225, 139, 233, 135, 36, 199, 127, 234, 138, 70, 238, 29, 220, 165, 41, 217, 225, 29, 58, 177, 204, 100, 87, 46, 201, 40, 28, 11, 70, 134, 135, 27, 169, 108, 222, 90, 66, 62, 70, 68, 88, 27, 82, 236, 166, 140, 97, 76, 5, 55, 254, 85, 29, 23, 225, 7, 246, 59, 33, 63, 119, 97, 178, 84, 80, 124, 49, 196, 231, 180, 133, 147, 18, 255, 240, 170, 16, 70, 158, 62, 158 },
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
                            MessageKey = "ệõĩṱũìÕẦkFÈĈ$ỢẮẵãắfừvĈặƵ-ÁẢỹẲẹÔ{@zỘúề",
                            PasswordHash = new byte[] { 180, 201, 240, 239, 243, 26, 190, 236, 232, 221, 209, 34, 110, 129, 208, 207, 77, 64, 202, 152, 211, 193, 26, 65, 202, 112, 190, 82, 93, 124, 34, 253, 23, 212, 14, 142, 233, 17, 86, 135, 199, 111, 197, 11, 236, 249, 127, 88, 79, 188, 108, 10, 154, 238, 167, 74, 112, 69, 97, 19, 248, 115, 94, 51 },
                            PasswordSalt = new byte[] { 158, 4, 239, 17, 115, 125, 100, 225, 197, 69, 181, 67, 111, 203, 55, 79, 40, 235, 144, 0, 21, 22, 177, 38, 92, 95, 171, 20, 231, 132, 214, 120, 144, 173, 152, 26, 141, 221, 96, 104, 8, 44, 102, 25, 66, 3, 38, 225, 139, 233, 135, 36, 199, 127, 234, 138, 70, 238, 29, 220, 165, 41, 217, 225, 29, 58, 177, 204, 100, 87, 46, 201, 40, 28, 11, 70, 134, 135, 27, 169, 108, 222, 90, 66, 62, 70, 68, 88, 27, 82, 236, 166, 140, 97, 76, 5, 55, 254, 85, 29, 23, 225, 7, 246, 59, 33, 63, 119, 97, 178, 84, 80, 124, 49, 196, 231, 180, 133, 147, 18, 255, 240, 170, 16, 70, 158, 62, 158 },
                            PhoneNumber1 = "0123456789",
                            PhoneNumber2 = "0123456789",
                            UpdatedBy = -1,
                            UpdatedDate = new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Username = "tiensi"
                        });
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppVerifyCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<DateTime>("Expired")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<bool>("IsVerified")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("TokenString")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AppVerifyCode", (string)null);
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
                        .HasForeignKey("ColorId");

                    b.HasOne("ChatServer.Data.Entities.AppConversation", "AppConversation")
                        .WithMany("AppInfoConversations")
                        .HasForeignKey("ConversationId")
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

            modelBuilder.Entity("ChatServer.Data.Entities.AppNickname", b =>
                {
                    b.HasOne("ChatServer.Data.Entities.AppConversation", "AppConversation")
                        .WithMany("AppNicknames")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppConversation");
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

            modelBuilder.Entity("ChatServer.Data.Entities.AppVerifyCode", b =>
                {
                    b.HasOne("ChatServer.Data.Entities.AppUser", "AppUser")
                        .WithMany("AppVerifyCodeNavigation")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppColorConversation", b =>
                {
                    b.Navigation("AppInfoConversations");
                });

            modelBuilder.Entity("ChatServer.Data.Entities.AppConversation", b =>
                {
                    b.Navigation("AppInfoConversations");

                    b.Navigation("AppNicknames");

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

                    b.Navigation("AppVerifyCodeNavigation");

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
