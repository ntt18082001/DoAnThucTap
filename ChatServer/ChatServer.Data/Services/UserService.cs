﻿using AutoMapper;
using ChatServer.Data.Entities;
using ChatServer.Data.Interfaces.UnitOfWork;
using ChatServer.Shared.DTOs.Friends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Services
{
	public class UserService
	{
		private readonly IUnitOfWork _unitOfWork;
		public UserService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<bool> Register(AppUser user)
		{
			var isSuccess = await _unitOfWork.UserRepository.AddAsync(user);
			if(isSuccess)
			{
				await _unitOfWork.SaveAsync();
				return true;
			}
			return false;
		}
		public async Task<bool> CheckUserExist(Expression<Func<AppUser, bool>> predicate)
		{
			return await _unitOfWork.UserRepository.AnyAsync(predicate);
		}
		public async Task<AppUser> GetUser(int id)
		{
			return await _unitOfWork.UserRepository.GetUser(id);
		}
		public async Task<bool> UpdateProfileUser(AppUser user)
		{
			if(user != null)
			{
				await _unitOfWork.SaveAsync();
				return true;
			}
			return false;
		}
		public async Task<bool> UpdateUser(AppUser user)
		{
			if(user != null)
			{
				await _unitOfWork.SaveAsync();
				return true;
			}
			return false;
		}
		public async Task<IEnumerable<FriendDTO>> GetListUserNotFriend(int id, string searchString = null)
		{
			return await _unitOfWork.UserRepository.GetUserNotFriend(id, searchString);
		}
		public async Task<IEnumerable<FriendDTO>> GetListFriend(int id)
		{
			return await _unitOfWork.UserRepository.GetListFriend(id);
		}
	}
}