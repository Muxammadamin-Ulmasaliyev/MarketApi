using MarketApi.Domain;
using MarketApi.Models;

namespace MarketApi.MapProfiles
{
	public static class Mapper
	{
		public static ProductModel Map(Product product)
		{
			return new ProductModel
			{

				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				Price = product.Price,
				Quantity = product.Quantity,
				IsInStock = product.IsInStock,
				BrandId = product.BrandId,
				CarId = product.CarId,
				ImageUrl = product.ImageUrl,


				BrandName = product.Brand.Name,
				CarName = product.Car.Company + " " + product.Car.Model


			};
		}

		public static Product Map(ProductModel productModel)
		{
			return new Product
			{
				Id = productModel.Id,
				Name = productModel.Name,
				Description = productModel.Description,
				Price = productModel.Price,
				Quantity = productModel.Quantity,
				IsInStock = productModel.Quantity > 0,
				ImageUrl = "ProductImages/" + String.Concat(productModel.CarName, " ", productModel.Name, " ", productModel.BrandName, ".jpg"),
				BrandId = productModel.BrandId,
				CarId = productModel.CarId
			};
		}
		public static Product Map(int id, ProductModel productModel)
		{
			return new Product
			{
				Id = id,
				Name = productModel.Name,
				Description = productModel.Description,
				Price = productModel.Price,
				Quantity = productModel.Quantity,
				IsInStock = productModel.Quantity > 0,
				ImageUrl = "ProductImages/" + productModel.Image.FileName,
				BrandId = productModel.BrandId,
				CarId = productModel.CarId
			};
		}



		public static BrandModel Map(Brand brand)
		{
			return new BrandModel
			{
				Id = brand.Id,
				Name = brand.Name,
				Description = brand.Description,
				ManufacturedCountry = brand.ManufacturedCountry,
				ImageUrl = brand.ImageUrl
			};
		}
		public static Brand Map(BrandModel brandModel)
		{
			return new Brand
			{
				Id = brandModel.Id,
				Name = brandModel.Name,
				Description = brandModel.Description,
				ManufacturedCountry = brandModel.ManufacturedCountry,
				ImageUrl = "BrandImages/" + brandModel.Name + ".jpg"
			};
		}

		public static Brand Map(int id, BrandModel brandModel)
		{
			return new Brand
			{
				Id = id,
				Name = brandModel.Name,
				Description = brandModel.Description,
				ManufacturedCountry = brandModel.ManufacturedCountry,
				ImageUrl = "BrandImages/" + brandModel.Image.FileName
			};
		}

		public static CarModel Map(Car car)
		{
			return new CarModel
			{
				Id = car.Id,
				Company = car.Company,
				Model = car.Model,
				ManufacturedCountry = car.ManufacturedCountry,
				ImageUrl = car.ImageUrl
			};
		}

		public static Car Map(CarModel carModel)
		{
			return new Car
			{
				Id = carModel.Id,
				Company = carModel.Company,
				Model = carModel.Model,
				ManufacturedCountry = carModel.ManufacturedCountry,
				ImageUrl = "CarImages/" + String.Concat(carModel.Company, " ", carModel.Model, ".jpg")
			};
		}
		public static Car Map(int id, CarModel carModel)
		{
			return new Car
			{
				Id = id,
				Company = carModel.Company,
				Model = carModel.Model,
				ManufacturedCountry = carModel.ManufacturedCountry,
				ImageUrl = "CarImages/" + carModel.Image.FileName
			};
		}



		

	}
}
