using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixMultiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            int m = 2, n = 2, p = 2, q = 2;

            Console.WriteLine("Masukan matriks A");
            int[,] a = InputMatrix(m, n);
            Console.WriteLine("Matriks A adalah:");
            TraversalMatrix(a, m, n);

            Console.WriteLine("Masukan matriks B");
            int[,] b = InputMatrix(p, q);
            Console.WriteLine("Matriks B adalah:");
            TraversalMatrix(b, p, q);

            int[,] c = MultiplyMatrix(a, b);
            Console.WriteLine("Perkalian Matriks A x B adalah:");
            TraversalMatrix(c, m, q);

            Console.ReadKey(); //hold windows from close
        }

        static int[,] InputMatrix(int m, int n)
        {
            int[,] a = new int[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    a[i, j] = int.Parse(Console.ReadLine());
                }
            }

            return a;
        }

        static void TraversalMatrix(int[,] matrix, int m, int n)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        static int[,] MultiplyMatrix(int[,] a, int[,] b)
        {
            int m = a.GetLength(0), n = a.GetLength(1);
            int p = b.GetLength(0), q = b.GetLength(1);
            int[,] c = new int[m, q];

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
