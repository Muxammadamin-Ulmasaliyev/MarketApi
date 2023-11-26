using MarketApi.Models;
using MarketApi.Services;
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
		public async Task<IActionResult> Post([FromForm] BrandModel model)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var createdBrand = await _brandsService.Create(model);
			var routeValues = new { id = createdBrand.Id };
			return CreatedAtRoute(routeValues, createdBrand);
		}

		[Route("{id:int:min(1)}")]
		[HttpPut]
		public async Task<IActionResult> Put(int id, [FromForm] BrandModel model)
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

		[Route("{brandId:int:min(1)}/products")]
		[HttpGet]
		public async Task<IActionResult> GetProductsByBrandId(int brandId)
		{
			return Ok(await _brandsService.GetProductsByBrandId(brandId));
		}


		[Route("search")]
		[HttpGet]
		public async Task<IActionResult> SearchBrands(string? searchTerm)
		{
			var brands = await _brandsService.GetBySearchTerm(searchTerm);
			return Ok(brands);
		}


		[Route("sort")]
		[HttpGet]
		public async Task<IActionResult> GetBrandsOrderBy(string orderBy)
		{
			var sortedBrands = await _brandsService.GetBrandsOrderBy(orderBy);
			return Ok(sortedBrands);
		}
	}
}
