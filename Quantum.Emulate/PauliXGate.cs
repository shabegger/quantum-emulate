using Quantum.Math;

namespace Quantum.Emulate
{
    public class PauliXGate : Gate
    {
        #region Static Members

        private static ComplexMatrix pauliX = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(0, 0), new Complex(1, 0) },
            new Complex[] { new Complex(1, 0), new Complex(0, 0) }
        });

        #endregion

        #region Constructors

        public PauliXGate() : base(pauliX) { }

        #endregion
    }
}
