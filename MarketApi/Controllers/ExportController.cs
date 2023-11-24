using MarketApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

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

		[HttpPost("pdf")]

		public async Task<IActionResult> ExportAsPdf()
		{
			if(await _exportService.ExportAsPdf())
			{
				return Ok();
			}
			return Conflict();
		}

		[HttpPost("excel")]

		public async Task<IActionResult> ExportAsExcel()
		{
			if (await _exportService.ExportAsExcel())
			{
				return Ok();
			}
			return Conflict();
		}


	}
}
