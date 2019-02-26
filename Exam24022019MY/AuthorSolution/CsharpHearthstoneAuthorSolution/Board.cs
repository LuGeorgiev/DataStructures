using System;
using System.Collections.Generic;
using System.Linq;

public class Board : IBoard
{

    private Dictionary<string, Card> cards;
    private Dictionary<string, Card> death;
    private Dictionary<int, List< Card>> levels;

    public Board()
    {
        this.cards = new Dictionary<string, Card>();
        this.death = new Dictionary<string, Card>();
        this.levels = new Dictionary<int,List<Card>>();
    }
    public bool Contains(string name)
    {
        return this.cards.ContainsKey(name);
    }

    public int Count()
    {
        return this.cards.Count;
    }

    public void Draw(Card card)
    {
        if (this.cards.ContainsKey(card.Name))
        {
            throw new ArgumentException();
        }

        this.cards.Add(card.Name, card);
        if (!this.levels.ContainsKey(card.Level))
        {
            this.levels.Add(card.Level, new List<Card>());
        }

        this.levels[card.Level].Add(card);
    }

    public IEnumerable<Card> GetBestInRange(int start, int end)
    {
        var result = this.cards.Values.Where(v => v.Score >= start && v.Score <= end).OrderByDescending(v => v.Level);

        if(result.Count() == 0)
        {
            return Enumerable.Empty<Card>();
        }

        return result;
    }

    public void Heal(int health)
    {
        var card = this.cards.Values.OrderBy(v => v.Health).First();

        card.Health += health;
    }

    public IEnumerable<Card> ListCardsByPrefix(string prefix)
    {
        var result = this.cards.Values.Where(v => v.Name.StartsWith(prefix)).OrderBy(v => new string(v.Name.ToCharArray().Reverse().ToArray())).ThenBy(v => v.Level);

        if(result.Count() == 0)
        {
            return Enumerable.Empty<Card>();
        }
        return result;
    }

    public void Play(string attackerCardName, string attackedCardName)
    {
        if (!this.cards.ContainsKey(attackerCardName))
        {
            throw new ArgumentException();
        }

        if (!this.cards.ContainsKey(attackedCardName))
        {
            throw new ArgumentException();
        }
        var attacker = this.cards[attackerCardName];
        var attacked = this.cards[attackedCardName];

        if (attacked.Level != attacker.Level)
        {
            throw new ArgumentException();
        }

        if (attacked.Health > 0)
        {
            attacked.Health -= attacker.Damage;
            if (attacked.Health <= 0)
            {
                attacker.Score += attacked.Level;
                this.death.Add(attacked.Name, attacked);
            }
        }

    }

    public void Remove(string name)
    {
        if (!this.cards.ContainsKey(name))
        {
            throw new ArgumentException();
        }

        this.cards.Remove(name);
        this.death.Remove(name);
    }

    public void RemoveDeath()
    {
        foreach (var item in death)
        {
            this.cards.Remove(item.Key);
        }
        this.death.Clear();
        
    }

    public IEnumerable<Card> SearchByLevel(int level)
    {
        return this.levels[level].OrderByDescending(c => c.Score);
    }
}
