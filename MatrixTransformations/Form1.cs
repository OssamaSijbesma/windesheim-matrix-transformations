using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MatrixTransformations
{
    public partial class Form1 : Form
    {
        // Axes
        AxisX x_axis;
        AxisY y_axis;

        // Objects
        //Square square;
        //Square square1;
        //Square square2;
        //Square square3;
        Cube cube;

        // Window dimensions
        const int WIDTH = 800;
        const int HEIGHT = 600;

        // Variables
        private float scale = 1.5F;
        private float xTranslation = 0F;
        private float yTranslation = 0F;
        private float zTranslation = 0F;
        private float xRotation = 0f;
        private float yRotation = 0f;
        private float zRotation = 0f;

        private float r = 10F;
        private float d = 800F;
        private float phi = -10F;
        private float theta = -100F;

        private Vector translation = new Vector(75, -25);

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
                2, 4, 0,
                -1, 3, 0,
                0, 0, 0);
            Console.WriteLine(m2);
            Console.WriteLine(m1 + m2); // 3, 4, -1, 4
            Console.WriteLine(m1 - m2); // -1, -4, 1, -2
            Console.WriteLine(m2 * m2); // 0, 20, -5, 5

            Console.WriteLine(m2 * v3); // 28, 16

            // Define axes
            x_axis = new AxisX(200);
            y_axis = new AxisY(200);

            // Create object
            //square = new Square(Color.Purple,100);
            //square1 = new Square(Color.Cyan, 100);
            //square2 = new Square(Color.Gold, 100);
            //square3 = new Square(Color.DarkBlue, 100);
            cube = new Cube(Color.Purple);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw axes
            x_axis.Draw(e.Graphics, ViewportTransformation(x_axis.vb));
            y_axis.Draw(e.Graphics, ViewportTransformation(y_axis.vb));

            // Draw squares
            //square.Draw(e.Graphics, ViewportTransformation(square.vb));
            //square1.Draw(e.Graphics, ViewportTransformation(ScaleTransformation(square1.vb, scale)));
            //square2.Draw(e.Graphics, ViewportTransformation(RotationTransformation(square2.vb, degrees)));
            //square3.Draw(e.Graphics, ViewportTransformation(TranslationTransformation(square3.vb, translation)));

            // Draw cube
            cube.Draw(e.Graphics, ViewportTransformation(cube.vertexbuffer));

            ShowInfo(e.Graphics);
        }

        private void ShowInfo(Graphics g)
        {
            // Create font and brush.
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // StringBuilder.
            StringBuilder sb = new StringBuilder();

            // Add lines.
            sb.AppendLine($"TranslateX: \t {xTranslation} \t Left / Right");
            sb.AppendLine($"TranslateY: \t {yTranslation} \t Up / Down");
            sb.AppendLine($"TranslateZ: \t {zTranslation} \t PgDn / PgUp");
            sb.AppendLine($"RotateX: \t {xRotation} \t x / X");
            sb.AppendLine($"RotateY: \t {yRotation} \t y / Y");
            sb.AppendLine($"RotateZ: \t {zRotation} \t z / Z");
            sb.AppendLine($"Scale: \t\t {scale} \t s / S");
            sb.AppendLine();
            sb.AppendLine($"r: \t {r} \t r / R");
            sb.AppendLine($"d: \t {d} \t d / D");
            sb.AppendLine($"Phi: \t {phi} \t p / P");
            sb.AppendLine($"Theta: \t {theta} \t t / T");

            // Draw String.
            g.DrawString(sb.ToString(), drawFont, drawBrush, 1,1);
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

        public static List<Vector> RotationTransformation(List<Vector> vb, float degrees)
        {
            List<Vector> result = new List<Vector>();
            Matrix rotateMatrix = Matrix.RotateMatrixZ(degrees);

            foreach (Vector v in vb)
                result.Add(rotateMatrix * v);

            return result;
        }

        public static List<Vector> TranslationTransformation(List<Vector> vb, Vector translation)
        {
            List<Vector> result = new List<Vector>();
            Matrix translateMatrix = Matrix.TranslateMatrix(translation);

            foreach (Vector v in vb)
                result.Add(translateMatrix * v);

            return result;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Application.Exit();
                    break;
                case Keys.PageUp:
                    zTranslation += 1;
                    break;
                case Keys.PageDown:
                    zTranslation -= 1;
                    break;
                case Keys.Left:
                    xTranslation += 1;
                    break;
                case Keys.Up:
                    yTranslation += 1;
                    break;
                case Keys.Right:
                    xTranslation -= 1;
                    break;
                case Keys.Down:
                    yTranslation -= 1;
                    break;
                case Keys.C:
                    d = 800F;
                    r = 10F;
                    theta = -100F;
                    phi = -10;
                    break;
                case Keys.D:
                    d = (e.Modifiers == Keys.Shift) ? d + 1F : d - 1F;
                    break;
                case Keys.P:
                    phi = (e.Modifiers == Keys.Shift) ? phi + 1F : phi - 1F;
                    break;
                case Keys.R:
                    r = (e.Modifiers == Keys.Shift) ? r + 1F : r - 1F;
                    break;
                case Keys.S:
                    scale = (e.Modifiers == Keys.Shift) ? scale + 0.1F : scale - 0.1F;
                    break;
                case Keys.T:
                    theta = (e.Modifiers == Keys.Shift) ? theta + 1F : theta - 1F;
                    break;
                case Keys.X:
                    xRotation = (e.Modifiers == Keys.Shift) ? xRotation + 1F : xRotation - 1F;
                    break;
                case Keys.Y:
                    yRotation = (e.Modifiers == Keys.Shift) ? yRotation + 1F : yRotation - 1F;
                    break;
                case Keys.Z:
                    zRotation = (e.Modifiers == Keys.Shift) ? zRotation + 1F : zRotation - 1F;
                    break;
            }

            Invalidate();
        }
    }
}
