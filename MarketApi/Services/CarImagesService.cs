using MarketApi.Data;
using MarketApi.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace MarketApi.Services
{
	public class CarImagesService : IImagesService<CarModel>
	{
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly ICarsRepository _carsRepository;
		public CarImagesService(IHostingEnvironment hostingEnvironment, ICarsRepository carsRepository)
		{
			_hostingEnvironment = hostingEnvironment;
			_carsRepository = carsRepository;
		}
		public async Task<bool> DeleteImage(int id)
		{
			try
			{
				var imageUrlToDelete = await _carsRepository.GetImageUrlById(id);
				if (imageUrlToDelete == null)
				{
					return false;
				}
				var filePath = Path.Combine(_hostingEnvironment.WebRootPath, imageUrlToDelete);
				if (!File.Exists(filePath))
				{
					return false;
				}
				File.Delete(filePath);
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}

		public async Task<bool> SaveImage(CarModel model)
		{
			try
			{
				var directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "CarImages/");

				Directory.CreateDirectory(directoryPath);

				var filename = String.Concat(model.Company , " " , model.Model, ".jpg" );

				using (var fileStream = new FileStream(Path.Combine(directoryPath, filename), FileMode.Create))
				{
					await model.Image.CopyToAsync(fileStream);
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<bool> UpdateImage(int id, CarModel model)
		{
			try
			{
				var directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "CarImages/");

				var rootDirectoryPath = _hostingEnvironment.WebRootPath;

				Directory.CreateDirectory(directoryPath);

				var oldCarsImageUrl = await _carsRepository.GetImageUrlById(id);
				if (oldCarsImageUrl == null)
				{
					return false;
				}
				var filePath = Path.Combine(rootDirectoryPath, oldCarsImageUrl);

				var newFilename = String.Concat(model.Company, " ", model.Model, ".jpg");

				if (File.Exists(filePath))
				{
					File.Delete(filePath);
					using (var fileStream = new FileStream(Path.Combine(rootDirectoryPath, "CarImages/", newFilename), FileMode.Create))
					{
						await model.Image.CopyToAsync(fileStream);
					}
					return true;
				}
				else
				{
					return false;
				}

			}
			catch (Exception ex)
			{
				return false;
			}
		}

	}
}
