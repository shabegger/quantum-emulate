using Quantum.Math;

namespace Quantum.Emulate
{
    public class TGate : Gate
    {
        #region Static Members

        private static ComplexMatrix t = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(1, 0), new Complex(0, 0) },
            new Complex[] { new Complex(0, 0), new Complex(1 / System.Math.Sqrt(2), 1 / System.Math.Sqrt(2)) }
        });

        #endregion

        #region Constructors

        public TGate() : base(t) { }

        #endregion
    }
}
