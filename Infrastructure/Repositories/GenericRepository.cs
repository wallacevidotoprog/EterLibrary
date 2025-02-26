using EterLibrary.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EterLibrary.Infrastructure.Repositories
{
	public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		public readonly DatabaseContext _context;
		public readonly DbSet<T> _dbSet;

		public GenericRepository()
		{
			_context = new DatabaseContext();
			_dbSet = _context.Set<T>();
		}
		public async Task<T> AddAsync(T entity)
		{
			var e = await _dbSet.AddAsync(entity);
			await _context.SaveChangesAsync();
			return e.Entity;
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

		public async Task<IEnumerable<T>> GetIncudeAsync(params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _dbSet;

			foreach (var include in includes)
			{
				query = query.Include(include);
			}

			return await query.ToListAsync();
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