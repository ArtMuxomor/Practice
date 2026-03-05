using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Collections.Task
{
    class SmartStack<T> : IEnumerable<T>
    {
        private T[] _values;
        private int _count;

        public int Count => throw new NotImplementedException();

        public int Capacity => throw new NotImplementedException();

        public SmartStack()
        {
            throw new NotImplementedException();
        }

        public SmartStack(int size)
        {
            throw new NotImplementedException();
        }

        public SmartStack(IEnumerable<T> values)
        {
            throw new NotImplementedException();
        }

        public void Push(T value)
        {
            throw new NotImplementedException();
        }

        public void PushRange(IEnumerable<T> values)
        {
            throw new NotImplementedException();
        }

        public T Pop()
        {
            throw new NotImplementedException();
        }

        public T Peek()
        {
            throw new NotImplementedException();
        }

        public bool Contains(T value)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}