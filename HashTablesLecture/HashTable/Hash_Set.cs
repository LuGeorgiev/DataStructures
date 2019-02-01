using System.Collections.Generic;
using System.Linq;

namespace Hash_Table
{
    public class Hash_Set<TKey>
    {
        private HashTable<TKey, TKey> table;

        public Hash_Set()
        {
            this.table = new HashTable<TKey, TKey>();
        }
        public Hash_Set(IEnumerable<KeyValue<TKey,TKey>> enumerable)
        {
            this.table = new HashTable<TKey, TKey>();
            foreach (var item in enumerable)
            {
                this.table.AddOrReplace(item.Key,item.Key);
            }
        }

        public void Add(TKey key)
        {
            this.table.AddOrReplace(key, key);
        }

        public Hash_Set<TKey> UnionWith(Hash_Set<TKey> other)
        {
            return new Hash_Set<TKey>(other.table.Concat(this.table).Distinct());
        }
        public Hash_Set<TKey> IntersectWith(Hash_Set<TKey> other)
        {
            return new Hash_Set<TKey>(this.table.Where(x => other.Contains(x.Key)));

        }
        public Hash_Set<TKey> Except(Hash_Set<TKey> other)
        {
            return new Hash_Set<TKey>(this.table.Where(x => !other.Contains(x.Key)));
        }
        public Hash_Set<TKey> SymetricExcept(Hash_Set<TKey> other)
        {
            return this.UnionWith(other).Except(this.IntersectWith(other));
        }

        private bool Contains(TKey key)
        {
            return this.table.ContainsKey(key);
        }
                 
    }
}
