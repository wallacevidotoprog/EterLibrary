using EterLibrary.Domain.Entities.DbModels;
using EterLibrary.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EterLibrary.Infrastructure.Repositories
{
	public class UserRepository : GenericRepository<UserDbModel>, IUserRepository<UserDbModel>
	{
		public async Task<IEnumerable<UserDbModel>> GetAllIncudeAsync()
		{
			return await _dbSet
				.Include(p => p.Position)
				.Include(p => p.Category)
				.ToListAsync() ;


		}
		public async Task<UserDbModel> GetIncudeAsync(long? id = null)
		{
			return await _dbSet
				.Include(p => p.Position)
				.Include(p => p.Category)
				.FirstOrDefaultAsync(p => p.ID == id);
		}
	}
}
