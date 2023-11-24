using MarketApi.Data;
using MarketApi.MapProfiles;
using MarketApi.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
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

		public async Task<bool> ExportAsExcel()
		{
			ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

			var productModels = new List<ProductModel>();
			var productsFromDb = await _productsRepository.GetAll();

			var file = new FileInfo("D:\\Products.xlsx");

			foreach (var product in productsFromDb)
			{
				var productModel = Mapper.Map(product);
				productModels.Add(productModel);
			}



			try
			{
				DeleteIfExists(file);

				using (var package = new ExcelPackage(file))
				{
					var sheet = package.Workbook.Worksheets.Add("ProductsList");

					var range = sheet.Cells["A2"].LoadFromCollection(productModels, true);

					range.AutoFitColumns();

					//Format the header row
					sheet.Cells["A1"].Value = "All Products";
					sheet.Cells["A1:K1"].Merge = true;

					sheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					
					sheet.Row(1).Style.Font.Size = 24;
					sheet.Row(1).Style.Font.Color.SetColor(Color.Blue);
					sheet.Row(1).Height = 30;

					sheet.Row(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					sheet.Row(2).Style.Font.Bold = true;



					await package.SaveAsync();
				}
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}

		private void DeleteIfExists(FileInfo file)
		{
			if (file.Exists)
			{
				file.Delete();
			}
		}

		public async Task<bool> ExportAsPdf()
		{
			var products = await _productsRepository.GetAll();


			QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
			try
			{
				Document.Create(container =>
				{
					container.Page(page =>
					{
						page.Size(PageSizes.A4);
						page.Margin(2, Unit.Centimetre);
						page.PageColor(Colors.White);
						page.DefaultTextStyle(x => x.FontSize(14));

						page.Content()
							.PaddingVertical(1, Unit.Centimetre)
							.Column(x =>
							{
								x.Spacing(10);
								foreach (var product in products)
								{
									x.Item().Text($"{product.Id} {product.Name} {product.Price} {product.Quantity} {product.Brand.Name} {product.Car.Company} {product.Car.Model}");
								}

							});

						page.Footer()
							.AlignCenter()
							.Text(x =>
							{
								x.Span("Page ");
								x.CurrentPageNumber();
							});

					});
				}).GeneratePdf("D:\\1542.pdf");
			}
			catch (Exception ex)
			{
				return false;
			}

			return true;

		}
	}
}
