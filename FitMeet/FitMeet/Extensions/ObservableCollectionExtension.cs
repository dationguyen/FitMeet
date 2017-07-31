using Microsoft.Practices.ObjectBuilder2;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FitMeet.Extensions
{
    public static class ObservableCollectionExtension
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            items.ToList().ForEach(collection.Add);
        }
    }
}
