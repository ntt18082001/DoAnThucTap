using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatServer.Data.Migrations
{
    /// <inheritdoc />
    public partial class update_nullable_ColorId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInfoConversation_AppColorConversation_ColorId",
                table: "AppInfoConversation");

            migrationBuilder.AlterColumn<int>(
                name: "ColorId",
                table: "AppInfoConversation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MessageKey", "PasswordHash", "PasswordSalt" },
                values: new object[] { "ệõĩṱũìÕẦkFÈĈ$ỢẮẵãắfừvĈặƵ-ÁẢỹẲẹÔ{@zỘúề", new byte[] { 180, 201, 240, 239, 243, 26, 190, 236, 232, 221, 209, 34, 110, 129, 208, 207, 77, 64, 202, 152, 211, 193, 26, 65, 202, 112, 190, 82, 93, 124, 34, 253, 23, 212, 14, 142, 233, 17, 86, 135, 199, 111, 197, 11, 236, 249, 127, 88, 79, 188, 108, 10, 154, 238, 167, 74, 112, 69, 97, 19, 248, 115, 94, 51 }, new byte[] { 158, 4, 239, 17, 115, 125, 100, 225, 197, 69, 181, 67, 111, 203, 55, 79, 40, 235, 144, 0, 21, 22, 177, 38, 92, 95, 171, 20, 231, 132, 214, 120, 144, 173, 152, 26, 141, 221, 96, 104, 8, 44, 102, 25, 66, 3, 38, 225, 139, 233, 135, 36, 199, 127, 234, 138, 70, 238, 29, 220, 165, 41, 217, 225, 29, 58, 177, 204, 100, 87, 46, 201, 40, 28, 11, 70, 134, 135, 27, 169, 108, 222, 90, 66, 62, 70, 68, 88, 27, 82, 236, 166, 140, 97, 76, 5, 55, 254, 85, 29, 23, 225, 7, 246, 59, 33, 63, 119, 97, 178, 84, 80, 124, 49, 196, 231, 180, 133, 147, 18, 255, 240, 170, 16, 70, 158, 62, 158 } });

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MessageKey", "PasswordHash", "PasswordSalt" },
                values: new object[] { "ệõĩṱũìÕẦkFÈĈ$ỢẮẵãắfừvĈặƵ-ÁẢỹẲẹÔ{@zỘúề", new byte[] { 180, 201, 240, 239, 243, 26, 190, 236, 232, 221, 209, 34, 110, 129, 208, 207, 77, 64, 202, 152, 211, 193, 26, 65, 202, 112, 190, 82, 93, 124, 34, 253, 23, 212, 14, 142, 233, 17, 86, 135, 199, 111, 197, 11, 236, 249, 127, 88, 79, 188, 108, 10, 154, 238, 167, 74, 112, 69, 97, 19, 248, 115, 94, 51 }, new byte[] { 158, 4, 239, 17, 115, 125, 100, 225, 197, 69, 181, 67, 111, 203, 55, 79, 40, 235, 144, 0, 21, 22, 177, 38, 92, 95, 171, 20, 231, 132, 214, 120, 144, 173, 152, 26, 141, 221, 96, 104, 8, 44, 102, 25, 66, 3, 38, 225, 139, 233, 135, 36, 199, 127, 234, 138, 70, 238, 29, 220, 165, 41, 217, 225, 29, 58, 177, 204, 100, 87, 46, 201, 40, 28, 11, 70, 134, 135, 27, 169, 108, 222, 90, 66, 62, 70, 68, 88, 27, 82, 236, 166, 140, 97, 76, 5, 55, 254, 85, 29, 23, 225, 7, 246, 59, 33, 63, 119, 97, 178, 84, 80, 124, 49, 196, 231, 180, 133, 147, 18, 255, 240, 170, 16, 70, 158, 62, 158 } });

            migrationBuilder.AddForeignKey(
                name: "FK_AppInfoConversation_AppColorConversation_ColorId",
                table: "AppInfoConversation",
                column: "ColorId",
                principalTable: "AppColorConversation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppInfoConversation_AppColorConversation_ColorId",
                table: "AppInfoConversation");

            migrationBuilder.AlterColumn<int>(
                name: "ColorId",
                table: "AppInfoConversation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_AppInfoConversation_AppColorConversation_ColorId",
                table: "AppInfoConversation",
                column: "ColorId",
                principalTable: "AppColorConversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
