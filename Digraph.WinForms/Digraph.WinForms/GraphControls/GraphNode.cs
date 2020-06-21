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
                Size = new Size(13, 13),
            };

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
    }
}
