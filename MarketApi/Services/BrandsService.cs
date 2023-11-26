using MarketApi.Data;
using MarketApi.MapProfiles;
using MarketApi.Models;

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


			await _imagesService.SaveImage(model);

			var brand = Mapper.Map(model);



			var createdBrand = await _brandsRepository.Create(brand);

			var result = Mapper.Map(createdBrand);



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
			if (brandFromDb != null)
			{
				var model = Mapper.Map(brandFromDb);


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
				var model = Mapper.Map(brand);


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
				var model = Mapper.Map(brand);


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
				var model = Mapper.Map(brand);


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
				var model = Mapper.Map(product);


				models.Add(model);
			}

			return models;

		}

		public async Task<BrandModel> Update(int id, BrandModel model)
		{
			// There is no validation for if brand exists or no. cos, when frontend developed user can only update existing products

			await _imagesService.UpdateImage(id, model);


			var brand = Mapper.Map(id,model);



			var updatedBrand = await _brandsRepository.Update(id, brand);
			var result = Mapper.Map(updatedBrand);



			return result;
		}
	}
}
