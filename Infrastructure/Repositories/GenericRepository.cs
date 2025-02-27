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

		public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[]? includes)
		{
			IQueryable<T> query = _dbSet;

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			if (filter != null)
			{
				query = query.Where(filter);
			}
			return await query.ToListAsync();
		}

		public async Task<T> GetByAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[]? includes)
		{
			IQueryable<T> query = _dbSet;

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			var entity = filter != null ? await _dbSet.FirstOrDefaultAsync(filter) : await query.FirstOrDefaultAsync();

			return entity ?? throw new KeyNotFoundException($"Registro não encontrado.");
		}

		public async Task<IEnumerable<T>> GetIncudeAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes)
		{
			IQueryable<T> query = _dbSet;

			if (includes != null)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}

			if (filter != null)
			{
				query = query.Where(filter);
			}

			return await query.ToListAsync();
		}

		public async Task RemoveAsync(int id)
		{
			var entity = await GetByAsync(e => EF.Property<long?>(e, "ID") == id);
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