using System;
using System.Collections.Generic;
using System.Linq;

public class Agency : IAgency
{
    Dictionary<DateTime, Dictionary<string, Invoice>> invoices;
    Dictionary<string, Invoice> allInvoices;
    HashSet<Invoice> payed;

    public Agency()
    {
        this.invoices = new Dictionary<DateTime, Dictionary<string, Invoice>>();
        this.allInvoices = new Dictionary<string, Invoice>();
        this.payed = new HashSet<Invoice>();
    }
    public bool Contains(string number)
    {
        return this.allInvoices.ContainsKey(number);
    }

    public int Count()
    {
        return this.allInvoices.Count;
    }

    public void Create(Invoice invoice)
    {
        if (!this.allInvoices.ContainsKey(invoice.SerialNumber))
        {
            if (!this.invoices.ContainsKey(invoice.DueDate))
            {
                this.invoices.Add(invoice.DueDate, new Dictionary<string, Invoice>());
            }
            this.invoices[invoice.DueDate].Add(invoice.SerialNumber, invoice);
            this.allInvoices.Add(invoice.SerialNumber, invoice);
            return;
        }

        throw new ArgumentException();
    }

    public void ExtendDeadline(DateTime dueDate, int days)
    {
        if (!this.invoices.ContainsKey(dueDate)) throw new ArgumentException();

        var entries = this.invoices[dueDate];

        foreach (var entry in entries)
        {
            entry.Value.DueDate = entry.Value.DueDate.AddDays(days);
        }
    }

    public IEnumerable<Invoice> GetAllByCompany(string company)
    {
        var allByCompany = this.allInvoices.Values.Where(x => x.CompanyName == company).OrderByDescending(c => c.SerialNumber);

        if(allByCompany.Count() == 0)
        {
            return Enumerable.Empty<Invoice>();
        }
        return allByCompany;
    }

    public IEnumerable<Invoice> GetAllFromDepartment(Department department)
    {
        var allByDep = this.allInvoices.Values.Where(x => x.Department == department).OrderByDescending(c => c.Subtotal).ThenBy(x => x.IssueDate);

        if (allByDep.Count() == 0)
        {
            return Enumerable.Empty<Invoice>();
        }
        return allByDep;
    }

    public IEnumerable<Invoice> GetAllInvoiceInPeriod(DateTime start, DateTime end)
    {
        var allInPeriod = this.allInvoices.Values.Where(x => x.IssueDate >= start && x.IssueDate <= end).OrderBy(c => c.IssueDate).ThenBy(x => x.DueDate);

        if (allInPeriod.Count() == 0)
        {
            return Enumerable.Empty<Invoice>();
        }
        return allInPeriod;
    }

    public void PayInvoice(DateTime due)
    {
        if (this.invoices.ContainsKey(due))
        {
            foreach (var value in invoices[due].Values)
            {
                value.Subtotal = 0;
                payed.Add(value);
            }
            return;
        }

        throw new ArgumentException();
    }

    public IEnumerable<Invoice> SearchBySerialNumber(string serialNumber)
    {
        var filtered = allInvoices.Where(x => x.Key.Contains(serialNumber));

        if (filtered.Count() == 0)
        {
            throw new ArgumentException();
        }
        return filtered.Select(x=>x.Value).OrderByDescending(x => x.SerialNumber);
    }

    public void ThrowInvoice(string number)
    {
        if (this.Contains(number))
        {
            var toRemove = this.allInvoices[number];
           this.allInvoices.Remove(number);
            this.invoices[toRemove.DueDate].Remove(number);
            return;
        }

        throw new ArgumentException();
    }

    public IEnumerable<Invoice> ThrowInvoiceInPeriod(DateTime start, DateTime end)
    {
        var allInPeriod = this.allInvoices.Values.Where(x => x.DueDate > start && x.DueDate < end).ToList();

        if (allInPeriod.Count() == 0)
        {
            throw new ArgumentException();
        }

        for (int i = 0; i <= allInPeriod.Count() - 1; i++)
        {
            this.invoices[allInPeriod[i].DueDate].Remove(allInPeriod[i].SerialNumber);
            allInvoices.Remove(allInPeriod[i].SerialNumber);
            this.payed.Remove(allInPeriod[i]);
        }
        return allInPeriod;
    }

    public void ThrowPayed()
    {
        foreach (var inv in this.payed)
        {
            var toRemove = this.allInvoices[inv.SerialNumber];
            this.allInvoices.Remove(inv.SerialNumber);
            this.invoices[toRemove.DueDate].Remove(inv.SerialNumber);
        }

        this.payed.Clear();
    }
}
