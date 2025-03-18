using EterLibrary.Domain.Interfaces;
using EterLibrary.Infrastructure.Log;
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
			await _context.SaveChangesAsync();
			var e = await _dbSet.AddAsync(entity);
			LogSistem.LogWriteLog(TypeLog.INSERT, e.Entity.GetType(), null, e.Entity);
			return e.Entity;
		}

		public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[]? includes)
		{


			IQueryable<T> query = _dbSet.AsNoTracking(); ;

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


			var entity = await query.ToListAsync();

			try
			{
				string includesString = includes != null ? string.Join(", ", includes.Select(i => i.Body.ToString())) : "Nenhum";
				string filterString = filter != null ? filter.Body.ToString() : "Nenhum";
				LogSistem.LogWriteLog(TypeLog.VIEW, "FN" + entity.GetType(), $"Filtro:{(!string.IsNullOrEmpty(filterString) ? filterString : "Nenhum")}\n=> Includes: {(!string.IsNullOrEmpty(includesString) ? includesString : "Nenhum")}", entity);
			}
			catch (Exception) { }

			return entity;
		}

		public async Task<T> GetByAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[]? includes)
		{

			IQueryable<T> query = _dbSet.AsNoTracking(); ;

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

			try
			{
				string includesString = includes != null ? string.Join(", ", includes.Select(i => i.Body.ToString())) : "Nenhum";
				string filterString = filter != null ? filter.Body.ToString() : "Nenhum";
				LogSistem.LogWriteLog(TypeLog.VIEW, entity.GetType(), $"Filtro:{(!string.IsNullOrEmpty(filterString) ? filterString : "Nenhum")}\n=> Includes: {(!string.IsNullOrEmpty(includesString) ? includesString : "Nenhum")}", entity);
			}
			catch (Exception) { }
			return entity;
		}

		public async Task<IEnumerable<T>> GetIncudeAsync(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includes)
		{

			IQueryable<T> query = _dbSet.AsNoTracking(); ;

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

			var entity = await query.ToListAsync();
			try
			{
				string includesString = includes != null ? string.Join(", ", includes.Select(i => i.Body.ToString())) : "Nenhum";
				string filterString = filter != null ? filter.Body.ToString() : "Nenhum";
				LogSistem.LogWriteLog(TypeLog.VIEW, entity.GetType(), $"Filtro:{(!string.IsNullOrEmpty(filterString) ? filterString : "Nenhum")}\n=> Includes: {(!string.IsNullOrEmpty(includesString) ? includesString : "Nenhum")}", entity);
			}
			catch (Exception) { }

			return entity;
		}

		public async Task RemoveAsync(int id)
		{

			var entity = await GetByAsync(e => EF.Property<long?>(e, "ID") == id);

			

			if (entity != null)
			{
				_context.Entry(entity).State = EntityState.Detached;
				_dbSet.Remove(entity);
				LogSistem.LogWriteLog(TypeLog.DELETE, entity.GetType(), "ID = " + id, entity);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<T> UpdateAsync(T entity)
		{
			LogSistem.LogWriteLog(TypeLog.UPDATE, entity.GetType(), null, entity);

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
			LogSistem.LogWriteLog(TypeLog.UPDATE, entity.GetType(), null, entity);
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

			if (keyValue == null || keyValue.Equals(Activator.CreateInstance(key.PropertyInfo.PropertyType)))
			{
				_context.Entry(entity).State = EntityState.Detached; // **Evita rastreamento duplicado**
				var addedEntity = await _dbSet.AddAsync(entity);
				LogSistem.LogWriteLog(TypeLog.INSERT, entity.GetType(), null, entity);
				await _context.SaveChangesAsync();
				return addedEntity.Entity;
			}

			// **🔹 Evita erro de múltiplas instâncias rastreadas**
			var existingTrackedEntity = _context.ChangeTracker.Entries<T>()
				.FirstOrDefault(e => key.PropertyInfo.GetValue(e.Entity).Equals(keyValue));

			if (existingTrackedEntity != null)
			{
				_context.Entry(existingTrackedEntity.Entity).State = EntityState.Detached;
			}

			// **🔹 Busca a entidade no banco SEM rastrear**
			var entities = await _dbSet.AsNoTracking().ToListAsync();
			var existingEntity = entities.FirstOrDefault(e => key.PropertyInfo.GetValue(e).Equals(keyValue));

			if (existingEntity == null)
			{
				var addedEntity = await _dbSet.AddAsync(entity);
				LogSistem.LogWriteLog(TypeLog.INSERT, addedEntity.GetType(), null, addedEntity);
				await _context.SaveChangesAsync();
				return addedEntity.Entity;
			}

			// **🔹 Atualiza a entidade corretamente**
			_context.Entry(entity).State = EntityState.Modified;
			//_context.Entry(entity).CurrentValues.SetValues(existingEntity);
			_context.Entry(existingEntity).CurrentValues.SetValues(entity);
			await _context.SaveChangesAsync();
			LogSistem.LogWriteLog(TypeLog.UPDATE, existingEntity.GetType(), null, existingEntity);
			return entity;
		}


		//public async Task<T> AddOrUpdateAsync(T entity)
		//{
		//	var key = _context.Model.FindEntityType(typeof(T))?
		//		.FindPrimaryKey()?.Properties.FirstOrDefault();

		//	if (key == null)
		//	{
		//		throw new InvalidOperationException("A entidade não possui chave primária definida.");
		//	}

		//	var keyValue = key.PropertyInfo?.GetValue(entity);

		//	// **🔹 Se a chave for nula ou zero, tratamos como novo registro**
		//	if (keyValue == null || keyValue.Equals(Activator.CreateInstance(key.PropertyInfo.PropertyType)))
		//	{
		//		_context.Entry(entity).State = EntityState.Detached; // **⚡ Evita erro de rastreamento**
		//		var addedEntity = await _dbSet.AddAsync(entity);
		//		await _context.SaveChangesAsync();
		//		return addedEntity.Entity;
		//	}

		//	// **🔹 Evita erro de múltiplas instâncias rastreadas**
		//	var existingTrackedEntity = _context.ChangeTracker.Entries<T>()
		//		.FirstOrDefault(e => e.Property(key.Name).CurrentValue.Equals(keyValue));

		//	if (existingTrackedEntity != null)
		//	{
		//		_context.Entry(existingTrackedEntity.Entity).State = EntityState.Detached;
		//	}

		//	// **🔹 Busca a entidade no banco SEM rastrear**
		//	var existingEntity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => key.PropertyInfo.GetValue(e).Equals(keyValue));

		//	if (existingEntity == null)
		//	{
		//		var addedEntity = await _dbSet.AddAsync(entity);
		//		await _context.SaveChangesAsync();
		//		return addedEntity.Entity;
		//	}

		//	// **🔹 Garante que o EF rastreie apenas UMA entidade**
		//	_context.Entry(entity).State = EntityState.Modified;
		//	_context.Entry(entity).CurrentValues.SetValues(existingEntity);

		//	await _context.SaveChangesAsync();
		//	return entity;
		//}


		//public async Task<T> AddOrUpdateAsync(T entity)
		//{
		//	var key = _context.Model.FindEntityType(typeof(T))?
		//		.FindPrimaryKey()?.Properties.FirstOrDefault();

		//	if (key == null)
		//	{
		//		throw new InvalidOperationException("A entidade não possui chave primária definida.");
		//	}

		//	var keyValue = key.PropertyInfo?.GetValue(entity);

		//	// Se a chave for nula ou zero, adicionamos como novo registro
		//	if (keyValue == null || keyValue.Equals(Activator.CreateInstance(key.PropertyInfo.PropertyType)))
		//	{
		//		var addedEntity = await _dbSet.AddAsync(entity);
		//		await _context.SaveChangesAsync();
		//		return addedEntity.Entity;
		//	}



		//	// Busca a entidade existente no banco
		//	var existingEntity = await _dbSet.FindAsync(keyValue);

		//	if (existingEntity == null)
		//	{
		//		var addedEntity = await _dbSet.AddAsync(entity);
		//		await _context.SaveChangesAsync();
		//		return addedEntity.Entity;
		//	}

		//	// Atualiza os valores da entidade existente
		//	_context.Entry(existingEntity).CurrentValues.SetValues(entity);

		//	if (existingEntity == null)
		//	{
		//		throw new InvalidOperationException("Entidade não encontrada para atualização.");
		//	}

		//	// Atualiza os relacionamentos corretamente
		//	foreach (var navigation in _context.Entry(existingEntity).Navigations)
		//	{
		//		if (navigation.Metadata is INavigation navMeta)
		//		{
		//			var newRelatedValue = _context.Entry(entity).Navigation(navMeta.Name).CurrentValue;

		//			if (newRelatedValue != null)
		//			{
		//				if (navMeta.IsCollection)
		//				{
		//					var collectionEntry = _context.Entry(existingEntity).Collection(navMeta.Name);
		//					await collectionEntry.LoadAsync(); // ⚡ Assegura que a coleção foi carregada
		//					collectionEntry.CurrentValue = (IEnumerable<object>)newRelatedValue;
		//				}
		//				else
		//				{
		//					var referenceEntry = _context.Entry(existingEntity).Reference(navMeta.Name);
		//					referenceEntry.CurrentValue = newRelatedValue;
		//				}
		//			}
		//		}
		//	}

		//	await _context.SaveChangesAsync();
		//	return existingEntity;
		//}



	}
}