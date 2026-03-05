using System;
using System.Collections.Generic;
using System.Text;

namespace OOP.Task
{
    class CustomRectangle
    {
        private static readonly double _epsilon = 1e-8;

        private double _x;
        private double _y;
        private double _width;
        private double _height;

        public double X => _x;
        public double Y => _y;
        public double Width => _width;
        public double Height => _height;

        public double Perimeter => (_width + _height) * 2;
        public double Area => _width * _height;
        public double Diagonal => Math.Sqrt(_width * _width + _height * _height);

        public CustomRectangle(double x, double y, double width, double height)
        {
            SetSize(width, height);
            _x = x;
            _y = y;
        }

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

        public void SetPosition(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public void Offset(double dx, double dy)
        {
            _x += dx;
            _y += dy;
        }
    }
}