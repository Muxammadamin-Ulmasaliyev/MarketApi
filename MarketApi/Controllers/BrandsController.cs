using MarketApi.Models;
using MarketApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BrandsController : ControllerBase
	{
		private readonly IBrandsService _brandsService;

		public BrandsController(IBrandsService brandsService)
		{
			_brandsService = brandsService;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _brandsService.GetAll());
		}

		[Route("{id:int:min(1)}")]
		[HttpGet]
		public async Task<IActionResult> Get(int id)
		{
			var brandFromDb = await _brandsService.Get(id);
			if (brandFromDb == null)
			{
				return NotFound();
			}
			return Ok(brandFromDb);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] BrandModel model)
		{
			var createdBrand = await _brandsService.Create(model);
			var routeValues = new { id = createdBrand.Id };
			return CreatedAtRoute(routeValues, createdBrand);
		}

		[Route("{id:int:min(1)}")]
		[HttpPut]
		public async Task<IActionResult> Put(int id, [FromBody] BrandModel model)
		{
			var updatedBrand = await _brandsService.Update(id, model);
			return Ok(updatedBrand);
		}


		[Route("{id:int:min(1)}")]
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{

			bool deleteResult = await _brandsService.Delete(id);
			if (deleteResult)
				return NoContent();
			else
				return NotFound();
		}

		[Route("{brandId:int:min(1)}")]
		[HttpGet]
		public async Task<IActionResult> GetProducts(int brandId)
		{
			return Ok(await _brandsService.GetProducts(brandId));
		}
	}
}
