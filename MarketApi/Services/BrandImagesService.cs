using MarketApi.Data;
using MarketApi.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MarketApi.Services
{
	public class BrandImagesService : IImagesService<BrandModel>
	{
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly IBrandsRepository _brandsRepository;
		public BrandImagesService(IHostingEnvironment hostingEnvironment, IBrandsRepository brandsRepository)
		{
			_hostingEnvironment = hostingEnvironment;
			_brandsRepository = brandsRepository;
		}
		public async Task<bool> DeleteImage(int id)
		{
			try
			{
				var imageUrlToDelete = await _brandsRepository.GetImageUrlById(id);
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

		public async Task<bool> SaveImage(BrandModel model)
		{
			try
			{
				var directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "BrandImages/");


				if (!Directory.Exists(directoryPath))
				{
					Directory.CreateDirectory(directoryPath);
				}

				var filename = model.Image.FileName;

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

		public async Task<bool> UpdateImage(int id, BrandModel model)
		{
			try
			{
				var directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "BrandImages/");

				var rootDirectoryPath = _hostingEnvironment.WebRootPath;

				if (!Directory.Exists(directoryPath))
				{
					Directory.CreateDirectory(directoryPath);
				}

				var oldBrandsImageUrl = await _brandsRepository.GetImageUrlById(id);
				if (oldBrandsImageUrl == null)
				{
					return false;
				}
				var filePath = Path.Combine(rootDirectoryPath, oldBrandsImageUrl);

				var newFilename = model.Image.FileName;

				if (File.Exists(filePath))
				{
					File.Delete(filePath);
					using (var fileStream = new FileStream(Path.Combine(rootDirectoryPath, "BrandImages/", newFilename), FileMode.Create))
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
