using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace FitMeet.Controls
{
    public class ObservableReadOnlyCollection<T> : IObservableReadOnlyCollection<T>
    {
        public ObservableCollection<T> _inner;
        public ObservableReadOnlyCollection( ObservableCollection<T> inner ) { _inner = inner; }

        public T this[int i] => _inner[i];
        public int Count => _inner.Count;

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { _inner.CollectionChanged += value; }
            remove { _inner.CollectionChanged -= value; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _inner.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _inner.GetEnumerator();
        }
    }
}