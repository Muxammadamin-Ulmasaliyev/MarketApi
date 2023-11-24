namespace MarketApi.Services
{
	public interface IExportService
	{
		Task<bool> ExportAsPdf();

		Task<bool> ExportAsExcel();
	}
}
