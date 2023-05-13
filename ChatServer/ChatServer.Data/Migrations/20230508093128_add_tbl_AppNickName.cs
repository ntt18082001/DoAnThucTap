using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_tbl_AppNickName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "AppInfoConversation");

            migrationBuilder.DropColumn(
                name: "FriendNickname",
                table: "AppInfoConversation");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppInfoConversation");

            migrationBuilder.DropColumn(
                name: "UserNickname",
                table: "AppInfoConversation");

            migrationBuilder.CreateTable(
                name: "AppNickname",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppNickname", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppNickname_AppConversation_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "AppConversation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MessageKey", "PasswordHash", "PasswordSalt" },
                values: new object[] { "ồ<ằặẫ3[ẐHơÁỦ.àÍừ3{ÃoUẾũệỆḨ=ỎĂờEỐẰéĈḤÓỤĨệ@Ịòỏ&", new byte[] { 11, 27, 187, 138, 193, 197, 134, 70, 129, 44, 237, 88, 115, 54, 226, 187, 221, 103, 215, 121, 131, 46, 80, 49, 154, 29, 179, 108, 107, 11, 143, 163, 21, 255, 95, 170, 23, 48, 130, 85, 161, 253, 160, 42, 31, 220, 63, 98, 138, 197, 255, 210, 79, 2, 219, 95, 8, 135, 35, 84, 31, 42, 100, 211 }, new byte[] { 228, 111, 208, 113, 209, 206, 222, 8, 151, 64, 145, 235, 199, 38, 157, 139, 72, 122, 185, 184, 148, 53, 76, 113, 86, 56, 133, 196, 32, 59, 77, 123, 53, 52, 124, 14, 226, 132, 67, 165, 70, 125, 38, 104, 173, 18, 12, 188, 115, 71, 11, 50, 223, 73, 0, 37, 149, 172, 29, 115, 149, 153, 34, 245, 10, 151, 13, 31, 32, 244, 125, 217, 25, 9, 87, 215, 110, 83, 215, 225, 150, 113, 127, 183, 134, 4, 218, 176, 72, 28, 26, 164, 128, 226, 230, 168, 175, 248, 44, 35, 116, 208, 242, 103, 41, 169, 231, 141, 232, 138, 189, 100, 212, 69, 240, 232, 119, 74, 195, 118, 56, 190, 204, 224, 8, 124, 183, 174 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MessageKey", "PasswordHash", "PasswordSalt" },
                values: new object[] { "ồ<ằặẫ3[ẐHơÁỦ.àÍừ3{ÃoUẾũệỆḨ=ỎĂờEỐẰéĈḤÓỤĨệ@Ịòỏ&", new byte[] { 11, 27, 187, 138, 193, 197, 134, 70, 129, 44, 237, 88, 115, 54, 226, 187, 221, 103, 215, 121, 131, 46, 80, 49, 154, 29, 179, 108, 107, 11, 143, 163, 21, 255, 95, 170, 23, 48, 130, 85, 161, 253, 160, 42, 31, 220, 63, 98, 138, 197, 255, 210, 79, 2, 219, 95, 8, 135, 35, 84, 31, 42, 100, 211 }, new byte[] { 228, 111, 208, 113, 209, 206, 222, 8, 151, 64, 145, 235, 199, 38, 157, 139, 72, 122, 185, 184, 148, 53, 76, 113, 86, 56, 133, 196, 32, 59, 77, 123, 53, 52, 124, 14, 226, 132, 67, 165, 70, 125, 38, 104, 173, 18, 12, 188, 115, 71, 11, 50, 223, 73, 0, 37, 149, 172, 29, 115, 149, 153, 34, 245, 10, 151, 13, 31, 32, 244, 125, 217, 25, 9, 87, 215, 110, 83, 215, 225, 150, 113, 127, 183, 134, 4, 218, 176, 72, 28, 26, 164, 128, 226, 230, 168, 175, 248, 44, 35, 116, 208, 242, 103, 41, 169, 231, 141, 232, 138, 189, 100, 212, 69, 240, 232, 119, 74, 195, 118, 56, 190, 204, 224, 8, 124, 183, 174 } });

            migrationBuilder.CreateIndex(
                name: "IX_AppNickname_ConversationId",
                table: "AppNickname",
                column: "ConversationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppNickname");

            migrationBuilder.AddColumn<int>(
                name: "FriendId",
                table: "AppInfoConversation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FriendNickname",
                table: "AppInfoConversation",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AppInfoConversation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserNickname",
                table: "AppInfoConversation",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MessageKey", "PasswordHash", "PasswordSalt" },
                values: new object[] { "ṯèy{ỹTHpƯA\\ÔmắGEṮỸZHoứẴỲ", new byte[] { 221, 184, 185, 90, 154, 180, 219, 232, 79, 149, 58, 149, 152, 5, 250, 36, 138, 20, 99, 203, 65, 115, 97, 184, 118, 56, 23, 243, 96, 23, 45, 70, 200, 1, 233, 179, 147, 222, 28, 147, 60, 85, 158, 173, 231, 188, 126, 169, 99, 89, 107, 34, 54, 14, 232, 115, 152, 101, 161, 208, 208, 238, 115, 231 }, new byte[] { 134, 43, 145, 113, 188, 170, 43, 13, 198, 178, 41, 7, 96, 130, 51, 140, 239, 86, 161, 42, 34, 255, 215, 212, 151, 214, 101, 101, 128, 138, 142, 228, 200, 188, 177, 37, 121, 244, 221, 23, 67, 203, 111, 60, 61, 131, 112, 204, 9, 133, 5, 49, 80, 210, 40, 83, 156, 250, 145, 152, 21, 73, 198, 247, 201, 16, 92, 139, 14, 195, 206, 13, 107, 100, 186, 36, 218, 160, 80, 217, 146, 196, 152, 229, 247, 73, 11, 101, 97, 37, 232, 67, 184, 10, 103, 136, 173, 162, 233, 45, 92, 103, 250, 228, 30, 35, 137, 252, 94, 43, 124, 180, 171, 216, 204, 117, 21, 221, 242, 42, 39, 187, 67, 255, 72, 70, 28, 208 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MessageKey", "PasswordHash", "PasswordSalt" },
                values: new object[] { "ṯèy{ỹTHpƯA\\ÔmắGEṮỸZHoứẴỲ", new byte[] { 221, 184, 185, 90, 154, 180, 219, 232, 79, 149, 58, 149, 152, 5, 250, 36, 138, 20, 99, 203, 65, 115, 97, 184, 118, 56, 23, 243, 96, 23, 45, 70, 200, 1, 233, 179, 147, 222, 28, 147, 60, 85, 158, 173, 231, 188, 126, 169, 99, 89, 107, 34, 54, 14, 232, 115, 152, 101, 161, 208, 208, 238, 115, 231 }, new byte[] { 134, 43, 145, 113, 188, 170, 43, 13, 198, 178, 41, 7, 96, 130, 51, 140, 239, 86, 161, 42, 34, 255, 215, 212, 151, 214, 101, 101, 128, 138, 142, 228, 200, 188, 177, 37, 121, 244, 221, 23, 67, 203, 111, 60, 61, 131, 112, 204, 9, 133, 5, 49, 80, 210, 40, 83, 156, 250, 145, 152, 21, 73, 198, 247, 201, 16, 92, 139, 14, 195, 206, 13, 107, 100, 186, 36, 218, 160, 80, 217, 146, 196, 152, 229, 247, 73, 11, 101, 97, 37, 232, 67, 184, 10, 103, 136, 173, 162, 233, 45, 92, 103, 250, 228, 30, 35, 137, 252, 94, 43, 124, 180, 171, 216, 204, 117, 21, 221, 242, 42, 39, 187, 67, 255, 72, 70, 28, 208 } });
        }
    }
}
