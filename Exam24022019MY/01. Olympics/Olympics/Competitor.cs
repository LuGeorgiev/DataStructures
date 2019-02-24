using System;
using System.Collections;
using System.Collections.Generic;

public class Competitor : IComparable<Competitor>
{
    public Competitor(int id, string name)
    {
        this.Id = id;
        this.Name = name;
        this.TotalScore = 0;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public long TotalScore { get; set; }


    public int CompareTo(Competitor other)
    {
        return this.Id.CompareTo(other.Id);
    }

    public override int GetHashCode()
    {
        int hash = this.Id;
        return (hash<<16).GetHashCode()^(hash>>16).GetHashCode();
    }

    //public override bool Equals(object obj)
    //{
    //    var item = obj as Competitor;
    //    if (item == null)
    //    {
    //        return false;
    //    }
    //    return this.Id.Equals(item.Id);
    //}
}
