using System;
using System.Collections.Generic;
using System.Text;
using Wintellect.PowerCollections;


public class TextEditor : ITextEditor
{
    private Trie<BigList<string>> users;
    private Dictionary<string, Stack<BigList<string>>> cache;

    public TextEditor()
    {
        this.users = new Trie<BigList<string>>();
        this.cache = new Dictionary<string, Stack<BigList<string>>>();
    }

    public void Clear(string username)
    {
        this.Cache(username);
        this.users.GetValue(username).Clear();
    }

    public void Delete(string username, int startIndex, int length)
    {
        this.Cache(username);
        var list = this.users.GetValue(username);
        list.RemoveRange(startIndex, length);
    }

    public void Insert(string username, int index, string str)
    {
        this.Cache(username);
        var list = this.users.GetValue(username);
        list.Insert(index, str);
    }

   

    public int Length(string username)
    {
        return this.users.GetValue(username).Count;
    }

    public void Login(string username)
    {
        this.users.Insert(username, new BigList<string>());
        this.cache[username] = new Stack<BigList<string>>();
    }

    public void Logout(string username)
    {
        //this.users.Insert(username, new BigList<string>());
        //this.cach[username] = new Stack<BigList<string>>();
    }

    public void Prepend(string username, string str)
    {
        this.Cache(username);
        this.users.GetValue(username).Add(str);
    }

    public string Print(string username)
    {
        var result = this.users.GetValue(username);
        return result.ToString();
    }

    public void Substring(string username, int startIndex, int length)
    {
        this.Cache(username);
        var list = this.users.GetValue(username);
        var builder = new StringBuilder();
        for (int i = startIndex; i < length+startIndex; i++)
        {
            builder.Append(list[i]);
        }
        //list = new BigList<string>(builder.ToString().Select(x=>x.ToString()));
        list.RemoveRange(startIndex, length);
    }

    public void Undo(string username)
    {
        var stack = this.cache[username];
        var newList = stack.Pop();
        this.users.Insert(username, newList);
    }

    public IEnumerable<string> Users(string prefix = "")
    {
        if (prefix=="")
        {
            foreach (var user in this.users.GetByPrefix(""))
            {
                yield return user;
            }
        }
    }

    private void Cache(string username)
    {
        var list = this.users.GetValue(username);
        this.cache[username].Push(list);
    }
}

