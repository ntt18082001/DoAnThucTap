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
	public class AppMessageConfig : IEntityTypeConfiguration<AppMessage>
	{
		public void Configure(EntityTypeBuilder<AppMessage> builder)
		{
			builder.ToTable(DB.AppMessage.TABLE_NAME);

			// Khóa chính
			builder.HasKey(x => x.Id);

			builder.Property(m => m.Content)
				.HasMaxLength(DB.AppMessage.CONTENT_LENGTH);
			builder.Property(m => m.UrlImage)
				.HasMaxLength(DB.AppMessage.IMAGE_LENGTH);

			// Khóa ngoại
			builder.HasOne(m => m.Conversation)
				.WithMany(m => m.Messages)
				.HasForeignKey(m => m.ConversationId);
			builder.HasOne(m => m.Sender)
				.WithMany(m => m.SendMessage)
				.HasForeignKey(m => m.SenderId)
				.OnDelete(DeleteBehavior.NoAction);

			builder.HasOne(m => m.Receiver)
				.WithMany(m => m.ReceiveMessage)
				.HasForeignKey(m => m.ReceiverId)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
