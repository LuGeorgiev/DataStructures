using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Judge : IJudge
{
    private HashSet<int> userIds = new HashSet<int>();
    private HashSet<int> contestIds = new HashSet<int>();
    private Dictionary<int, Submission> submissions = new Dictionary<int, Submission>();
    
    public void AddContest(int contestId)
    {
        this.contestIds.Add(contestId);
    }

    public void AddSubmission(Submission submission)
    {
        if (this.submissions.ContainsKey(submission.Id))
        {
            return;
        }

        if (!this.userIds.Contains(submission.UserId) 
            || !this.contestIds.Contains(submission.ContestId))
        {
            throw new InvalidOperationException();
        }
        this.submissions.Add(submission.Id, submission);
    }

    public void AddUser(int userId)
    {
        this.userIds.Add(userId);
    }

    public void DeleteSubmission(int submissionId)
    {
        if (!this.submissions.ContainsKey(submissionId))
        {
            throw new InvalidOperationException();
        }        

        this.submissions.Remove(submissionId);
    }

    public IEnumerable<Submission> GetSubmissions()
    {
        return this.submissions.Values.OrderBy(x => x.Id);
    }

    public IEnumerable<int> GetUsers()
    {
        return this.userIds.OrderBy(x => x);
    }

    public IEnumerable<int> GetContests()
    {
        return this.contestIds.OrderBy(x => x);
    }

    public IEnumerable<int> ContestsByUserIdOrderedByPointsDescThenBySubmissionId(int userId)
    {           
        return this.submissions.Values
            .Where(x => x.UserId == userId)
            .Distinct()
            .OrderByDescending(x => x.Points)
            .ThenBy(x => x.Id)
            .Select(x => x.ContestId);
    }

    public IEnumerable<Submission> SubmissionsInContestIdByUserIdWithPoints(int points, int contestId, int userId)
    {
        IEnumerable<Submission> result =  this.submissions.Values
            .Where(x => x.Points == points
                && x.ContestId == contestId
                && x.UserId == userId);
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public IEnumerable<int> ContestsBySubmissionType(SubmissionType submissionType)
    {
        return this.submissions.Values
            .Where(x => x.Type == submissionType)
            .Select(x => x.ContestId)
            .Distinct()
            .OrderBy(x=>x);
    }

    public IEnumerable<Submission> SubmissionsWithPointsInRangeBySubmissionType(int minPoints, int maxPoints, SubmissionType submissionType)
    {
        return this.submissions.Values
            .Where(x => x.Points >= minPoints
                && x.Points <= maxPoints
                && x.Type == submissionType);
    }

}
