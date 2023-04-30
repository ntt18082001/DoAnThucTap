using ChatServer.Data.Entities;
using ChatServer.Shared.Consts;
using ChatServer.Shared.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.DataSeeders
{
	public static class AppUserSeeder
	{
		public static void SeedData(this EntityTypeBuilder<AppUser> builder)
		{
			var now = new DateTime(year: 2023, month: 4, day: 4);

			var hashResult = PwdHashHelper.HashHMACSHA512(AppDefault.Password.DEFAULT);
			var pwdHash = hashResult.Value;
			var pwdSalt = hashResult.Key;
			var messageKey = StringHasher.CreateSalt();

			builder.HasData(
				new AppUser
				{
					Id = 1,
					Username = "admin",
					PasswordHash = pwdHash,
					PasswordSalt = pwdSalt,
					Address = "Thành phố Cần Thơ",
					Email = "ntt18082001@gmail.com",
					FullName = "Tiến Sĩ",
					PhoneNumber1 = "0928666158",
					PhoneNumber2 = "0928666156",
					CreatedBy = -1,
					UpdatedBy = -1,
					UpdatedDate = now,
					CreatedDate = now,
					AppRoleId = RoleConst.AdminRole.ID,              // Vai trò được tạo ở AppRoleSeeder
					MessageKey = messageKey
				},
				new AppUser
				{
					Id = 2,
					Username = "tiensi",
					PasswordHash = pwdHash,
					PasswordSalt = pwdSalt,
					Address = "Thành phố Cần Thơ",
					Email = "ntt180801@gmail.com",
					FullName = "Tiến Sĩ 2",
					PhoneNumber1 = "0123456789",
					PhoneNumber2 = "0123456789",
					CreatedBy = -1,
					UpdatedBy = -1,
					UpdatedDate = now,
					CreatedDate = now,
					AppRoleId = RoleConst.UserRole.ID,              // Vai trò được tạo ở AppRoleSeeder
					MessageKey = messageKey
				}
			);
		}
	}
}
