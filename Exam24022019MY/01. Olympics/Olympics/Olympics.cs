using System;
using System.Collections.Generic;
using System.Linq;

public class Olympics : IOlympics
{
    private Dictionary<int, Competitor> athleths;
    private Dictionary<int, Competition> contests;    

    public Olympics()
    {
        this.athleths = new Dictionary<int, Competitor>();
        this.contests = new Dictionary<int, Competition>();
        //this.byScoreOrdered = new SortedSet<Competitor>(Comparer<Competitor>.Create((x, y) =>
        //{
        //    int comp = y.TotalScore.CompareTo(x.TotalScore);
        //    if (comp == 0)
        //    {
        //        return x.Id.CompareTo(y.Id);
        //    }
        //    return comp;
        //}));
    }


    public void AddCompetition(int id, string name, int participantsLimit)
    {
        if (!this.contests.TryAdd(id, new Competition(name, id, participantsLimit)))
        {
            throw new ArgumentException();
        }
        
    }

    public void AddCompetitor(int id, string name)
    {
        if (!this.athleths.TryAdd(id, new Competitor(id,name)))
        {
            throw new ArgumentException();
        }
        
    }

    public void Compete(int competitorId, int competitionId)
    {
        if (!this.athleths.TryGetValue(competitorId, out var competitor))
        {
            throw new ArgumentException();
        }
        if (!this.contests.TryGetValue(competitionId, out var contest))
        {
            throw new ArgumentException();
        }

        competitor.TotalScore += contest.Score;
        contest.competitors.Add(competitorId, competitor);
    }

   
    public int CompetitionsCount()
    => this.contests.Count;

    public int CompetitorsCount()
    => this.athleths.Count;

    public bool Contains(int competitionId, Competitor comp)
    {      

        return contests.TryGetValue(competitionId, out var contest) 
            && contest.competitors.ContainsKey(comp.Id);
    }

    public void Disqualify(int competitionId, int competitorId)
    {
        if (!this.contests.TryGetValue(competitionId, out var competition))
        {
            throw new ArgumentException();
        }
        if (!competition.competitors.ContainsKey(competitorId))
        {
            throw new ArgumentException();
        }


        var atlethe = athleths[competitorId];
        
        atlethe.TotalScore -= competition.Score;
        competition.competitors.Remove(atlethe.Id);

        //if (!this.contests.ContainsKey(competitionId))
        //{
        //    throw new ArgumentException();
        //}
        //if (!this.contests[competitionId].Competitors.Any(x=>x.Id==competitorId))
        //{
        //    throw new ArgumentException();
        //}

        //var athlete = this.contests[competitionId].Competitors
        //    .First(x => x.Id == competitorId);
        //athlete.TotalScore -= this.contests[competitionId].Score;
        //this.contests[competitionId].Competitors.Remove(athlete);
    }

    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
    {
        var result = this.athleths.Values
            .Where(x => x.TotalScore > min && x.TotalScore <= max)
            .OrderBy(x=>x.Id);

        if (result.Count() == 0)
        {
            return Enumerable.Empty<Competitor>();
        }

        return result;
    }

    public IEnumerable<Competitor> GetByName(string name)
    {
        var result = this.athleths.Values
            .Where(x => x.Name == name)
            .OrderBy(x => x.Id);

        ThrowIfEmpty(result);

        return result;
    }

    private static void ThrowIfEmpty(IEnumerable<Competitor> result)
    {
        if (result.Count() == 0)
        {
            throw new ArgumentException();
        }
    }

    public Competition GetCompetition(int id)
    {
        if (!this.contests.TryGetValue(id, out var contest))
        {
            throw new ArgumentException();
        }
        return contest;
    }

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
    {
        var result = this.athleths.Values
            .Where(x => x.Name.Length >= min && x.Name.Length <= max)
            .OrderBy(x => x.Id);
        if (result.Count()==0)
        {
            return Enumerable.Empty<Competitor>();
        }

        return result;
    }
}