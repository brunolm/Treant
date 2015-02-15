using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treant.Core.Extenders
{
    public static class IListExtender
    {
        public static int AddSorted<TSource, TKey>(this IList<TSource> list, TSource item, Func<TSource, TKey> orderByKeySelector, Comparer<TKey> comparer = null)
        {
            if (comparer == null)
                comparer = Comparer<TKey>.Default;

            int i = 0;

            while (i < list.Count && comparer.Compare(orderByKeySelector(list[i]), orderByKeySelector(item)) < 0)
                i++;

            list.Insert(i, item);

            return i;
        }
    }
}
