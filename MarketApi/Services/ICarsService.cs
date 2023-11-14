using MarketApi.Models;

namespace MarketApi.Services
{
	public interface ICarsService
	{
		Task<IEnumerable<CarModel>> GetAll();
		Task<CarModel> Get(int id);
		Task<CarModel> Create(CarModel model);

		Task<CarModel> Update(int id, CarModel model);
		Task<bool> Delete(int id);


		Task<IEnumerable<ProductModel>> GetProductsByCarId(int carId);

		Task<IEnumerable<CarModel>> GetBySearchTerm(string searchTerm);
		Task<IEnumerable<CarModel>> GetCarsOrderBy(string orderBy);
	}
}
