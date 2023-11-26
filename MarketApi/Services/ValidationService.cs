using MarketApi.Data;

namespace MarketApi.Services
{
    public class ValidationService : IValidationService
	{ 

		private readonly IBrandsRepository _brandsRepository;
		private readonly ICarsRepository _carsRepository;
        public ValidationService(IBrandsRepository brandsRepository, ICarsRepository carsRepository)
        {
			_brandsRepository = brandsRepository;
			_carsRepository = carsRepository;
        }
        public async Task<bool> IsValidBrandAndCarId(int brandId, int carId)
		{
			return await _brandsRepository.CheckIfExists(brandId) && await _carsRepository.CheckIfExists(carId);
		}
	}
}
