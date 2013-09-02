using Quantum.Math;

namespace Quantum.Emulate
{
    public class HadamardGate : Gate
    {
        #region Static Members

        private static ComplexMatrix hadamard = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(1 / System.Math.Sqrt(2), 0), new Complex(1 / System.Math.Sqrt(2), 0) },
            new Complex[] { new Complex(1 / System.Math.Sqrt(2), 0), new Complex(-1 / System.Math.Sqrt(2), 0) }
        });

        #endregion

        #region Constructors

        public HadamardGate() : base(hadamard) { }

        #endregion
    }
}
