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
        public LinkedList<Mypoint> points;
        public LinkedList<Mypoint> mch;
        public struct Mypoint
        {
            public double X;
            public double Y;
        }
        public Geometry(LinkedList<Mypoint> _points)
        {
            points = new LinkedList<Mypoint>(_points);
            LinkedListNode<Mypoint> current = points.First;
        }
        public LinkedListNode<Mypoint> CallcMin()
        {
            LinkedListNode<Mypoint> current = points.First;
            LinkedListNode<Mypoint> min = current;
            current = current.Next;
            while (current != null)
            {
                if (current.Value.Y < min.Value.Y)
                {
                    min = current;
                }
                else if (current.Value.Y == min.Value.Y)
                {
                    if (current.Value.X < min.Value.X)
                        min = current;
                }
                current = current.Next;
            }
            mch = new LinkedList<Mypoint>();
            mch.AddFirst(min.Value);
            return min;
        }//Анимированный поиск минимумма
        public double CallR(Mypoint main, Mypoint pret, Mypoint cur)
        {
            double res = 0;
            double a1 = main.X - cur.X;
            double a2 = main.Y - cur.Y;
            double b1 = pret.X - cur.X;
            double b2 = pret.Y - cur.Y;
            res = (a2 * b1) - (b2 * a1);
            return res;
        }//Сравнение углов через ореинтированную площадь треугольника
        public double CallcRad(Mypoint main, Mypoint frs)
        {
            double frsr = Math.Sqrt(Math.Pow((main.X - frs.X), 2) + Math.Pow((main.Y - frs.Y), 2));
            return frsr;
        }//Расстояние между точками
        public void CallcBorder()
        {
            points.Remove(mch.First.Value);
            points.AddLast(mch.First.Value);
            LinkedListNode<Mypoint> copy;
            while ((mch.Last.Value.X != mch.First.Value.X)&&(mch.Last.Value.Y != mch.First.Value.Y) || (mch.Count < 3))
            {
                LinkedListNode<Mypoint> main = mch.Last;
                LinkedListNode<Mypoint> pret = points.First;
                LinkedListNode<Mypoint> curent = pret.Next;
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
                    else if (CallR(main.Value, pret.Value, curent.Value) > 0)
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
            Mypoint res = new Mypoint();
            for (int i = 0; i < n; i++)
            {
                double x = rnd.NextDouble() * 200-100;
                double y = rnd.NextDouble() * 200-100;
                res = new Mypoint();
                res.X = x;
                res.Y = y;
                points.AddLast(res);
            }
        }
        public void AddtrangleTest(int n)
        {
            Random rnd = new Random();
            Mypoint res = new Mypoint();
            points.AddLast(res);
            for (int i = 0; i < n; i++)
            {
                double x = rnd.NextDouble()*200-100;
                double y = rnd.NextDouble() * 200-100;
                res = new Mypoint();
                res.X = x;
                res.Y = y;
                points.AddLast(res);
            }
        }
        public void AddElipse(int n)
        {
            Random rnd = new Random();
            Mypoint res = new Mypoint();
            for (int i = 0; i < n; i++)
            {
                double arc = rnd.NextDouble() * 360;
                arc = arc *Math.PI/180;
                double x = 100 * Math.Cos(arc);
                double y = 100 * Math.Sin(arc);
                res = new Mypoint();
                res.X = x;
                res.Y = y;
                points.AddLast(res);
            }
        }
        private bool CirculTest(double x,double y)
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
            Mypoint res = new Mypoint();
            bool flag = true;
            double x;
            double y;
            for (int i = 0; i < n; i++)
            {
                do
                {
                    x = rnd.NextDouble() * 200 - 100;
                    y = rnd.NextDouble() * 200 - 100;
                    flag = CirculTest(x, y);
                } while (flag);
                res = new Mypoint();
                res.X = x;
                res.Y = y;
                points.AddLast(res);
            }
        }
        public void Save()
        {
            int k = points.Count;
            string[] lines = new string[k];
            string x = "";
            string y = "";
            Mypoint pr = new Mypoint();
            LinkedListNode<Mypoint> res = points.First;
            for(int i=0;i<k;i++)
            {
                x = res.Value.X.ToString();
                y = res.Value.Y.ToString();
                lines[i] = x +"  "+ y;
                res = res.Next;
            }
            System.IO.File.WriteAllLines(@"D:\Компьютерная графика\OpenGl\Программа\JarvisTest\input.txt", lines);
        }
        public void Clear()
        {
            points = new LinkedList<Mypoint>();
        }
        public void SaveRes()
        {
            int k = mch.Count;
            string[] lines = new string[k];
            string x = "";
            string y = "";
            Mypoint pr = new Mypoint();
            LinkedListNode<Mypoint> res = mch.First;
            for (int i = 0; i < k; i++)
            {
                x = res.Value.X.ToString();
                y = res.Value.Y.ToString();
                lines[i] = x + "  " + y;
                res = res.Next;
            }
            System.IO.File.WriteAllLines(@"D:\Компьютерная графика\OpenGl\Программа\JarvisTest\MCH.txt", lines);
        }
    }
}
