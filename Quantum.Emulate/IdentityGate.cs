using Quantum.Math;

namespace Quantum.Emulate
{
    public class IdentityGate : Gate
    {
        #region Constructors

        public IdentityGate(int registerSize) : base(GetMatrix(registerSize)) { }

        #endregion

        #region Methods

        private static ComplexMatrix GetMatrix(int registerSize)
        {
            var matrixSize = (int)System.Math.Pow(2, registerSize);
            var matrix = new ComplexMatrix(new OrderedPair(matrixSize, matrixSize));

            for (var i = 0; i < matrixSize; i++)
            {
                matrix[i][i] = new Complex(1, 0);
            }

            return matrix;
        }

        #endregion
    }
}
