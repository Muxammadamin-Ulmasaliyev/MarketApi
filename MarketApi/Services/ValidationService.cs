using MarketApi.Data;

namespace MarketApi.Services
{
	public class ValidationService : IValidationService
	{ 

		private readonly IBrandsRepository _brandsRepository;
        public ValidationService(IBrandsRepository brandsRepository)
        {
			_brandsRepository = brandsRepository;
        }
        public async Task<bool> IsValidBrandId(int brandId)
		{
			return await _brandsRepository.CheckIfExists(brandId);
		}
	}
}
