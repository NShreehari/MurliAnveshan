using System;
using System.Collections.Generic;

namespace MurliSearch.Classes
{
    public static class ExtensionMehods
    {
        /// <summary>
        /// Attempts to add a key-value pair to the dictionary. Prevents duplicate keys.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dictionary">The dictionary to add to.</param>
        /// <param name="key">The key to add.</param>
        /// <param name="value">The value to associate with the key.</param>
        /// <returns>True if the key-value pair was added; otherwise, false.</returns>
        public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (dictionary.ContainsKey(key))
                return false;

            dictionary.Add(key, value);
            return true;
        }

        public static void TryAdd<T>(this List<T> list, T item)
        {
            if (!list.Contains(item))
            {
                list.Add(item);
            }
        }

        public static void TryAddRange<T>(this List<T> list, List<T> itemsList)
        {
            foreach (var item in itemsList)
            {
                if (!list.Contains(item))
                {
                    list.Add(item);
                }
            }
        }
    }
}
