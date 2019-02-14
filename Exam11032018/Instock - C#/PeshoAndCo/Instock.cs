using System;
using System.Collections;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using System.Linq;

public class Instock : IProductStock
{
    private List<string> lablesByInput = new List<string>(512);
    private SortedSet<string> labelsAlphabetoc = new SortedSet<string>();
    private Dictionary<string, Product> byLable = new Dictionary<string, Product>();
    //private SortedSet<Product> byPrice = new
    public int StepCounter { get; set; } = 0;

    public int Count => this.lablesByInput.Count;

    public void Add(Product product)
    {
        product.Stamp = this.StepCounter;
        this.StepCounter++;
        var label = product.Label;
        if (this.byLable.ContainsKey(label))
        {
            throw new ArgumentException("Label should be unique");
        }
        
        this.labelsAlphabetoc.Add(label);

        this.lablesByInput.Add(label);
        this.byLable[label] = product;
    }

    public void ChangeQuantity(string product, int quantity)
    {
        if (!this.byLable.ContainsKey(product))
        {
            throw new ArgumentException();
        }

        this.byLable[product].Quantity = quantity;
        this.byLable[product].Stamp = this.StepCounter;
        this.StepCounter++;
    }

    public bool Contains(Product product)
    {
        return this.byLable.Any(x => x.Key == product.Label);
            //&& x.Value.Price == product.Price
            //&& x.Value.Quantity == product.Quantity);
    }

    public Product Find(int index)
    {
        if (index > this.Count-1 || index<0)
        {
            throw new IndexOutOfRangeException();
        }
        var label = this.lablesByInput[index];
        return this.byLable[label];
    }

    public IEnumerable<Product> FindAllByPrice(double price)
    {
        var result = this.byLable
            .Where(x => x.Value.Price == price)
            .Select(x=>x.Value);

        if (result.Count()==0)
        {
            return Enumerable.Empty<Product>();
        }

        return result;
    }

    public IEnumerable<Product> FindAllByQuantity(int quantity)
    {
        var result = this.byLable
            .Where(x => x.Value.Quantity == quantity)
            .OrderBy(x=>x.Value.Stamp)
            .Select(x => x.Value);

        if (result.Count() == 0)
        {
            return Enumerable.Empty<Product>();
        }

        return result;
    }

    public IEnumerable<Product> FindAllInRange(double lo, double hi)
    {
        var result = this.byLable
             .Where(x => x.Value.Price >lo && x.Value.Price<=hi)
             .OrderByDescending(x=>x.Value.Price)
             .Select(x => x.Value);

        if (result.Count() == 0)
        {
            return Enumerable.Empty<Product>();
        }

        return result;
    }

    public Product FindByLabel(string label)
    {
        if (!this.byLable.ContainsKey(label))
        {
            throw new ArgumentException();
        }
        return this.byLable[label];
    }

    public IEnumerable<Product> FindFirstByAlphabeticalOrder(int count)
    {
        if (this.Count < count || count < 0)
        {
            throw new ArgumentException();
        }

        var firstLabels = this.labelsAlphabetoc.Take(count);
        foreach (var label in firstLabels)
        {
            yield return this.byLable[label];
        }
        
        //too slow:
        //return this.byLable
        //    .OrderBy(x => x.Key)
        //    .Take(count)
        //    .Select(x => x.Value);
    }

    public IEnumerable<Product> FindFirstMostExpensiveProducts(int count)
    {
        if (this.Count < count || count < 0)
        {
            throw new ArgumentException();
        }

        return this.byLable
            .OrderByDescending(x => x.Value.Price)
            .Take(count)
            .Select(x => x.Value);
    }

    public IEnumerator<Product> GetEnumerator()
    {
        foreach (var product in this.byLable)
        {
            yield return product.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
