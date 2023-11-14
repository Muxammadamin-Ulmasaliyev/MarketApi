using MarketApi.Data;
using MarketApi.Domain;
using MarketApi.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MarketApi.Services
{
	public class ProductsService : IProductsService
	{
		private readonly IProductsRepository _productsRepository;
		private readonly IValidationService _validationService;
		private readonly IImagesService<ProductModel> _imagesService;
		public ProductsService(IProductsRepository productsRepository, IValidationService validationService, IImagesService<ProductModel> imagesService)
		{
			_productsRepository = productsRepository;
			_validationService = validationService;
			_imagesService = imagesService;
		}

		public async Task<ProductModel> Create(ProductModel model)
		{

			if (!await _validationService.IsValidBrandAndCarId(model.BrandId, model.CarId))
			{
				throw new Exception("Invalid BrandId or CarId");
			}

			var imageName = model.Image.FileName;

			await _imagesService.SaveImage(model);


			var product = new Product
			{
				Id = model.Id,
				Name = model.Name,
				Description = model.Description,
				Price = model.Price,
				Quantity = model.Quantity,
				IsInStock = model.Quantity > 0,
				BrandId = model.BrandId,
				ImageUrl = "ProductImages/" + imageName,
				CarId = model.CarId,
			};

			var createdProduct = await _productsRepository.Create(product);

			var result = new ProductModel
			{
				Id = createdProduct.Id,
				Name = createdProduct.Name,
				Description = createdProduct.Description,
				Price = createdProduct.Price,
				Quantity = createdProduct.Quantity,
				IsInStock = createdProduct.IsInStock,
				BrandId = createdProduct.BrandId,
				ImageUrl = createdProduct.ImageUrl,
				CarId = createdProduct.CarId
			};

			return result;

		}

		public async Task<bool> Delete(int id)
		{
			if (!await _imagesService.DeleteImage(id))
			{
				return false;
			}
			return await _productsRepository.Delete(id);
		}

		public async Task<ProductModel> Get(int id)
		{
			var productFromDb = await _productsRepository.Get(id);
			if (productFromDb != null)
			{
				var model = new ProductModel
				{
					Id = productFromDb.Id,                                                          // FIX
					Name = productFromDb.Name,
					Description = productFromDb.Description,
					Price = productFromDb.Price,
					Quantity = productFromDb.Quantity,
					IsInStock = productFromDb.IsInStock,
					BrandId = productFromDb.BrandId,
					ImageUrl = productFromDb.ImageUrl,
					CarId = productFromDb.CarId
				};
				return model;
			}
			return null;
		}

		public async Task<IEnumerable<ProductModel>> GetAll()
		{
			var models = new List<ProductModel>();
			var productsFromDb = await _productsRepository.GetAll();
			foreach (var product in productsFromDb)
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
					ImageUrl = product.ImageUrl,
					CarId = product.CarId
				};
				models.Add(model);
			}

			return models;
		}

		public async Task<ProductModel> Update(int id, ProductModel model)
		{


			await _imagesService.UpdateImage(id, model);

			var product = new Product
			{
				Id = id,
				Name = model.Name,
				Description = model.Description,
				Price = model.Price,
				Quantity = model.Quantity,
				IsInStock = model.Quantity > 0,
				BrandId = model.BrandId,
				ImageUrl = "ProductImages/" + model.Image.FileName,
				CarId= model.CarId
			};

			var updatedProduct = await _productsRepository.Update(id, product);
			var result = new ProductModel
			{
				Id = updatedProduct.Id,
				Name = updatedProduct.Name,
				Description = updatedProduct.Description,
				Price = updatedProduct.Price,
				Quantity = updatedProduct.Quantity,
				IsInStock = updatedProduct.IsInStock,

				BrandId = updatedProduct.BrandId,
				ImageUrl = updatedProduct.ImageUrl,
				CarId = updatedProduct.CarId
			};
			return result;
		}

		public async Task<IEnumerable<ProductModel>> GetBySearchTerm(string searchTerm)
		{
			var productsFromDb = await _productsRepository.GetProductsBySearchTerm(searchTerm);
			var models = new List<ProductModel>();

			foreach (var product in productsFromDb)
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
					ImageUrl = product.ImageUrl,
					CarId = product.CarId
				};
				models.Add(model);
			}

			return models;
		}

		public async Task<IEnumerable<ProductModel>> FilterProducts(double minPrice, double maxPrice)
		{
			var models = new List<ProductModel>();

			var productsFromDb = await _productsRepository.FilterProducts(minPrice, maxPrice);

			foreach (var product in productsFromDb)
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
					ImageUrl = product.ImageUrl,
					CarId = product.CarId
				};
				models.Add(model);
			}

			return models;

		}

		public async Task<IEnumerable<ProductModel>> GetProductsOrderBy(string orderBy)
		{
			var models = new List<ProductModel>();
			var productsFromDb = await _productsRepository.GetProductsOrderBy(orderBy);

			foreach (var product in productsFromDb)
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
					ImageUrl = product.ImageUrl,
					CarId = product.CarId
				};
				models.Add(model);
			}

			return models;
		}
	}
}
