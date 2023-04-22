using ChatServer.Data.Entities;
using ChatServer.Shared.Consts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.DataSeeders
{
	public static class AppRoleSeeder
	{
		public static void SeedData(this EntityTypeBuilder<AppRole> builder)
		{
			var now = new DateTime(year: 2023, month: 4, day: 4);
			// Tạo vai trò
			var roleUser = new AppRole
			{
				Id = RoleConst.AdminRole.ID,
				Name = RoleConst.AdminRole.NAME,
				Desc = RoleConst.AdminRole.DESC,
				CreatedDate = now,
				UpdatedDate = now
			};

			var roleAdmin = new AppRole
			{
				Id = RoleConst.UserRole.ID,
				Name = RoleConst.UserRole.NAME,
				Desc = RoleConst.UserRole.DESC,
				CreatedDate = now,
				UpdatedDate = now
			};

			builder.HasData(roleUser, roleAdmin);
		}
	}
}
