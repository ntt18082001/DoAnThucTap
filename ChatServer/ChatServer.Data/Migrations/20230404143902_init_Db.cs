using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChatServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class init_Db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Desc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MstStatusRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MstStatusRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(200)", maxLength: 200, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber1 = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    PhoneNumber2 = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BlockedTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BlockedBy = table.Column<int>(type: "int", nullable: true),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    AppRoleId = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUser_AppRole_AppRoleId",
                        column: x => x.AppRoleId,
                        principalTable: "AppRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppConversation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId1 = table.Column<int>(type: "int", nullable: false),
                    UserId2 = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConversation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppConversation_AppUser_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AppUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppConversation_AppUser_UserId2",
                        column: x => x.UserId2,
                        principalTable: "AppUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppFriendRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFriendRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFriendRequest_AppUser_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AppUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFriendRequest_AppUser_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AppUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFriendRequest_MstStatusRequest_StatusId",
                        column: x => x.StatusId,
                        principalTable: "MstStatusRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppFriendShip",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId1 = table.Column<int>(type: "int", nullable: false),
                    UserId2 = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFriendShip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFriendShip_AppUser_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AppUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppFriendShip_AppUser_UserId2",
                        column: x => x.UserId2,
                        principalTable: "AppUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    SenderId = table.Column<int>(type: "int", nullable: false),
                    ReceiverId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: true),
                    UrlImage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsLiked = table.Column<bool>(type: "bit", nullable: false),
                    SentTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppMessage_AppConversation_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "AppConversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppMessage_AppUser_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AppUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppMessage_AppUser_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AppUser",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AppRole",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedDate", "Desc", "DisplayOrder", "Name", "UpdatedBy", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Quản trị hệ thống", null, "Admin", null, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Người dùng trong hệ thống", null, "User", null, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "MstStatusRequest",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "DisplayOrder", "StatusName" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Đang chờ" },
                    { 2, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Đã chấp nhận" },
                    { 3, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Đã từ chối" }
                });

            migrationBuilder.InsertData(
                table: "AppUser",
                columns: new[] { "Id", "Address", "AppRoleId", "Avatar", "BlockedBy", "BlockedTo", "CreatedBy", "CreatedDate", "DeletedDate", "DisplayOrder", "Email", "FullName", "IsOnline", "PasswordHash", "PasswordSalt", "PhoneNumber1", "PhoneNumber2", "UpdatedBy", "UpdatedDate", "Username" },
                values: new object[,]
                {
                    { 1, "Thành phố Cần Thơ", 1, null, null, null, -1, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "ntt18082001@gmail.com", "Tiến Sĩ", false, new byte[] { 87, 117, 218, 177, 117, 30, 113, 92, 135, 8, 55, 206, 140, 206, 73, 192, 48, 186, 220, 182, 50, 54, 168, 54, 16, 89, 156, 59, 28, 43, 67, 6, 115, 50, 75, 201, 38, 223, 241, 190, 221, 121, 225, 105, 1, 247, 166, 123, 161, 134, 170, 75, 173, 142, 38, 232, 54, 226, 165, 161, 32, 68, 46, 87 }, new byte[] { 29, 117, 113, 156, 96, 61, 6, 207, 70, 230, 158, 144, 22, 4, 17, 71, 232, 226, 94, 237, 0, 228, 52, 184, 103, 110, 186, 232, 39, 86, 184, 233, 227, 73, 25, 76, 173, 149, 41, 89, 39, 77, 186, 169, 67, 90, 29, 63, 11, 44, 214, 41, 244, 77, 195, 154, 155, 132, 45, 18, 128, 112, 27, 191, 77, 35, 97, 115, 73, 4, 43, 99, 21, 85, 157, 201, 176, 96, 145, 80, 65, 209, 103, 125, 237, 156, 182, 12, 220, 219, 176, 85, 208, 125, 132, 184, 40, 172, 170, 102, 98, 99, 107, 220, 173, 18, 92, 166, 193, 14, 169, 115, 21, 106, 49, 29, 82, 126, 23, 39, 248, 107, 207, 30, 128, 200, 179, 190 }, "0928666158", "0928666156", -1, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin" },
                    { 2, "Thành phố Cần Thơ", 2, null, null, null, -1, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "ntt180801@gmail.com", "Tiến Sĩ 2", false, new byte[] { 87, 117, 218, 177, 117, 30, 113, 92, 135, 8, 55, 206, 140, 206, 73, 192, 48, 186, 220, 182, 50, 54, 168, 54, 16, 89, 156, 59, 28, 43, 67, 6, 115, 50, 75, 201, 38, 223, 241, 190, 221, 121, 225, 105, 1, 247, 166, 123, 161, 134, 170, 75, 173, 142, 38, 232, 54, 226, 165, 161, 32, 68, 46, 87 }, new byte[] { 29, 117, 113, 156, 96, 61, 6, 207, 70, 230, 158, 144, 22, 4, 17, 71, 232, 226, 94, 237, 0, 228, 52, 184, 103, 110, 186, 232, 39, 86, 184, 233, 227, 73, 25, 76, 173, 149, 41, 89, 39, 77, 186, 169, 67, 90, 29, 63, 11, 44, 214, 41, 244, 77, 195, 154, 155, 132, 45, 18, 128, 112, 27, 191, 77, 35, 97, 115, 73, 4, 43, 99, 21, 85, 157, 201, 176, 96, 145, 80, 65, 209, 103, 125, 237, 156, 182, 12, 220, 219, 176, 85, 208, 125, 132, 184, 40, 172, 170, 102, 98, 99, 107, 220, 173, 18, 92, 166, 193, 14, 169, 115, 21, 106, 49, 29, 82, 126, 23, 39, 248, 107, 207, 30, 128, 200, 179, 190 }, "0123456789", "0123456789", -1, new DateTime(2023, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "tiensi" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppConversation_UserId1",
                table: "AppConversation",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppConversation_UserId2",
                table: "AppConversation",
                column: "UserId2");

            migrationBuilder.CreateIndex(
                name: "IX_AppFriendRequest_ReceiverId",
                table: "AppFriendRequest",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFriendRequest_SenderId",
                table: "AppFriendRequest",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFriendRequest_StatusId",
                table: "AppFriendRequest",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFriendShip_UserId1",
                table: "AppFriendShip",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_AppFriendShip_UserId2",
                table: "AppFriendShip",
                column: "UserId2");

            migrationBuilder.CreateIndex(
                name: "IX_AppMessage_ConversationId",
                table: "AppMessage",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMessage_ReceiverId",
                table: "AppMessage",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_AppMessage_SenderId",
                table: "AppMessage",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_AppRoleId",
                table: "AppUser",
                column: "AppRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUser_Username",
                table: "AppUser",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppFriendRequest");

            migrationBuilder.DropTable(
                name: "AppFriendShip");

            migrationBuilder.DropTable(
                name: "AppMessage");

            migrationBuilder.DropTable(
                name: "MstStatusRequest");

            migrationBuilder.DropTable(
                name: "AppConversation");

            migrationBuilder.DropTable(
                name: "AppUser");

            migrationBuilder.DropTable(
                name: "AppRole");
        }
    }
}
