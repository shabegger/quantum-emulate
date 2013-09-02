using Quantum.Math;

namespace Quantum.Emulate
{
    public class PhaseZGate : Gate
    {
        #region Constructors

        public PhaseZGate(double theta) : base(GetPhaseShift(theta)) { }

        #endregion

        #region Private Methods

        private static ComplexMatrix GetPhaseShift(double theta)
        {
            return new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(-System.Math.Cos(theta / 2), -System.Math.Sin(theta / 2)), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(System.Math.Cos(theta / 2), System.Math.Sin(theta / 2)) }
            });
        }

        #endregion
    }
}
