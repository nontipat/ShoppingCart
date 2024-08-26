using ShoppingCart.Models;

namespace ShoppingCart.Function
{
    public class GetProducts
    {
        public static IConfigurationRoot _config = new ConfigurationBuilder()
                                   .SetBasePath(Directory.GetCurrentDirectory())
                                   .AddJsonFile("appsettings.json")
                                   .Build();
        public class Get
        {

            public static List<Product> GetAll()
            {
                try
                {
                    using (var context = new shopping_cartContext())
                    {
                        var data = context.Products.ToList();

                        return data;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            public static Product GetById(int id)
            {
                try
                {
                    using (var context = new shopping_cartContext())
                    {
                        var data = context.Products.Find(id);

                        return data;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return null;
                }
            }
        }
    }
}
