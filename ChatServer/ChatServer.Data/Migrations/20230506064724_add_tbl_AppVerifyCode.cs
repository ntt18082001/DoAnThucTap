using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_tbl_AppVerifyCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInfoConversation_AppColorConversation_AppColorConversationId",
                table: "AppInfoConversation");

            migrationBuilder.DropForeignKey(
                name: "FK_AppInfoConversation_AppConversation_AppConversationId",
                table: "AppInfoConversation");

            migrationBuilder.DropIndex(
                name: "IX_AppInfoConversation_AppColorConversationId",
                table: "AppInfoConversation");

            migrationBuilder.DropIndex(
                name: "IX_AppInfoConversation_AppConversationId",
                table: "AppInfoConversation");

            migrationBuilder.DropColumn(
                name: "AppColorConversationId",
                table: "AppInfoConversation");

            migrationBuilder.DropColumn(
                name: "AppConversationId",
                table: "AppInfoConversation");

            migrationBuilder.AlterColumn<string>(
                name: "UserNickname",
                table: "AppInfoConversation",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FriendNickname",
                table: "AppInfoConversation",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TextColorCode",
                table: "AppColorConversation",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "BackgroundColorCode",
                table: "AppColorConversation",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "AppVerifyCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TokenString = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Expired = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "GETDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppVerifyCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppVerifyCode_AppUser_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUser",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MessageKey", "PasswordHash", "PasswordSalt" },
                values: new object[] { "Êổịế`3Yḩ2Q|.ỂŨJỚƵKẴểX0&ƯČỪỶpŦ", new byte[] { 210, 173, 218, 83, 65, 215, 109, 70, 150, 140, 101, 131, 141, 162, 86, 16, 183, 242, 230, 204, 202, 254, 211, 25, 213, 144, 22, 159, 6, 69, 90, 30, 68, 121, 184, 4, 207, 194, 147, 200, 216, 218, 23, 8, 76, 60, 61, 97, 172, 62, 233, 142, 176, 10, 214, 81, 154, 223, 201, 204, 112, 92, 83, 204 }, new byte[] { 12, 229, 116, 52, 129, 164, 130, 2, 213, 177, 218, 178, 1, 122, 67, 6, 54, 172, 45, 232, 220, 189, 140, 125, 29, 237, 193, 187, 88, 68, 240, 175, 152, 0, 0, 110, 207, 179, 210, 177, 90, 173, 213, 209, 225, 95, 8, 154, 224, 23, 145, 105, 236, 5, 172, 101, 149, 46, 124, 153, 170, 101, 153, 55, 46, 52, 9, 84, 12, 45, 51, 136, 65, 36, 55, 20, 118, 166, 35, 202, 238, 111, 99, 201, 8, 175, 13, 226, 213, 34, 41, 40, 81, 234, 216, 244, 77, 175, 214, 9, 58, 211, 212, 125, 106, 124, 237, 112, 58, 58, 4, 14, 34, 29, 86, 246, 13, 250, 141, 112, 51, 26, 222, 111, 212, 128, 216, 68 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MessageKey", "PasswordHash", "PasswordSalt" },
                values: new object[] { "Êổịế`3Yḩ2Q|.ỂŨJỚƵKẴểX0&ƯČỪỶpŦ", new byte[] { 210, 173, 218, 83, 65, 215, 109, 70, 150, 140, 101, 131, 141, 162, 86, 16, 183, 242, 230, 204, 202, 254, 211, 25, 213, 144, 22, 159, 6, 69, 90, 30, 68, 121, 184, 4, 207, 194, 147, 200, 216, 218, 23, 8, 76, 60, 61, 97, 172, 62, 233, 142, 176, 10, 214, 81, 154, 223, 201, 204, 112, 92, 83, 204 }, new byte[] { 12, 229, 116, 52, 129, 164, 130, 2, 213, 177, 218, 178, 1, 122, 67, 6, 54, 172, 45, 232, 220, 189, 140, 125, 29, 237, 193, 187, 88, 68, 240, 175, 152, 0, 0, 110, 207, 179, 210, 177, 90, 173, 213, 209, 225, 95, 8, 154, 224, 23, 145, 105, 236, 5, 172, 101, 149, 46, 124, 153, 170, 101, 153, 55, 46, 52, 9, 84, 12, 45, 51, 136, 65, 36, 55, 20, 118, 166, 35, 202, 238, 111, 99, 201, 8, 175, 13, 226, 213, 34, 41, 40, 81, 234, 216, 244, 77, 175, 214, 9, 58, 211, 212, 125, 106, 124, 237, 112, 58, 58, 4, 14, 34, 29, 86, 246, 13, 250, 141, 112, 51, 26, 222, 111, 212, 128, 216, 68 } });

            migrationBuilder.CreateIndex(
                name: "IX_AppInfoConversation_ColorId",
                table: "AppInfoConversation",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_AppInfoConversation_ConversationId",
                table: "AppInfoConversation",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppVerifyCode_UserId",
                table: "AppVerifyCode",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppInfoConversation_AppColorConversation_ColorId",
                table: "AppInfoConversation",
                column: "ColorId",
                principalTable: "AppColorConversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppInfoConversation_AppConversation_ConversationId",
                table: "AppInfoConversation",
                column: "ConversationId",
                principalTable: "AppConversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInfoConversation_AppColorConversation_ColorId",
                table: "AppInfoConversation");

            migrationBuilder.DropForeignKey(
                name: "FK_AppInfoConversation_AppConversation_ConversationId",
                table: "AppInfoConversation");

            migrationBuilder.DropTable(
                name: "AppVerifyCode");

            migrationBuilder.DropIndex(
                name: "IX_AppInfoConversation_ColorId",
                table: "AppInfoConversation");

            migrationBuilder.DropIndex(
                name: "IX_AppInfoConversation_ConversationId",
                table: "AppInfoConversation");

            migrationBuilder.AlterColumn<string>(
                name: "UserNickname",
                table: "AppInfoConversation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "FriendNickname",
                table: "AppInfoConversation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<int>(
                name: "AppColorConversationId",
                table: "AppInfoConversation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AppConversationId",
                table: "AppInfoConversation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TextColorCode",
                table: "AppColorConversation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "BackgroundColorCode",
                table: "AppColorConversation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

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

            migrationBuilder.AddForeignKey(
                name: "FK_AppInfoConversation_AppColorConversation_AppColorConversationId",
                table: "AppInfoConversation",
                column: "AppColorConversationId",
                principalTable: "AppColorConversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppInfoConversation_AppConversation_AppConversationId",
                table: "AppInfoConversation",
                column: "AppConversationId",
                principalTable: "AppConversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
