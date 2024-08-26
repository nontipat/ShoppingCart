using ShoppingCart.Models;
using System.Collections.Generic;

namespace ShoppingCart.Function
{
    public class GetCarts
    {
        public static IConfigurationRoot _config = new ConfigurationBuilder()
                                   .SetBasePath(Directory.GetCurrentDirectory())
                                   .AddJsonFile("appsettings.json")
                                   .Build();
        public class Get
        {

            public static List<CartResponseModel> GetAll()
            {
                try
                {
                    using (var context = new shopping_cartContext())
                    {
                        var list = from c in context.Carts.ToList()
                                   join pt in context.Products.ToList() on c.ProductId equals pt.Id into ps_jointable
                                   from p in ps_jointable.DefaultIfEmpty()
                                   select new CartResponseModel  { 
                                       Id = c.Id,
                                       ProductId = p.Id, 
                                       Name= p.Name , 
                                       Amount = c.Amount ,
                                       Price = p.Price ,

                                   };
                        var model = new List<CartResponseModel>();
                        model = list.ToList();
                        return model;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            public static Cart GetById(int id)
            {
                try
                {
                    using (var context = new shopping_cartContext())
                    {
                        var data = context.Carts.Find(id);

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

        public class Manage
        {
            public static bool AddOrUpdate(int productId)
            {
                if (productId == null) throw new ArgumentNullException(nameof(productId));
                try
                {
                    using (var context = new shopping_cartContext())
                    {
                        using (var dbContextTransaction = context.Database.BeginTransaction())
                        {
                            var cart = context.Carts.Where(o => o.ProductId == productId).FirstOrDefault();
                            var product = context.Products.Where(o => o.Id == productId).FirstOrDefault();
                            if (product.Amount == 0)
                            {
                                dbContextTransaction.Commit();
                                return false;
                            }


                            if (cart != null)
                            {
                                cart.Amount = cart.Amount + 1;
                                context.Carts.Update(cart);

                                product.Amount = product.Amount - 1;
                                context.Products.Update(product);
                            }
                            else
                            {
                                context.Carts.Add(new Cart
                                {
                                    ProductId = product.Id,
                                    Amount = 1,
                                });
                                product.Amount = product.Amount - 1;
                                context.Products.Update(product);
                            }
                            context.SaveChanges();
                            dbContextTransaction.Commit();

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            public static bool Delete(int productId)
            {
                try
                {
                    using (var context = new shopping_cartContext())
                    {
                        using (var dbContextTransaction = context.Database.BeginTransaction())
                        {
                            var cart = context.Carts.Where(o => o.ProductId == productId).FirstOrDefault();
                            var product = context.Products.Where(o => o.Id == productId).FirstOrDefault();
                            if (cart != null && cart.Amount !=1)
                            {
                                cart.Amount = cart.Amount - 1;
                                context.Carts.Update(cart);

                                product.Amount = product.Amount + 1;
                                context.Products.Update(product);
                            }
                            else if(cart != null && cart.Amount == 1)
                            {
                                product.Amount = product.Amount + 1;
                                context.Products.Update(product);
                                context.Carts.Remove(cart);
                            }
                            context.SaveChanges();
                            dbContextTransaction.Commit();

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }

            public static bool DeleteAll(int productId)
            {
                try
                {
                    using (var context = new shopping_cartContext())
                    {
                        using (var dbContextTransaction = context.Database.BeginTransaction())
                        {
                            var cart = context.Carts.Where(o => o.ProductId == productId).FirstOrDefault();
                            var product = context.Products.Where(o => o.Id == productId).FirstOrDefault();
                            if (cart != null)
                            {
                                product.Amount = product.Amount + cart.Amount;
                                context.Products.Update(product);
                                context.Carts.Remove(cart);
                            }
                           
                            context.SaveChanges();
                            dbContextTransaction.Commit();

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }

            public static bool DeleteAllCheckout(int productId)
            {
                try
                {
                    using (var context = new shopping_cartContext())
                    {
                        using (var dbContextTransaction = context.Database.BeginTransaction())
                        {
                            var cart = context.Carts.Where(o => o.ProductId == productId).FirstOrDefault();
                            var product = context.Products.Where(o => o.Id == productId).FirstOrDefault();
                            if (cart != null)
                            {
                                context.Carts.Remove(cart);
                            }

                            context.SaveChanges();
                            dbContextTransaction.Commit();

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }
        }
    }
}
