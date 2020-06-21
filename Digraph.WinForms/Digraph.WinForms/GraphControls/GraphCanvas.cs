using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Digraph.WinForms.GraphInformation;

namespace Digraph.WinForms.GraphControls
{
    public class GraphCanvas : UserControl
    {
        public GraphCanvas()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.SlateGray;
            Font = new Font(Font.FontFamily, 14);

            Controls.Add(new GraphFigure(DigraphFigure.FromMethod(typeof(TestClass).GetMethod("Do"))));
            Controls.Add(new GraphFigure(DigraphFigure.FromMethod(typeof(TestClass).GetMethod("Read"))));
        }
    }
}
