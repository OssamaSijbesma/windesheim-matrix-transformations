using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Timers;

namespace MatrixTransformations
{
    public partial class Form1 : Form
    {
        // Axes
        AxisX x_axis;
        AxisY y_axis;
        AxisZ z_axis;

        // Objects
        Cube cube;
        Piramid piramid;

        // Window dimensions
        const int WIDTH = 800;
        const int HEIGHT = 600;

        // Variables
        private float scale = 1F;
        private float xTranslation = 0F;
        private float yTranslation = 0F;
        private float zTranslation = 0F;
        private float xRotation = 0F;
        private float yRotation = 0F;
        private float zRotation = 0F;

        private float r = 10F; // length of the vector
        private float d = 800F;
        private float phi = -10F; // angle z-axis
        private float theta = -100F; // angle y-axis

        private int modelType = 0;

        // Matrices
        private Matrix scaleMatrix;
        private Matrix rotationMatrix;
        private Matrix translationMatrix;
        private Matrix transformationMatrix;

        // Animation
        System.Timers.Timer timer;
        private bool animation = false;
        private bool animationToggel = true;
        private int phase = 0;

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
            z_axis = new AxisZ(200);

            // Initialize objects
            cube = new Cube(Color.Purple);
            piramid = new Piramid(Color.BurlyWood);

            // Initialize the timer
            timer = new System.Timers.Timer(50);
            timer.AutoReset = true;
            timer.Elapsed += CubeAnimation;
            timer.Start();
        }

        

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw axes
            x_axis.Draw(e.Graphics,
                ViewportTransformation(
                ProjectionTransformation(d,
                ViewTransformation(r, phi, theta, x_axis.vb))));
            y_axis.Draw(e.Graphics,
                ViewportTransformation(
                ProjectionTransformation(d,
                ViewTransformation(r, phi, theta, y_axis.vb))));
            z_axis.Draw(e.Graphics, 
                ViewportTransformation(
                ProjectionTransformation(d,
                ViewTransformation(r, phi, theta, z_axis.vb))));

            // Redefine matrices
            scaleMatrix = Matrix.ScaleMatrix(scale);
            rotationMatrix = Matrix.RotateMatrixX(xRotation) * Matrix.RotateMatrixY(yRotation) * Matrix.RotateMatrixZ(zRotation);
            translationMatrix = Matrix.TranslateMatrix(new Vector(xTranslation, yTranslation, zTranslation));

            // Combine the matrices
            transformationMatrix = scaleMatrix * rotationMatrix * translationMatrix;

            switch (modelType)
            {
                case 2:
                    // Draw piramid
                    piramid.Draw(e.Graphics,
                        ViewportTransformation(
                        ProjectionTransformation(d,
                        ViewTransformation(r, phi, theta,
                        Transformation(transformationMatrix, piramid.vertexbuffer)))));
                    break;
                case 1: default:
                    // Draw cube
                    cube.Draw(e.Graphics,
                        ViewportTransformation(
                        ProjectionTransformation(d,
                        ViewTransformation(r, phi, theta,
                        Transformation(transformationMatrix, cube.vertexbuffer)))));
                    break;
            }

            ShowInfo(e.Graphics);
        }

        private void ShowInfo(Graphics g)
        {
            // Create font and brush
            Font drawFont = new Font("Arial", 10);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            // StringBuilder
            StringBuilder sb = new StringBuilder();

            // Add lines
            sb.AppendLine($"TranslateX: \t {string.Format("{0:0.##}", xTranslation)} \t Left / Right");
            sb.AppendLine($"TranslateY: \t {string.Format("{0:0.##}", yTranslation)} \t Up / Down");
            sb.AppendLine($"TranslateZ: \t {string.Format("{0:0.##}", zTranslation)} \t PgDn / PgUp");
            sb.AppendLine($"RotateX: \t {string.Format("{0:0.##}", xRotation)} \t x / X");
            sb.AppendLine($"RotateY: \t {string.Format("{0:0.##}", yRotation)} \t y / Y");
            sb.AppendLine($"RotateZ: \t {string.Format("{0:0.##}", zRotation)} \t z / Z");
            sb.AppendLine($"Scale: \t\t {string.Format("{0:0.##}", scale)} \t s / S");
            sb.AppendLine();
            sb.AppendLine($"r: \t {string.Format("{0:0.##}", r)} \t r / R");
            sb.AppendLine($"d: \t {string.Format("{0:0.##}", d)} \t d / D");
            sb.AppendLine($"Phi: \t {string.Format("{0:0.##}", phi)} \t p / P");
            sb.AppendLine($"Theta: \t {string.Format("{0:0.##}", theta)} \t t / T");
            sb.AppendLine();
            sb.AppendLine($"Phase: \t {phase}");
            sb.AppendLine();
            sb.AppendLine("Press 1 for a cube");
            sb.AppendLine("Press 2 for a piramid");

            // Draw String
            g.DrawString(sb.ToString(), drawFont, drawBrush, 1,1);
        }

        public static List<Vector> ViewportTransformation(List<Vector> vb) 
        {
            float delta_x = WIDTH / 2;
            float delta_y = HEIGHT / 2;

            // Center the model trough matrix calculations
            List<Vector> result = new List<Vector>();
            vb.ForEach(v => result.Add(new Vector(v.x + delta_x, delta_y - v.y)));
            return result;
        }

        public static List<Vector> Transformation(Matrix transformationMatrix, List<Vector> vb) 
        {
            // Use the scale, rotation and translation matrix to transfrom the vectors
            List<Vector> result = new List<Vector>();
            vb.ForEach(v => result.Add(transformationMatrix * v));
            return result;
        }

        public static List<Vector> ViewTransformation(float r, float phi, float theta, List<Vector> vb)
        {
            Matrix inverseMatrix = Matrix.InverseMatrix(r, phi, theta);

            // Use the inverse matrix to transfrom the vectors
            List<Vector> result = new List<Vector>();
            vb.ForEach(v => result.Add(inverseMatrix * v));                
            return result;
        }

        public static List<Vector> ProjectionTransformation(float d, List<Vector> vb)
        {
            // Use the projection matrix to transfrom the vectors
            List<Vector> result = new List<Vector>();
            vb.ForEach(v => result.Add(Matrix.ProjectionMatrix(d, v.z) * v));
            return result;
        }

        private void CubeAnimation(object sender, ElapsedEventArgs e)
        {
            // An animation which ticks every 50 ms
            if (animation)
            {
                switch (phase)
                {
                    case 1:
                        if (scale < 1F) phase = 2;
                        if (scale > 1.5F || scale < 1F) animationToggel = !animationToggel;

                        scale = animationToggel ? scale + 0.01F : scale - 0.01F;
                        theta -= 1F;
                        break;
                    case 2:
                        if (xRotation < 0F) phase = 3;
                        if (xRotation > 45F || xRotation < 0F) animationToggel = !animationToggel;

                        xRotation = animationToggel ? xRotation + 1F : xRotation - 1F;
                        theta -= 1F;
                        break;
                    case 3:
                        if (yRotation < 0F) phase = 0;
                        if (yRotation > 45F || yRotation < 0F) animationToggel = !animationToggel;

                        yRotation = animationToggel ? yRotation + 1F : yRotation - 1F;
                        phi += 1F;
                        break;
                    default:
                        if (phi == -10F && theta == -100F) phase = 1;
                        if (theta != -100F) theta += 1F;
                        if (phi != -10F) phi -= 1F;
                        break;
                }
                Invalidate();
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Application.Exit();
                    break;
                case Keys.PageUp:
                    zTranslation += 0.1F;
                    break;
                case Keys.PageDown:
                    zTranslation -= 0.1F;
                    break;
                case Keys.Left:
                    xTranslation += 0.1F;
                    break;
                case Keys.Up:
                    yTranslation += 0.1F;
                    break;
                case Keys.Right:
                    xTranslation -= 0.1F;
                    break;
                case Keys.Down:
                    yTranslation -= 0.1F;
                    break;
                case Keys.A:
                    animation = true;
                    break;
                case Keys.C:
                    scale = 1F;
                    xTranslation = 0F;
                    yTranslation = 0F;
                    zTranslation = 0F;
                    xRotation = 0F;
                    yRotation = 0F;
                    zRotation = 0F;
                    d = 800F;
                    r = 10F;
                    theta = -100F;
                    phi = -10;
                    animation = false;
                    phase = 0;
                    break;
                case Keys.D:
                    d = (e.Modifiers == Keys.Shift) ? d + 1F : d - 1F;
                    break;
                case Keys.P:
                    phi = (e.Modifiers == Keys.Shift) ? phi + 0.1F : phi - 0.1F;
                    break;
                case Keys.R:
                    r = (e.Modifiers == Keys.Shift) ? r + 1F : r - 1F;
                    break;
                case Keys.S:
                    scale = (e.Modifiers == Keys.Shift) ? scale + 0.1F : scale - 0.1F;
                    break;
                case Keys.T:
                    theta = (e.Modifiers == Keys.Shift) ? theta + 0.1F : theta - 0.1F;
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
                case Keys.D1: case Keys.NumPad1:
                    modelType = 1;
                    break;
                case Keys.D2: case Keys.NumPad2:
                    modelType = 2;
                    break;
            }

            Invalidate();
        }
    }
}
