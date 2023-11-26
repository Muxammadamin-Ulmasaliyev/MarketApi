using MarketApi.Models;
using MarketApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class CarsController : ControllerBase
	{
		private readonly ICarsService _carsService;

		public CarsController(ICarsService carsService)
		{
			_carsService = carsService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _carsService.GetAll());
		}

		[Route("{id:int:min(1)}")]
		[HttpGet]
		public async Task<IActionResult> Get(int id)
		{
			var carFromDb = await _carsService.Get(id);
			if (carFromDb == null)
			{
				return NotFound();
			}
			return Ok(carFromDb);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] CarModel carModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var createdCar = await _carsService.Create(carModel);
			var routeValues = new { id = createdCar.Id };
			return CreatedAtRoute(routeValues, createdCar);
		}

		[Route("{id:int:min(1)}")]
		[HttpPut]
		public async Task<IActionResult> Put(int id, [FromForm] CarModel carModel)
		{
			var updatedCar = await _carsService.Update(id, carModel);
			return Ok(updatedCar);
		}
		[Route("{id:int:min(1)}")]
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			bool deleteResult = await _carsService.Delete(id);
			if (deleteResult)
				return NoContent();
			else
				return NotFound();
		}

		[Route("{carId:int:min(1)}/products")]
		[HttpGet]
		public async Task<IActionResult> GetProductsByCarId(int carId)
		{
			return Ok(await _carsService.GetProductsByCarId(carId));
		}

		[Route("search")]
		[HttpGet]
		public async Task<IActionResult> SearchCars(string? searchTerm)
		{
			var cars = await _carsService.GetBySearchTerm(searchTerm);
			return Ok(cars);
		}

		[Route("sort")]
		[HttpGet]
		public async Task<IActionResult> GetCarsOrderBy(string orderBy)
		{
			var sortedCars = await _carsService.GetCarsOrderBy(orderBy);
			return Ok(sortedCars);
		}

	}
}
