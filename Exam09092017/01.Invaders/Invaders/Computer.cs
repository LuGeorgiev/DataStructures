using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using System.Linq;

public class Computer : IComputer
{
    
    private OrderedBag<IInvader> invaders;
    private int energy;

    public Computer(int energy)
    {
        if (energy <= 0)
        {
            throw new ArgumentException();
        }
        this.Energy = energy;
        this.invaders = new OrderedBag<IInvader>();
    }

    public int Energy
    {
        private set
        {            
            this.energy = value;
        }
        get
        {
            return this.energy < 0 
                ? 0 
                : this.energy;
        }
    }

    public void Skip(int turns)
    {
        if (turns<=0)
        {
            return;
        }

        foreach (var invader in this.invaders)
        {
            invader.Distance -= turns;
            if (invader.Distance<=0)
            {
                this.energy -= invader.Damage;
            }
        }

        this.invaders.RemoveAll(x => x.Distance <= 0);
    }

    public void AddInvader(Invader invader)
    {
        this.invaders.Add(invader);
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        if (this.invaders.Count<=0)
        {
            return;
        }
        var lastToRemove = this.invaders[count - 1];
        var toRemove = this.invaders.RangeTo(lastToRemove, true).ToArray();

        this.invaders.RemoveMany(toRemove);
    }

    public void DestroyTargetsInRadius(int radius)
    {
        this.invaders.RemoveAll(x => x.Distance <= radius);
    }

    public IEnumerable<Invader> Invaders()
    {
        foreach (var invader in this.invaders)
        {
            yield return (Invader)invader;
        }
    }
}
