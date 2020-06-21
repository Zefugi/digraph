using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digraph.WinForms.GraphInformation
{
    public class DigraphFigure
    {
        public static DigraphFigure FromMethod(MethodInfo method)
        {
            Type objType = method.DeclaringType;

            DigraphFigure figure = new DigraphFigure()
            {
                Title = $"{method.Name}\n({objType.Name})",
            };

            figure.ControlNodes.Add(new DigraphNode()
            {
                Title = "",
                DataType = null,
                Direction = NodeDirection.Input
            });
            figure.ControlNodes.Add(new DigraphNode()
            {
                Title = "",
                DataType = null,
                Direction = NodeDirection.Output
            });

            if (method.ReturnType != typeof(void))
                figure.DataNodes.Add(new DigraphNode()
                {
                    Title = "Ret",
                    DataType = method.ReturnType,
                    Direction = NodeDirection.Output
                });
            foreach(var param in method.GetParameters())
            {
                figure.DataNodes.Add(new DigraphNode()
                {
                    Title = param.Name,
                    DataType = param.ParameterType,
                    Direction = param.IsOut ? NodeDirection.Output : NodeDirection.Input
                });
            }

            return figure;
        }

        public readonly List<DigraphNode> ControlNodes = new List<DigraphNode>();
        public readonly List<DigraphNode> DataNodes = new List<DigraphNode>();

        public string Title;
    }
}
