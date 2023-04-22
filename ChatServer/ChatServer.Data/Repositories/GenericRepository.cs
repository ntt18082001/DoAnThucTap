using AutoMapper;
using AutoMapper.QueryableExtensions;
using ChatServer.Data.Entities.Base;
using ChatServer.Data.Interfaces;
using ChatServer.Shared.Consts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : AppEntityBase
	{
		protected readonly AppChatDbContext _context;
		internal DbSet<T> dbSet;
		public GenericRepository(AppChatDbContext context)
		{
			_context = context;
			dbSet = context.Set<T>();
		}

		public async Task<bool> AddAsync(T entity)
		{
			await dbSet.AddAsync(entity);
			return true;
		}

		public async Task<IEnumerable<T>> GetAll()
		{
			return await dbSet
				.AsNoTracking()
				.OrderByDescending(x => x.DisplayOrder)
				.ThenByDescending(x => x.Id)
				.ToListAsync();
		}

		public async Task<bool> CreateOrUpdate(T entity)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> Delete(long id)
		{
			throw new NotImplementedException();
		}

		public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
		{
			return await dbSet.Where(predicate).SingleOrDefaultAsync();
		}

		public async Task<T> GetById(long id)
		{
			return await dbSet.FindAsync(id);
		}

		public IQueryable<T> GetListEntity()
		{
			throw new NotImplementedException();
		}

		public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
		{
			return await dbSet.AnyAsync(predicate);
		}
		public async Task<IEnumerable<int>> GetListFriendId(int id)
		{
			return await _context
				.AppFriendsShip
				.AsNoTracking()
				.Where(x => (x.UserId1 == id || x.UserId2 == id) && x.DeletedDate == null)
				.Select(x => x.UserId1 == id ? x.UserId2 : x.UserId1)
				.ToListAsync();
		}
	}
}
