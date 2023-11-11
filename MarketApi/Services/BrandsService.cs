using MarketApi.Data;
using MarketApi.Domain;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MarketApi.Services
{
	public class BrandsService : IBrandsService
	{
		private readonly IBrandsRepository _brandsRepository;
		private readonly IImagesService<BrandModel> _imagesService;
        public BrandsService(IBrandsRepository brandsRepository, IImagesService<BrandModel> imagesService)
        {
            _brandsRepository = brandsRepository;
			_imagesService = imagesService;
        }

		public async Task<BrandModel> Create(BrandModel model)
		{

			var imageName = model.Image.FileName;

			await _imagesService.SaveImage(model);

			var brand = new Brand
			{
				Id = model.Id,
				Name = model.Name,
				Description = model.Description,
				ManufacturedCountry = model.ManufacturedCountry,
				ImageUrl = "BrandImages/" + imageName
			};


			var createdBrand = await _brandsRepository.Create(brand);

			var result = new BrandModel
			{
				Id = createdBrand.Id,
				Name = createdBrand.Name,
				Description = createdBrand.Description,
				ManufacturedCountry = createdBrand.ManufacturedCountry,
				ImageUrl = createdBrand.ImageUrl
			};

			return result;
		}

		public async Task<bool> Delete(int id)
		{
			if (!await _imagesService.DeleteImage(id))
			{
				return false;
			}
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
					ManufacturedCountry = brandFromDb.ManufacturedCountry,
					ImageUrl = brandFromDb.ImageUrl
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
					ManufacturedCountry = brand.ManufacturedCountry,
					ImageUrl = brand.ImageUrl	
				};
				models.Add(model);
			}

			return models;
		}

		public async Task<IEnumerable<BrandModel>> GetBrandsOrderBy(string orderBy)
		{
			var models = new List<BrandModel>();
			var brandsFromDb = await _brandsRepository.GetBrandsOrderBy(orderBy);

			foreach (var brand in brandsFromDb)
			{
				var model = new BrandModel
				{
					Id = brand.Id,
					Name = brand.Name,
					Description = brand.Description,
					ManufacturedCountry = brand.ManufacturedCountry,
					ImageUrl = brand.ImageUrl
				};
				models.Add(model);
			}

			return models;
		}

		public async Task<IEnumerable<BrandModel>> GetBySearchTerm(string searchTerm)
		{
			var models = new List<BrandModel>();
			var brandsFromDb = await _brandsRepository.GetBrandsBySearchTerm(searchTerm);

			foreach (var brand in brandsFromDb)
			{
				var model = new BrandModel
				{
					Id = brand.Id,
					Name = brand.Name,
					Description = brand.Description,
					ManufacturedCountry = brand.ManufacturedCountry,
					ImageUrl = brand.ImageUrl
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

					BrandId = product.BrandId,
					ImageUrl= product.ImageUrl
				};
				models.Add(model);
			}

			return models;
			
		}

		public async Task<BrandModel> Update(int id, BrandModel model)
		{

			await _imagesService.UpdateImage(id, model);


			var brand = new Brand
			{
				Id = id,
				Name = model.Name,
				Description = model.Description,
				ManufacturedCountry = model.ManufacturedCountry,
				ImageUrl = "BrandImages/" + model.Image.FileName
			};

			var updatedBrand = await _brandsRepository.Update(id, brand);
			var result = new BrandModel
			{
				Id = updatedBrand.Id,
				Name = updatedBrand.Name,
				Description = updatedBrand.Description,
				ManufacturedCountry = updatedBrand.ManufacturedCountry,
				ImageUrl = updatedBrand.ImageUrl
			};

			return result;
		}
	}
}
