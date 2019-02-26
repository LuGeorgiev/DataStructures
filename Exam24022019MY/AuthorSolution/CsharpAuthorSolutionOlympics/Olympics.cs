
using System;
using System.Collections.Generic;
using System.Linq;

public class Olympics : IOlympics
{
    private readonly Dictionary<int, Competitor> competitorsById;
    private readonly Dictionary<int, Competition> competitionsById;
    private readonly Dictionary<string, SortedSet<int>> competitorsByName;

    public Olympics()
    {
        this.competitorsById = new Dictionary<int, Competitor>();
        this.competitionsById = new Dictionary<int, Competition>();
        this.competitorsByName = new Dictionary<string, SortedSet<int>>();
    }

    public void AddCompetitor(int id, string name)
    {

        if (!this.competitorsById.ContainsKey(id))
        {
            var competitor = new Competitor(id, name);
            this.competitorsById.Add(id, competitor);
            if (!this.competitorsByName.ContainsKey(name))
            {
                this.competitorsByName.Add(name, new SortedSet<int>());
            }
            this.competitorsByName[name].Add(id);

        }
        else
        {
            throw new ArgumentException();
        }
    }

    public void AddCompetition(int id, string name, int participantsLimit)
    {
        if (this.competitionsById.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        this.competitionsById.Add(id, new Competition(name, id, participantsLimit));
    }

    public void Compete(int competitorId, int competitionId)
    {
        if (!this.competitorsById.ContainsKey(competitorId) || !this.competitionsById.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        this.competitionsById[competitionId].AddCompetitor(this.competitorsById[competitorId]);
    }

    public int CompetitionsCount() => this.competitionsById.Count;

    public int CompetitorsCount() => this.competitorsById.Count;

    public bool Contains(int competitionId, Competitor comp)
    {
        if (!this.competitionsById.ContainsKey(competitionId))
        {
            throw new ArgumentException();
        }

        var competition = this.competitionsById[competitionId];

        return competition.Competitors.Contains(comp);
    }

    public void Disqualify(int competitionId, int competitorId)
    {
        if (!this.competitionsById.ContainsKey(competitionId) || !this.competitorsById.ContainsKey(competitorId))
        {
            throw new ArgumentException();
        }

        var isSuccess = this.competitionsById[competitionId].Disqualify(this.competitorsById[competitorId]);

        if (!isSuccess)
        {
            throw new ArgumentException();
        }
    }

    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
    {
        return this.competitorsById
            .Values
            .Where(c => c.TotalScore > min && c.TotalScore <= max)
            .OrderBy(c => c.Id);
    }

    public IEnumerable<Competitor> GetByName(string name)
    {
        if (this.competitorsByName.ContainsKey(name))
        {

            var integers = this.competitorsByName[name];
            var res = new List<Competitor>();

            foreach (var item in integers)
            {
                res.Add(this.competitorsById[item]);
            }
            return res;

        }

        throw new ArgumentException();
    }

    public Competition GetCompetition(int id)
    {
        if (!this.competitionsById.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        return this.competitionsById[id];
    }

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
    {
        return this.competitorsById
            .Values
            .Where(p => p.Name.Length >= min && p.Name.Length <= max)
            .OrderBy(p => p.Id);
    }
}
