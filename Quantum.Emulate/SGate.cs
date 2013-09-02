using Quantum.Math;

namespace Quantum.Emulate
{
    public class SGate : Gate
    {
        #region Static Members

        private static ComplexMatrix s = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(1, 0), new Complex(0, 0) },
            new Complex[] { new Complex(0, 0), new Complex(0, 1) }
        });

        #endregion

        #region Constructors

        public SGate() : base(s) { }

        #endregion
    }
}
