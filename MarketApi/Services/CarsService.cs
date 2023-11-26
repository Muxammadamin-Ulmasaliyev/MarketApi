using MarketApi.Data;
using MarketApi.MapProfiles;
using MarketApi.Models;

namespace MarketApi.Services
{
    public class CarsService : ICarsService
	{
		private readonly ICarsRepository _carsRepository;
		private readonly IImagesService<CarModel> _imagesService;
		public CarsService(ICarsRepository carsRepository, IImagesService<CarModel> imagesService)
		{
			_carsRepository = carsRepository;
			_imagesService = imagesService;
		}
		public async Task<CarModel> Create(CarModel model)
		{

			await _imagesService.SaveImage(model);

			var car = Mapper.Map(model);

			


			var createdCar = await _carsRepository.Create(car);

			var result = Mapper.Map(createdCar);
			

			return result;
		}

		public async Task<bool> Delete(int id)
		{
			if (!await _imagesService.DeleteImage(id))
			{
				return false;
			}
			return await _carsRepository.Delete(id);
		}

		public async Task<CarModel> Get(int id)
		{
			var carFromDb = await _carsRepository.Get(id);
			if (carFromDb != null)
			{
				var model = Mapper.Map(carFromDb);
				
				return model;
			}
			return null;
		}

		public async Task<IEnumerable<CarModel>> GetAll()
		{
			var models = new List<CarModel>();
			var carsFromDb = await _carsRepository.GetAll();
			foreach (var car in carsFromDb)
			{
				var model = Mapper.Map(car);

				
				models.Add(model);
			}

			return models;
		}

		public async Task<IEnumerable<CarModel>> GetCarsOrderBy(string orderBy)
		{
			var models = new List<CarModel>();
			var carsFromDb = await _carsRepository.GetCarsOrderBy(orderBy);

			foreach (var car in carsFromDb)
			{
				var model = Mapper.Map(car);

				models.Add(model);
			}

			return models;
		}

		public async Task<IEnumerable<CarModel>> GetBySearchTerm(string searchTerm)
		{
			var models = new List<CarModel>();
			var carsFromDb = await _carsRepository.GetCarsBySearchTerm(searchTerm);

			foreach (var car in carsFromDb)
			{
				var model = Mapper.Map(car);

				models.Add(model);
			}

			return models;
		}

		public async Task<IEnumerable<ProductModel>> GetProductsByCarId(int carId)
		{
			var models = new List<ProductModel>();
			var products = await _carsRepository.GetProductsByCarId(carId);

			foreach (var product in products)
			{
				var model = Mapper.Map(product);

				
				models.Add(model);
			}

			return models;
		}

		public async Task<CarModel> Update(int id, CarModel model)
		{
			await _imagesService.UpdateImage(id, model);

			var car = Mapper.Map(id,model);
			

			var updatedCar = await _carsRepository.Update(id, car);
			var result = Mapper.Map(updatedCar);
			

			return result;
		}
	}
}
