using ChatServer.Data.Configurations;
using ChatServer.Data.DataSeeders;
using ChatServer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data
{
	public class AppChatDbContext : DbContext
	{
		public DbSet<AppRole> AppRoles { get; set; }
		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<MstStatusRequest> MstStatusRequests { get; set; }
		public DbSet<AppConversation> AppConversations { get; set; }
		public DbSet<AppMessage> AppMessages { get; set; }
		public DbSet<AppFriendRequest> AppFriendsRequests { get; set; }
		public DbSet<AppFriendShip> AppFriendsShip { get; set; }

		public AppChatDbContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new AppRoleConfig());
			modelBuilder.ApplyConfiguration(new AppUserConfig());
			modelBuilder.ApplyConfiguration(new MstStatusRequestConfig());
			modelBuilder.ApplyConfiguration(new AppConversationConfig());
			modelBuilder.ApplyConfiguration(new AppMessageConfig());
			modelBuilder.ApplyConfiguration(new AppFriendRequestConfig());
			modelBuilder.ApplyConfiguration(new AppFriendShipConfig());

			// SeedData
			modelBuilder.Entity<AppRole>().SeedData();
			modelBuilder.Entity<AppUser>().SeedData();
			modelBuilder.Entity<MstStatusRequest>().SeedData();
		}
	}
}
