using System;
using System.Text;

namespace MatrixTransformations
{
    public class Matrix
    {
        float[,] mat = new float[4, 4];

        public Matrix()
        {
            mat[0, 0] = 1; mat[0, 1] = 0; mat[0, 2] = 0; mat[0, 3] = 0;
            mat[1, 0] = 0; mat[1, 1] = 1; mat[1, 2] = 0; mat[1, 3] = 0;
            mat[2, 0] = 0; mat[2, 1] = 0; mat[2, 2] = 1; mat[2, 3] = 0;
            mat[3, 0] = 0; mat[3, 1] = 0; mat[3, 2] = 0; mat[3, 3] = 1;
        }

        public Matrix(float m11, float m12, float m13,
                      float m21, float m22, float m23,
                      float m31, float m32, float m33) : this()
        {

            mat[0, 0] = m11; mat[0, 1] = m12; mat[0, 2] = m13;
            mat[1, 0] = m21; mat[1, 1] = m22; mat[1, 2] = m23;
            mat[2, 0] = m31; mat[2, 1] = m32; mat[2, 2] = m33;
        }

        public Matrix(Vector v)
        {
            mat[0, 0] = v.x; 
            mat[1, 0] = v.y;
            mat[2, 0] = v.z;
            mat[3, 0] = 1;
        }

        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            Matrix matrix = new Matrix();

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    matrix.mat[i, j] = m1.mat[i, j] + m2.mat[i, j];

            return matrix;
        }

        public static Matrix operator -(Matrix m1, Matrix m2)
        {
            Matrix matrix = new Matrix();

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    matrix.mat[i, j] = m1.mat[i, j] - m2.mat[i, j];

            return matrix;
        }
        public static Matrix operator *(Matrix m1, float f)
        {
            Matrix matrix = new Matrix();

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    matrix.mat[i, j] = m1.mat[i, j] * f;

            return matrix;
        }

        public static Matrix operator *(float f, Matrix m1)
        {
            Matrix matrix = new Matrix();

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    matrix.mat[i, j] = f * m1.mat[i, j];

            return matrix;
        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            Matrix matrix = new Matrix();

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    float sum = 0f;

                    for (int k = 0; k < 4; k++)
                        sum += m1.mat[i, k] * m2.mat[k, j];

                    matrix.mat[i, j] = sum;
                }

            return matrix;
        }

        public static Vector operator *(Matrix m, Vector v)
        {
            Matrix matrix = m * new Matrix(v);
            return new Vector(matrix.mat[0, 0], matrix.mat[1, 0], matrix.mat[2,0]);
        }

        public static Matrix Identity()
        {
            return new Matrix();
        }

        public static Matrix ScaleMatrix(float scale) 
        {
            Matrix matrix = new Matrix() * scale;
            matrix.mat[3, 3] = 1;
            return matrix;
        }

        public static Matrix RotateMatrix(float degrees) 
        {
            var radians = degrees * Math.PI / 180;
            Matrix matrix = new Matrix();

            matrix.mat[0, 0] = (float) Math.Cos(radians);
            matrix.mat[0, 1] = (float) -Math.Sin(radians);
            matrix.mat[1, 0] = (float) Math.Sin(radians);
            matrix.mat[1, 1] = (float) Math.Cos(radians);

            return matrix;
        }

        public static Matrix TranslateMatrix(Vector vector)
        {
            Matrix matrix = new Matrix();
            matrix.mat[0, 3] = vector.x;
            matrix.mat[1, 3] = vector.y;
            matrix.mat[2, 3] = vector.z;
            return matrix;
        }

        public override string ToString()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(mat[i,j] + " ");
                }
                Console.WriteLine();
            }
            return "-----";
        }
    }
}
