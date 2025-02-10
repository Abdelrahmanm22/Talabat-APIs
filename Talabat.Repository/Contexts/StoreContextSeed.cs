using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Contexts
{
    public class StoreContextSeed
    {
        //Function To Seed
        public static async Task SeedAsync(AppDbContext dbContext)
        {
            ///Seeding Brands
            if (!dbContext.ProductBrands.Any())  /// 3shan adddman en el seeding hy7sl mra wa7da bs
            {
                var BrandsData = File.ReadAllText("../Talabat.Repository/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                if (Brands?.Count > 0)
                {                 ///insted of ==> if(Brands is not null && Brands.Count>0)
                    foreach (var Brand in Brands)
                    {
                        await dbContext.Set<ProductBrand>().AddAsync(Brand);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            ///Seeding Types
            if (!dbContext.ProductTypes.Any()) {
                var TypesData = File.ReadAllText("../Talabat.Repository/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        await dbContext.Set<ProductType>().AddAsync(Type);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }

            ///Seeding Products
            if (!dbContext.Products.Any()) {
                var ProductsData = File.ReadAllText("../Talabat.Repository/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (Products?.Count > 0)
                {
                    foreach (var Product in Products)
                    {
                        await dbContext.Set<Product>().AddAsync(Product);
                    }
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
