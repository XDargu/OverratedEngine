using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Space_Pirates.Util
{
    public class Pool<T>
    {
        private readonly List<T> items = new List<T>();
        private readonly Queue<T> freeItems = new Queue<T>();

        private readonly Func<T> createItemAction;
        private readonly int limit;

        public List<T> Items
        {
            get { return items; }
        }

        public int FreeItems
        {
            get { return freeItems.Count; }
        }

        public Pool(Func<T> createItemAction)
        {
            this.createItemAction = createItemAction;
            this.limit = int.MaxValue;
        }

        public Pool(Func<T> createItemAction, int limit)
        {
            this.createItemAction = createItemAction;
            this.limit = limit;
        }

        public void FlagFreeItem(T item)
        {
            if (freeItems.Count < limit)
            {
                freeItems.Enqueue(item);
            }
            else
            {
                items.Remove(item);
            }
        }

        public T GetFreeItem()
        {
            if (freeItems.Count == 0)
            {
                T item = createItemAction();
                items.Add(item);

                return item;
            }

            return freeItems.Dequeue();
        }

        public void Clear()
        {
            items.Clear();
            freeItems.Clear();
        }
    }
}
