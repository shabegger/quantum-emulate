using System;
using Quantum.Math;

namespace Quantum.Emulate
{
    public class GateConstraintException : Exception
    {
        internal const string UNITARY_MESSAGE =
            "The matrix representing a quantum logic gate must be unitary.";
        internal const string FUNCTION_MESSAGE =
            "A logic function must be represented by a 2^M x 2^N matrix.";

        public GateConstraintException(string message) : base(message) { }
    }

    public class Gate
    {
        #region Private Members

        private ComplexMatrix matrix;

        #endregion

        #region Properties

        internal ComplexMatrix Matrix
        {
            get { return matrix; }
        }

        #endregion

        #region Constructors

        public Gate(ComplexMatrix matrix)
        {
            if (!matrix.IsUnitary) throw new GateConstraintException(GateConstraintException.UNITARY_MESSAGE);
            this.matrix = matrix;
        }

        #endregion

        #region Methods

        public Gate Compose(Gate other)
        {
            return new Gate(other.Matrix * this.Matrix);
        }

        public Gate Combine(Gate other)
        {
            return new Gate(ComplexMatrix.TensorProduct(this.Matrix, other.Matrix));
        }

        public static Gate FromFunction(ComplexMatrix function, int deadBits = 0)
        {
            var logM = System.Math.Round(System.Math.Log(function.Size.M, 2), 10);
            var logMRound = System.Math.Round(logM);
            var isMFactorOf2 = logM == logMRound;

            var logN = System.Math.Round(System.Math.Log(function.Size.N, 2), 10);
            var logNRound = System.Math.Round(logN);
            var isNFactorOf2 = logN == logNRound;

            var deadBitFactor = (int)System.Math.Pow(2, deadBits);

            if (!isMFactorOf2 || !isNFactorOf2)
                throw new GateConstraintException(GateConstraintException.FUNCTION_MESSAGE);

            var matrix = new ComplexMatrix(
                new OrderedPair(function.Size.N * function.Size.M * deadBitFactor, function.Size.N * function.Size.M * deadBitFactor));

            for (var input = 0; input < function.Size.N; input++)
            {
                var inputMatrix = new ComplexMatrix(new OrderedPair(function.Size.N, 1));
                inputMatrix[input][0] = new Complex(1, 0);

                var fnOutput = function * inputMatrix;
                var inputBits = input * function.Size.M * deadBitFactor;

                for (var control = 0; control < function.Size.M; control++)
                {
                    for (var output = 0; output < fnOutput.Size.M; output++)
                    {
                        for (var d = 0; d < deadBitFactor; d++)
                        {
                            matrix[inputBits + (2 * d) + (output ^ control)][inputBits + (2 * d) + control] =
                                fnOutput[output][0];
                        }
                    }
                }
            }

            return new Gate(matrix);
        }

        #endregion
    }
}
