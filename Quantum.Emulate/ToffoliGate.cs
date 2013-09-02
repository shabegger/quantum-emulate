using Quantum.Math;

namespace Quantum.Emulate
{
    public class ToffoliGate : Gate
    {
        #region Static Members

        private static ComplexMatrix[] toffoli = new ComplexMatrix[] {
            new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) }
            }),
            new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0) }
            }),
            new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0) }
            })
        };

        #endregion

        #region Constructors

        public ToffoliGate(int notBit) : base(toffoli[notBit]) { }

        public ToffoliGate(int registerSize, int control1Index, int control2Index, int notIndex) :
            base(GetMatrix(registerSize, control1Index, control2Index, notIndex)) { }

        #endregion

        #region Methods

        private static ComplexMatrix GetMatrix(int registerSize, int control1Index, int control2Index, int notIndex)
        {
            var matrixSize = (int)System.Math.Pow(2, registerSize);
            var matrix = new ComplexMatrix(new OrderedPair(matrixSize, matrixSize));

            var control1Bit = (int)System.Math.Pow(2, registerSize - control1Index - 1);
            var control2Bit = (int)System.Math.Pow(2, registerSize - control2Index - 1);
            var notBit = (int)System.Math.Pow(2, registerSize - notIndex - 1);

            // Go through each column of the matrix and determine which row it should map to
            for (var i = 0; i < matrixSize; i++)
            {
                // Are the control bits set to 1
                if ((i & control1Bit) > 0 && (i & control2Bit) > 0)
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
