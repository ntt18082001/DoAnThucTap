using ChatServer.Data.Entities;
using ChatServer.Shared.Consts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Configurations
{
	public class AppFriendShipConfig : IEntityTypeConfiguration<AppFriendShip>
	{
		public void Configure(EntityTypeBuilder<AppFriendShip> builder)
		{
			builder.ToTable(DB.AppFriendShip.TABLE_NAME);

			// Khóa chính
			builder.HasKey(x => x.Id);

			// Khóa ngoại
			builder.HasOne(m => m.AppUser1)
				.WithMany(m => m.AppFriends1)
				.HasForeignKey(m => m.UserId1)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(m => m.AppUser2)
				.WithMany(m => m.AppFriends2)
				.HasForeignKey(m => m.UserId2)
				.OnDelete(DeleteBehavior.NoAction);

		}
	}
}
