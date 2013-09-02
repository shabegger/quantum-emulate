using Quantum.Math;

namespace Quantum.Emulate
{
    public class PhaseShiftGate : Gate
    {
        #region Constructors

        public PhaseShiftGate(double theta) : base(GetPhaseShift(theta)) { }

        #endregion

        #region Private Methods

        private static ComplexMatrix GetPhaseShift(double theta)
        {
            return new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(1, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(System.Math.Exp(theta), 0) }
            });
        }

        #endregion
    }
}
