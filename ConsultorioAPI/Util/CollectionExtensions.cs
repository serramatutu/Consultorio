using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsultorioAPI.Util
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Remove todos os elementos da coleção que satisfazem a condição
        /// </summary>
        public static int RemoveAll<T>(this ICollection<T> collection, Func<T, bool> condition)
        {
            int c = 0;
            foreach(T elem in collection)
            {
                if (condition.Invoke(elem))
                {
                    collection.Remove(elem);
                    c++; // hue
                }
            }

            return c;
        }
    }
}