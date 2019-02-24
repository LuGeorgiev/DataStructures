using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Board : IBoard
{
    private Dictionary<string, Card> deck;

    public Board()
    {
        this.deck = new Dictionary<string, Card>();
    }

    public bool Contains(string name)
    => this.deck.ContainsKey(name);

    public int Count()
    => this.deck.Count;

    public void Draw(Card card)
    {
        if (this.deck.ContainsKey(card.Name))
        {
            throw new ArgumentException();
        }
        this.deck.Add(card.Name, card);
    }

    public IEnumerable<Card> GetBestInRange(int start, int end)
    {
        var result = this.deck.Values
            .Where(x => x.Score >= start && x.Score <= end)
            .OrderByDescending(x => x.Level);
        if (result.Count()==0)
        {
            return Enumerable.Empty<Card>();
        }

        return result;
    }

    public void Heal(int health)
    {
        var card = this.deck.Values
            .OrderBy(x => x.Health)
            .Take(1);

        card.First().Health += health;
    }

    public IEnumerable<Card> ListCardsByPrefix(string prefix)
    {
        if (prefix=="")
        {
            return this.deck.Values
            .OrderBy(x => ReverseStr(x.Name));
            
        }

        //var result = this.deck.Keys
        //    .Where(x => x.StartsWith(prefix))
        //    .OrderBy(x => ReverseStr(x));


        //foreach (var key in result)
        //{
        //    yield return deck[key];
        //}

        return this.deck
            .Where(x => x.Key.StartsWith(prefix))
            .OrderBy(x => ReverseStr(x.Key))
            .Select(x => x.Value);

    }

    public void Play(string attackerCardName, string attackedCardName)
    {
        if (!this.Contains(attackedCardName) 
            || !this.Contains(attackerCardName)
            || this.deck[attackerCardName].Level!=this.deck[attackedCardName].Level)
        {
            throw new ArgumentException();
        }

        var aggressor = this.deck[attackerCardName];
        var victim = this.deck[attackedCardName];

        if (victim.Health>0)
        {
            victim.Health -= aggressor.Damage;
            if (victim.Health<=0)
            {
                aggressor.Score += victim.Level;
            }
        }
    }

    public void Remove(string name)
    {
        if (!this.Contains(name))
        {
            throw new ArgumentException();
        }
        this.deck.Remove(name);
    }

    public void RemoveDeath()
    {
        var deadCards = new List<Card>();
        foreach (var card in this.deck.Values)
        {
            if (card.Health<=0)
            {
                deadCards.Add(card);
            }
        }
        deadCards.ForEach(x => this.deck.Remove(x.Name));
    }

    public IEnumerable<Card> SearchByLevel(int level)
    {
        var result = this.deck.Values
            .Where(x=>x.Level==level)
            .OrderByDescending(x => x.Score);

        if (result.Count()==0)
        {
            return Enumerable.Empty<Card>();
        }

        return result;
    }
    private static string ReverseStr(string input)
    {
        var result = input
            .ToCharArray()
            .ToList();
        result.Reverse();

        return string.Join("", result);
    }
}