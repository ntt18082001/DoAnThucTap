using AutoMapper;
using ChatServer.Data.Entities;
using ChatServer.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Interfaces.Repositories
{
	public interface ILoginRepository : IGenericRepository<AppUser>
	{
		Task<AppUser> GetUser(LoginDTO loginDTO);
		Task<bool> CheckIsblock(long id);
		Task<bool> UpdateLastLogin(long id);
		Task<AppUser> FindAccount(string Email);
	}
}
