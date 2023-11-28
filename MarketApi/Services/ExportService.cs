using MarketApi.Data;
using MarketApi.MapProfiles;
using MarketApi.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace MarketApi.Services
{
    public class ExportService : IExportService
	{
		private readonly IProductsRepository _productsRepository;
		public ExportService(IProductsRepository productsRepository)
		{
			_productsRepository = productsRepository;
		}

		public async Task<byte[]> ExportAsExcel()
		{
			ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

			var productModels = new List<ProductModel>();
			var productsFromDb = await _productsRepository.GetAll();


			foreach (var product in productsFromDb)
			{
				var productModel = Mapper.Map(product);
				productModels.Add(productModel);
			}

			try
			{

				using (var package = new ExcelPackage())
				{
					var sheet = package.Workbook.Worksheets.Add("ProductsList");

					var range = sheet.Cells["A2"].LoadFromCollection(productModels, true);

					range.AutoFitColumns();

					//Format the header row
					sheet.Cells["A1"].Value = "All Products";
					sheet.Cells["A1:L1"].Merge = true;


					sheet.Cells["M1"].Value = "Date : ";
					sheet.Cells["M1:N1"].Merge = true;
					sheet.Cells["M2"].Value = DateTime.Now.Date.ToString("dd/MM/yyyy");
					sheet.Cells["M2:N2"].Merge = true;

					sheet.Cells["M1"].AutoFitColumns();
					sheet.Cells["M2"].AutoFitColumns();


					sheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

					sheet.Row(1).Style.Font.Size = 24;
					sheet.Row(1).Style.Font.Color.SetColor(Color.Blue);
					sheet.Row(1).Height = 30;

					sheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					sheet.Row(2).Style.Font.Bold = true;



					await package.SaveAsync();

					return package.GetAsByteArray();
				}
			}
			catch (Exception ex)
			{
				throw new Exception("Error in ExportService.cs");
			}
		}

		
	}
}
