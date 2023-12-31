﻿using MarketApi.Models;
using MarketApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductsService _productsService;
		private readonly Microsoft.Extensions.Hosting.IHostingEnvironment _hostingEnvironment;
		public ProductsController(IProductsService productsService, Microsoft.Extensions.Hosting.IHostingEnvironment hostingEnvironment)
		{
			_productsService = productsService;
			_hostingEnvironment = hostingEnvironment;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			return Ok(await _productsService.GetAll());
		}

		[Route("{id:int:min(1)}")]
		[HttpGet]
		public async Task<IActionResult> Get(int id)
		{
			var productFromDb = await _productsService.Get(id);
			if(productFromDb == null)
			{
				return NotFound();
			}
			return Ok(productFromDb);
		}

		[HttpPost]

		//[Authorize(Roles = "Admin")]	****************************************************************************************************************
		public async Task<IActionResult> Post([FromForm] ProductModel model)
		{
			if(!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var createdProduct = await _productsService.Create(model);
			var routeValues = new { id = createdProduct.Id };
			return CreatedAtRoute(routeValues, createdProduct);
		}

		[Route("{id:int:min(1)}")]
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromForm] ProductModel model)
        {

            var updatedProduct = await _productsService.Update(id, model);
            return Ok(updatedProduct);
        }

		[Route("{id:int:min(1)}")]
		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{

			bool deleteResult = await _productsService.Delete(id);
			if (deleteResult)
				return NoContent();
			else
				return NotFound();
		}

		[Route("search")]
		[HttpGet]
		public async Task<IActionResult> SearchProducts(string? searchTerm)
		{
			var products = await _productsService.GetBySearchTerm(searchTerm);
			return Ok(products);
		}

		[Route("filter")]
		[HttpGet]
		public async Task<IActionResult> FilterProducts(double minPrice, double maxPrice)
		{
			var filteredProducts = await _productsService.FilterProducts(minPrice, maxPrice);
			return Ok(filteredProducts);
		}

		[Route("sort")]
		[HttpGet]
		public async Task<IActionResult> GetProductsOrderBy(string orderBy)
		{
			var sortedProducts = await _productsService.GetProductsOrderBy(orderBy);
			return Ok(sortedProducts);
		}
	}
}


