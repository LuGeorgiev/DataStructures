using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

namespace ShoppingCenter
{
    public class ProductsRepository
    {
        private Dictionary<string,OrderedBag< Product>> byProducer;
        private Dictionary<string,OrderedBag< Product>> byName;
        private OrderedBag<Product> byPrice;

        public ProductsRepository()
        {
            this.byProducer = new Dictionary<string, OrderedBag<Product>>();
            this.byName = new Dictionary<string, OrderedBag<Product>>();
            this.byPrice = new OrderedBag<Product>((x,y)=>x.Price.CompareTo(y.Price));
            //this.byPrice = byPrice = new OrderedBag<Product>(
            //    (x,y)=>x.Price.CompareTo(y.Price)
            //    );
        }

        public void Add(Product product)
        {
            if (!this.byProducer.ContainsKey(product.Producer))
            {
                this.byProducer[product.Producer] = new OrderedBag<Product>();
            }
            this.byProducer[product.Producer].Add( product);

            if (!this.byName.ContainsKey(product.Name))
            {
                this.byProducer[product.Name] = new OrderedBag<Product>();
            }
            this.byName[product.Name].Add( product);


            this.byPrice.Add(product);
        }

        public int DeleteProducts(string producer)
        {
            if (!this.byProducer.ContainsKey(producer))
            {
                return 0;
            }
            IEnumerable<Product> productsToRemove = this.byProducer[producer];
            int count = 0;
            foreach (var product in productsToRemove)
            {
                var name = product.Name;
                this.byName[name].Remove(product);
                this.byPrice.Remove(product);
                count++;
            }

            this.byProducer.Remove(producer);
            return count;
        }

        public int DeleteProducts(string productName, string producer)
        {
            if (!this.byProducer.ContainsKey(producer))
            {
                return 0;
            }
            IEnumerable<Product> productsToRemove = this.byProducer[producer]
                .Where(x => x.Name == productName)
                .ToList();
            int count = 0;
            foreach (var product in productsToRemove)
            {
                var name = product.Name;
                this.byName[name].Remove(product);
                this.byPrice.Remove(product);
                count++;
            }
            if (count != 0)
            {
                this.byProducer.Remove(producer);
            }

            return count;
        }

        public IEnumerable<Product> FindProductsByName(string name)
        {
            if (!this.byName.ContainsKey(name))
            {
                Console.WriteLine("No products found");
                return Enumerable.Empty<Product>();

            }
            return this.byName[name];
        }

        public IEnumerable<Product> FindProductsByProducer(string producer)
        {
            if (!this.byProducer.ContainsKey(producer))
            {
                Console.WriteLine("No products found");
                return Enumerable.Empty<Product>();
            }
            return this.byProducer[producer];
        }

        public IEnumerable<Product> FindProductsByPriceRange(double low, double high)
        {
            //return this.byPrice.Where(x=>x.Price>=low && x.Price<=high);

            return this.byPrice
                .Range(new Product("", low, ""), true, new Product("", high, ""), true)
                .OrderBy(x => x);
        }

    }
}
