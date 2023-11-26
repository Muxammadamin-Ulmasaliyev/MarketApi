using MarketApi.Data;
using MarketApi.MapProfiles;
using MarketApi.Models;

namespace MarketApi.Services
{
	public class UsersService : IUsersService
	{
		private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<bool> Delete(string username)
		{
			return await _usersRepository.Delete(username);
		}

		public async Task<IEnumerable<UserModel>> GetAll()
		{
			var users = new List<UserModel>();
			var usersFromDb = await _usersRepository.GetAll();
			foreach (var user in usersFromDb)
			{
				var model = Mapper.Map(user);
				users.Add(model);
			}

			return users;

		}

		public async Task<UserModel> GetByName(string username)
		{
			var userFromDb = await _usersRepository.GetByName(username);
			if(userFromDb != null)
			{
				var userModel = Mapper.Map(userFromDb);
				return userModel;
			}
			return null;
		}

		public async Task<IEnumerable<UserModel>> GetUsersBySearchTerm(string searchTerm)
		{
			var usersFromDb = await _usersRepository.GetUsersBySearchTerm(searchTerm);
			var users = new List<UserModel>();
			foreach (var user in usersFromDb)
			{
				var model = Mapper.Map(user);
				users.Add(model);
			}
			return users;
		}

		public async Task<IEnumerable<UserModel>> GetUsersOrderBy(string orderBy)
		{
			var usersFromDb = await _usersRepository.GetUsersOrderBy(orderBy);
			var users = new List<UserModel>();
			foreach (var user in usersFromDb)
			{
				var model = Mapper.Map(user);
				users.Add(model);
			}
			return users;
		}
	}
}
