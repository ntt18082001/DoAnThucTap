using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer.Data.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAll();
		//Task<PageListResult<T>> PageList(int page, int pageSize);
		Task<T> GetById(long id);
		Task<bool> AddAsync(T entity);
		Task<bool> Delete(long id);
		Task<bool> CreateOrUpdate(T entity);
		Task<T> FindAsync(Expression<Func<T, bool>> predicate);
		Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
		IQueryable<T> GetListEntity();
	}
}
