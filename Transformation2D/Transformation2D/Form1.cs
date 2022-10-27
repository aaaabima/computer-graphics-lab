using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transformation2D
{
    public partial class Form1 : Form
    {
        float x = 50, y = 50, size = 100;
        private static PointF[] points;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = null;
            comboBox1.SelectedText = "--select--";
            panel1.Paint += new PaintEventHandler(panel1_Paint);
            panel2.Paint += new PaintEventHandler(panel2_Paint);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.Black, 3);
            if (comboBox1.SelectedIndex == 0)
            {
                PointF point1 = new PointF(50.0F, 125.0F);
                PointF point2 = new PointF(100.0F, 50.0F);
                PointF point3 = new PointF(150.0F, 150.0F);
                points = new PointF[] { point1, point2, point3 };
                e.Graphics.DrawPolygon(blackPen, points);
            }
            else if (comboBox1.SelectedIndex == 1)
                e.Graphics.DrawRectangle(blackPen, x, y, size, size);
            else if (comboBox1.SelectedIndex == 2)
                e.Graphics.DrawEllipse(blackPen, x, y, size, size);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Pen blackPen = new Pen(Color.Black, 3);
            if (comboBox1.SelectedIndex == 0)
            {
                e.Graphics.DrawPolygon(blackPen, points);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < points.GetLength(0); i++)
            {
                points[i].X += float.Parse(textBox1.Text);
                points[i].Y += float.Parse(textBox2.Text);
            }
            panel2.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            float deg = float.Parse(textBox3.Text);
            float theta =(float) (Math.PI * deg / 180.0);
            float[,] a = new float[points.GetLength(0), 2];

            translatePoint(points, -100, -100);
            a = convertPoint2Matrix(points);

            float [,] b = new float[,] { 
                {(float)Math.Cos(theta), (float)-Math.Sin(theta)},
                {(float)Math.Sin(theta), (float)Math.Cos(theta)}
            };
            float[,] c = MultiplyMatrix(a, b);

            convertMatrix2Point(c);
            translatePoint(points, 100, 100);
      
            panel2.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            float[,] a = new float[points.GetLength(0), 2];
            float[,] b = new float[,] { { 1, 0 }, { 0, 1 } };

            translatePoint(points, -100, -100);
            a = convertPoint2Matrix(points);

            if (comboBox2.SelectedIndex == 0)
            {
               b = new float[,] { {1, 0}, {0, -1} };
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                b = new float[,] { { -1, 0 }, { 0, 1 } };
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                b = new float[,] { { 0, 1 }, { 1, 0 } };
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                b = new float[,] { { 0, -1 }, { -1, 0 } };
            }
            float[,] c = MultiplyMatrix(a, b);
        
            convertMatrix2Point(c);
            translatePoint(points, 100, 100);

            panel2.Invalidate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            float sx = float.Parse(textBox4.Text);
            float sy = float.Parse(textBox5.Text);
            float[,] a = new float[points.GetLength(0), 2];

            translatePoint(points, -100, -100);
            a = convertPoint2Matrix(points);

            float[,] b = new float[,] {
                {sx, 0},
                {0, sy}
            };
            float[,] c = MultiplyMatrix(a, b);

            convertMatrix2Point(c);
            translatePoint(points, 100, 100);

            panel2.Invalidate();
        }

        static void translatePoint(PointF[] points, float x, float y)
        {
            for (int i = 0; i < points.GetLength(0); i++)
            {
                points[i].X += x;
                points[i].Y += y;
            }
        }
        
        static float[,] convertPoint2Matrix(PointF[] points)
        {
            float[,] a = new float[points.GetLength(0), 2];
            for (int i = 0; i < points.GetLength(0); i++)
            {
                a[i, 0] = points[i].X;
                a[i, 1] = points[i].Y;
            }
            return a;
        }

        static void convertMatrix2Point(float[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                points[i].X = a[i, 0];
                points[i].Y = a[i, 1];
            }
        }

        static float[,] MultiplyMatrix(float[,] a, float[,] b)
        {
            int m = a.GetLength(0), n = a.GetLength(1);
            int p = b.GetLength(0), q = b.GetLength(1);
            float[,] c = new float[m, q];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < q; j++)
                {
                    c[i, j] = 0;
                    for (int k = 0; k < n; k++)
                    {
                        c[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return c;
        }
    }
}
