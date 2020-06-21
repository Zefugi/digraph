using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
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

                    e.Graphics.DrawLine(line, p0, p1);
                }
            }
        }
    }
}
