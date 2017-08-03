using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FitMeet.Controls
{
    public class GroupsCollection<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public GroupsCollection(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}
