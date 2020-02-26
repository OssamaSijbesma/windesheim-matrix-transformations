using System;
using System.Text;

namespace MatrixTransformations
{
    public class Vector
    {
        public float x, y;

        public Vector()
        {
        }

        public Vector(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            Vector vector = new Vector(0,0);
            vector.x = v1.x + v2.x;
            vector.y = v2.y + v2.y;
            return vector;
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            Vector vector = new Vector(0,0);
            vector.x = v1.x - v2.x;
            vector.y = v2.y - v2.y;
            return vector;
        }

        public static Vector operator *(Vector v, float f)
        {
            Vector vector = new Vector(0,0);
            vector.x = v.x * f;
            vector.y = v.y * f;
            return vector;
        }
        
        public static Vector operator *(float f, Vector v)
        {
            Vector vector = new Vector(0, 0);
            vector.x = v.x * f;
            vector.y = v.y * f;
            return vector;
        }

        public override string ToString()
        {
            return "x: "+this.x+" y: "+this.y;
        }
    }
}
