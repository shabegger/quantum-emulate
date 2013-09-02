using Quantum.Math;

namespace Quantum.Emulate
{
    public class FredkinGate : Gate
    {
        #region Static Members

        private static ComplexMatrix[] fredkin = new ComplexMatrix[] {
            new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0) }
            }),
            new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0) }
            }),
            new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0) }
            })
        };

        #endregion

        #region Constructors

        public FredkinGate(int controlBit) : base(fredkin[controlBit]) { }

        public FredkinGate(int registerSize, int controlIndex, int flip1Index, int flip2Index) :
            base(GetMatrix(registerSize, controlIndex, flip1Index, flip2Index)) { }

        #endregion

        #region Methods

        private static ComplexMatrix GetMatrix(int registerSize, int controlIndex, int flip1Index, int flip2Index)
        {
            var matrixSize = (int)System.Math.Pow(2, registerSize);
            var matrix = new ComplexMatrix(new OrderedPair(matrixSize, matrixSize));

            var controlBit = (int)System.Math.Pow(2, registerSize - controlIndex - 1);
            var flip1Bit = (int)System.Math.Pow(2, registerSize - flip1Index - 1);
            var flip2Bit = (int)System.Math.Pow(2, registerSize - flip2Index - 1);

            // Go through each column of the matrix and determine which row it should map to
            for (var i = 0; i < matrixSize; i++)
            {
                // Is the control bit set to 1
                if ((i & controlBit) > 0)
                {
                    // Is the first flip bit set to 1
                    if ((i & flip1Bit) > 0)
                    {
                        // Is the second flip bit set to 1
                        if ((i & flip2Bit) > 0)
                        {
                            // The flip bits are the same, so no need to flip
                            matrix[i][i] = new Complex(1, 0);
                        }
                        else
                        {
                            matrix[i - flip1Bit + flip2Bit][i] = new Complex(1, 0);
                        }
                    }
                    else
                    {
                        // Is the second flip bit set to 1
                        if ((i & flip2Bit) > 0)
                        {
                            matrix[i + flip1Bit - flip2Bit][i] = new Complex(1, 0);
                        }
                        else
                        {
                            // The flip bits are the same, so no need to flip
                            matrix[i][i] = new Complex(1, 0);
                        }
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
