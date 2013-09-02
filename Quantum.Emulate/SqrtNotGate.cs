using Quantum.Math;

namespace Quantum.Emulate
{
    public class SqrtNotGate : Gate
    {
        #region Static Members

        private static ComplexMatrix sqrtNot = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(1 / System.Math.Sqrt(2), 0), new Complex(-1 / System.Math.Sqrt(2), 0) },
            new Complex[] { new Complex(1 / System.Math.Sqrt(2), 0), new Complex(1 / System.Math.Sqrt(2), 0) }
        });

        #endregion

        #region Constructors

        public SqrtNotGate() : base(sqrtNot) { }

        #endregion
    }
}
