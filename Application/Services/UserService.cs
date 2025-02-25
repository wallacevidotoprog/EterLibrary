using EterLibrary.Domain.Entities.DbModels;
using EterLibrary.Domain.Interfaces;

namespace EterLibrary.Application.Services
{
	public class UserService
	{
		private readonly IGenericRepository<UserDbModel> _userRepository;

		public UserService(IGenericRepository<UserDbModel> userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task AddUserAsync(UserDbModel user)
		{
			await _userRepository.AddAsync(user);
		}

		public async Task RemoveAsync(int id)
		{
			await _userRepository.RemoveAsync(id);
		}

		public async Task UpdateAsync(UserDbModel user)
		{
			await _userRepository.UpdateAsync(user);
		}

		public async Task<IEnumerable<UserDbModel>> GetAllUsersAsync()
		{
			return await _userRepository.GetAllAsync();
		}

		public async Task<UserDbModel> GetUserByIdAsync(int id)
		{
			return await _userRepository.GetByIdAsync(id);
		}

	}
}
