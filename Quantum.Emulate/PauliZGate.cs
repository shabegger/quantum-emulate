using Quantum.Math;

namespace Quantum.Emulate
{
    public class PauliZGate : Gate
    {
        #region Static Members

        public static readonly ComplexMatrix PauliZ = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(1, 0), new Complex(0, 0) },
            new Complex[] { new Complex(0, 0), new Complex(-1, 0) }
        });

        #endregion

        #region Constructors

        public PauliZGate() : base(PauliZ) { }

        #endregion
    }
}
