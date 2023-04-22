﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Entities.Base
{
	public abstract class AppEntityBase
	{
		public int Id { get; set; }
		public int? DisplayOrder { get; set; }
		public int? CreatedBy { get; set; }
		public int? UpdatedBy { get; set; }
		public DateTime? CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
	}
}
