using MarketApi.Data;
using MarketApi.Domain;
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
			var imageName = model.Image.FileName;

			await _imagesService.SaveImage(model);

			var car = new Car
			{
				Id = model.Id,
				Company = model.Company,
				Model = model.Model,
				ManufacturedCountry = model.ManufacturedCountry,
				ImageUrl = "CarImages/" + imageName
			};


			var createdCar = await _carsRepository.Create(car);

			var result = new CarModel
			{
				Id = createdCar.Id,
				Company = createdCar.Company,
				Model = model.Model,
				ManufacturedCountry = createdCar.ManufacturedCountry,
				ImageUrl = createdCar.ImageUrl
			};

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
				var model = new CarModel
				{
					Id = carFromDb.Id,
					Company = carFromDb.Company,
					Model = carFromDb.Model,
					ManufacturedCountry = carFromDb.ManufacturedCountry,
					ImageUrl = carFromDb.ImageUrl
				};
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
				var model = new CarModel
				{
					Id = car.Id,
					Company = car.Company,
					Model = car.Model,
					ManufacturedCountry = car.ManufacturedCountry,
					ImageUrl = car.ImageUrl
				};
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
				var model = new CarModel
				{
					Id = car.Id,
					Company = car.Company,
					Model = car.Model,
					ManufacturedCountry = car.ManufacturedCountry,
					ImageUrl = car.ImageUrl
				};
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
				var model = new CarModel
				{
					Id = car.Id,
					Company = car.Company,
					Model = car.Model,
					ManufacturedCountry = car.ManufacturedCountry,
					ImageUrl = car.ImageUrl
				};
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
				var model = new ProductModel
				{
					Id = product.Id,
					Name = product.Name,
					Description = product.Description,
					Price = product.Price,
					Quantity = product.Quantity,
					IsInStock = product.IsInStock,

					BrandId = product.BrandId,
					ImageUrl = product.ImageUrl
				};
				models.Add(model);
			}

			return models;
		}

		public async Task<CarModel> Update(int id, CarModel model)
		{
			await _imagesService.UpdateImage(id, model);


			var car = new Car
			{
				Id = id,
				Company = model.Company,
				Model = model.Model,
				ManufacturedCountry = model.ManufacturedCountry,
				ImageUrl = "CarImages/" + model.Image.FileName
			};

			var updatedCar = await _carsRepository.Update(id, car);
			var result = new CarModel
			{
				Id = updatedCar.Id,
				Company = updatedCar.Company,
				Model = updatedCar.Model,
				ManufacturedCountry = updatedCar.ManufacturedCountry,
				ImageUrl = updatedCar.ImageUrl
			};

			return result;
		}
	}
}
