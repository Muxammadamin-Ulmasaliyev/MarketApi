using MarketApi.Models;

namespace MarketApi.Services
{
	public interface IImagesService<T> where T : class
	{
		public Task<bool> SaveImage(T model);
		public Task<bool> UpdateImage(int id, T model);

		public Task<bool> DeleteImage(int id);
	}
}
