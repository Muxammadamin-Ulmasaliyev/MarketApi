using MarketApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApi.Data
{
	public interface ICarsRepository
	{
		Task<IEnumerable<Car>> GetAll();
		Task<Car> Get(int id);
		Task<Car> Create(Car car);
		Task<Car> Update(int id, Car car);
		Task<bool> Delete(int id);
		Task<bool> CheckIfExists(int id);
		Task<string> GetImageUrlById(int id);

		Task<IEnumerable<Product>> GetProductsByCarId(int carId);


		Task<IEnumerable<Car>> GetCarsBySearchTerm(string searchTerm);
		Task<IEnumerable<Car>> GetCarsOrderBy(string orderBy);





	}
}
