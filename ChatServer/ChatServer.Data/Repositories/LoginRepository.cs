using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChatServer.Data.Entities;
using ChatServer.Data.Interfaces.Repositories;
using ChatServer.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Repositories
{
	public class LoginRepository : GenericRepository<AppUser>, ILoginRepository
	{
		public LoginRepository(AppChatDbContext context) : base(context)
		{
		}

		public Task<bool> CheckIsblock(long id)
		{
			throw new NotImplementedException();
		}

		public async Task<AppUser> FindAccount(string email)
		{
			return await dbSet.Include(s => s.AppRole).SingleOrDefaultAsync(s => s.Email == email);
		}

		public Task<AppUser> GetUser(LoginDTO loginDTO)
		{
			throw new NotImplementedException();
		}

		public Task<bool> UpdateLastLogin(long id)
		{
			throw new NotImplementedException();
		}
	}
}
