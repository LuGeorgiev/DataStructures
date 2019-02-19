using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class RoyaleArena : IArena
{
    private Dictionary<int, Battlecard> byId;
    private Dictionary<CardType, SortedSet<Battlecard>> byTypeSorted;

    public RoyaleArena()
    {
        this.byId = new Dictionary<int, Battlecard>();
        this.byTypeSorted = new Dictionary<CardType, SortedSet<Battlecard>>();

        this.byTypeSorted.Add(CardType.MELEE, new SortedSet<Battlecard>(Comparer<Battlecard>.Create((a,b)=> 
        {
            int comparer = b.Damage.CompareTo(a.Damage);
            if (comparer==0)
            {
                return a.Id.CompareTo(b.Id);
            }
            return comparer;
        })));        

        this.byTypeSorted.Add(CardType.BUILDING, new SortedSet<Battlecard>(Comparer<Battlecard>.Create((a, b) =>
        {
            int comparer = b.Damage.CompareTo(a.Damage);
            if (comparer == 0)
            {
                return a.Id.CompareTo(b.Id);
            }
            return comparer;
        })));

        this.byTypeSorted.Add(CardType.RANGED, new SortedSet<Battlecard>(Comparer<Battlecard>.Create((a, b) =>
        {
            int comparer = b.Damage.CompareTo(a.Damage);
            if (comparer == 0)
            {
                return a.Id.CompareTo(b.Id);
            }
            return comparer;
        })));

        this.byTypeSorted.Add(CardType.SPELL, new SortedSet<Battlecard>(Comparer<Battlecard>.Create((a, b) =>
        {
            int comparer = b.Damage.CompareTo(a.Damage);
            if (comparer == 0)
            {
                return a.Id.CompareTo(b.Id);
            }
            return comparer;
        })));
    }

    public int Count => this.byId.Count;

    public void Add(Battlecard card)
    {
        this.byId[card.Id] = card;
        this.byTypeSorted[card.Type].Add(card);
    }

    public void ChangeCardType(int id, CardType type)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new ArgumentException();
        }
        var card = this.byId[id];
        this.byTypeSorted[card.Type].Remove(card);
        card.Type = type;
        this.byTypeSorted[card.Type].Add(card);
    }

    public bool Contains(Battlecard card)
    {
        return this.byId.ContainsKey(card.Id);
    }

    public IEnumerable<Battlecard> FindFirstLeastSwag(int n)
    {
        if (n<=0 || n>this.Count)
        {
            throw new InvalidOperationException();
        }

        var result = this.byId.Values
            .OrderBy(x => x.Swag)
            .ThenBy(x => x.Id)
            .Take(n);

        return result;
    }

    public IEnumerable<Battlecard> GetAllByNameAndSwag()
    {
        var result = new Dictionary<string, Battlecard>();
        foreach (var card in this.byId.Values)
        {
            if (!result.ContainsKey(card.Name))
            {
                result[card.Name] = card;
            }
            else
            {
                if (result[card.Name].Swag<card.Swag)
                {
                    result[card.Name] = card;
                }
            }
        }

        return result.Values;       
    }

    public IEnumerable<Battlecard> GetAllInSwagRange(double lo, double hi)
    {
        var result = this.byId.Values
            .Where(x => x.Swag >= lo && x.Swag <= hi)
            .OrderBy(x => x.Swag)
            .ThenBy(x => x.Id);
        if (result.Count()==0)
        {
            return Enumerable.Empty<Battlecard>();
        }

        return result;
    }

    public IEnumerable<Battlecard> GetByCardType(CardType type)
    {        
        ThrowIfEmpty(this.byTypeSorted[type]);

        return this.byTypeSorted[type];
    }

    public IEnumerable<Battlecard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
    {
        var result = this.byTypeSorted[type]
            .Where(x => x.Damage <= damage);
        ThrowIfEmpty(result);

        return result;
    }

    public Battlecard GetById(int id)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }

        return this.byId[id];
    }

    public IEnumerable<Battlecard> GetByNameAndSwagRange(string name, double lo, double hi)
    {
        var result = this.byId.Values
            .Where(x => x.Name == name && x.Swag >= lo && x.Swag < hi)
            .OrderByDescending(x => x.Swag)
            .ThenBy(x => x.Id);

        ThrowIfEmpty(result);

        return result;
    }

    public IEnumerable<Battlecard> GetByNameOrderedBySwagDescending(string name)
    {
        var result = this.byId.Values
            .Where(x => x.Name == name)
            .OrderByDescending(x => x.Swag)
            .ThenBy(x=>x.Id);

        ThrowIfEmpty(result);

        return result;
    }

    public IEnumerable<Battlecard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
    {
        var result = this.byTypeSorted[type]
            .Where(x => x.Damage > lo && x.Damage < hi);
        ThrowIfEmpty(result);

        return result;
    }

    public IEnumerator<Battlecard> GetEnumerator()
    {
        foreach (var card in this.byId.Values)
        {
            yield return card;
        }
    }

    public void RemoveById(int id)
    {
        if (!this.byId.TryGetValue(id, out var card ))
        {
            throw new InvalidOperationException();
        }
        this.byId.Remove(id);
        this.byTypeSorted[card.Type].Remove(card);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private static void ThrowIfEmpty(IEnumerable<Battlecard> result)
    {
        if (result.Count() == 0)
        {
            throw new InvalidOperationException();
        }
    }
}
