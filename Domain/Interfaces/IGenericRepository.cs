using System.Linq.Expressions;

namespace EterLibrary.Domain.Interfaces
{
	public interface IGenericRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<IEnumerable<T>> GetIncudeAsync(params Expression<Func<T, object>>[] includes);
		Task<T> GetByIdAsync(int id);
		Task<T> AddAsync(T entity);
		Task UpdateAsync(T entity);
		Task RemoveAsync(int id);
	}
}
