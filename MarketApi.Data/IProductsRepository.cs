using MarketApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApi.Data
{
	public interface IProductsRepository
	{
		Task<string> GetImageUrlById(int id);
		Task<IEnumerable<Product>> GetAll();
		Task<Product> Get(int id);
		Task<Product> Create(Product product);
		Task<Product> Update(int id, Product product);
		Task<bool> Delete(int id);

		Task<IEnumerable<Product>> GetProductsBySearchTerm(string searchTerm);

		Task<IEnumerable<Product>> FilterProducts(double minPrice, double maxPrice);

		Task<IEnumerable<Product>> GetProductsOrderBy(string orderBy);

	}
}
