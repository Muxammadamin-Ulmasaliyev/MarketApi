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
		Task<IEnumerable<Brand>> GetAll();
		Task<Brand> Get(int id);
		Task<Brand> Create(Brand brand);
		Task<Brand> Update(int id, Brand brand);
		Task<bool> Delete(int id);


		Task<IEnumerable<Product>> GetProducts(int brandId);
	}
}
