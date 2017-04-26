using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace JarvisTest
{
    public partial class Form1 : Form
    {
        string[] mytime=new string[440];
        int n = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LinkedList<Point> pr = new LinkedList<Point>();
            Geometry gr = new Geometry(pr);
            for (int i = 1; i <= 100; i++)
            {
                int kol = 10000*i;
                textBox1.Text = i.ToString();
                textBox1.Refresh();
                Stopwatch stopWatch = new Stopwatch();
                gr.AddRectanglePoint(kol);
                stopWatch.Start();
                gr.CallcMin();
                gr.CallcBorder();
                stopWatch.Stop();
                SentReport(stopWatch.ElapsedMilliseconds,kol,"Rectangle");
                gr.Clear();
                gr.AddCirculPoint(kol);
                stopWatch.Restart();
                gr.CallcMin();
                gr.CallcBorder();
                stopWatch.Stop();
                SentReport(stopWatch.ElapsedMilliseconds, kol, "Circul");
                gr.Clear();
                gr.AddElipse(kol);
                stopWatch.Restart();
                gr.CallcMin();
                gr.CallcBorder();
                stopWatch.Stop();
                SentReport(stopWatch.ElapsedMilliseconds, kol, "Elipse");
                myEnter();
            }
            System.IO.File.WriteAllLines(@"D:\Компьютерная графика\OpenGl\Программа\JarvisTest\resulttime.txt", mytime);
            this.Close();
        }
        void SentReport(long time,int kol, string type)
        {
            string stime = time.ToString();
            string skol = kol.ToString();
            mytime[n] = skol + "  " + stime+"   "+type;
            n++;
        }
        void myEnter()
        {
            mytime[n] = "";
            n++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LinkedList<Point> pr = new LinkedList<Point>();
            Geometry gr = new Geometry(pr);
            gr.AddCirculPoint(200);
            gr.Save();
            gr.CallcMin();
            gr.CallcBorder();
            gr.SaveRes();
            this.Close();
        }
    }
}
