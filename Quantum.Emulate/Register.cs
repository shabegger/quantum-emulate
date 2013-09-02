using System;
using Quantum.Math;

namespace Quantum.Emulate
{
    public class Register
    {
        #region Private Members

        private Qubit[] register;
        private ComplexMatrix matrix;

        #endregion

        #region Static Members

        private static Random random;

        #endregion

        #region Properties

        public int Size
        {
            get { return register.Length; }
        }

        #endregion

        #region Constructors

        public Register(int length) : this(GetQubitArray(length)) { }

        public Register(Qubit[] qubits)
        {
            this.register = qubits;
            this.matrix = this.register[0].Matrix;

            for (var i = 1; i < this.register.Length; i++)
            {
                this.matrix = ComplexMatrix.TensorProduct(this.matrix, this.register[i].Matrix);
            }

            random = new Random();
        }

        #endregion

        #region Methods

        private static Qubit[] GetQubitArray(int length)
        {
            var array = new Qubit[length];
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = new Qubit();
            }

            return array;
        }

        public void ApplyGate(Gate gate)
        {
            this.matrix = gate.Matrix * this.matrix;

            for (var i = 0; i < this.register.Length; i++)
            {
                this.register[i] = null;
            }
        }

        public bool Measure(int i)
        {
            var rand = random.NextDouble();

            var byteValue = -1;
            var totalProb = 0.0;

            while (totalProb < rand)
            {
                byteValue++;
                totalProb += this.matrix[byteValue][0].ModulusSquared;
            }

            var exp = (int)System.Math.Pow(2, this.register.Length - i - 1);
            var result = (exp & byteValue) > 0;

            // Set any results that are inconsistent with the measurement to 0
            for (var m = 0; m < this.matrix.Size.M; m++)
            {
                if (((m & exp) == 0) == result) this.matrix[m][0] = new Complex(0, 0);
            }

            this.matrix.Normalize();

            return result;
        }

        public bool[] Measure()
        {
            var result = new bool[this.register.Length];

            var rand = random.NextDouble();

            var byteValue = -1;
            var totalProb = 0.0;

            while (totalProb < rand)
            {
                byteValue++;
                totalProb += this.matrix[byteValue][0].ModulusSquared;
            }

            for (var i = 0; i < result.Length; i++)
            {
                var exp = (int)System.Math.Pow(2, result.Length - i - 1);
                result[i] = (exp & byteValue) > 0;
            }

            // Set the register to the determined value
            for (var m = 0; m < this.matrix.Size.M; m++)
            {
                this.matrix[m][0] = (m == byteValue) ? new Complex(1, 0) : new Complex(0, 0);
            }

            return result;
        }

        public void AddQubits(Qubit[] qubits)
        {
            for (var i = 0; i < qubits.Length; i++)
            {
                this.matrix = ComplexMatrix.TensorProduct(this.matrix, qubits[i].Matrix);
                this.register = new Qubit[this.register.Length + 1];
            }
        }

        public void SetQubitValue(int i, bool value)
        {
            var byteValue = (int)System.Math.Pow(2, this.register.Length - i - 1);

            // Set the register to compatible values
            for (var m = 0; m < this.matrix.Size.M; m++)
            {
                if (((byteValue & m) > 0) != value)
                    this.matrix[m][0] = new Complex(0, 0);
            }

            this.matrix.Normalize();
        }

        #endregion
    }
}
