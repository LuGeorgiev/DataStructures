using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enterprise : IEnterprise
{
    private Dictionary<Guid, Employee> byGuid;
    private Dictionary<Position, List<Employee>> byPosition;
    private Dictionary<string, List<Employee>> byFirstName;

    public Enterprise()
    {
        this.byGuid = new Dictionary<Guid, Employee>();
        this.byPosition = new Dictionary<Position, List<Employee>>();
        this.byFirstName = new Dictionary<string, List<Employee>>();
    }

    public int Count =>this.byGuid.Count;

    public void Add(Employee employee)
    {
        this.byGuid.Add(employee.Id, employee);

        AddByPosition(employee);

        AddByFirstName(employee);
    }

    private void AddByFirstName(Employee employee)
    {
        if (!this.byFirstName.ContainsKey(employee.FirstName))
        {
            this.byFirstName[employee.FirstName] = new List<Employee>();
        }
        this.byFirstName[employee.FirstName].Add(employee);
    }

    private void AddByPosition(Employee employee)
    {
        if (!this.byPosition.ContainsKey(employee.Position))
        {
            this.byPosition[employee.Position] = new List<Employee>();
        }
        this.byPosition[employee.Position].Add(employee);
    }

    public bool Contains(Guid guid)
        => this.byGuid.ContainsKey(guid);

    public bool Contains(Employee employee)
        => this.Contains(employee.Id);

    public bool Change(Guid guid, Employee employee)
    {
        try
        {
            if (!this.byGuid.ContainsKey(guid))
            {
                return false;
            }

            var oldPosition = this.byGuid[guid].Position;
            var oldFirstName = this.byGuid[guid].FirstName;
            this.byGuid[guid] = employee;

            if (oldPosition != employee.Position)
            {
                var toDeletEmploye = this.byPosition[oldPosition]
                    .FirstOrDefault(x => x.Id == guid);
                this.byPosition[oldPosition].Remove(toDeletEmploye);

                this.AddByPosition(employee);
            }
            

            if (oldFirstName!=employee.FirstName)
            {
                var toDeletEmploye = this.byFirstName[oldFirstName]
                     .FirstOrDefault(x => x.Id == guid);
                this.byFirstName[oldFirstName].Remove(toDeletEmploye);

                this.AddByFirstName(employee);
            }

        }
        catch (Exception e)
        {

            return false;
        }
        return true;
    }


    public IEnumerable<Employee> AllWithPositionAndMinSalary(Position position, double minSalary)
    {
        if (!this.byPosition.ContainsKey(position))
        {
            return Enumerable.Empty<Employee>();
        }

        var result = this.byPosition[position].Where(x => x.Salary >= minSalary);
        if (result.Count()==0)
        {
            return Enumerable.Empty<Employee>();
        }

        return result;
    }


    public bool Fire(Guid guid)
    {
        if (!this.Contains(guid))
        {
            return false;
        }
        var firstName = this.byGuid[guid].FirstName;
        var position = this.byGuid[guid].Position;
        var employee = this.byGuid[guid];

        try
        {
            this.byGuid.Remove(guid);
            this.byPosition[position].Remove(employee);
            this.byFirstName[firstName].Remove(employee);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public Employee GetByGuid(Guid guid)
    {
        if (!this.byGuid.ContainsKey(guid))
        {
            throw new ArgumentException();
        }

        return this.byGuid[guid];
    }

    public IEnumerable<Employee> GetByPosition(Position position)
    {
        if (!this.byPosition.ContainsKey(position) 
            || this.byPosition[position].Count==0)
        {
            return Enumerable.Empty<Employee>();
        }
        return this.byPosition[position];
    }

    public IEnumerable<Employee> GetBySalary(double minSalary)
    {
        var result = this.byGuid.Values.Where(x => x.Salary >= minSalary).ToList();
        ThrowIfEmpty(result);
        return result;
    }

    private static void ThrowIfEmpty(IEnumerable<Employee> result)
    {
        if (result.Count() == 0)
        {
            throw new InvalidOperationException();
        }
    }

    public IEnumerable<Employee> GetBySalaryAndPosition(double salary, Position position)
    {
        var result = this.GetByPosition(position);
        result = result
            .Where(x => x.Salary == salary)
            .ToList();

        ThrowIfEmpty(result);
        return result;
    }

    public IEnumerator<Employee> GetEnumerator()
    {
        foreach (var employee in this.byGuid.Values)
        {
            yield return employee;
        }
    }

    public Position PositionByGuid(Guid guid)
    {
        if (!this.Contains(guid))
        {
            throw new InvalidOperationException();
        }

        return this.byGuid[guid].Position;
    }

    public bool RaiseSalary(int months, int percent)
    {
        var empToRiseSalary = this.byGuid.Values
            .Where(x => (DateTime.UtcNow.Year - x.HireDate.Year) * 12
            + (DateTime.UtcNow.Month - x.HireDate.Month) >= months);

        if (empToRiseSalary.Count()==0)
        {
            return false;
        }

        foreach (var employee in empToRiseSalary)
        {
            employee.Salary *= (100 + percent) / 100d;
        }
        return true;
    }

    public IEnumerable<Employee> SearchByFirstName(string firstName)
    {
        if (!this.byFirstName.ContainsKey(firstName))
        {
            return Enumerable.Empty<Employee>();
        }
        return this.byFirstName[firstName];
    }

    public IEnumerable<Employee> SearchByNameAndPosition(string firstName, string lastName, Position position)
    {
        if (!this.byFirstName.ContainsKey(firstName))
        {
            return Enumerable.Empty<Employee>();
        }
        var result = this.byFirstName[firstName].Where(x => x.Position == position && x.LastName == lastName);

        return result;
    }

    public IEnumerable<Employee> SearchByPosition(IEnumerable<Position> positions)
    {
        var result = new List<Employee>();
        foreach (var position in positions)
        {
            if (!this.byPosition.ContainsKey(position))
            {
                continue;
            }
            result.AddRange(this.byPosition[position]);
        }

        if (result.Count==0)
        {
            return Enumerable.Empty<Employee>();
        }

        return result;
    }

    public IEnumerable<Employee> SearchBySalary(double minSalary, double maxSalary)
    {
        var result = this.byGuid.Values
            .Where(x => x.Salary >= minSalary && x.Salary <= maxSalary);

        ThrowIfEmpty(result);

        return result;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

