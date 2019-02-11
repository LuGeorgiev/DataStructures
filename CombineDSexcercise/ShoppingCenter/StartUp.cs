using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCenter
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var shoppingCenter = new ProductsRepository();
            int commandNumber = int.Parse(Console.ReadLine());

            for (int i = 0; i < commandNumber; i++)
            {
                string line = Console.ReadLine();
                int firstSpace = line.IndexOf(' ');
                string command = line.Substring(0, firstSpace);
                string[] tokens = line
                    .Substring(firstSpace + 1)
                    .Split(';');

                if (command == "AddProduct")
                {
                    var name = tokens[0];
                    var price = double.Parse(tokens[1]);
                    var producer = tokens[2];
                    var product = new Product(name, price, producer);

                    shoppingCenter.Add(product);
                }
                else if (command == "DeleteProducts")
                {
                    if (tokens.Length == 1)
                    {
                        Console.WriteLine(shoppingCenter.DeleteProducts(tokens[0]));
                    }
                    else
                    {
                        Console.WriteLine(shoppingCenter.DeleteProducts(tokens[0],tokens[1]));

                    }
                }
                else if (command == "FindProductsByName")
                {
                    var result = shoppingCenter.FindProductsByName(tokens[0]).ToList();
                    if (result.Count!=0)
                    {
                        result.ForEach(x=>Console.WriteLine(x));
                    }
                    else
                    {
                        Console.WriteLine("No products found!");
                    }
                }
                else if (command == "FindProductsByProducer")
                {
                    var result = shoppingCenter.FindProductsByProducer(tokens[0]).ToList();
                    if (result.Count != 0)
                    {
                        result.ForEach(x => Console.WriteLine(x));
                    }
                    else
                    {
                        Console.WriteLine("No products found!");
                    }
                }
                else if (command == "FindProductsByPriceRange")
                {
                    var result = shoppingCenter
                        .FindProductsByPriceRange(
                             double.Parse(tokens[0]),
                             double.Parse(tokens[1])
                         )
                         .OrderBy(x=>x)
                         .ToList();
                    if (result.Count != 0)
                    {
                        result.ForEach(x => Console.WriteLine(x));
                    }
                    else
                    {
                        Console.WriteLine("No products found!");
                    }
                }

            }
        }
    }
}
