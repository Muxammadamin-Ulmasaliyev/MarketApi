using MarketApi.Domain;

namespace MarketApi.Data
{
    public interface IUsersRepository
	{
		Task<IEnumerable<AppUser>> GetAll();
		Task<AppUser> GetByName(string username);
		Task<bool> Delete(string username);
		Task<IEnumerable<AppUser>> GetUsersBySearchTerm(string searchTerm);
		Task<IEnumerable<AppUser>> GetUsersOrderBy(string orderBy);


	}
}
