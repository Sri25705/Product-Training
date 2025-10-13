using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

    public class Product
    {
        public required string Name { get; set; } 
        public required string Category { get; set; }
        public double Price { get; set; }
    }
    public class Program
    {
        public static void Main(string[]args)
        {
            List<Product> products=new List<Product>
        {
            new Product{Name="Ipad",Category="Electronics",Price=30000},
            new Product{Name="Pen",Category="Stationery",Price=40},
            new Product{Name="Pencil",Category="Stationery",Price=30},
            new Product{Name="Bluetooth",Category="Electronics",Price=4000},
            new Product{Name="Laptop",Category="Electronics",Price=20000}
        };
        Console.WriteLine("enter a category(Electronics/Stationery)");
        String searchCategory = Console.ReadLine()!;
            var SelectedProducts = from p in products
                                   where p.Category==searchCategory
                                   select p;

        if (SelectedProducts.Any())
        {
            Console.WriteLine($"Products in '{searchCategory}'Category:");
            foreach (var p in SelectedProducts)
            {
                Console.WriteLine($"{p.Name} : {p.Price}");
            }

            double averagePrice = SelectedProducts.Average(p => p.Price);
            Console.WriteLine($"\n Average Price in {searchCategory} category: {averagePrice}");
        }
        else
        {
            Console.WriteLine($"\n No Products found in category'{searchCategory}'");
        }
            var groupByCategory = from p in products
                                  group p by p.Category into g
                                  orderby g.Count() descending
                                  select new
                                  {
                                      Category = g.Key,
                                      Count = g.Count(),
                                      AvgPrice = g.Average(x => x.Price)
                                  };
        Console.WriteLine("\nProduct grouped by category");
        foreach (var g in groupByCategory)
        {
            Console.WriteLine($"{g.Category} - {g.Count} products - Avg price:{g.AvgPrice:F2}");
        }
    }
}