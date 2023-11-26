using System.ComponentModel.DataAnnotations.Schema;

namespace MarketApi.Domain
{
    public class Product
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool IsInStock { get; set; }

        public string ImageUrl { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
