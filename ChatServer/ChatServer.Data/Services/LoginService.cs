using AutoMapper;
using ChatServer.Data.Entities;
using ChatServer.Data.Interfaces.UnitOfWork;
using ChatServer.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Services
{
	public class LoginService
	{
		private readonly IUnitOfWork _unitOfWork;
		public LoginService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<AppUser> GetUser(LoginDTO loginDTO)
		{
			return await _unitOfWork.LoginRepository.GetUser(loginDTO);
		}
		public async Task<AppUser> FindAccount(string email)
		{
			return await _unitOfWork.LoginRepository.FindAccount(email);
		}
	}
}
