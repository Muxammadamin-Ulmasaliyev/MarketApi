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

		public async Task<string> GetImageUrlById(int id)
		{
			var brand = await _appDbContext.Brands.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
			if(brand == null)
			{
				return null;
			}
			return brand.ImageUrl;
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


		public async Task<bool> CheckIfExists(int id)
		{
			return await _appDbContext.Brands.AnyAsync(b => b.Id == id);
		}

		public async Task<IEnumerable<Product>> GetProductsByBrandId(int brandId)
		{
			return await _appDbContext.Products.Where(p => p.BrandId == brandId).ToListAsync();
		}

		public async Task<IEnumerable<Brand>> GetBrandsBySearchTerm(string searchTerm)
		{
			if (string.IsNullOrEmpty(searchTerm))
			{
				return await _appDbContext.Brands.ToListAsync();
			}
			var brands = await _appDbContext.Brands.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm) || p.ManufacturedCountry.Contains(searchTerm)).ToListAsync();
			return brands;
		}

		

		public async Task<IEnumerable<Brand>> GetBrandsOrderBy(string orderBy)
		{
			var brands = _appDbContext.Brands.AsQueryable();
			orderBy = orderBy.ToLower();
			switch (orderBy)
			{
				case "name":
					brands = brands.OrderBy(b => b.Name);
					break;
				case "namedescending":
					brands = brands.OrderByDescending(b => b.Name);
					break;
				case "country":
					brands = brands.OrderBy(b => b.ManufacturedCountry);
					break;
				case "countrydescending":
					brands = brands.OrderByDescending(b => b.ManufacturedCountry);
					break;
				default:
					brands = brands.OrderBy(b => b.Id);
					break;
			}

			return await brands.ToListAsync();
		}
	}
}
