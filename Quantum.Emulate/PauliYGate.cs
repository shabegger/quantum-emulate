using Quantum.Math;

namespace Quantum.Emulate
{
    public class PauliYGate : Gate
    {
        #region Static Members

        private static ComplexMatrix pauliY = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(0, 0), new Complex(0, -1) },
            new Complex[] { new Complex(0, 1), new Complex(0, 0) }
        });

        #endregion

        #region Constructors

        public PauliYGate() : base(pauliY) { }

        #endregion
    }
}
