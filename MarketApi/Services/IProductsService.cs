using MarketApi.Data;
using MarketApi.Domain;
using MarketApi.Models;

namespace MarketApi.Services
{
	public interface IProductsService
	{
		Task<IEnumerable<ProductModel>> GetAll();
		Task<ProductModel> Get(int id);	
		Task<ProductModel> Create(ProductModel model);

		Task<ProductModel> Update(int id, ProductModel model);
		Task<bool> Delete(int id);

		Task<IEnumerable<ProductModel>> GetBySearchTerm(string searchTerm);
		Task<IEnumerable<ProductModel>> FilterProducts(double minPrice, double maxPrice);

		Task<IEnumerable<ProductModel>> GetProductsOrderBy(string orderBy);
	}
}
