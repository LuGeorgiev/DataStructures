using System;
using System.Collections.Generic;
using System.Linq;

public static class JudgeLauncher
{
    public static void Main()
    {
        Dictionary<int, Submission> submissions = new Dictionary<int, Submission>();
        var idGen = new Random();
        var judge = new Judge();

        for (int i = 0; i < 500; i++)
        {
            int submissionId = idGen.Next(0, 100000);
            int userId = idGen.Next(0, 5);
            SubmissionType type = (SubmissionType)idGen.Next(0, 3);
            int contestId = idGen.Next(0, 15);
            int points = idGen.Next(0, 30);

            Submission submission = new Submission(submissionId, points, type, contestId, userId);

            if (!submissions.ContainsKey(submissionId))
            {
                submissions.Add(submissionId, submission);
            }


            judge.AddContest(contestId);
            judge.AddUser(userId);
            judge.AddSubmission(submission);
        }

        var expected = submissions.Values
            .Where(x => x.UserId == 3)
            .GroupBy(x => x.ContestId)
            .Select(x => x.OrderByDescending(s => s.Points)
                .ThenBy(s => s.Id)
                .First())
            .OrderByDescending(x => x.Points)
            .ThenBy(x => x.Id)
            .Select(x => x.ContestId);

        IEnumerable<int> result = judge.ContestsByUserIdOrderedByPointsDescThenBySubmissionId(3);
        ;
    }
}

