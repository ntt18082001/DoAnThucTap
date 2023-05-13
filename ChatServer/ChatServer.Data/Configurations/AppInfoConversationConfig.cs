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
	public class AppInfoConversationConfig : IEntityTypeConfiguration<AppInfoConversation>
	{
		public void Configure(EntityTypeBuilder<AppInfoConversation> builder)
		{
			builder.ToTable(DB.AppInfoConversation.TABLE_NAME);

			// Khóa chính
			builder.HasKey(x => x.Id);
			// Khóa ngoại
			builder.HasOne(m => m.AppConversation)
				.WithMany(m => m.AppInfoConversations)
				.HasForeignKey(m => m.ConversationId);
			builder.HasOne(m => m.AppColorConversation)
				.WithMany(m => m.AppInfoConversations)
				.HasForeignKey(m => m.ColorId);
		}
	}
}
