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
	public class AppConversationConfig : IEntityTypeConfiguration<AppConversation>
	{
		public void Configure(EntityTypeBuilder<AppConversation> builder)
		{
			builder.ToTable(DB.AppConversation.TABLE_NAME);

			// Khóa chính
			builder.HasKey(c => c.Id);

			// Khóa ngoại
			builder.HasOne(m => m.AppUser1)
				.WithMany(m => m.ConversationsOfUser1)
				.HasForeignKey(m => m.UserId1)
				.OnDelete(DeleteBehavior.NoAction);
			builder.HasOne(m => m.AppUser2)
				.WithMany(m => m.ConversationsOfUser2)
				.HasForeignKey(m => m.UserId2)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
