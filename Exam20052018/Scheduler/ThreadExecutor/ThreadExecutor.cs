using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

/// <summary>
/// The ThreadExecutor is the concrete implementation of the IScheduler.
/// You can send any class to the judge system as long as it implements
/// the IScheduler interface. The Tests do not contain any <e>Reflection</e>!
/// </summary>
public class ThreadExecutor : IScheduler
{
    private List<Task> tasksList;
    private Dictionary<int, Task> byId;

  
    public ThreadExecutor()
    {
        this.tasksList = new List<Task>(512);
        this.byId = new Dictionary<int, Task>();
    }

    public int Count
    {
        get
        {
            return this.byId.Count;
        }
    }

    public void ChangePriority(int id, Priority newPriority)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new ArgumentException();
        }
        this.byId[id].TaskPriority = newPriority;

        //Following maight not be needed
        //var task = this.tasksList.First(x => x.Id == id);
        //task.TaskPriority = newPriority;
       
    }

    public bool Contains(Task task)
    {
        if (this.byId.ContainsKey(task.Id))
        {
            return true;
        }

        return false;
    }

    public int Cycle(int cycles)
    {
        var completedTasks = 0;

        ThrowIfPoolIsEmpty();
        var tasksToDelete = new List<Task>();

        foreach (var task in this.byId.Values)
        {
            if (task.RemainingConsumption(cycles) <= 0)
            {
                tasksToDelete.Add(task);
                completedTasks++;
            }
        }
        foreach (var toDelete in tasksToDelete)
        {
            this.byId.Remove(toDelete.Id);
        }
        return completedTasks;
    }

    private void ThrowIfPoolIsEmpty()
    {
        if (this.byId.Count == 0)
        {
            throw new InvalidOperationException();
        }
    }

    public void Execute(Task task)
    {
        if (this.Contains(task))
        {
            throw new ArgumentException();
        }

        this.tasksList.Add(task);
        this.byId[task.Id] = task;
    }

    public IEnumerable<Task> GetByConsumptionRange(int lo, int hi, bool inclusive)
    {
        var result = Enumerable.Empty<Task>();
        if (inclusive)
        {
            result = this.byId.Values
                .Where(x => x.Consumption >= lo && x.Consumption <= hi)
                .OrderBy(x=>x.Consumption)
                .ThenByDescending(x=>x.TaskPriority);
        }
        else
        {
            result = this.byId.Values
                .Where(x => x.Consumption > lo && x.Consumption < hi)
                .OrderBy(x => x.Consumption)
                .ThenByDescending(x => x.TaskPriority);
        }
        return result;
    }

    public Task GetById(int id)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new ArgumentException();
        }
        return this.byId[id];
    }

    public Task GetByIndex(int index)
    {
        if (index<0||index>this.Count-1)
        {
            throw new ArgumentOutOfRangeException();
        }
        for (int i = this.tasksList.Count-1; i >=0; i--)
        {
            if (!this.byId.ContainsKey(this.tasksList[i].Id))
            {
                this.tasksList.RemoveAt(i);
            } 
        }
        return this.tasksList[index];
    }

    public IEnumerable<Task> GetByPriority(Priority type)
    {
        var result = this.byId.Values
            .Where(x => x.TaskPriority == type)
            .OrderByDescending(x => x.Id);
        if (result.Count()==0)
        {
            return Enumerable.Empty<Task>();
        }

        return result;
    }

    public IEnumerable<Task> GetByPriorityAndMinimumConsumption(Priority priority, int lo)
    {
        var result = this.byId.Values
            .Where(x => x.TaskPriority == priority && x.Consumption >= lo)
            .OrderByDescending(x=>x.Id);
        if (result.Count()==0)
        {
            return Enumerable.Empty<Task>();
        }
        return result;
    }


    public IEnumerator<Task> GetEnumerator()
    {
        foreach (var task in this.byId.Values)
        {
            yield return task;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
