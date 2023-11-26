namespace MarketApi.Services
{
    public interface IExportService
	{

		Task<byte[]> ExportAsExcel();
	}
}
