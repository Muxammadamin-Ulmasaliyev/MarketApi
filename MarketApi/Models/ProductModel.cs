using System.ComponentModel.DataAnnotations;

namespace MarketApi.Models
{
	public class ProductModel
	{
		public int Id { get; set; }
		[MaxLength(30, ErrorMessage = "Name should be shorter than 30 characters")]
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }
		public string Description { get; set; }
		[Required(ErrorMessage = "Price is required")]
		public double Price { get; set; }
		[Required(ErrorMessage = "Quantity is required")]

		public int Quantity { get; set; }
		public bool IsInStock { get; set; }

		[Required]
		public IFormFile Image { get; set; }


		[Required]
		public int BrandId { get; set; }
        [Required]
        public int CarId { get; set; }

        public string ImageUrl { get; set; } = string.Empty;




        




    }
}
