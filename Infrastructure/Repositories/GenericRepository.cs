﻿using EterLibrary.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
			if (filter != null)
			{
				query = query.Where(filter);
			}
			var entity = query.FirstOrDefault();

			return entity;
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

		public async Task AddOrUpdateAsync(T entity)
		{
			var key = _context.Model.FindEntityType(typeof(T))?
		.FindPrimaryKey()?.Properties.FirstOrDefault();

			if (key == null)
			{
				throw new InvalidOperationException("A entidade não possui chave primária definida.");
			}

			var keyValue = key.PropertyInfo?.GetValue(entity);

			if (keyValue == null || Convert.ToInt64(keyValue) == 0) // Considera 0 como entidade nova
			{
				var addedEntity = await _dbSet.AddAsync(entity);
				await _context.SaveChangesAsync();
				//return addedEntity.Entity;
			}
			else
			{
				var existingEntity = await _dbSet
					.FirstOrDefaultAsync(e => EF.Property<long?>(e, key.Name) == (long?)keyValue);

				if (existingEntity == null)
				{
					var addedEntity = await _dbSet.AddAsync(entity);
					await _context.SaveChangesAsync();
					//return addedEntity.Entity;
				}
				else
				{
					_context.Entry(existingEntity).CurrentValues.SetValues(entity);

					// Atualiza os relacionamentos manualmente
					var navigationProperties = _context.Entry(existingEntity).Navigations
						.Where(n => n.Metadata is INavigation)
						.Select(n => n.Metadata.Name);

					foreach (var navProp in navigationProperties)
					{
						var newRelatedValue = _context.Entry(entity).Reference(navProp).CurrentValue;
						if (newRelatedValue != null)
						{
							_context.Entry(existingEntity).Reference(navProp).CurrentValue = newRelatedValue;
						}
					}

					await _context.SaveChangesAsync();
					//return existingEntity;
				}
			}
		}



	}
}