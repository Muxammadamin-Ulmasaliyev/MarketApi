using MarketApi.Data;
using MarketApi.Domain;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MarketApi.Services
{
	public class BrandsService : IBrandsService
	{
		private readonly IBrandsRepository _brandsRepository;
        public BrandsService(IBrandsRepository brandsRepository)
        {
            _brandsRepository = brandsRepository;
        }

		public async Task<BrandModel> Create(BrandModel model)
		{
			
			var brand = new Brand
			{
				Id = model.Id,
				Name = model.Name,
				Description = model.Description,
				ManufacturedCountry = model.ManufacturedCountry
			};


			var createdBrand = await _brandsRepository.Create(brand);

			var result = new BrandModel
			{
				Id = createdBrand.Id,
				Name = createdBrand.Name,
				Description = createdBrand.Description,
				ManufacturedCountry = createdBrand.ManufacturedCountry
			};

			return result;
		}

		public async Task<bool> Delete(int id)
		{
			return await _brandsRepository.Delete(id);
		}

		public async Task<BrandModel> Get(int id)
		{
			var brandFromDb = await _brandsRepository.Get(id);
			if(brandFromDb != null)
			{
				var model = new BrandModel
				{
					Id = brandFromDb.Id,
					Name = brandFromDb.Name,
					Description = brandFromDb.Description,
					ManufacturedCountry = brandFromDb.ManufacturedCountry
				};
				return model;
			}
			return null;


		}

		public async Task<IEnumerable<BrandModel>> GetAll()
		{
			var models = new List<BrandModel>();
			var brandsFromDb = await _brandsRepository.GetAll();
			foreach (var brand in brandsFromDb)
			{
				var model = new BrandModel
				{
					Id = brand.Id,
					Name = brand.Name,
					Description = brand.Description,
					ManufacturedCountry = brand.ManufacturedCountry
				};
				models.Add(model);
			}

			return models;
		}


		public async Task<IEnumerable<ProductModel>> GetProductsByBrandId(int brandId)
		{
			var models = new List<ProductModel>();
			var products = await _brandsRepository.GetProductsByBrandId(brandId);

			foreach (var product in products)
			{
				var model = new ProductModel
				{
					Id = product.Id,
					Name = product.Name,
					Description = product.Description,
					Price = product.Price,
					Quantity = product.Quantity,
					IsInStock = product.IsInStock,

					BrandId = product.BrandId
				};
				models.Add(model);
			}

			return models;
			
		}

		public async Task<BrandModel> Update(int id, BrandModel model)
		{
			var brand = new Brand
			{
				Id = id,
				Name = model.Name,
				Description = model.Description,
				ManufacturedCountry = model.ManufacturedCountry
			};

			var updatedBrand = await _brandsRepository.Update(id, brand);
			var result = new BrandModel
			{
				Id = updatedBrand.Id,
				Name = updatedBrand.Name,
				Description = updatedBrand.Description,
				ManufacturedCountry = updatedBrand.ManufacturedCountry
			};

			return result;
		}
	}
}
