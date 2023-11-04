using ECommerce.Domain;
using MarketApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace MarketApi.Data
{
	public class BrandsRepository : IBrandsRepository
	{
		private readonly AppDbContext _appDbContext;
		public BrandsRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<Brand> Create(Brand brand)
		{
			await _appDbContext.Brands.AddAsync(brand);
			await _appDbContext.SaveChangesAsync();
			return brand;
		}

		public async Task<bool> Delete(int id)
		{
			var brandFromDb = await _appDbContext.Brands.FindAsync(id);
			if (brandFromDb != null)
			{
				_appDbContext.Brands.Remove(brandFromDb);
				await _appDbContext.SaveChangesAsync();	
				return true;
			}
			return false;
		}

		public async Task<Brand> Get(int id)
		{
			return await _appDbContext.Brands.FindAsync(id);
		}

		public async Task<IEnumerable<Brand>> GetAll()
		{
			return await _appDbContext.Brands.ToListAsync();
		}

		public async Task<Brand> Update(int id, Brand brand)
		{
			var updatedBrand = _appDbContext.Brands.Attach(brand);
			updatedBrand.State = EntityState.Modified;
			await _appDbContext.SaveChangesAsync();
			return brand;
		}

		public async Task<IEnumerable<Product>> GetProducts(int brandId)
		{
			return await _appDbContext.Products.Where(p => p.BrandId == brandId).ToListAsync();
		}
	}
}
