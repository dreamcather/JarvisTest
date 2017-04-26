using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace JarvisTest
{
    class Geometry
    {
        LinkedList<Point> points;
        LinkedList<Point> mch;
        public Geometry(LinkedList<Point> _points)
        {
            points = new LinkedList<Point>(_points);
            LinkedListNode<Point> current = points.First;
        }
        public LinkedListNode<Point> CallcMin()
        {
            LinkedListNode<Point> current = points.First;
            LinkedListNode<Point> min = current;
            current = current.Next;
            while (current != null)
            {
                if (current.Value.Y > min.Value.Y)
                {
                    min = current;
                }
                current = current.Next;
            }
            current = points.First;
            while (current != null)
            {
                bool fl = false;
                if ((current.Value.Y == min.Value.Y) && (current.Value != min.Value))
                {
                    if (!fl)
                    {
                        fl = true;
                    }
                }
                if ((fl) && (current.Value.Y == min.Value.Y) && (current.Value != min.Value))
                {
                    if (current.Value.X < min.Value.X)
                    {
                        min = current;
                    }
                }
                current = current.Next;
            }
            mch = new LinkedList<Point>();
            mch.AddFirst(min.Value);
            return min;
        }//Анимированный поиск минимумма
        public float CallR(Point main, Point pret, Point cur)
        {
            float res = 0;
            double a1 = main.X - cur.X;
            double a2 = main.Y - cur.Y;
            double b1 = pret.X - cur.X;
            double b2 = pret.Y - cur.Y;
            res = (float)((a2 * b1) - (b2 * a1));
            return res;
        }//Сравнение углов через ореинтированную площадь треугольника
        public double CallcRad(Point main, Point frs)
        {
            double frsr = Math.Sqrt(Math.Pow((main.X - frs.X), 2) + Math.Pow((main.Y - frs.Y), 2));
            return frsr;
        }//Расстояние между точками
        public void CallcBorder()
        {
            points.Remove(mch.First.Value);
            points.AddLast(mch.First.Value);
            LinkedListNode<Point> copy;
            while ((mch.Last.Value != mch.First.Value) || (mch.Count < 2))
            {
                LinkedListNode<Point> main = mch.Last;
                LinkedListNode<Point> pret = points.First;
                LinkedListNode<Point> curent = pret.Next;
                while (curent != null)
                {
                    if (CallR(main.Value, pret.Value, curent.Value) == 0)
                    {
                        if (CallcRad(main.Value, curent.Value) > (CallcRad(main.Value, pret.Value)))
                        {
                            copy = curent;
                            curent = pret;
                            pret = copy;
                        }
                    }
                    if (CallR(main.Value, pret.Value, curent.Value) < 0)
                    {
                        copy = curent;
                        curent = pret;
                        pret = copy;
                    }
                    curent = curent.Next;
                }
                mch.AddLast(pret.Value);
            }

        }//Плохо анимированный поиск оболочки
        public void AddRectanglePoint(int n)
        {
            Random rnd = new Random();
            Point res = new Point();
            for (int i = 0; i < n; i++)
            {
                int x = rnd.Next(-100, 100);
                int y = rnd.Next(-100, 100);
                res = new Point(x, y);
                points.AddLast(res);
            }
        }
        public void AddElipse(int n)
        {
            Random rnd = new Random();
            Point res = new Point();
            for (int i = 0; i < n; i++)
            {
                double arc = rnd.Next(0,359);
                arc = arc *Math.PI/180;
                double x = 100 * Math.Cos(arc);
                double y = 100 * Math.Sin(arc);
                res = new Point((int)x, (int)y);
                points.AddLast(res);
            }
        }
        private bool CirculTest(int x,int y)
        {
            double res = Math.Pow(x, 2) + Math.Pow(y, 2);
            if (res <= 10000)
                return false;
            else
                return true;
        }
        public void AddCirculPoint(int n)
        {
            Random rnd = new Random();
            Point res = new Point();
            bool flag = true;
            int x;
            int y;
            for (int i = 0; i < n; i++)
            {
                do
                {
                    x = rnd.Next(-100, 100);
                    y = rnd.Next(-100, 100);
                    flag = CirculTest(x, y);
                } while (flag);
                res = new Point(x, y);
                points.AddLast(res);
            }
        }
        public void Save()
        {
            int k = points.Count;
            string[] lines = new string[k];
            string x = "";
            string y = "";
            Point pr = new Point();
            LinkedListNode<Point> res = points.First;
            for(int i=0;i<k;i++)
            {
                x = res.Value.X.ToString();
                y = res.Value.Y.ToString();
                lines[i] = x +"  "+ y;
                res = res.Next;
            }
            System.IO.File.WriteAllLines(@"C:\Users\Евгений\Desktop\Игры\JarvisAlgorithm\input.txt", lines);
        }
        public void Clear()
        {
            points = new LinkedList<Point>();
        }
        public void SaveRes()
        {
            int k = mch.Count;
            string[] lines = new string[k];
            string x = "";
            string y = "";
            Point pr = new Point();
            LinkedListNode<Point> res = mch.First;
            for (int i = 0; i < k; i++)
            {
                x = res.Value.X.ToString();
                y = res.Value.Y.ToString();
                lines[i] = x + "  " + y;
                res = res.Next;
            }
            System.IO.File.WriteAllLines(@"C:\Users\Евгений\Desktop\Игры\JarvisAlgorithm\MCH.txt", lines);
        }
    }
}
