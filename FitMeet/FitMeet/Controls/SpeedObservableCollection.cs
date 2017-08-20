using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace FitMeet.Controls
{
    public class SpeedObservableCollection<T> : ObservableCollection<T>
    {
        public SpeedObservableCollection() : base() { }

        public SpeedObservableCollection(IEnumerable<T> collection) : base(collection) { }

        public SpeedObservableCollection(List<T> list) : base(list) { }

        public void AddRange(IEnumerable<T> range)
        {
            int startIndex = Count;
            foreach (var item in range)
            {
                Items.Add(item);
            }

            this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add,range.ToList(),startIndex));
        }

        public void Reset(IEnumerable<T> range)
        {
            this.Items.Clear();

            AddRange(range);
        }
    }
}
