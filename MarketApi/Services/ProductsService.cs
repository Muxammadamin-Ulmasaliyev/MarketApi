using MarketApi.Data;
using MarketApi.Domain;
using MarketApi.Models;

namespace MarketApi.Services
{
	public class ProductsService : IProductsService
	{
		private readonly IProductsRepository _productsRepository;
		public ProductsService(IProductsRepository productsRepository)
		{
			_productsRepository = productsRepository;
		}

		public async Task<ProductModel> Create(ProductModel model)
		{
			var product = new Product
			{
				Id = model.Id,
				Name = model.Name,
				Description = model.Description,
				Price = model.Price,
				Quantity = model.Quantity,
				OutOfStock = model.Quantity <= 0,

				BrandId = model.BrandId,
			};

			var createdProduct = await _productsRepository.Create(product);

			var result = new ProductModel
			{
				Id = createdProduct.Id,
				Name = createdProduct.Name,
				Description = createdProduct.Description,
				Price = createdProduct.Price,
				Quantity = createdProduct.Quantity,
				OutOfStock = createdProduct.OutOfStock,

				BrandId = createdProduct.BrandId
			};

			return result;

		}

		public async Task<bool> Delete(int id)
		{
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
					OutOfStock = productFromDb.OutOfStock,

					BrandId = productFromDb.BrandId
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
					OutOfStock = product.OutOfStock,

					BrandId = product.BrandId
				};
				models.Add(model);
			}

			return models;
		}

		public async Task<ProductModel> Update(int id, ProductModel model)
		{
			var product = new Product
			{
				Id = id,
				Name = model.Name,
				Description = model.Description,
				Price = model.Price,
				Quantity = model.Quantity,
				OutOfStock = model.Quantity <= 0,
				BrandId = model.BrandId
			};
			var updatedProduct = await _productsRepository.Update(id, product);
			var result = new ProductModel
			{
				Id = updatedProduct.Id,
				Name = updatedProduct.Name,
				Description = updatedProduct.Description,
				Price = updatedProduct.Price,
				Quantity = updatedProduct.Quantity,
				OutOfStock = updatedProduct.OutOfStock,

				BrandId = updatedProduct.BrandId
			};
			return result;
		}
	}
}
