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

        public int Count => _count;
        public int Capacity => _values.Length;

        public SmartStack()
        {
            _values = new T[4];
            _count = 0;
        }

        public SmartStack(int size)
        {
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException("Размер должен быть не меньше 0.");
            }
            _values = new T[size];
            _count = 0;
        }

        public SmartStack(IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException();
            }

            int count = 0;
            foreach (var item in values)
            {
                count++;
            }

            _values = new T[count];
            foreach (var value in values)
            {
                _values[_count] = value;
                _count++;
            }
        }

        public void Push(T value)
        {
            if (_count == _values.Length)
            {
                ExpandStack(_values.Length == 0 ? 4 : _values.Length * 2);
            }

            _values[_count] = value;
            _count++;
        }

        public void PushRange(IEnumerable<T> values)
        {
            if (values == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var value in values)
            {
                Push(value);
            }
        }

        private void ExpandStack(int newSize)
        {
            T[] newStack = new T[newSize];
            Array.Copy(_values, newStack, _count);
            _values = newStack;
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