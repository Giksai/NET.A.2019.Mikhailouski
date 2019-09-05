using System;
using System.Collections;
using System.Collections.Generic;

namespace _13_thDay
{
    public sealed class Queue<T> : IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
    {
        private T[] array;
        private int head;
        private int tail;
        private int counter;
        private int capacity = 8;

        public int Count { get { return counter; } }
        public bool IsReadOnly { get { return false; } }

        public Queue()
        {
            array = new T[capacity];
        }
        /// <summary>
        /// Ctor when we need to initialize queue with existing object
        /// </summary>
        /// <param name="obj">IEnumerable object of elements</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Queue(IEnumerable<T> obj) : this()
        {
            if (obj == null) throw new ArgumentNullException($"{nameof(obj)} mut not be null");
            foreach (var i in obj)
            {
                Enqueue(i);
            }
        }
        /// <summary>
        /// Adds element to queue
        /// </summary>
        /// <param name="obj">element to be added</param>
        /// <exception cref="ArgumentNullException">Argument must not be null</exception>
        public void Enqueue(T obj)
        {
            if (obj == null) throw new ArgumentNullException($"{nameof(obj)} must not be null");
            if (counter == array.Length)
            {
                Resize(counter * 2);
            }
            array[tail] = obj;
            tail = (tail + 1) % array.Length;
            counter++;
        }
        /// <summary>
        /// Dequeue element
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>Removed element from the queue</returns>
        public T Dequeue()
        {
            if (Count == 0) throw new InvalidOperationException("No elements in the queue");
            T removedObj = array[head];
            array[head] = default(T);
            head = (head + 1) % array.Length;
            counter--;
            return removedObj;
        }
        /// <summary>
        /// Remove all elements from the queue
        /// </summary>
        public void Clear()
        {
            array = new T[capacity];
            head = 0;
            tail = 0;
            counter = 0;
        }
        /// <summary>
        /// Check if the queue contains an element
        /// </summary>
        /// <param name="obj">Element to be checked</param>
        /// <param name="comparer">Rule of compearing elements</param>
        /// <returns>True if queue contains the element and false if not</returns>
        public bool Contains(T obj, EqualityComparer<T> comparer = null)
        {
            if (obj == null) return false;
            if (comparer == null) comparer = EqualityComparer<T>.Default;

            int size = Count;
            int index = head;
            while (size-- > 0)
            {
                if (index == array.Length) index = 0;
                if (comparer.Equals(array[index], obj)) return true;
                index++;
            }
            return false;
        }
        /// <summary>
        /// Finds the first element in queue
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>First element in queue</returns>
        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException("No elements in the queue");

            return array[head];
        }

        private void Resize(int newSize)
        {
            T[] newArray = new T[newSize];
            if (head < tail)
            {
                Array.Copy(array, head, newArray, 0, counter);
            }
            else
            {
                Array.Copy(array, head, newArray, 0, array.Length - head);
                Array.Copy(array, 0, newArray, array.Length - head, tail);
            }
            array = newArray;
            head = 0;
            tail = counter;
        }
        /// <summary>
        /// Deletes the first element in queue elements
        /// </summary>
        public void Trim()
        {
            if (Count != 0)
                Resize(counter);
        }
        /// <summary>
        /// Makes an iterator
        /// </summary>
        /// <returns>Iterator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            this.Trim();
            return new Iterator(this);

        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        private struct Iterator : IEnumerator<T>
        {
            private readonly Queue<T> queue;
            private int currentIndex;

            public Iterator(Queue<T> queue)
            {
                this.queue = queue;
                currentIndex = -1;
            }
            object IEnumerator.Current { get { return Current; } }
            public T Current
            {
                get
                {
                    if (currentIndex == -1 || currentIndex == queue.Count)
                        throw new InvalidOperationException();

                    return queue.array[currentIndex];
                }
            }

            public bool MoveNext()
            {
                return ++currentIndex < queue.Count;
            }

            public void Reset()
            {
                currentIndex = -1;
            }

            public void Dispose()
            {
            }
        }
        //public IEnumerator<T> GetEnumerator()
        //{
        //    int index = head;
        //    int size = Count;
        //    while (size-- > 0)
        //    {
        //        if (index == array.Length) index = 0;
        //        yield return array[index];
        //        index++;
        //    }
        //}
        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return array.GetEnumerator();
        //}
    }
}
