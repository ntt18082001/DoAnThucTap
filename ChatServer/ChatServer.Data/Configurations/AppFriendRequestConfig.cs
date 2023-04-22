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
	public class AppFriendRequestConfig : IEntityTypeConfiguration<AppFriendRequest>
	{
		public void Configure(EntityTypeBuilder<AppFriendRequest> builder)
		{
			builder.ToTable(DB.AppFriendRequest.TABLE_NAME);

			// Khóa chính
			builder.HasKey(x => x.Id);

			// Khóa ngoại
			builder.HasOne(m => m.StatusRequest)
				.WithMany(m => m.AppFriendRequests)
				.HasForeignKey(m => m.StatusId);

			builder.HasOne(m => m.UserSendRequest)
				.WithMany(m => m.SendRequests)
				.HasForeignKey(m => m.SenderId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(m => m.UserReceiveRequest)
				.WithMany(m => m.ReceiveRequests)
				.HasForeignKey(m => m.ReceiverId)
				.OnDelete(DeleteBehavior.NoAction);

		}
	}
}
