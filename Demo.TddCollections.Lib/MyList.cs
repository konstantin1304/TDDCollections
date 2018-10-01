using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.TddCollections.Lib
{
    public class MyList<T> : IList<T>
    {
        private class ListItem
        {
            public T Value { get; set; }
            public ListItem Next { get; set; }
        }

        private ListItem Head { get; set; }
        private ListItem Tail { get; set; }

        public T this[int index]
        {
            get => GetItem(index).Value;
            set => GetItem(index).Value = value;
        }

        public int Count { get; private set; }
        public bool IsReadOnly { get => false; }

        private ListItem GetItem(int index)
        {
            if(index<0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Index Out Of Range");
            }
            ListItem item = Head;
            while(index-- > 0)
            {
                item = item.Next;
            }
            return item;
        }

        public void Add(T value)
        {
            if(Head==null && Tail == null)
            {
                Head = Tail = new ListItem { Value = value };
                Count = 1;
                return;
            }
            Tail = Tail.Next = new ListItem { Value = value };
            ++Count;
        }

        public void Clear()
        {
            Tail = Head = null;
            Count = 0;
        }

        public bool Contains(T value)
        {
            foreach(T el in GetNextItem)
            {
                if (el.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ListItem listItem = Head;
            int count = array.Length - arrayIndex - 1;
            while (--arrayIndex > 0)
            {
                if (listItem == null)
                {
                    throw new MyListException("The list is uncordinated");
                }
                listItem = listItem.Next;
            }

            for(int i=0; i<count; i++)
            {
                array[i] = listItem.Value;
                listItem = listItem.Next;
                if (listItem == null) throw new MyListException("The list is uncordinated");
            }
            
        }

        public int IndexOf(T value)
        {
            ListItem listItem = Head;
            for (int i = 0; !listItem.Value.Equals(value)&&listItem != Tail; i++)
            {
                if (listItem.Value.Equals(value))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Insert(int index, T value)
        {
            ListItem valueItem = new ListItem();
            valueItem.Value = value;
            if (Count == 0)
            {
                Head = valueItem;
                Tail = valueItem;
                valueItem.Next = Tail;
                return;
            }


            if (index == 0)
            {
                valueItem.Next = Head;
                Head = valueItem;
                return;
            }
            ListItem listItem = Head; //item i-1
            for (int i = 1; i<index; i++)
            {
                if (listItem == null) throw new IndexOutOfRangeException("Index Out Of Range");
                listItem = listItem.Next;
            }
            if (listItem == null) throw new IndexOutOfRangeException("Index Out Of Range");
            if (listItem == Tail)
            {
                Tail.Next = valueItem;
                Tail = valueItem;
                return;
            }
            valueItem.Next = listItem.Next;
            listItem.Next = valueItem;
        }

        public bool Remove(T value)
        {
            if (Count == 0) return false;
            if (Count == 1)
            {
                if (Head.Value.Equals(value))
                {
                    Head = Tail = null;
                    Count--;
                    return true;
                }

                return false;
            }


            if (Head.Value.Equals(value))
            {
                Head = Head.Next;
                return true;
            }

            ListItem listItem = Head;
            ListItem listItemP = Head;
            for(int i=1; i<Count; i++)
            {
                listItem = listItem.Next;
                if (listItem.Value.Equals(value))
                {
                    listItemP.Next = listItem.Next;
                    Count--;
                    return true;
                }
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Index Out Of Range");
            }

            if (index == 0)
            {
                
                if (Count == 1)
                {
                    Head = Tail = null;
                    Count--;
                    return;
                }

                Head = Head.Next;
                Count--;
                return;
            }
            ListItem listItem = Head;
            for(int i=1; i<index; i++)
            {
                listItem = listItem.Next;
            }
            if (index == Count - 1)
            {
                Tail = listItem;
                listItem.Next = null;
                return;
            }
            listItem.Next = listItem.Next.Next;
            Count--;
        }

        private IEnumerable<T> GetNextItem
        {
            get
            {
                ListItem listItem = Head;
                while (listItem != null)
                {
                    yield return listItem.Value;
                    listItem = listItem.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<T> GetEnumerator()
        {
            foreach(T item in GetNextItem)
            {
                yield return item;
            }
        }

        public class MyListException : Exception
        {
            public MyListException(string message) : base(message)
            {
            }
        }

    }
}
