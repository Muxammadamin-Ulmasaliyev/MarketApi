using MarketApi.Domain;
using System.ComponentModel.DataAnnotations;

namespace MarketApi.Models
{
	public class BrandModel
	{
		public int Id { get; set; }

		[MaxLength(30, ErrorMessage = "Name should be shorter than 30 characters")]
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }
		public string Description { get; set; }

		[Required(ErrorMessage = "Country is required")]

		public string ManufacturedCountry { get; set; }

	}
}
