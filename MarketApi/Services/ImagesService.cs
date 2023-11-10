using ECommerce.Domain;
using MarketApi.Data;
using MarketApi.Models;
using System.Runtime.CompilerServices;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MarketApi.Services
{
	public class ImagesService : IImagesService
	{
		private readonly IHostingEnvironment _hostingEnvironment;
		private readonly IProductsRepository _productsRepository;
		public ImagesService(IHostingEnvironment hostingEnvironment, IProductsRepository productsRepository)
		{
			_hostingEnvironment = hostingEnvironment;
			_productsRepository = productsRepository;
		}



		public async Task<bool> SaveImage(ProductModel model)
		{
			try
			{
				var directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "ProductImages/");


				if (!Directory.Exists(directoryPath))
				{
					Directory.CreateDirectory(directoryPath);                                                               
				}

				var filename = model.ProductImage.FileName;

				using (var fileStream = new FileStream(Path.Combine(directoryPath, filename), FileMode.Create))
				{
					await model.ProductImage.CopyToAsync(fileStream);
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<bool> UpdateImage(int id, ProductModel model)
		{
			try
			{
				var directoryPath = Path.Combine(_hostingEnvironment.WebRootPath, "ProductImages/");

				var rootDirectoryPath = _hostingEnvironment.WebRootPath;

				if (!Directory.Exists(directoryPath))
				{
					Directory.CreateDirectory(directoryPath);                                                               
				}

				var oldProductsImageUrl = await _productsRepository.GetImageUrlById(id);

				var filePath = Path.Combine(rootDirectoryPath, oldProductsImageUrl);

				var newFilename = model.ProductImage.FileName;

				if (File.Exists(filePath))
				{
					File.Delete(filePath);
					using (var fileStream = new FileStream(Path.Combine(rootDirectoryPath, "ProductImages/", newFilename), FileMode.Create))
					{
						await model.ProductImage.CopyToAsync(fileStream);
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
		public async Task<bool> DeleteImage(int id)
		{
			try
			{ 
				var imageToDelete = await _productsRepository.GetImageUrlById(id);
				if (imageToDelete == null)
				{
					return false;
				}
				var filePath = Path.Combine(_hostingEnvironment.WebRootPath, imageToDelete);
				if (!File.Exists(filePath))
				{
					return false;
				}
				File.Delete(filePath);
			}
			catch(Exception ex)
			{
				return false;
			}
			return true;
		}


	}
}
