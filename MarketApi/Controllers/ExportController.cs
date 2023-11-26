using MarketApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ExportController : ControllerBase
	{
		private readonly IExportService _exportService;

		public ExportController(IExportService exportService) 
		{
			_exportService = exportService;
		}

		

		[HttpGet("excel/download")]

		public async Task<IActionResult> ExportAsExcel()
		{
            var fileContents = await _exportService.ExportAsExcel();
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = $"Products_{DateTime.Now}.xlsx";

            return File(fileContents, contentType, fileName);
        }


	}
}
