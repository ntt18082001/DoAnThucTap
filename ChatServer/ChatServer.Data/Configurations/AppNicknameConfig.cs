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
	public class AppNicknameConfig : IEntityTypeConfiguration<AppNickname>
	{
		public void Configure(EntityTypeBuilder<AppNickname> builder)
		{
			builder.ToTable(DB.AppNickname.TABLE_NAME);

			// Khóa chính
			builder.HasKey(x => x.Id);

			builder.Property(m => m.Nickname)
				.HasMaxLength(DB.AppNickname.NICKNAME_LENGTH);

			// Khóa ngoại
			builder.HasOne(m => m.AppConversation)
				.WithMany(m => m.AppNicknames)
				.HasForeignKey(m => m.ConversationId);
		}
	}
}
