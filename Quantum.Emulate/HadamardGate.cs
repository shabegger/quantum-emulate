using Quantum.Math;

namespace Quantum.Emulate
{
    public class HadamardGate : Gate
    {
        #region Static Members

        public static readonly ComplexMatrix Hadamard = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(1 / System.Math.Sqrt(2), 0), new Complex(1 / System.Math.Sqrt(2), 0) },
            new Complex[] { new Complex(1 / System.Math.Sqrt(2), 0), new Complex(-1 / System.Math.Sqrt(2), 0) }
        });

        #endregion

        #region Constructors

        public HadamardGate() : base(Hadamard) { }

        #endregion
    }
}
