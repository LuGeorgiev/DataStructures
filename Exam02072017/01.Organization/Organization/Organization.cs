using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Organization : IOrganization
{
    private List<Person> byInesrtion;
    private Dictionary<string, List<Person>> byName;

    public Organization()
    {
        this.byInesrtion = new List<Person>(128);
        this.byName = new Dictionary<string, List<Person>>();
    }

    public IEnumerator<Person> GetEnumerator()
    {
        foreach (var person in this.byInesrtion)
        {
            yield return person;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public int Count
        => this.byInesrtion.Count;

    public bool Contains(Person person)
        => this.byName.ContainsKey(person.Name) 
            && this.byName[person.Name].Contains(person);

    public bool ContainsByName(string name)
        => this.byName.ContainsKey(name);

    public void Add(Person person)
    {
        this.byInesrtion.Add(person);
        if (!this.byName.ContainsKey(person.Name))
        {
            this.byName[person.Name] = new List<Person>();
        }
        this.byName[person.Name].Add(person);
    }

    public Person GetAtIndex(int index)
    {
        if (index<0 || index>=this.Count)
        {
            throw new IndexOutOfRangeException();
        }

        return this.byInesrtion[index];
    }

    public IEnumerable<Person> GetByName(string name)
    {
        if (!this.byName.ContainsKey(name))
        {
            return Enumerable.Empty<Person>();
        }

        return this.byName[name];
    }

    public IEnumerable<Person> FirstByInsertOrder(int count = 1)
    {
        if (this.byInesrtion.Count == 0)
        {
            return Enumerable.Empty<Person>();
        }
        if (count>this.Count)
        {
            count = this.Count;
        }
        return this.byInesrtion.Take(count);
    }

    public IEnumerable<Person> SearchWithNameSize(int minLength, int maxLength)
    {
        var result = this.byName
            .Where(x => x.Key.Length >= minLength && x.Key.Length <= maxLength)
            .SelectMany(x => x.Value);

        if (result.Count() == 0)
        {
            return Enumerable.Empty<Person>();
        }

        return result;
    }

    public IEnumerable<Person> GetWithNameSize(int length)
    {
        var result = this.byName
            .Where(x => x.Key.Length == length)
            .SelectMany(x => x.Value);

        if (result.Count()==0)
        {
            throw new ArgumentException();
        }
        return result;
    }

    public IEnumerable<Person> PeopleByInsertOrder()
    {
        foreach (var person in this.byInesrtion)
        {
            yield return person;
        }
    }
}