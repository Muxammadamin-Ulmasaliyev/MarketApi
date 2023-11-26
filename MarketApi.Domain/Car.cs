namespace MarketApi.Domain
{
    public class Car
	{
        public int Id { get; set; }
		public string Company { get; set; }
        public string Model { get; set; }
        public string ManufacturedCountry { get; set; }
        public string ImageUrl { get; set; }



        public ICollection<Product> Products { get; set; }
    }
}
