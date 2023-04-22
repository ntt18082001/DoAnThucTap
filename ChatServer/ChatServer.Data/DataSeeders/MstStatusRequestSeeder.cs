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
	public static class MstStatusRequestSeeder
	{
		public static void SeedData(this EntityTypeBuilder<MstStatusRequest> builder)
		{
			var now = new DateTime(year: 2023, month: 4, day: 4);
			builder.HasData(
				new MstStatusRequest
				{
					Id = StatusRequest.PendingRequest.ID,
					StatusName = StatusRequest.PendingRequest.NAME,
					CreatedDate = now
				},
				new MstStatusRequest
				{
					Id = StatusRequest.AcceptedRequest.ID,
					StatusName = StatusRequest.AcceptedRequest.NAME,
					CreatedDate = now
				},
				new MstStatusRequest
				{
					Id = StatusRequest.RejectedRequest.ID,
					StatusName = StatusRequest.RejectedRequest.NAME,
					CreatedDate = now
				});
		}
	}
}
