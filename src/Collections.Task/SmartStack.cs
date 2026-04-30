using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Collections.Task
{
    /// <summary>
    /// Умный стек.
    /// </summary>
    /// <typeparam name="T">Тип элементов стека.</typeparam>
    public class SmartStack<T> : IEnumerable<T>
    {
        /// <summary>
        /// Массив с элементами стека.
        /// </summary>
        private T[] _values;
        /// <summary>
        /// Количество добавленных элементов.
        /// </summary>
        private int _count;

        /// <summary>
        /// Количество добавленных элементов.
        /// </summary>
        public int Count => _count;
        /// <summary>
        /// Вместимость стека.
        /// </summary>
        public int Capacity => _values.Length;

        /// <summary>
        /// Конструктор стека.
        /// </summary>
        /// <param name="capacity">Начальная вместимость.</param>
        /// <exception cref="ArgumentOutOfRangeException">Если вместимость оказалась отрицательной.</exception>
        public SmartStack(int capacity = 4)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(capacity);

            _values = new T[capacity];
            _count = 0;
        }

        /// <summary>
        /// Конструктор стека.
        /// </summary>
        /// <param name="values">Коллекция, элементы которой будут добавлены на вершину стека.</param>
        /// <exception cref="ArgumentNullException">Если передан указатель null.</exception>
        public SmartStack(IEnumerable<T> values)
        {
            ArgumentNullException.ThrowIfNull(values);

            int count = 0;
            foreach (var value in values)
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

        /// <summary>
        /// Добавляет элемент на вершину стека.
        /// </summary>
        /// <param name="value">Добавляемый элемент.</param>
        public void Push(T value)
        {
            if (_count == _values.Length)
            {
                ExpandStack(_values.Length == 0 ? 4 : _values.Length * 2);
            }

            _values[_count] = value;
            _count++;
        }

        /// <summary>
        /// Добавляет коллекцию элементов на вершину стека.
        /// </summary>
        /// <param name="values">Коллекция добавляемых элементов.</param>
        /// <exception cref="ArgumentNullException">Если передал указатель null.</exception>
        public void PushRange(IEnumerable<T> values)
        {
            ArgumentNullException.ThrowIfNull(values);

            foreach (var value in values)
            {
                Push(value);
            }
        }

        /// <summary>
        /// Увеличивает вместимость стека.
        /// </summary>
        /// <param name="newCapacity">Новая вместимость.</param>
        /// <exception cref="InvalidOperationException">Если новая вместимость не больше текущей.</exception>
        private void ExpandStack(int newCapacity)
        {
            if (newCapacity <= _values.Length)
            {
                throw new InvalidOperationException($"Недопустимая вместимость, должно быть значение больше {_values.Length}.");
            }

            T[] newStack = new T[newCapacity];
            Array.Copy(_values, newStack, _count);
            _values = newStack;
        }

        /// <summary>
        /// Удаляет и возвращает элемент, находящийся на вершине стека.
        /// </summary>
        /// <returns>Элемент типа T, который был удалён с вершины стека.</returns>
        /// <exception cref="InvalidOperationException">Если стек пуст.</exception>
        public T Pop()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("В стеке нет элементов.");
            }

            _count--;
            T value = _values[_count];
            _values[_count] = default;
            return value;
        }

        /// <summary>
        /// Возвращает элемент, находящийся на вершине стека, не удаляя его.
        /// </summary>
        /// <returns>Элемент типа T, который находится на вершине стека.</returns>
        /// <exception cref="InvalidOperationException">Если стек пуст.</exception>
        public T Peek()
        {
            if (_count == 0)
            {
                throw new InvalidOperationException("В стеке нет элементов.");
            }

            return _values[_count - 1];
        }

        /// <summary>
        /// Проверяет, содержит ли стек элемент.
        /// </summary>
        /// <param name="value">Значение, которое требуется найти в коллекции.</param>
        /// <returns>Значение типа bool с результатом поиска.</returns>
        public bool Contains(T value)
        {
            for (int index = 0; index < _count; index++)
            {
                if (Equals(_values[index], value))
                {
                    return true;
                }
            }
            return false;
        }

        /// <inheritdoc/>
        public IEnumerator<T> GetEnumerator()
        {
            for (int index = _count - 1; index >= 0; index--)
            {
                yield return _values[index];
            }
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Возвращает элемент по заданному индексу, начиная от вершины.
        /// </summary>
        /// <param name="index">
        /// Индекс, находящийся в границах размера массива и отсчитываемый от 0, по которому возвращается элемент.
        /// </param>
        /// <returns>Элемент, расположенный по указанному индексу, начиная от вершины стека.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Если индекс меньше 0 или не меньше количества элементов в стеке.
        /// </exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                return _values[_count - index - 1];
            }
        }
    }
}