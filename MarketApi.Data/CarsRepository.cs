using ECommerce.Domain;
using MarketApi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApi.Data
{
	public class CarsRepository : ICarsRepository
	{
		private readonly AppDbContext _appDbContext;
		public CarsRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<bool> CheckIfExists(int id)
		{
			return await _appDbContext.Cars.AnyAsync(c => c.Id == id);
		}

		public async Task<Car> Create(Car car)
		{
			await _appDbContext.Cars.AddAsync(car);
			await _appDbContext.SaveChangesAsync();
			return car;
		}

		public async Task<bool> Delete(int id)
		{
			var carFromDb = await _appDbContext.Cars.FindAsync(id);
			if (carFromDb != null)
			{
				_appDbContext.Cars.Remove(carFromDb);
				await _appDbContext.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<Car> Get(int id)
		{
			return await _appDbContext.Cars.FindAsync(id);
		}

		public async Task<IEnumerable<Car>> GetAll()
		{
			return await _appDbContext.Cars.ToListAsync();
		}

		public async Task<IEnumerable<Car>> GetCarsBySearchTerm(string searchTerm)
		{
			if (string.IsNullOrEmpty(searchTerm))
			{
				return await _appDbContext.Cars.ToListAsync();
			}
			var cars = await _appDbContext.Cars.Where(c => c.Company.Contains(searchTerm) || c.Model.Contains(searchTerm)||c.ManufacturedCountry.Contains(searchTerm)).ToListAsync();
			return cars;
		}

		public async Task<IEnumerable<Car>> GetCarsOrderBy(string orderBy)
		{
			var cars = _appDbContext.Cars.AsQueryable();
			orderBy = orderBy.ToLower();
			switch (orderBy)
			{
				case "company":
					cars = cars.OrderBy(c => c.Company);
					break;
				case "companydescending":
					cars = cars.OrderByDescending(c => c.Company);
					break;
				default:
					cars = cars.OrderBy(c => c.Id);
					break;
			}

			return await cars.ToListAsync();
		}

		public async Task<string> GetImageUrlById(int id)
		{
			var car = await _appDbContext.Cars.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
			if (car == null)
			{
				return null;
			}
			return car.ImageUrl;
		}

		public async Task<IEnumerable<Product>> GetProductsByCarId(int carId)
		{
			return await _appDbContext.Products.Where(p => p.CarId == carId).ToListAsync();

		}

		public async Task<Car> Update(int id, Car car)
		{
			var updatedCar = _appDbContext.Cars.Attach(car);
			updatedCar.State = EntityState.Modified;
			await _appDbContext.SaveChangesAsync();
			return car;
		}
	}
}
