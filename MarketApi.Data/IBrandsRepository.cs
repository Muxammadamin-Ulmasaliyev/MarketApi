using MarketApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApi.Data
{
	public interface IBrandsRepository
	{
		Task<string> GetImageUrlById(int id);

		Task<IEnumerable<Brand>> GetAll();
		Task<Brand> Get(int id);
		Task<Brand> Create(Brand brand);
		Task<Brand> Update(int id, Brand brand);
		Task<bool> Delete(int id);

		Task<bool> CheckIfExists(int id);

		Task<IEnumerable<Product>> GetProductsByBrandId(int brandId);


		Task<IEnumerable<Brand>> GetBrandsBySearchTerm(string searchTerm);


		Task<IEnumerable<Brand>> GetBrandsOrderBy(string orderBy);
	}
}
