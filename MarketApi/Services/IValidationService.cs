namespace MarketApi.Services
{
	public interface IValidationService
	{
		Task<bool> IsValidBrandId(int brandId);
	}
}
