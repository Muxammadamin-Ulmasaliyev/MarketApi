﻿namespace MarketApi.Domain
{
	public class Brand
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ManufacturedCountry { get; set; }

		public ICollection<Product> Products { get; set; }
    }
}
