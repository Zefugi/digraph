using Digraph.WinForms.GraphInformation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Digraph.WinForms.GraphControls
{
    public class GraphNode : UserControl
    {
        public readonly DigraphNode Node;

        private Label _title;
        private UserControl _connectionPoint;

        public GraphNode(DigraphNode node)
        {
            Node = node;

            _title = new Label()
            {
                AutoSize = true,
                Text = node.Title,
                ForeColor = Color.White,
            };

            _connectionPoint = new UserControl()
            {
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Silver,
                Size = new Size(20, 20),
                AllowDrop = true,
            };

            _connectionPoint.MouseDown += _connectionPoint_MouseDown;
            _connectionPoint.DragEnter += _connectionPoint_DragEnter;
            _connectionPoint.DragDrop += _connectionPoint_DragDrop;

            switch(node.Direction)
            {
                case NodeDirection.Input:
                    Controls.Add(_connectionPoint);
                    Controls.Add(_title);
                    break;
                case NodeDirection.Output:
                    Controls.Add(_title);
                    Controls.Add(_connectionPoint);
                    break;
            }

            Resize += RecomputeLayout;
            _title.Resize += RecomputeLayout;
            _connectionPoint.Resize += RecomputeLayout;

            RecomputeLayout(this, null);
        }

        private void _connectionPoint_DragEnter(object sender, DragEventArgs e)
        {
            GraphNode source = (GraphNode)e.Data.GetData(typeof(GraphNode));

            if (source.Node.CanConnectTo(Node))
                e.Effect = DragDropEffects.Link;
        }

        private void _connectionPoint_DragDrop(object sender, DragEventArgs e)
        {
            GraphNode source = (GraphNode)e.Data.GetData(typeof(GraphNode));

            Node.ConnectTo(source.Node);

            Canvas.UpdateConnectionsForNode();
        }

        private void _connectionPoint_MouseDown(object sender, MouseEventArgs e)
        {
            switch(e.Button)
            {
                case MouseButtons.Left:
                    _connectionPoint.DoDragDrop(this, DragDropEffects.Link);
                    break;
                case MouseButtons.Right:
                    while (Node.Connections.Count != 0)
                    {
                        Node.DisconnectFrom(Node.Connections[0]);
                    }
                    Canvas.UpdateConnectionsForNode();
                    break;
            }
        }

        private void RecomputeLayout(object sender, EventArgs e)
        {
            int xOffset = 0;
            int maxHeight = 0;
            foreach(Control ctrl in Controls)
            {
                ctrl.Left = xOffset;
                xOffset += ctrl.Width;
                if (maxHeight < ctrl.Height)
                    maxHeight = ctrl.Height;
            }
            foreach (Control ctrl in Controls)
            {
                if (maxHeight > ctrl.Height)
                    ctrl.Top = (maxHeight - ctrl.Height) / 2;
                else
                    ctrl.Top = 0;
            }
            if (Width != xOffset)
                Width = xOffset;
            if (Height != maxHeight)
                Height = maxHeight;
        }

        public GraphCanvas Canvas
        {
            get
            {
                Control c = this;
                while (!(c is GraphCanvas) && c.Parent != null)
                    c = c.Parent;
                return (c is GraphCanvas ? (GraphCanvas)c : null);
            }
        }

        public GraphFigure Figure
        {
            get
            {
                Control c = this;
                while (!(c is GraphFigure) && c.Parent != null)
                    c = c.Parent;
                return (c is GraphFigure ? (GraphFigure)c : null);
            }
        }

        public Point CanvasPoint
        {
            get
            {
                Control c = this;
                Point p = Point.Empty;
                while(!(c is GraphCanvas) && c.Parent != null)
                {
                    p.Offset(c.Location);
                    c = c.Parent;
                }
                if (c is GraphCanvas)
                    return p;
                else
                    return Point.Empty;
            }
        }
    }
}
