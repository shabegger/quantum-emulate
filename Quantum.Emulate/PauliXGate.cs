using Quantum.Math;

namespace Quantum.Emulate
{
    public class PauliXGate : Gate
    {
        #region Static Members

        public static readonly ComplexMatrix PauliX = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(0, 0), new Complex(1, 0) },
            new Complex[] { new Complex(1, 0), new Complex(0, 0) }
        });

        #endregion

        #region Constructors

        public PauliXGate() : base(PauliX) { }

        #endregion
    }
}
