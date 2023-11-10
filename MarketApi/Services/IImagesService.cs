using MarketApi.Models;

namespace MarketApi.Services
{
	public interface IImagesService
	{
		public Task<bool> SaveImage(ProductModel model);
		public Task<bool> UpdateImage(int id,ProductModel model);

		public Task<bool> DeleteImage(int id);
	}
}
