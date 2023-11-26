using MarketApi.Domain;
using MarketApi.Models;

namespace MarketApi.Services
{
	public interface IUsersService
	{
		Task<IEnumerable<UserModel>> GetAll();
		Task<UserModel> GetByName(string username);
		Task<bool> Delete(string username);
		Task<IEnumerable<UserModel>> GetUsersBySearchTerm(string searchTerm);
		Task<IEnumerable<UserModel>> GetUsersOrderBy(string orderBy);
	}
}
