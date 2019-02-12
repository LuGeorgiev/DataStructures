using System.Collections.Generic;
using System.Linq;

public class Program
{
    static void Main(string[] args)
    {
        Computer computer = new Computer(100);
        List<Invader> expected = new List<Invader>();

        for (int i = 1; i <= 20000; i++)
        {
            var invader = new Invader(1, i);
            computer.AddInvader(invader);
            expected.Add(invader);
        }

        expected = expected.OrderBy(x => x.Distance).ThenBy(x => -x.Damage).Skip(10000).ToList();

        computer.DestroyHighestPriorityTargets(10000);


        var actual = computer.Invaders().ToList();
        ;
    }
}
