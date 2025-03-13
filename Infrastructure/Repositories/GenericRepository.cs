using EterLibrary.Domain.Interfaces;
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

		public async Task<T> UpdateAsync(T entity)
		{
			var key = _context.Model.FindEntityType(typeof(T))?
				.FindPrimaryKey()?.Properties.FirstOrDefault();

			if (key == null)
			{
				throw new InvalidOperationException("A entidade não possui chave primária definida.");
			}

			var keyValue = key.PropertyInfo?.GetValue(entity);
			if (keyValue == null || keyValue.Equals(Activator.CreateInstance(key.PropertyInfo.PropertyType)))
			{
				throw new InvalidOperationException("A entidade precisa ter um ID válido para ser atualizada.");
			}

			// Verifica se a entidade já está sendo rastreada
			var existingEntity = await _dbSet.FindAsync(keyValue);
			if (existingEntity != null)
			{
				// Desanexa a entidade rastreada
				_context.Entry(existingEntity).State = EntityState.Detached;
			}

			// Atualiza os valores
			_context.Entry(entity).State = EntityState.Modified;

			await _context.SaveChangesAsync();

			return await _dbSet.FindAsync(keyValue);
		}

		public async Task<T> AddOrUpdateAsync(T entity)
		{
			var key = _context.Model.FindEntityType(typeof(T))?
				.FindPrimaryKey()?.Properties.FirstOrDefault();

			if (key == null)
			{
				throw new InvalidOperationException("A entidade não possui chave primária definida.");
			}

			var keyValue = key.PropertyInfo?.GetValue(entity);

			// Se a chave for nula ou zero, adicionamos como novo registro
			if (keyValue == null || keyValue.Equals(Activator.CreateInstance(key.PropertyInfo.PropertyType)))
			{
				var addedEntity = await _dbSet.AddAsync(entity);
				await _context.SaveChangesAsync();
				return addedEntity.Entity;
			}



			// Busca a entidade existente no banco
			var existingEntity = await _dbSet.FindAsync(keyValue);

			if (existingEntity == null)
			{
				var addedEntity = await _dbSet.AddAsync(entity);
				await _context.SaveChangesAsync();
				return addedEntity.Entity;
			}

			// Atualiza os valores da entidade existente
			_context.Entry(existingEntity).CurrentValues.SetValues(entity);

			if (existingEntity == null)
			{
				throw new InvalidOperationException("Entidade não encontrada para atualização.");
			}

			// Atualiza os relacionamentos corretamente
			foreach (var navigation in _context.Entry(existingEntity).Navigations)
			{
				if (navigation.Metadata is INavigation navMeta)
				{
					var newRelatedValue = _context.Entry(entity).Navigation(navMeta.Name).CurrentValue;

					if (newRelatedValue != null)
					{
						if (navMeta.IsCollection)
						{
							var collectionEntry = _context.Entry(existingEntity).Collection(navMeta.Name);
							await collectionEntry.LoadAsync(); // ⚡ Assegura que a coleção foi carregada
							collectionEntry.CurrentValue = (IEnumerable<object>)newRelatedValue;
						}
						else
						{
							var referenceEntry = _context.Entry(existingEntity).Reference(navMeta.Name);
							referenceEntry.CurrentValue = newRelatedValue;
						}
					}
				}
			}

			await _context.SaveChangesAsync();
			return existingEntity;
		}



	}
}