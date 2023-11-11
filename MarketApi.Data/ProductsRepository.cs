using ECommerce.Domain;
using MarketApi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApi.Data
{
	public class ProductsRepository : IProductsRepository
	{
		private readonly AppDbContext _appDbContext;
		public ProductsRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}


		public async Task<string> GetImageUrlById(int id)
		{
			var product = await _appDbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
			if (product == null)
			{
				return null;
			}
			return product.ImageUrl;
		}


		public async Task<Product> Get(int id)
		{
			return await _appDbContext.Products.Include(p => p.Brand).FirstOrDefaultAsync(p => p.Id == id);
		}

		public async Task<IEnumerable<Product>> GetAll()
		{
			return await _appDbContext.Products.Include(p => p.Brand).ToListAsync();
		}
		public async Task<Product> Create(Product product)
		{
			await _appDbContext.Products.AddAsync(product);
			await _appDbContext.SaveChangesAsync();
			return product;
		}

		public async Task<bool> Delete(int id)
		{
			var productFromDb = await _appDbContext.Products.FindAsync(id);
			if (productFromDb != null)
			{
				_appDbContext.Products.Remove(productFromDb);
				await _appDbContext.SaveChangesAsync();
				return true;
			}
			return false;
		}
		public async Task<Product> Update(int id, Product product)
		{
			var updatedProduct = _appDbContext.Products.Attach(product);
			updatedProduct.State = EntityState.Modified;
			await _appDbContext.SaveChangesAsync();
			return product;
		}

		public async Task<IEnumerable<Product>> GetProductsBySearchTerm(string searchTerm)
		{
			if (string.IsNullOrEmpty(searchTerm))
			{
				return await _appDbContext.Products.Include(p => p.Brand).ToListAsync();
			}
			var products = await _appDbContext.Products.Include(p => p.Brand).Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm)).ToListAsync();
			return products;
		}

		public async Task<IEnumerable<Product>> FilterProducts(double minPrice, double maxPrice)
		{
			var products = await _appDbContext.Products.Include(p => p.Brand).Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToListAsync();
			return products;
		}

		public async Task<IEnumerable<Product>> GetProductsOrderBy(string orderBy)
		{
			var products = _appDbContext.Products.Include(p => p.Brand).AsQueryable();
			orderBy = orderBy.ToLower();
			switch(orderBy)
			{
				case "name":
					products = products.OrderBy(p => p.Name);
					break;
				case "namedescending":
					products = products.OrderByDescending(p => p.Name);
					break;
				case "price":
					products = products.OrderBy(p => p.Price);
					break;
				case "pricedescending":
					products = products.OrderByDescending(p => p.Price);
					break;
				default:
					products = products.OrderBy(p => p.Id);
					break;
			}

			return await products.ToListAsync();
		}
	}
}
