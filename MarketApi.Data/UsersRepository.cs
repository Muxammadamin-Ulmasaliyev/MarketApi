using ECommerce.Domain;
using MarketApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace MarketApi.Data
{
    public class UsersRepository : IUsersRepository
	{
		private readonly AppDbContext _appDbContext;
		public UsersRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<bool> Delete(string username)
		{
			var user = await _appDbContext.Users.FirstOrDefaultAsync(u => string.Equals(u.UserName, username));
			if (user != null)
			{
				_appDbContext.Users.Remove(user);
				await _appDbContext.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<IEnumerable<AppUser>> GetAll()
		{
			return await _appDbContext.Users.ToListAsync();

		}

		public async Task<AppUser> GetByName(string username)
		{
			return await _appDbContext.Users.FirstOrDefaultAsync(u => string.Equals(u.UserName, username));
		}

		public async Task<IEnumerable<AppUser>> GetUsersBySearchTerm(string searchTerm)
		{
			if(string.IsNullOrEmpty(searchTerm))
			{
				return await _appDbContext.Users.ToListAsync();
			}
			
			var users = await _appDbContext.Users.Where(u => 
												u.UserName.Contains(searchTerm) || 
												u.Email.Contains(searchTerm))
												.ToListAsync();
			return users;
		}

		public async Task<IEnumerable<AppUser>> GetUsersOrderBy(string orderBy)
		{
			var users = _appDbContext.Users.AsQueryable();
			orderBy = orderBy.ToLower();

			switch (orderBy)
			{
				case "name":
					users = users.OrderBy(u => u.UserName);
					break;
				case "namedescending":
					users = users.OrderByDescending(p => p.UserName);
					break;
				default:
					users = users.OrderBy(p => p.Id);
					break;
			}

			return await users.ToListAsync();
		}
	}
}
