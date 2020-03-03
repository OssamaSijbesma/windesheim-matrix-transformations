using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixTransformations
{
    class Piramid : Model
    {
        public Piramid(Color color) : base(color) 
        {
            vertexbuffer.Add(new Vector(1.0f, -1.0f, 1.0f));    // 0
            vertexbuffer.Add(new Vector(-1.0f, -1.0f, 1.0f));   // 1
            vertexbuffer.Add(new Vector(-1.0f, -1.0f, -1.0f));  // 2
            vertexbuffer.Add(new Vector(1.0f, -1.0f, -1.0f));   // 3

            vertexbuffer.Add(new Vector(0f, 1.0f, 0f));     // 4
        }

        public override void Draw(Graphics g, List<Vector> vb)
        {

            Pen pen = new Pen(color, 3f);
            g.DrawLine(pen, vb[0].x, vb[0].y, vb[1].x, vb[1].y);    //0 -> 1
            g.DrawLine(pen, vb[1].x, vb[1].y, vb[2].x, vb[2].y);    //1 -> 2
            g.DrawLine(pen, vb[2].x, vb[2].y, vb[3].x, vb[3].y);    //2 -> 3
            g.DrawLine(pen, vb[3].x, vb[3].y, vb[0].x, vb[0].y);    //3 -> 0

            g.DrawLine(pen, vb[0].x, vb[0].y, vb[4].x, vb[4].y);    //0 -> 4
            g.DrawLine(pen, vb[1].x, vb[1].y, vb[4].x, vb[4].y);    //0 -> 4
            g.DrawLine(pen, vb[2].x, vb[2].y, vb[4].x, vb[4].y);    //0 -> 4
            g.DrawLine(pen, vb[3].x, vb[3].y, vb[4].x, vb[4].y);    //0 -> 4
        }
    }
}
