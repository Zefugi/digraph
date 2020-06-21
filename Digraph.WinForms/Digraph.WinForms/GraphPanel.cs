using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using Digraph.WinForms.GraphControls;

namespace Digraph.WinForms
{
    public class GraphPanel : Panel
    {
        public readonly GraphCanvas Canvas;

        public GraphPanel()
        {
            AutoScroll = true;
            BackColor = Color.Magenta;
            
            Canvas = new GraphCanvas() {
                Size = ClientSize,
            };
            Controls.Add(Canvas);
        }

        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            Canvas.Size = ClientSize;
        }
    }
}
