using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blokus_1._12
{
    public partial class Form1 : Form
    {
        Graphics g;
        Shape currentShape;
        //Отвечает за размер квадратика
        int size;
        //Массив для карты
        int[,] map = new int[20, 20];
        public Form1()
        {
            InitializeComponent();
            Init();      
        
        }
        //Init иницивлизирует переменные
        public void Init()
        {
            //Подключает клавиши
            this.KeyUp += new KeyEventHandler(keyFunc);
            //То где будет появлятся фигура
            currentShape = new Shape(5, 0);
            size = 25;
            Invalidate();
        }
        //Подключает клавиши
        private void keyFunc(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    currentShape.MoveDown();
                    break;
                case Keys.Up:
                    currentShape.MoveUp();
                    break;
                case Keys.Right:
                    currentShape.MoveRight();
                    break;
                case Keys.Left:
                    currentShape.MoveLeft();
                    break;
            }
            Refresh(); // Обновление отображения после каждого движения
        }

        private void update(object sender, EventArgs e)
        {
            Merge();
            Invalidate();
            ResetArea();

        }



        //Синхронизирует матрицу фигуры с картой
        public void Merge()
        {
            for(int i = currentShape.y; i < currentShape.y + 5; i++)
            {
                for (int j = currentShape.x; j < currentShape.x + 5; j++)
                {
                    map[i, j] = currentShape.matrix[i - currentShape.y, j - currentShape.x];
                }
            }
        }
        //Для того чтобы обджект не оставлял следов
        public void ResetArea()
        {
            for (int i = currentShape.y; i < currentShape.y + 5; i++)
            {
                for (int j = currentShape.x; j < currentShape.x + 5; j++)
                {
                    map[i, j] = 0;
                }
            }
        }

        //public void DrawMap(Graphics e)
        //{
        //    for(int i = 0; i < 20; i++)
        //    {
        //        for (int j = 0; j < 20; j++)
        //        {
        //            if (map[i, j] == 1)
        //            {
        //                e.FillRectangle(Brushes.Red, new Rectangle(50 + j * size, 50 + i * size, size, size));
        //            }
        //        }
        //    }
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            // Очистка поля
            Graphics g = this.CreateGraphics();
            g.Clear(this.BackColor);

            // Отрисовка сетки
            DrawGrid(g);

            // Создание и отрисовка фигуры в центре поля
            int centerX = 7; // координаты центра поля
            int centerY = 7;
            DrawShapeAtCenter(centerX, centerY, g);

            // Обработка нажатий стрелок для движения фигуры
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Получаем нажатую стрелку и сдвигаем фигуру
            switch (e.KeyCode)
            {
                case Keys.Up:
                    currentShape.MoveUp();
                    break;
                case Keys.Down:
                    currentShape.MoveDown();
                    break;
                case Keys.Left:
                    currentShape.MoveLeft();
                    break;
                case Keys.Right:
                    currentShape.MoveRight();
                    break;
            }

            // Перерисовываем поле с учетом нового положения фигуры
            Graphics g = this.CreateGraphics();
            g.Clear(this.BackColor);
            DrawGrid(g);
            DrawShapeAtCenter(currentShape.x, currentShape.y, g);
        }
        private void DrawShapeAtCenter(int centerX, int centerY, Graphics g)
        {
            int size = 25; // Размер ячейки сетки

            Shape shape = new Shape(centerX, centerY); // Создаем объект Shape с заданными координатами

            // Отрисовка объекта Shape на поле
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (shape.matrix[i, j] == 1)
                    {
                        g.FillRectangle(Brushes.Red, 50 + (centerX + i) * (size)+1, 100 + (centerY + j) * (size)+1, size - 1, size - 1); // Отрисовка клетки
                    }
                }
            }
        }


        //Отрисовка клеток двойным циклом(а точнее всего поля) 
        public void DrawGrid(Graphics g)
        {
            int size = 25; // Размер ячейки сетки

            for (int i = 0; i <= 20; i++)
            {
                // Отрисовка горизонтальных линий
                g.DrawLine(Pens.Black, new Point(50, 100 + i * size), new Point(50 + 20 * size, 100 + i * size));
            }
            for (int i = 0; i <= 20; i++)
            {
                // Отрисовка вертикальных линий
                g.DrawLine(Pens.Black, new Point(50 + i * size, 100), new Point(50 + i * size, 100 + 20 * size));
            }

        }

        //Добавил саморучно через Paint
        private void OnPaint(object sender, PaintEventArgs e)
        {
            DrawGrid(e.Graphics);
            //DrawMap(e.Graphics);
        }

       
    }
}
