using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MarketApi.Models
{
	public class CarModel
	{
		public int Id { get; set; }

        [Required(ErrorMessage = "Company name is required")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string ManufacturedCountry { get; set; }



		[Required(ErrorMessage = "Image is required")]
		public IFormFile Image { get; set; }
		public string ImageUrl { get; set; } = string.Empty;

		


	}
}
