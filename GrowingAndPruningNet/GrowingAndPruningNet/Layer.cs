using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrowingAndPruningNet
{
    public class Layer
    {
        internal static Random _rng = new Random();

        public readonly int Nodes;
        public readonly int Inputs;

        internal double[] _stimuli;
        internal double[,] _weights;
        internal double[] _inputs;
        internal double[] _outputs;

        public Layer(int nodes, int inputs)
        {
            Nodes = nodes;
            Inputs = inputs;

            _stimuli = new double[Nodes];
            _weights = new double[Nodes, Inputs + 1];
            _inputs = new double[Inputs];
            _outputs = new double[Nodes];

            for (int n = 0; n < Nodes; n++)
            {
                for (int i = 0; i < Inputs; i++)
                    _weights[n, i] = _rng.NextDouble() * 2.0 - 1.0;
            }
        }

        public double[] Parse(double[] inputs)
        {
            _inputs = inputs;
            for (int n = 0; n < Nodes; n++)
            {
                double stimuli = 0.0;
                for (int i = 0; i < Inputs; i++)
                    stimuli -= _inputs[i] * _weights[n, i];
                stimuli += _weights[n, Inputs];
                _stimuli[n] = stimuli;
                _outputs[n] = Math.Tanh(stimuli);
            }
            return _outputs;
        }

        public double ComputeMeanSquaredError(double[] ideal)
        {
            double mse = 0.0;

            Console.Write($"Errors: ");
            for (int n = 0; n < Nodes; n++)
            {
                Console.Write($"{_outputs[n]} {ideal[n]} {Math.Round(ideal[n] - _outputs[n], 2)}");
                mse += Math.Pow(ideal[n] - _outputs[n], 2);
            }
            Console.WriteLine();

            return mse / Nodes;
        }

        public double TestMSE(double[][] sets, double[][] ideals)
        {
            double mse = 0;
            for (int s = 0; s < sets.Length; s++)
            {
                double[] output = Parse(sets[s]);
                mse += Math.Pow(ComputeMeanSquaredError(ideals[s]), 2);
            }
            mse /= sets.Length;
            return mse;
        }

        public void Reward(double value)
        {
            if(value < 0)
            {
                for(int n = 0; n < Nodes; n++)
                {
                    for(int i = 0; i < Inputs; i++)
                    {
                        _weights[n, i] += value * (1.0 - Math.Pow(Math.Tanh(_weights[n, i]), 2));
                    }
                    _weights[n, Inputs] += value * (1.0 - Math.Pow(Math.Tanh(_weights[n, Inputs]), 2));
                }
            }
        }
    }
}
