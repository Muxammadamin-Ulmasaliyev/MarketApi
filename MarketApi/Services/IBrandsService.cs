using MarketApi.Models;

namespace MarketApi.Services
{
    public interface IBrandsService
	{
		Task<IEnumerable<BrandModel>> GetAll();
		Task<BrandModel> Get(int id);
		Task<BrandModel> Create(BrandModel model);

		Task<BrandModel> Update(int id, BrandModel model);
		Task<bool> Delete(int id);


		Task<IEnumerable<ProductModel>> GetProductsByBrandId(int brandId);

		Task<IEnumerable<BrandModel>> GetBySearchTerm(string searchTerm);
		Task<IEnumerable<BrandModel>> GetBrandsOrderBy(string orderBy);


	}
}
