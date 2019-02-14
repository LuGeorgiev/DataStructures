using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Chainblock : IChainblock
{
    private Dictionary<int, Transaction> byId;
    private Dictionary<TransactionStatus, OrderedBag<Transaction>> byStatusSorted;

    public Chainblock()
    {
        this.byId = new Dictionary<int, Transaction>();
        this.byStatusSorted = new Dictionary<TransactionStatus, OrderedBag<Transaction>>();
    }

    public int Count => this.byId.Count;

    public void Add(Transaction tx)
    {
        if (!this.byStatusSorted.ContainsKey(tx.Status))
        {
            this.byStatusSorted[tx.Status] = new OrderedBag<Transaction>();
        }

        this.byId[tx.Id] = tx;
        this.byStatusSorted[tx.Status].Add(tx);
    }

    public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        var transaction = this.byId[id];
        var oldStatus = transaction.Status;
        this.byStatusSorted[oldStatus].Remove(transaction);
        if (this.byStatusSorted[oldStatus].Count==0)
        {
            this.byStatusSorted.Remove(oldStatus);
        }

        transaction.Status = newStatus;
        if (!this.byStatusSorted.ContainsKey(newStatus))
        {
            this.byStatusSorted[newStatus] = new OrderedBag<Transaction>();
        }
        this.byStatusSorted[newStatus].Add(transaction);     
    }

    public bool Contains(Transaction tx)
      => this.Contains(tx.Id);

    public bool Contains(int id)
      => this.byId.ContainsKey(id);


    public IEnumerable<Transaction> GetAllInAmountRange(double lo, double hi)
    {
        var result = this.byId
            .Where(x => x.Value.Amount >= lo && x.Value.Amount <= hi)
            .Select(x=>x.Value);
        if (result.Count()==0)
        {
            return Enumerable.Empty<Transaction>();
        }

        return result;
    }

    public IEnumerable<Transaction> GetAllOrderedByAmountDescendingThenById()
        => this.byId
        .OrderByDescending(x => x.Value.Amount)
        .ThenBy(x => x.Key)
        .Select(x => x.Value);

    public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
    {
        if (!this.byStatusSorted.ContainsKey(status))
        {
            throw new InvalidOperationException();
        }

        return this.byStatusSorted[status]
            .Select(x => x.To);
    }

    public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
    {
        if (!this.byStatusSorted.ContainsKey(status))
        {
            throw new InvalidOperationException();
        }

        return this.byStatusSorted[status]
            .Select(x => x.From);
    }

    public Transaction GetById(int id)
    {
        if (!this.Contains(id))
        {
            throw new InvalidOperationException();
        }

        return this.byId[id];
    }

    public IEnumerable<Transaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
    {
        var result = this.byId
            .Where(x => x.Value.To == receiver
                && x.Value.Amount >= lo
                && x.Value.Amount < hi)
            .OrderByDescending(x => x.Value.Amount)
            .ThenBy(x => x.Value.Id)
            .Select(x => x.Value);
        if (result.Count()==0)
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<Transaction> GetByReceiverOrderedByAmountThenById(string receiver)
    {
        var result = this.byId
           .Where(x => x.Value.To == receiver)
           .OrderByDescending(x => x.Value.Amount)
           .ThenBy(x => x.Value.Id)
           .Select(x => x.Value);
        if (result.Count() == 0)
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<Transaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
    {
        var result = this.byId
            .Where(x => x.Value.From == sender
                && x.Value.Amount > amount)
            .OrderByDescending(x => x.Value.Amount)
            .Select(x => x.Value);
        if (result.Count()==0)
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<Transaction> GetBySenderOrderedByAmountDescending(string sender)
    {
        var result = this.byId
         .Where(x => x.Value.From == sender)
         .OrderByDescending(x => x.Value.Amount)
         .Select(x => x.Value);
        if (result.Count() == 0)
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<Transaction> GetByTransactionStatus(TransactionStatus status)
    {
        if (!this.byStatusSorted.ContainsKey(status))
        {
            throw new InvalidOperationException();
        }
        if( this.byStatusSorted[status].Count==0)
        {
            return Enumerable.Empty<Transaction>();
        }

        return this.byStatusSorted[status];
    }

    public IEnumerable<Transaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
    {
        var result = this.GetByTransactionStatus(status);
        if (result.Count()==0)
        {
            return result;
        }

        return result.Where(x => x.Amount < amount);
    }

    public IEnumerator<Transaction> GetEnumerator()
    {
        foreach (var transaction in this.byId)
        {
            yield return transaction.Value;
        }
    }

    public void RemoveTransactionById(int id)
    {
        var transaction = this.byId[id];
        this.byStatusSorted[transaction.Status].Remove(transaction);

        if (this.byStatusSorted[transaction.Status].Count==0)
        {
            this.byStatusSorted.Remove(transaction.Status);
        }
        this.byId.Remove(transaction.Id);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

