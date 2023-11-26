using MarketApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketApi.Controllers
{
    [Authorize(Roles = "Admin")]
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

		[HttpDelete]
		public async Task<IActionResult> Delete(string username)
		{
			bool deleteResult = await _usersService.Delete(username);
			if (deleteResult)
				return NoContent();
			else 
				return NotFound();
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _usersService.GetAll());
		}

		[Route("{username}")]
		[HttpGet]
		public async Task<IActionResult> GetByName(string username)
		{
			var user = await _usersService.GetByName(username);
			if(user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}

		[Route("search")]
		[HttpGet]
		public async Task<IActionResult> SearchUsers(string? searchTerm)
		{
			var users = await _usersService.GetUsersBySearchTerm(searchTerm);
			return Ok(users);
		}

		[Route("sort")]
		[HttpGet]
		public async Task<IActionResult> GetUsersOrderBy(string orderBy)
		{
			var sortedUsers = await _usersService.GetUsersOrderBy(orderBy);
			return Ok(sortedUsers);
		}
	}
}
