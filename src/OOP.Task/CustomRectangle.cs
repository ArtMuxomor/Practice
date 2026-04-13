using System;
using System.Collections.Generic;
using System.Text;

namespace OOP.Task
{
    /// <summary>
    /// Новая реализация прямоугольника.
    /// </summary>
    public class CustomRectangle
    {
        /// <summary>
        /// Стандартная точность, по которой проверяются значения.
        /// </summary>
        private static readonly double _epsilon = 1e-8;

        /// <summary>
        /// Координата x верхней левой вершины прямоугольника.
        /// </summary>
        private double _x;
        /// <summary>
        /// Координата y верхней левой вершины прямоугольника.
        /// </summary>
        private double _y;
        /// <summary>
        /// Ширина прямоугольника.
        /// </summary>
        private double _width;
        /// <summary>
        /// Высота прямоугольника.
        /// </summary>
        private double _height;

        /// <summary>
        /// Свойство, отражающее координату x верхней левой вершины прямоугольника.
        /// </summary>
        public double X => _x;
        /// <summary>
        /// Свойство, отражающее координату y верхней левой вершины прямоугольника.
        /// </summary>
        public double Y => _y;
        /// <summary>
        /// Свойство, отражающее ширину прямоугольника.
        /// </summary>
        public double Width => _width;
        /// <summary>
        /// Свойство, отражающее высоту прямоугольника.
        /// </summary>
        public double Height => _height;

        /// <summary>
        /// Свойство, отражающее периметр прямоугольника.
        /// </summary>
        public double Perimeter => (_width + _height) * 2;
        /// <summary>
        /// Свойство, отражающее площадь прямоугольника.
        /// </summary>
        public double Area => _width * _height;
        /// <summary>
        /// Свойство, отражающее длину диагонали прямоугольника.
        /// </summary>
        public double Diagonal => Math.Sqrt(_width * _width + _height * _height);

        /// <summary>
        /// Конструктор прямоугольника.
        /// </summary>
        /// <param name="x">Координата x верхней левой вершины прямоугольника.</param>
        /// <param name="y">Координата y верхней левой вершины прямоугольника.</param>
        /// <param name="width">Ширина прямоугольника.</param>
        /// <param name="height">Высота прямоугольника.</param>
        /// <exception cref="ArgumentOutOfRangeException">Если передан не положительный размер.</exception>
        public CustomRectangle(double x, double y, double width, double height)
        {
            SetSize(width, height);
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Изменение размера прямоугольника.
        /// </summary>
        /// <param name="width">Новая ширина прямоугольника.</param>
        /// <param name="height">Новая высота прямоугольника.</param>
        /// <exception cref="ArgumentOutOfRangeException">Если передан не положительный размер.</exception>
        public void SetSize(double width, double height)
        {
            if (width < _epsilon)
            {
                throw new ArgumentOutOfRangeException(nameof(width), $"Ширина фигуры должна быть больше {_epsilon}.");
            }
            if (height < _epsilon)
            {
                throw new ArgumentOutOfRangeException(nameof(height), $"Высота фигуры должна быть больше {_epsilon}.");
            }
            _width = width;
            _height = height;
        }

        /// <summary>
        /// Изменение расположения прямоугольника.
        /// </summary>
        /// <param name="x">Новая координата x верхней левой вершины прямоугольника.</param>
        /// <param name="y">Новая координата y верхней левой вершины прямоугольника.</param>
        public void SetPosition(double x, double y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Смещение прямоугольника на заданное значение.
        /// </summary>
        /// <param name="dx">
        /// Значение, на которое нужно изменить координату x верхней левой вершины прямоугольника.
        /// </param>
        /// <param name="dy">
        /// Значение, на которое нужно изменить координату y верхней левой вершины прямоугольника.
        /// </param>
        public void Offset(double dx = 0.0, double dy = 0.0)
        {
            _x += dx;
            _y += dy;
        }
    }
}