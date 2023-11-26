namespace MarketApi.Services
{
    public interface IValidationService
	{
		Task<bool> IsValidBrandAndCarId(int brandId, int carId);
	}
}
