using Quantum.Math;

namespace Quantum.Emulate
{
    public class PhaseXGate : Gate
    {
        #region Constructors

        public PhaseXGate(double theta) : base(GetPhaseShift(theta)) { }

        #endregion

        #region Private Methods

        private static ComplexMatrix GetPhaseShift(double theta)
        {
            return new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(System.Math.Cos(theta / 2), 0), new Complex(0, -System.Math.Sin(theta / 2)) },
                new Complex[] { new Complex(0, -System.Math.Sin(theta / 2)), new Complex(System.Math.Cos(theta / 2), 0) }
            });
        }

        #endregion
    }
}
