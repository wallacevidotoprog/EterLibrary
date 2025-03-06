using System.Linq.Expressions;

namespace EterLibrary.Domain.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[]? includes);
		Task<IEnumerable<T>> GetIncudeAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes);
		Task<T> GetByAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[]? includes);
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task RemoveAsync(int id);

		Task<T> AddOrUpdateAsync(T entity);
	}
}
