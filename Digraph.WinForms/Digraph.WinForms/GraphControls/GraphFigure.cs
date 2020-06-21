using Digraph.WinForms.GraphInformation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Digraph.WinForms.GraphControls
{
    public class GraphFigure : UserControl
    {
        public readonly DigraphFigure Figure;

        private Label _title;
        private UserControl _controlNodes;
        private UserControl _dataNodes;

        private Point _mouseMoveOffset = Point.Empty;

        public ControlCollection ControlNodes { get { return _controlNodes.Controls; } }
        public ControlCollection DataNodes { get { return _dataNodes.Controls; } }

        public GraphFigure(DigraphFigure figure)
        {
            Figure = figure;

            BackColor = Color.DarkSlateGray;
            BorderStyle = BorderStyle.FixedSingle;

            _title = new Label()
            {
                AutoSize = true,
                Text = figure.Title,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.White,
                BackColor = Color.Navy,
            };
            _title.MouseDown += _title_MouseDown;
            _title.MouseMove += _title_MouseMove;
            _title.Cursor = Cursors.Hand;
            Controls.Add(_title);

            _controlNodes = new UserControl()
            {
                BackColor = Color.CornflowerBlue,
            };
            Controls.Add(_controlNodes);

            _dataNodes = new UserControl()
            {
                BackColor = Color.SlateBlue,
            };
            Controls.Add(_dataNodes);

            foreach (var node in Figure.ControlNodes)
            {
                var ctrl = new GraphNode(node);
                node.Tag = ctrl;
                ctrl.Resize += RecomputeLayout;
                _controlNodes.Controls.Add(ctrl);
            }

            foreach (var node in Figure.DataNodes)
            {
                var data = new GraphNode(node);
                node.Tag = data;
                data.Resize += RecomputeLayout;
                _dataNodes.Controls.Add(data);
            }

            RecomputeLayout(this, null);
        }

        private void _title_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                _mouseMoveOffset = new Point(-e.X, -e.Y);
                BringToFront();
            }
        }

        private void _title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point loc = Location;
                loc.Offset(_mouseMoveOffset);
                loc.Offset(e.Location);
                Location = loc;
                Canvas.Refresh();
            }
        }

        private void RecomputeLayout(object sender, EventArgs e)
        {
            ControlCollection[] ctrlLists = new ControlCollection[]
            {
                _controlNodes.Controls,
                _dataNodes.Controls,
            };
            int yOffset = _title.Height;
            int leftWidth = 0;
            int rightWidth = 0;
            foreach (ControlCollection ctrlList in ctrlLists)
            {
                int left = 0;
                int right = 0;
                foreach (GraphNode ctrl in ctrlList)
                {
                    switch (ctrl.Node.Direction)
                    {
                        case NodeDirection.Input:
                            ctrl.Left = 0;
                            ctrl.Top = left;
                            left += ctrl.Height;
                            if (ctrl.Width > leftWidth)
                                leftWidth = ctrl.Width;
                            break;
                        case NodeDirection.Output:
                            ctrl.Left = Width - ctrl.Width;
                            ctrl.Top = right;
                            right += ctrl.Height;
                            if (ctrl.Width > rightWidth)
                                rightWidth = ctrl.Width;
                            break;
                    }
                }
                ctrlList.Owner.Top = yOffset;
                ctrlList.Owner.Height = left > right ? left : right;
                yOffset += ctrlList.Owner.Height;
            }
            int newWidth = leftWidth + rightWidth + 10;
            if (newWidth < _title.Width)
                newWidth = _title.Width;
            if (Height != yOffset || Width != newWidth)
            {
                Height = yOffset;
                Width = newWidth;
                _title.Location = new Point((Width - _title.Width) / 2, 0);
                RecomputeLayout(sender, e);
            }
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
    }
}
