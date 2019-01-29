using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

public class Program
{
    static void Main(string[] args)
    {
        FirstLastList<int> list = new FirstLastList<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        Console.WriteLine();

        var items = FirstLastListFactory.Create<string>();
        items.Add("coffee");
        items.Add("coffee");
        items.Add("milk");

        // Act
        var returnedItems = items.First(3).ToList();
        Console.WriteLine(string.Join(", ",returnedItems));

        // Assert
        var expectedItems = new string[] {
            "coffee", "coffee", "milk" };
    }
}
