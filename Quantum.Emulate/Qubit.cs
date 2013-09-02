using System;
using Quantum.Math;

namespace Quantum.Emulate
{
    public class QubitException : Exception
    {
        internal const string MOD_MESSAGE =
            "The sum of the squared moduli must be equal to 1.";

        public QubitException(string message) : base(message) { }
    }

    public class Qubit
    {
        #region Private Members

        private ComplexMatrix qubit;

        #endregion

        #region Static Members

        private static Random random;

        #endregion

        #region Properties

        internal ComplexMatrix Matrix
        {
            get { return qubit; }
        }

        #endregion

        #region Constructors

        public Qubit() : this(new Complex(1, 0), new Complex(0, 0)) { }

        public Qubit(bool one) : this(new Complex(one ? 0 : 1, 0), new Complex(one ? 1 : 0, 0)) { }

        public Qubit(Complex zero, Complex one)
        {
            if (zero.Modulus * zero.Modulus + one.Modulus * one.Modulus != 1)
                throw new QubitException(QubitException.MOD_MESSAGE);

            this.qubit = new ComplexMatrix(new Complex[][] { new Complex[] { zero }, new Complex[] { one } });
            if (random == null) random = new Random();
        }

        #endregion

        #region Methods

        public bool Measure()
        {
            var zeroBit = qubit[0][0];
            var prZero = zeroBit.Modulus * zeroBit.Modulus;

            if (random.NextDouble() < prZero)
            {
                this.qubit = new ComplexMatrix(new Complex[][] {
                    new Complex[] { new Complex(1, 0) }, new Complex[] { new Complex(0, 0) } });
                return false;
            }
            else
            {
                this.qubit = new ComplexMatrix(new Complex[][] {
                    new Complex[] { new Complex(0, 0) }, new Complex[] { new Complex(1, 0) } });
                return true;
            }
        }

        #endregion
    }
}
