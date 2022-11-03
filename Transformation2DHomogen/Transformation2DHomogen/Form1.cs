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
        private static PointF[] points;
        private static List<PointF> NewPolygon = new List<PointF>();
        private PointF NewPoint;
        private bool isFinishedDraw = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Paint += new PaintEventHandler(panel1_Paint);
            panel2.Paint += new PaintEventHandler(panel2_Paint);
            panel1.MouseDown += new MouseEventHandler(this.panel1_MouseDown);
            panel1.MouseMove += new MouseEventHandler(this.panel1_MouseMove);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(panel1.BackColor);

            int xOrigem = 200 / 2;
            int yOrigem = 200 / 2;

            g.DrawLine(Pens.Black, new Point(xOrigem, 0), new Point(xOrigem, this.Bottom));
            g.DrawLine(Pens.Black, new Point(0, yOrigem), new Point(this.Right, yOrigem));
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(xOrigem - 2, yOrigem - 2), new Size(4, 4)));

            if (isFinishedDraw && NewPolygon.Count > 2)
            {
                e.Graphics.FillPolygon(Brushes.White, NewPolygon.ToArray());
                e.Graphics.DrawPolygon(Pens.Black, NewPolygon.ToArray());
            }

            if (!isFinishedDraw)
            {
                if (NewPolygon.Count > 1)
                    e.Graphics.DrawLines(Pens.Black, NewPolygon.ToArray());

                if (NewPolygon.Count > 0)
                {
                    using (Pen dashed_pen = new Pen(Color.Black))
                    {
                        dashed_pen.DashPattern = new float[] { 3, 3 };
                        e.Graphics.DrawLine(dashed_pen,
                            NewPolygon[NewPolygon.Count - 1],
                            NewPoint);
                    }
                }
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isFinishedDraw)
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (NewPolygon.Count > 2)
                        isFinishedDraw = true;
                }
                else
                {
                    if (NewPolygon[NewPolygon.Count - 1] != e.Location)
                        NewPolygon.Add(e.Location);
                }
            }
            else
            {
                //NewPolygon = new List<Point>();
                if (NewPolygon.Count > 0) return;
                isFinishedDraw = false;
                NewPoint = e.Location;
                NewPolygon.Add(e.Location);
            }

            panel1.Invalidate();
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isFinishedDraw) return;
            NewPoint = e.Location;
            panel1.Invalidate();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int xOrigem = 200 / 2;
            int yOrigem = 200 / 2;
            g.DrawLine(Pens.Black, new Point(xOrigem, 0), new Point(xOrigem, this.Bottom));
            g.DrawLine(Pens.Black, new Point(0, yOrigem), new Point(this.Right, yOrigem));
            g.FillEllipse(Brushes.Black, new Rectangle(new Point(xOrigem - 2, yOrigem - 2), new Size(4, 4)));

            if (isFinishedDraw && NewPolygon.Count > 2)
            {
                e.Graphics.FillPolygon(Brushes.White, points);
                e.Graphics.DrawPolygon(Pens.Black, points);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            float tx = float.Parse(textBox1.Text);
            float ty = float.Parse(textBox2.Text);

            points = NewPolygon.ToArray();
            translatePoint(points, -100, -100);
            float[,] a = convertPoint2Matrix(points);

            float[,] b = new float[,] {
                {1, 0, 0},
                {0, 1, 0},
                {tx, ty, 1}
            };
            float[,] c = MultiplyMatrix(a, b);

            convertMatrix2Point(c);
            translatePoint(points, 100, 100);

            NewPolygon = points.ToList();
            panel2.Invalidate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            float deg = float.Parse(textBox3.Text);
            float theta =(float) (Math.PI * deg / 180.0);
            float cx = float.Parse(textBox6.Text);
            float cy = float.Parse(textBox7.Text);

            points = NewPolygon.ToArray();
            translatePoint(points, -(100 + cx), -(100 + cy));
            float[,] a = convertPoint2Matrix(points);

            float [,] b = new float[,] { 
                {(float)Math.Cos(theta), (float)-Math.Sin(theta), 0},
                {(float)Math.Sin(theta), (float)Math.Cos(theta), 0},
                {0, 0, 1}
            };
            float[,] c = MultiplyMatrix(a, b);

            convertMatrix2Point(c);
            translatePoint(points, 100 + cx, 100 + cy);

            NewPolygon = points.ToList();
            panel2.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            float[,] b = new float[,] { { 1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1} };

            points = NewPolygon.ToArray();
            translatePoint(points, -100, -100);
            float[,] a = convertPoint2Matrix(points);

            if (comboBox2.SelectedIndex == 0)
            {
               b = new float[,] { {1, 0, 0}, {0, -1, 0}, { 0, 0, 1 } };
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                b = new float[,] { { -1, 0, 0 }, { 0, 1, 0 }, { 0, 0, 1 } };
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                b = new float[,] { { 0, 1, 0 }, { 1, 0, 0 }, { 0, 0, 1 } };
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                b = new float[,] { { 0, -1, 0 }, { -1, 0, 0 }, { 0, 0, 1 } };
            }
            float[,] c = MultiplyMatrix(a, b);
        
            convertMatrix2Point(c);
            translatePoint(points, 100, 100);

            NewPolygon = points.ToList();
            panel2.Invalidate();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            float sx = float.Parse(textBox4.Text);
            float sy = float.Parse(textBox5.Text);

            points = NewPolygon.ToArray();
            translatePoint(points, -100, -100);
            float[,] a = convertPoint2Matrix(points);

            float[,] b = new float[,] {
                {sx, 0, 0},
                {0, sy, 0},
                {0, 0, 1}
            };
            float[,] c = MultiplyMatrix(a, b);

            convertMatrix2Point(c);
            translatePoint(points, 100, 100);

            NewPolygon = points.ToList();
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
            float[,] a = new float[points.GetLength(0), 3];
            for (int i = 0; i < points.GetLength(0); i++)
            {
                a[i, 0] = points[i].X;
                a[i, 1] = points[i].Y;
                a[i, 2] = 1;
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
