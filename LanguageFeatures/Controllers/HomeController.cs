using LanguageFeatures.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Przejście do adresu URL pokazującego przykład";
        }

        public ViewResult AutoProperty()
        {
            // utworzenie nowego obiektu produkt
            Product myProduct = new Product();

            // odczytanie właściwości
            myProduct.Name = "Kajak";

            // wygenerowanie widoku
            return View("Result", (object)String.Format("Nazwa produktu: {0}, productName"));
        }

        public ViewResult CreateProduct()
        {
            Product myProduct = new Product { ProductID = 100, Name = "Kajak", Description = "Łódka jednoosobowa", Price = 275M, Category = "Sporty wodne" };
            return View("Result", (object)String.Format("Kategoria: {0}", myProduct.Category));
        }

        public ViewResult CreateAnonArray()
        {
            var oddsAndEnds = new[]
            {
                    new { Name = "MVC", Category = "Wzorzec"},
                    new { Name = "Kapelusz", Category = "Odzież"},
                    new { Name = "Jabłko", Category = "Owoc"}
            };

            StringBuilder result = new StringBuilder();
            foreach (var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }
            return View("Result", (object)result.ToString());
        }

        public ViewResult FindProducts()
        {
            Product[] products =
            {
                new Product {Name = "Kajak", Category="Sporty wodne", Price = 275M},
                new Product {Name = "Kamizelka ratunkowa", Category="Sporty wodne", Price = 48.95M},
                new Product {Name = "Piłka nożna", Category="Piłka nożna", Price = 19.50M},
                new Product {Name = "Flaga narożna", Category="Piłka nożna", Price = 34.95M}
            };

        //    var foundProducts = from match in products
        //                        orderby match.Price descending
        //                        select new { match.Name, match.Price };

            var foundProducts = products.OrderByDescending(e => e.Price)
                                .Take(3)
                                .Select(e => new { e.Name, e.Price });

            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Cena: {0} ", p.Price);
                if (++count == 3)
                {
                    break;
                }
            }
            return View("Result", (object)result.ToString());
        }

        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product {Name ="Kajak", Price = 275M},
                    new Product {Name ="Kamizelka ratunkowa", Category="Sporty wodne",  Price = 48.95M},
                    new Product {Name ="Piłka nożna", Category="Piłka nożna",  Price = 19.50M},
                    new Product {Name ="Flaga narożna", Category="Piłka nożna",  Price = 34.95M}
                }
            };

            Product[] productArray =
            {
                    new Product {Name ="Kajak", Price = 275M},
                    new Product {Name ="Kamizelka ratunkowa", Price = 48.95M},
                    new Product {Name ="Piłka nożna", Price = 19.50M},
                    new Product {Name ="Flaga narożna", Price = 34.95M}
            };

            decimal total = 0;
            foreach (Product prod in products.Filter(prod => prod.Category == "Piłka nożna" || prod.Price > 20))
            {
                total += prod.Price;
            }
            decimal cartTotal = products.TotalPrices();
            decimal arrayTotal = products.TotalPrices();

            return View("Result", (object)String.Format("Razem koszyk: {0}", total));
        }
    }
}