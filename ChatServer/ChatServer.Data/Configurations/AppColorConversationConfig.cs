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
	public class AppColorConversationConfig : IEntityTypeConfiguration<AppColorConversation>
	{
		public void Configure(EntityTypeBuilder<AppColorConversation> builder)
		{
			builder.ToTable(DB.AppColorConversation.TABLE_NAME);

			// Khóa chính
			builder.HasKey(c => c.Id);

			builder.Property(m => m.BackgroundColorCode)
				.HasMaxLength(DB.AppColorConversation.BG_COLOR_CODE_LENGTH);
			builder.Property(m => m.TextColorCode)
				.HasMaxLength(DB.AppColorConversation.TEXT_COLOR_CODE_LENGTH);
		}
	}
}
