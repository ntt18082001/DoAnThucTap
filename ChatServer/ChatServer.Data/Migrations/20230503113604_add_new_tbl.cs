using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_new_tbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNotify",
                table: "AppMessage",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "AppColorConversation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BackgroundColorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TextColorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppColorConversation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppInfoConversation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    UserNickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FriendNickname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainEmoji = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppColorConversationId = table.Column<int>(type: "int", nullable: false),
                    AppConversationId = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppInfoConversation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppInfoConversation_AppColorConversation_AppColorConversationId",
                        column: x => x.AppColorConversationId,
                        principalTable: "AppColorConversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppInfoConversation_AppConversation_AppConversationId",
                        column: x => x.AppConversationId,
                        principalTable: "AppConversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MessageKey", "PasswordHash", "PasswordSalt" },
                values: new object[] { "ẶƵ(éÃ#ÝẰXXVẮẫỮẻẳčŦỐF{ⱦKĨḤRaḨỷÝẾẼấọnọỈ", new byte[] { 220, 16, 100, 231, 28, 202, 30, 229, 123, 178, 21, 103, 119, 199, 225, 88, 247, 58, 103, 140, 97, 197, 78, 37, 232, 27, 214, 41, 11, 231, 56, 67, 229, 15, 15, 206, 34, 171, 87, 26, 55, 232, 222, 129, 56, 136, 209, 221, 159, 126, 88, 178, 189, 7, 172, 217, 169, 252, 112, 2, 66, 197, 112, 102 }, new byte[] { 10, 7, 211, 186, 34, 17, 69, 184, 241, 142, 152, 135, 204, 119, 55, 124, 136, 248, 62, 60, 84, 227, 169, 138, 207, 218, 253, 70, 130, 144, 253, 122, 180, 63, 109, 169, 223, 129, 63, 0, 212, 210, 207, 234, 174, 191, 81, 49, 158, 57, 55, 35, 180, 207, 174, 100, 21, 194, 103, 9, 188, 157, 153, 149, 178, 41, 20, 173, 188, 0, 172, 150, 14, 95, 179, 41, 224, 253, 154, 42, 98, 44, 13, 62, 172, 184, 177, 59, 167, 223, 225, 68, 237, 94, 35, 197, 163, 75, 97, 73, 215, 72, 245, 17, 24, 239, 111, 130, 99, 200, 89, 141, 248, 34, 238, 196, 39, 84, 43, 142, 77, 93, 81, 72, 214, 9, 105, 202 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MessageKey", "PasswordHash", "PasswordSalt" },
                values: new object[] { "ẶƵ(éÃ#ÝẰXXVẮẫỮẻẳčŦỐF{ⱦKĨḤRaḨỷÝẾẼấọnọỈ", new byte[] { 220, 16, 100, 231, 28, 202, 30, 229, 123, 178, 21, 103, 119, 199, 225, 88, 247, 58, 103, 140, 97, 197, 78, 37, 232, 27, 214, 41, 11, 231, 56, 67, 229, 15, 15, 206, 34, 171, 87, 26, 55, 232, 222, 129, 56, 136, 209, 221, 159, 126, 88, 178, 189, 7, 172, 217, 169, 252, 112, 2, 66, 197, 112, 102 }, new byte[] { 10, 7, 211, 186, 34, 17, 69, 184, 241, 142, 152, 135, 204, 119, 55, 124, 136, 248, 62, 60, 84, 227, 169, 138, 207, 218, 253, 70, 130, 144, 253, 122, 180, 63, 109, 169, 223, 129, 63, 0, 212, 210, 207, 234, 174, 191, 81, 49, 158, 57, 55, 35, 180, 207, 174, 100, 21, 194, 103, 9, 188, 157, 153, 149, 178, 41, 20, 173, 188, 0, 172, 150, 14, 95, 179, 41, 224, 253, 154, 42, 98, 44, 13, 62, 172, 184, 177, 59, 167, 223, 225, 68, 237, 94, 35, 197, 163, 75, 97, 73, 215, 72, 245, 17, 24, 239, 111, 130, 99, 200, 89, 141, 248, 34, 238, 196, 39, 84, 43, 142, 77, 93, 81, 72, 214, 9, 105, 202 } });

            migrationBuilder.CreateIndex(
                name: "IX_AppInfoConversation_AppColorConversationId",
                table: "AppInfoConversation",
                column: "AppColorConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInfoConversation_AppConversationId",
                table: "AppInfoConversation",
                column: "AppConversationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppInfoConversation");

            migrationBuilder.DropTable(
                name: "AppColorConversation");

            migrationBuilder.DropColumn(
                name: "IsNotify",
                table: "AppMessage");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MessageKey", "PasswordHash", "PasswordSalt" },
                values: new object[] { null, new byte[] { 125, 134, 53, 58, 204, 169, 103, 229, 48, 6, 56, 19, 247, 30, 155, 157, 172, 209, 174, 212, 140, 227, 237, 138, 244, 206, 155, 121, 232, 119, 71, 99, 10, 214, 107, 133, 250, 232, 5, 202, 101, 20, 64, 204, 225, 112, 66, 62, 73, 202, 224, 33, 83, 184, 31, 141, 24, 60, 251, 60, 228, 27, 243, 204 }, new byte[] { 186, 71, 145, 68, 2, 30, 192, 113, 172, 31, 89, 162, 224, 37, 67, 53, 132, 26, 200, 155, 181, 84, 85, 4, 188, 202, 143, 169, 92, 1, 157, 215, 225, 88, 2, 250, 222, 170, 163, 38, 19, 58, 140, 192, 238, 96, 227, 99, 190, 142, 32, 201, 153, 23, 60, 124, 120, 213, 88, 236, 165, 171, 104, 22, 15, 55, 250, 244, 90, 131, 121, 61, 245, 68, 150, 180, 5, 232, 96, 91, 233, 17, 114, 16, 74, 76, 195, 125, 176, 184, 105, 175, 45, 8, 175, 244, 45, 225, 81, 31, 22, 17, 140, 195, 26, 215, 129, 51, 16, 230, 145, 46, 112, 184, 40, 244, 129, 81, 20, 150, 210, 54, 209, 235, 10, 126, 218, 203 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MessageKey", "PasswordHash", "PasswordSalt" },
                values: new object[] { null, new byte[] { 125, 134, 53, 58, 204, 169, 103, 229, 48, 6, 56, 19, 247, 30, 155, 157, 172, 209, 174, 212, 140, 227, 237, 138, 244, 206, 155, 121, 232, 119, 71, 99, 10, 214, 107, 133, 250, 232, 5, 202, 101, 20, 64, 204, 225, 112, 66, 62, 73, 202, 224, 33, 83, 184, 31, 141, 24, 60, 251, 60, 228, 27, 243, 204 }, new byte[] { 186, 71, 145, 68, 2, 30, 192, 113, 172, 31, 89, 162, 224, 37, 67, 53, 132, 26, 200, 155, 181, 84, 85, 4, 188, 202, 143, 169, 92, 1, 157, 215, 225, 88, 2, 250, 222, 170, 163, 38, 19, 58, 140, 192, 238, 96, 227, 99, 190, 142, 32, 201, 153, 23, 60, 124, 120, 213, 88, 236, 165, 171, 104, 22, 15, 55, 250, 244, 90, 131, 121, 61, 245, 68, 150, 180, 5, 232, 96, 91, 233, 17, 114, 16, 74, 76, 195, 125, 176, 184, 105, 175, 45, 8, 175, 244, 45, 225, 81, 31, 22, 17, 140, 195, 26, 215, 129, 51, 16, 230, 145, 46, 112, 184, 40, 244, 129, 81, 20, 150, 210, 54, 209, 235, 10, 126, 218, 203 } });
        }
    }
}
