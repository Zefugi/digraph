using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowingAndPruningNet
{
    class Program
    {
        static void Main(string[] args)
        {
            TraningSets.AND(out double[][] sets, out double[][] ideals);

            Layer layer = new Layer(1, 2);

            double mse = layer.TestMSE(sets, ideals);
            Console.WriteLine($"Cycle 0 resulted in a MSE of {MR(mse)}.");

            int cycles = 10;
            for(int c = 1; c <= cycles; c++)
            {
                layer.Reward(-mse);
                mse = layer.TestMSE(sets, ideals);
                Console.WriteLine($"Cycle {c} resulted in a MSE of {MR(mse)}.");
            }

            Console.ReadKey();
        }

        static double MR(double value)
        {
            return Math.Round(value, 2);
        }
    }
}
