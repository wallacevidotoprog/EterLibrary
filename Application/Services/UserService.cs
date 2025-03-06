using EterLibrary.Domain.Entities.DbModels;
using EterLibrary.Domain.Interfaces;
using EterLibrary.Infrastructure.Repositories;
using System.Linq.Expressions;

namespace EterLibrary.Application.Services
{
	public class UserService : IUserRepository<UserDbModel>
	{
		private readonly IUserRepository<UserDbModel> _userRepository;

		public UserService()
		{
			_userRepository = new UserRepository();
		}

		public async Task<UserDbModel> AddAsync(UserDbModel user)
		{
			return await _userRepository.AddAsync(user);
		}

		public async Task RemoveAsync(int id)
		{
			await _userRepository.RemoveAsync(id);
		}

		public async Task UpdateAsync(UserDbModel user)
		{
			await _userRepository.UpdateAsync(user);
		}

		public async Task<IEnumerable<UserDbModel>> GetAllIncudeAsync()
		{
			return await _userRepository.GetAllIncudeAsync();
		}

		public async Task<UserDbModel> GetIncudeAsync(long? id = null)
		{
			return await _userRepository.GetIncudeAsync(id);
		}

		public async Task<IEnumerable<UserDbModel>> GetAllAsync(Expression<Func<UserDbModel, bool>>? filter = null, params Expression<Func<UserDbModel, object>>[]? includes)
		{
			return await _userRepository.GetAllAsync(filter, includes);
		}

		public async Task<IEnumerable<UserDbModel>> GetIncudeAsync(Expression<Func<UserDbModel, bool>>? filter = null, params Expression<Func<UserDbModel, object>>[] includes)
		{
			return await _userRepository.GetIncudeAsync(filter, includes);
		}

		public async Task<UserDbModel> GetByAsync(Expression<Func<UserDbModel, bool>>? filter = null, params Expression<Func<UserDbModel, object>>[]? includes)
		{
			return await _userRepository.GetByAsync(filter, includes);
		}

		public async Task<UserDbModel> AddOrUpdateAsync(UserDbModel entity)
		{
			return await _userRepository.AddOrUpdateAsync(entity);
		}
	}
}
