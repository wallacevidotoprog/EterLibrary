using EterLibrary.Domain.Entities.DbModels;

namespace EterLibrary.Domain.Interfaces
{
	public interface IUserRepository<T> : IGenericRepository<T> where T : class
	{
		Task<IEnumerable<T>> GetAllIncudeAsync();
		Task<UserDbModel> GetIncudeAsync(long? id = null);
	}
}
