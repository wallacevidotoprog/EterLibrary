using EterLibrary.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EterLibrary.Infrastructure.Repositories
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		public readonly DatabaseContext _context;
		public readonly DbSet<T> _dbSet;

		public GenericRepository(/*DatabaseContext context*/)
		{
			_context = new DatabaseContext();
			//_context = context ?? throw new ArgumentNullException(nameof(context));
			_dbSet = _context.Set<T>();
		}
		public async Task AddAsync(T entity)
		{
			await _dbSet.AddAsync(entity);
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T> GetByIdAsync(int id)
		{
			var entity = await _dbSet.FindAsync(id);
			return entity ?? throw new KeyNotFoundException($"Registro com ID {id} não encontrado.");
		}

		public async Task RemoveAsync(int id)
		{
			var entity = await GetByIdAsync(id);
			if (entity != null)
			{
				_dbSet.Remove(entity);
				await _context.SaveChangesAsync();
			}
		}

		public async Task UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
			await _context.SaveChangesAsync();
		}
	}
}