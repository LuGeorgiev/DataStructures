using System.Collections.Generic;

public class Competition
{

    public Dictionary<int, Competitor> competitors;

    public Competition(string name, int id, int score)
    {
        this.Name = name;
        this.Id = id;
        this.Score = score;

        this.competitors = new Dictionary<int, Competitor>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public int Score { get; set; }

    public ICollection<Competitor> Competitors
    {
        get
        {
            return this.competitors.Values;
        }
        //set;
    }

    public override bool Equals(object obj)
    {
        var item = obj as Competition;
        if (item==null)
        {
            return false;
        }
        return this.Id.Equals(item.Id);
    }
}
