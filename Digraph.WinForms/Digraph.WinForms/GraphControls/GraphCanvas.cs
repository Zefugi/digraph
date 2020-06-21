using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Digraph.WinForms.GraphInformation;

namespace Digraph.WinForms.GraphControls
{
    public class GraphCanvas : UserControl
    {
        private readonly List<GraphNode> _connections = new List<GraphNode>();

        public ReadOnlyCollection<GraphNode> Connections { get { return _connections.AsReadOnly(); } }

        public GraphCanvas()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);

            BackColor = Color.SlateGray;
            Font = new Font(Font.FontFamily, 14);

            Controls.Add(new GraphFigure(DigraphFigure.FromMethod(typeof(TestClass).GetMethod("Do"))));
            Controls.Add(new GraphFigure(DigraphFigure.FromMethod(typeof(TestClass).GetMethod("Read"))));

            Controls[0].Location = new Point(100, 50);
            Controls[1].Location = new Point(300, 75);
        }

        public void UpdateConnectionsForNode()
        {
            Refresh();
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            List<GraphNode> conns = new List<GraphNode>();

            foreach(GraphFigure figure in Controls)
            {
                foreach (GraphNode node in figure.ControlNodes)
                {
                    if(node.Node.Direction == NodeDirection.Output)
                        conns.Add(node);
                }
                foreach (GraphNode node in figure.DataNodes)
                {
                    if (node.Node.Direction == NodeDirection.Output)
                        conns.Add(node);
                }
            }

            foreach(GraphNode node in conns)
            {
                Pen line = new Pen(Color.Navy, 3);
                
                Point p0 = node.CanvasPoint;
                p0.Offset(node.Width / 2, node.Height / 2);

                foreach(DigraphNode destNode in node.Node.Connections)
                {
                    GraphNode dest = (GraphNode)destNode.Tag;

                    Point p1 = dest.CanvasPoint;
                    p1.Offset(dest.Width / 2, dest.Height / 2);

                    DrawConnection(e.Graphics, line, p0, p1);
                }
            }
        }

        #region Connection Drawing
        
        // Source: https://github.com/komorra/NodeEditorWinforms/blob/master/NodeEditor/NodesGraph.cs

        public static void DrawConnection(Graphics g, Pen pen, PointF output, PointF input)
        {
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.SmoothingMode = SmoothingMode.HighQuality;

            if (input == output) return;
            const int interpolation = 48;

            PointF[] points = new PointF[interpolation];
            for (int i = 0; i < interpolation; i++)
            {
                float amount = i / (float)(interpolation - 1);

                var lx = Lerp(output.X, input.X, amount);
                var d = Math.Min(Math.Abs(input.X - output.X), 100);
                var a = new PointF((float)Scale(amount, 0, 1, output.X, output.X + d),
                    output.Y);
                var b = new PointF((float)Scale(amount, 0, 1, input.X - d, input.X), input.Y);

                var bas = Sat(Scale(amount, 0.1, 0.9, 0, 1));
                var cos = Math.Cos(bas * Math.PI);
                if (cos < 0)
                {
                    cos = -Math.Pow(-cos, 0.2);
                }
                else
                {
                    cos = Math.Pow(cos, 0.2);
                }
                amount = (float)cos * -0.5f + 0.5f;

                var f = Lerp(a, b, amount);
                points[i] = f;
            }

            g.DrawLines(pen, points);
        }

        public static double Sat(double x)
        {
            if (x < 0) return 0;
            if (x > 1) return 1;
            return x;
        }

        public static double Scale(double x, double a, double b, double c, double d)
        {
            double s = (x - a) / (b - a);
            return s * (d - c) + c;
        }

        public static float Lerp(float a, float b, float amount)
        {
            return a * (1f - amount) + b * amount;
        }

        public static PointF Lerp(PointF a, PointF b, float amount)
        {
            PointF result = new PointF();

            result.X = a.X * (1f - amount) + b.X * amount;
            result.Y = a.Y * (1f - amount) + b.Y * amount;

            return result;
        }

        #endregion
    }
}
