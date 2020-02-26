using System;
using System.Text;

namespace MatrixTransformations
{
    public class Matrix
    {
        float[,] mat = new float[2, 2];

        public Matrix()
        {
            mat[0, 0] = 1; mat[0, 1] = 0;
            mat[1, 0] = 0; mat[1, 1] = 1;
        }

        public Matrix(float m11, float m12,
                      float m21, float m22)
        {
            mat[0, 0] = m11; mat[0, 1] = m12;
            mat[1, 0] = m21; mat[1, 1] = m22;
        }

        public Matrix(Vector v)
        {
            mat[0, 0] = v.x; mat[0, 1] = 0;
            mat[1, 0] = v.y; mat[1, 1] = 0;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            Matrix matrix = new Matrix();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    matrix.mat[i, j] = m1.mat[i, j] + m2.mat[i, j];
                }
            }

            return matrix;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            Matrix matrix = new Matrix();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    matrix.mat[i, j] = m1.mat[i, j] - m2.mat[i, j];
                }
            }

            return matrix;
        }
        public static Matrix operator *(Matrix m1, float f)
        {
            Matrix matrix = new Matrix();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    matrix.mat[i, j] = m1.mat[i, j] * f;
                }
            }

            return matrix;
        }

        public static Matrix operator *(float f, Matrix m1)
        {
            Matrix matrix = new Matrix();

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    matrix.mat[i, j] = f * m1.mat[i, j];
                }
            }

            return matrix;
        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            Matrix matrix = new Matrix();

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                {
                    float sum = 0f;

                    for (int k = 0; k < 2; k++)
                        sum += m1.mat[i, k] * m2.mat[k, j];

                    matrix.mat[i, j] = sum;
                }

            return matrix;
        }

        public static Vector operator *(Matrix m, Vector v)
        {
            Matrix matrix = m * new Matrix(v);
            return new Vector(matrix.mat[0, 0], matrix.mat[1, 0]);
        }

        public static Matrix Identity()
        {
            return new Matrix();
        }

        public static Matrix ScaleMatrix(float scale) 
        {
            return new Matrix() * scale;
        }

        public static Matrix RotateMatrix() 
        {
            return new Matrix();
        }

        public override string ToString()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.Write(mat[i,j] + " ");
                }
                Console.WriteLine();
            }
            return "-----";
        }
    }
}
