using System.Collections.Generic;
using System.Collections.Specialized;

namespace FitMeet.Controls
{
    public interface IObservableReadOnlyCollection<out T> : IEnumerable<T>, INotifyCollectionChanged
    {
        T this[int i] { get; }
        int Count { get; }
    }
}