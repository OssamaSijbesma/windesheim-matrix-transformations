using System;
using System.Text;

namespace MatrixTransformations
{
    public class Vector
    {
        public float x, y, z, w;

        public Vector()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
            this.w = 1;
        }

        public Vector(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0;
            this.w = 1;
        }

        public Vector(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = 1;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            Vector vector = new Vector();
            vector.x = v1.x + v2.x;
            vector.y = v1.y + v2.y;
            vector.z = v1.z + v2.z;
            return vector;
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            Vector vector = new Vector();
            vector.x = v1.x - v2.x;
            vector.y = v1.y - v2.y;
            vector.z = v1.z - v2.z;
            return vector;
        }

        public static Vector operator *(Vector v, float f)
        {
            Vector vector = new Vector();
            vector.x = v.x * f;
            vector.y = v.y * f;
            vector.z = v.z * f;
            return vector;
        }
        
        public static Vector operator *(float f, Vector v)
        {
            Vector vector = new Vector();
            vector.x = v.x * f;
            vector.y = v.y * f;
            vector.z = v.z * f;
            return vector;
        }

        public override string ToString()
        {
            return $"x: {x}, y: {y}, z: {z}";
        }
    }
}
