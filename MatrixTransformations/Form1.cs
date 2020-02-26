﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MatrixTransformations
{
    public partial class Form1 : Form
    {
        // Axes
        AxisX x_axis;
        AxisY y_axis;

        // Objects
        Square square;
        Square square1;
        Square square2;


        // Window dimensions
        const int WIDTH = 800;
        const int HEIGHT = 600;

        public Form1()
        {
            InitializeComponent();

            this.Width = WIDTH;
            this.Height = HEIGHT;
            this.DoubleBuffered = true;

            Vector v1 = new Vector();
            Console.WriteLine(v1);
            Vector v2 = new Vector(1, 2);
            Console.WriteLine(v2);
            Vector v3 = new Vector(2, 6);
            Console.WriteLine(v3);
            Vector v4 = v2 + v3;
            Console.WriteLine(v4); // 3, 8

            Matrix m1 = new Matrix();
            Console.WriteLine(m1); // 1, 0, 0, 1
            Matrix m2 = new Matrix(
                2, 4,
                -1, 3);
            Console.WriteLine(m2);
            Console.WriteLine(m1 + m2); // 3, 4, -1, 4
            Console.WriteLine(m1 - m2); // -1, -4, 1, -2
            Console.WriteLine(m2 * m2); // 0, 20, -5, 5

            Console.WriteLine(m2 * v3); // 28, 16

            // Define axes
            x_axis = new AxisX(200);
            y_axis = new AxisY(200);

            // Create object
            square = new Square(Color.Purple,100);
            square1 = new Square(Color.Cyan, 100);
            square2 = new Square(Color.Gold, 100);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw axes
            x_axis.Draw(e.Graphics, ViewportTransformation(x_axis.vb));
            y_axis.Draw(e.Graphics, ViewportTransformation(y_axis.vb));

            // Draw square
            square.Draw(e.Graphics, ViewportTransformation(square.vb));

            // Draw scaled square
            square1.Draw(e.Graphics, ViewportTransformation(ScaleTransformation(square.vb, 1.5F)));
        }

        public static List<Vector> ViewportTransformation(List<Vector> vb) 
        {
            List<Vector> result = new List<Vector>();

            float delta_x = WIDTH / 2;
            float delta_y = HEIGHT / 2;

            foreach (Vector v in vb) 
                result.Add(new Vector(v.x + delta_x, delta_y - v.y));

            return result;
        }

        public static List<Vector> ScaleTransformation(List<Vector> vb, float scale)
        {
            List<Vector> result = new List<Vector>();
            Matrix scaleMatrix = Matrix.ScaleMatrix(scale);

            foreach (Vector v in vb)
                result.Add(scaleMatrix * v);

            return result;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();

            Invalidate();
        }
    }
}