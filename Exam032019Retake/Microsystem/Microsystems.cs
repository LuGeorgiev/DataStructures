using System;
using System.Collections.Generic;
using System.Linq;

public class Microsystems : IMicrosystem
{
 
    private readonly Dictionary<int, Computer> computersWithIds;
  
    public Microsystems()
    {
       
        this.computersWithIds = new Dictionary<int, Computer>();
       
    }
    public bool Contains(int number)
    {
        return this.computersWithIds.ContainsKey(number);
    }

    public int Count()
    {
        return this.computersWithIds.Count;
    }

    public void CreateComputer(Computer computer)
    {
        if (this.computersWithIds.ContainsKey(computer.Number))
        {
            throw new ArgumentException();
        }
      
        this.computersWithIds.Add(computer.Number, computer);
    }

    public IEnumerable<Computer> GetAllFromBrand(Brand brand)
    {
        if (!this.computersWithIds.Values.Any(x => x.Brand == brand))
        {
            return Enumerable.Empty<Computer>();
        }
        return this.computersWithIds.Values.Where(v => v.Brand == brand).OrderByDescending(x => x.Price);
    }

    public IEnumerable<Computer> GetAllWithColor(string color)
    {
        if (!this.computersWithIds.Values.Any(x => x.Color == color))
        {
            return Enumerable.Empty<Computer>();
        }
        return this.computersWithIds.Values.Where(v => v.Color == color).OrderByDescending(x => x.Price);
      
    }

    public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
    {
        var result = this.computersWithIds.Values.Where(x => x.ScreenSize == screenSize).OrderByDescending(x => x.Number);
        if (result.Count() == 0)
        {
            return Enumerable.Empty<Computer>();
        }

        return result;
    }

    public Computer GetComputer(int number)
    {
        if (!this.computersWithIds.ContainsKey(number))
        {
            throw new ArgumentException();
        }

        return this.computersWithIds[number];
    }

    public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
    {
        var result = this.computersWithIds.Values.Where(x => x.Price >= minPrice && x.Price <= maxPrice).OrderByDescending(p => p.Price);
        if (result.Count() == 0)
        {
            return Enumerable.Empty<Computer>();
        }

        return result;
    }

    public void Remove(int number)
    {
        if (this.computersWithIds.ContainsKey(number))
        {
            var computer = computersWithIds[number];
            this.computersWithIds.Remove(number);
          
        }
        else
        {
            throw new ArgumentException();
        }
    }

    public void RemoveWithBrand(Brand brand)
    {
        if (!this.computersWithIds.Values.Any(x => x.Brand ==brand))
        {
            throw new ArgumentException();
        }
        var computersToRemove = this.computersWithIds.Values.Where(x => x.Brand == brand).ToList();
        for (int i = 0; i < computersToRemove.Count(); i++)
        {
            this.computersWithIds.Remove(computersToRemove[i].Number);
        }
    

    }

    public void UpgradeRam(int ram, int number)
    {
        if (!this.computersWithIds.ContainsKey(number))
        {
            throw new ArgumentException();
        }
        var computerToUpgrade = this.computersWithIds[number];
        if (computerToUpgrade.RAM < ram)
        {
            computerToUpgrade.RAM = ram;
        }
    }
}
