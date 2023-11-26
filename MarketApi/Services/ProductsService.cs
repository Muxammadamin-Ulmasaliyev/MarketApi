using MarketApi.Data;
using MarketApi.MapProfiles;
using MarketApi.Models;

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


			await _imagesService.SaveImage(model);


			var product = Mapper.Map(model);


			#region Will be fixed soon . . .

			var createdProduct = await _productsRepository.Create(product);
			var createdProductGettedFromDb = await _productsRepository.Get(createdProduct.Id);
			var result = Mapper.Map(createdProductGettedFromDb);

			#endregion

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
				var model = Mapper.Map(productFromDb);

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
				var model = Mapper.Map(product);
				models.Add(model);
			}

			return models;
		}

		public async Task<ProductModel> Update(int id, ProductModel model)
		{


			await _imagesService.UpdateImage(id, model);

			var product = Mapper.Map(id, model);

			#region Will be fixed soon . . .

			var updatedProduct = await _productsRepository.Update(id, product);
			var updatedProductGettedFromDb = await _productsRepository.Get(updatedProduct.Id);
			var result = Mapper.Map(updatedProduct);
			#endregion
			return result;
		}

		public async Task<IEnumerable<ProductModel>> GetBySearchTerm(string searchTerm)
		{
			var productsFromDb = await _productsRepository.GetProductsBySearchTerm(searchTerm);
			var models = new List<ProductModel>();

			foreach (var product in productsFromDb)
			{
				var model = Mapper.Map(product);


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
				var model = Mapper.Map(product);

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
				var model = Mapper.Map(product);


				models.Add(model);
			}

			return models;
		}
	}
}
