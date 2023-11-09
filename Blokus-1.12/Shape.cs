using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Blokus_1._12
{
    internal class Shape
    {
        //Обьявление переменных
        public int x;
        public int y;
        public int[,] matrix;
        //Создание фигуры
        public Shape(int _x, int _y)
        {
            x = _x;
            y = _y;
            matrix = new int[5, 5]
            {
                {0,0,0,0,0},
                {0,0,0,0,0},
                {0,0,0,0,0},
                {1,1,1,1,1},
                {0,0,0,0,0},
            };
        }
        //Движение фигуры
        public void MoveDown()
        {
            y++;
        }
        public void MoveUp()
        {
            y--;
        }
        public void MoveRight()
        {
            x--;
        }
        public void MoveLeft()
        {
            x++;
        }
    }
}
