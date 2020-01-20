using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zefugi.Digraph.Dom
{
    public class ObservableList<T>
    {
        public enum ChangeAction
        {
            Add,
            Remove,
            Clear,
        }

        public class ChangeEventArgs : EventArgs
        {
            public readonly IEnumerable<T> OldItems;
            public readonly IEnumerable<T> NewItems;
            public readonly ChangeAction Action;

            public ChangeEventArgs(IEnumerable<T> oldItems, IEnumerable<T> newItems, ChangeAction action)
            {
                OldItems = oldItems;
                NewItems = newItems;
                Action = action;
            }
        }

        public event EventHandler<ChangeEventArgs> ListChanged;

        protected virtual void OnListChanged(ChangeEventArgs e)
        {
            ListChanged?.Invoke(this, e);
        }

        private List<T> _list = new List<T>();

        public void Add(T item)
        {
            _list.Add(item);
            OnListChanged(new ChangeEventArgs(null, new T[] { item }, ChangeAction.Add));
        }

        public void AddRange(IEnumerable<T> items)
        {
            _list.AddRange(items);
            OnListChanged(new ChangeEventArgs(null, items, ChangeAction.Add));
        }

        public void Remove(T item)
        {
            _list.Remove(item);
            OnListChanged(new ChangeEventArgs(new T[] { item }, null, ChangeAction.Remove));
        }

        public void RemoveAll(Predicate<T> predicate)
        {
            List<T> items = new List<T>();
            foreach (var item in _list)
                if (predicate.Invoke(item))
                    items.Add(item);
            _list.RemoveAll(predicate);
            OnListChanged(new ChangeEventArgs(items, null, ChangeAction.Remove));
        }

        public void RemoveAt(int index)
        {
            var item = _list[index];
            _list.RemoveAt(index);
            OnListChanged(new ChangeEventArgs(new T[] { item }, null, ChangeAction.Remove));
        }

        public void Clear()
        {
            var items = _list.ToArray<T>();
            _list.Clear();
            OnListChanged(new ChangeEventArgs(items, null, ChangeAction.Clear));
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _list.Count)
                    throw new ArgumentOutOfRangeException("index");
                return _list[index];
            }
        }

        public int Count { get { return _list.Count; } }
    }
}
