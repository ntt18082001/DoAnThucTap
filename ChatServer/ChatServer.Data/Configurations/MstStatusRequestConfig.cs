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
	public class MstStatusRequestConfig : IEntityTypeConfiguration<MstStatusRequest>
	{
		public void Configure(EntityTypeBuilder<MstStatusRequest> builder)
		{
			builder.ToTable(DB.MstStatusRequest.TABLE_NAME);

			// Khóa chính
			builder.HasKey(m => m.Id);

			builder.Property(m => m.StatusName)
				.HasMaxLength(DB.MstStatusRequest.STATUS_NAME_LENGTH)
				.IsRequired();
		}
	}
}
