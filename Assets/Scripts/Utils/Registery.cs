using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    public delegate T ObjectsInRegistery<T>(IEnumerable<T> items);
    public class Registery<T> where T : class
    {
        static readonly HashSet<T> items = new();

        public static bool TryAdd(T item)
        {
            return item != null && items.Add(item);
        }

        public static bool Remove(T item)
        {
            return items.Remove(item);
        }

        public static T GetFirst()
        {
            return items.FirstOrDefault();
        }
    
        public static T Get(ObjectsInRegistery<T> itemsRegistery) => itemsRegistery(items);
    
        public static IEnumerable<T> All => items;
    }
}