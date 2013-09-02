using Quantum.Math;

namespace Quantum.Emulate
{
    public class ControlledNotGate : Gate
    {
        #region Static Members

        private static ComplexMatrix[] controlledNot = new ComplexMatrix[] {
            new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0) }
            }),
            new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0) }
            })
        };

        #endregion

        #region Constructors

        public ControlledNotGate(int controlBit) : base(controlledNot[controlBit]) { }

        public ControlledNotGate(int registerSize, int controlIndex, int notIndex) :
            base(GetMatrix(registerSize, controlIndex, notIndex)) { }

        #endregion

        #region Methods

        private static ComplexMatrix GetMatrix(int registerSize, int controlIndex, int notIndex)
        {
            var matrixSize = (int)System.Math.Pow(2, registerSize);
            var matrix = new ComplexMatrix(new OrderedPair(matrixSize, matrixSize));

            var controlBit = (int)System.Math.Pow(2, registerSize - controlIndex - 1);
            var notBit = (int)System.Math.Pow(2, registerSize - notIndex - 1);

            // Go through each column of the matrix and determine which row it should map to
            for (var i = 0; i < matrixSize; i++)
            {
                // Is the control bit set to 1
                if ((i & controlBit) > 0)
                {
                    // Is the not bit set to 1
                    if ((i & notBit) > 0)
                    {
                        matrix[i - notBit][i] = new Complex(1, 0);
                    }
                    else
                    {
                        matrix[i + notBit][i] = new Complex(1, 0);
                    }
                }
                else
                {
                    matrix[i][i] = new Complex(1, 0);
                }
            }

            return matrix;
        }

        #endregion
    }
}
