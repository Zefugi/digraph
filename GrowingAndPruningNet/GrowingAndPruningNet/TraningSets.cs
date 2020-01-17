using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowingAndPruningNet
{
    public class TraningSets
    {
        public static void XOR(out double[][] sets, out double[][] ideals)
        {
            sets = new double[][]
            {
                new double[] { 0, 0 },
                new double[] { 1, 0 },
                new double[] { 0, 1 },
                new double[] { 1, 1 },
            };
            ideals = new double[][]
            {
                new double[] { 0 },
                new double[] { 1 },
                new double[] { 1 },
                new double[] { 0 },
            };
        }

        public static void AND(out double[][] sets, out double[][] ideals)
        {
            sets = new double[][]
            {
                new double[] { 0, 0 },
                new double[] { 1, 0 },
                new double[] { 0, 1 },
                new double[] { 1, 1 },
            };
            ideals = new double[][]
            {
                new double[] { 0 },
                new double[] { 0 },
                new double[] { 0 },
                new double[] { 1 },
            };
        }
    }
}
