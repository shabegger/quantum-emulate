using Microsoft.VisualStudio.TestTools.UnitTesting;
using Quantum.Math;

namespace Quantum.Test
{
    [TestClass]
    public class Math
    {
        private Complex c1 = new Complex(3, -1);
        private Complex c2 = new Complex(1, 4);
        private Complex c3 = new Complex(-2, 1);
        private Complex c4 = new Complex(1, 2);
        private Complex c5 = new Complex(1, -1);
        private Complex c6 = new Complex(1, 1);
        private Complex c7 = new Complex(System.Math.Sqrt(2), System.Math.PI / 4.0, Representation.Polar);
        private Complex c8 = new Complex(8, 3.0 / 4.0 * System.Math.PI, Representation.Polar);
        private Complex c9 = new Complex(3, 2);

        private ComplexVector v1 = new ComplexVector(
            new Complex[] { new Complex(6, -4), new Complex(7, 3), new Complex(4.2, -8.1), new Complex(0, -3) });
        private ComplexVector v2 = new ComplexVector(
            new Complex[] { new Complex(16, 2.3), new Complex(0, -7), new Complex(6, 0), new Complex(0, -4) });
        private ComplexVector v3 = new ComplexVector(
            new Complex[] { new Complex(6, 3), new Complex(0, 0), new Complex(5, 1), new Complex(4, 0) });
        private ComplexVector v4 = new ComplexVector(
            new Complex[] { new Complex(2, 1), new Complex(0, 0), new Complex(4, -5) });
        private ComplexVector v5 = new ComplexVector(
            new Complex[] { new Complex(1, 1), new Complex(2, 1), new Complex(0, 0) });

        private ComplexMatrix m1 = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(2, 3), new Complex(4, -1), new Complex(3, -2), new Complex(5, -4) },
            new Complex[] { new Complex(3, -1), new Complex(1, 4), new Complex(-2, 1), new Complex(1, -1) },
            new Complex[] { new Complex(1, 1), new Complex(8, 3), new Complex(3, 2), new Complex(-4, 2) }
        });
        private ComplexMatrix m2 = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(5, 3), new Complex(-2, -2), new Complex(-1, 3), new Complex(3, 2) },
            new Complex[] { new Complex(7, 5), new Complex(-4, 2), new Complex(6, 3), new Complex(6, -3) },
            new Complex[] { new Complex(-3, 5), new Complex(9, -2), new Complex(1, 0), new Complex(0, -3) }
        });
        private ComplexMatrix m3 = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(2, -1), new Complex(3, -4), new Complex(1, 1) },
            new Complex[] { new Complex(3, 0), new Complex(0, 5), new Complex(2, -4) },
            new Complex[] { new Complex(5, 0), new Complex(-1, -2), new Complex(3, 4) },
            new Complex[] { new Complex(6, 7), new Complex(2, -3), new Complex(1, -1) }
        });
        private ComplexMatrix m4 = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(3, 4), new Complex(2, -2), new Complex(-4, 1) },
            new Complex[] { new Complex(-2, 3), new Complex(-2, 1), new Complex(-8, 2) },
            new Complex[] { new Complex(-2, 5), new Complex(4, 5), new Complex(-3, 4) }
        });
        private ComplexMatrix m5 = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(0.5, 0), new Complex(0.5, 0.5), new Complex(-0.5, 0) },
            new Complex[] { new Complex(0, -1 / System.Math.Sqrt(3)), new Complex(0, 1 / System.Math.Sqrt(3)), new Complex(1 / System.Math.Sqrt(3), 0) },
            new Complex[] { new Complex(0, 5 / (2 * System.Math.Sqrt(15))), new Complex(3 / (2 * System.Math.Sqrt(15)), 1 / (2 * System.Math.Sqrt(15))), new Complex(4 / (2 * System.Math.Sqrt(15)), 3 / (2 * System.Math.Sqrt(15))) }
        });
        private ComplexMatrix m6 = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(0, 0), new Complex(3, -2) },
            new Complex[] { new Complex(3, -2), new Complex(4, 0) }
        });
        private ComplexMatrix m7 = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(3, 0), new Complex(2, -1), new Complex(0, -3) },
            new Complex[] { new Complex(2, 1), new Complex(0, 0), new Complex(1, -1) },
            new Complex[] { new Complex(0, 3), new Complex(1, 1), new Complex(0, 0) }
        });
        private ComplexMatrix m8 = new ComplexMatrix(new Complex[][] {
            new Complex[] { new Complex(-3, 1), new Complex(-2, 5) },
            new Complex[] { new Complex(-2, -2), new Complex(1, 3) }
        });

        [TestMethod]
        public void ComplexAddition()
        {
            Assert.AreEqual(new Complex(4, 3), c1 + c2);
        }

        [TestMethod]
        public void ComplexMultiplication()
        {
            Assert.AreEqual(new Complex(7, 11), c1 * c2);
        }

        [TestMethod]
        public void ComplexSubtraction()
        {
            Assert.AreEqual(new Complex(-3, -1), c3 - c4);
        }

        [TestMethod]
        public void ComplexDivision()
        {
            Assert.AreEqual(new Complex(0, 1), c3 / c4);
        }

        [TestMethod]
        public void ComplexInverse()
        {
            Assert.AreEqual(new Complex(-1, -2), c4.Inverse);
        }

        [TestMethod]
        public void ComplexModulus()
        {
            Assert.AreEqual(System.Math.Sqrt(2), c5.Modulus);
        }

        [TestMethod]
        public void ComplexConjugate()
        {
            Assert.AreEqual(new Complex(-2, -1), c3.Conjugate);
        }

        [TestMethod]
        public void ComplexPhase()
        {
            Assert.AreEqual(System.Math.PI / 4, c6.Phase);
        }

        [TestMethod]
        public void ComplexPolarToCartesian()
        {
            Assert.AreEqual(new Complex(1, 1), c7);
        }

        [TestMethod]
        public void ComplexPower()
        {
            Assert.AreEqual(new Complex(28, -96), c1.Pow(4));
        }

        [TestMethod]
        public void ComplexRoot()
        {
            var expected = new Complex(2, 11.0 / 12.0 * System.Math.PI, Representation.Polar);

            var resultIsInArray = false;
            var roots = c8.Root(3);

            foreach (var root in roots)
            {
                if (root == expected)
                {
                    resultIsInArray = true;
                    break;
                }
            }

            Assert.IsTrue(resultIsInArray);
        }

        [TestMethod]
        public void VectorAddition()
        {
            var expected = new ComplexVector(
                new Complex[] { new Complex(22, -1.7), new Complex(7, -4), new Complex(10.2, -8.1), new Complex(0, -7) });
            var result = v1 + v2;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void VectorSubtraction()
        {
            var expected = new ComplexVector(
                new Complex[] { new Complex(-10, -6.3), new Complex(7, 10), new Complex(-1.8, -8.1), new Complex(0, 1) });
            var result = v1 - v2;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void VectorScalarMultiplication()
        {
            var expected = new ComplexVector(
                new Complex[] { new Complex(12, 21), new Complex(0, 0), new Complex(13, 13), new Complex(12, 8) });
            var result = v3.ScalarProduct(c9);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void VectorInverse()
        {
            var expected = new ComplexVector(
                new Complex[] { new Complex(-6, 4), new Complex(-7, -3), new Complex(-4.2, 8.1), new Complex(0, 3) });
            var result = v1.Inverse;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void VectorConjugate()
        {
            var expected = new ComplexVector(
                new Complex[] { new Complex(6, -3), new Complex(0, 0), new Complex(5, -1), new Complex(4, 0) });
            var result = v3.Conjugate;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void VectorInnerProduct()
        {
            Assert.AreEqual(new Complex(3, -1), ComplexVector.InnerProduct(v4, v5));
        }

        [TestMethod]
        public void VectorNorm()
        {
            Assert.AreEqual(System.Math.Sqrt(46), v4.Norm);
        }

        [TestMethod]
        public void VectorDistance()
        {
            Assert.AreEqual(System.Math.Sqrt(47), ComplexVector.Distance(v4, v5));
        }

        [TestMethod]
        public void MatrixTrace()
        {
            Assert.AreEqual(new Complex(-2, 9), m4.Trace);
        }

        [TestMethod]
        public void MatrixUnitary()
        {
            Assert.IsFalse(m4.IsUnitary);
            Assert.IsTrue(m5.IsUnitary);
        }

        [TestMethod]
        public void MatrixHermitian()
        {
            Assert.IsFalse(m6.IsHermitian);
            Assert.IsTrue(m7.IsHermitian);
        }

        [TestMethod]
        public void MatrixAddition()
        {
            var expected = new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(7, 6), new Complex(2, -3), new Complex(2, 1), new Complex(8, -2) },
                new Complex[] { new Complex(10, 4), new Complex(-3, 6), new Complex(4, 4), new Complex(7, -4) },
                new Complex[] { new Complex(-2, 6), new Complex(17, 1), new Complex(4, 2), new Complex(-4, -1) }
            });
            var result = m1 + m2;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MatrixSubtraction()
        {
            var expected = new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(-3, 0), new Complex(6, 1), new Complex(4, -5), new Complex(2, -6) },
                new Complex[] { new Complex(-4, -6), new Complex(5, 2), new Complex(-8, -2), new Complex(-5, 2) },
                new Complex[] { new Complex(4, -4), new Complex(-1, 5), new Complex(2, 2), new Complex(-4, 5) }
            });
            var result = m1 - m2;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MatrixMultiplication()
        {
            var expected = new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(6, 43), new Complex(56, -27), new Complex(-20, 16) },
                new Complex[] { new Complex(94, 48), new Complex(34, -72), new Complex(11, 56) },
                new Complex[] { new Complex(52, -11), new Complex(11, 64), new Complex(2, -37) }
            });
            var result = m2 * m3;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MatrixScalarMultiplication()
        {
            var expected = new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(9, 19), new Complex(-2, -10), new Complex(-9, 7), new Complex(5, 12) },
                new Complex[] { new Complex(11, 29), new Complex(-16, -2), new Complex(12, 21), new Complex(24, 3) },
                new Complex[] { new Complex(-19, 9), new Complex(31, 12), new Complex(3, 2), new Complex(6, -9) }
            });
            var result = m2.ScalarProduct(c9);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MatrixRow()
        {
            var expected = new ComplexVector(
                new Complex[] { new Complex(3, -1), new Complex(1, 4), new Complex(-2, 1), new Complex(1, -1) });
            var result = m1.Row(1);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MatrixColumn()
        {
            var expected = new ComplexVector(
                new Complex[] { new Complex(5, -4), new Complex(1, -1), new Complex(-4, 2) });
            var result = m1.Column(3);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MatrixInverse()
        {
            var expected = new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(-2, -3), new Complex(-4, 1), new Complex(-3, 2), new Complex(-5, 4) },
                new Complex[] { new Complex(-3, 1), new Complex(-1, -4), new Complex(2, -1), new Complex(-1, 1) },
                new Complex[] { new Complex(-1, -1), new Complex(-8, -3), new Complex(-3, -2), new Complex(4, -2) }
            });
            var result = m1.Inverse;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MatrixConjugate()
        {
            var expected = new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(5, -3), new Complex(-2, 2), new Complex(-1, -3), new Complex(3, -2) },
                new Complex[] { new Complex(7, -5), new Complex(-4, -2), new Complex(6, -3), new Complex(6, 3) },
                new Complex[] { new Complex(-3, -5), new Complex(9, 2), new Complex(1, 0), new Complex(0, 3) }
            });
            var result = m2.Conjugate;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MatrixTranspose()
        {
            var expected = new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(5, 3), new Complex(7, 5), new Complex(-3, 5) },
                new Complex[] { new Complex(-2, -2), new Complex(-4, 2), new Complex(9, -2) },
                new Complex[] { new Complex(-1, 3), new Complex(6, 3), new Complex(1, 0) },
                new Complex[] { new Complex(3, 2), new Complex(6, -3), new Complex(0, -3) }
            });
            var result = m2.Transpose;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MatrixAdjoint()
        {
            var expected = new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(5, -3), new Complex(7, -5), new Complex(-3, -5) },
                new Complex[] { new Complex(-2, 2), new Complex(-4, -2), new Complex(9, 2) },
                new Complex[] { new Complex(-1, -3), new Complex(6, -3), new Complex(1, 0) },
                new Complex[] { new Complex(3, -2), new Complex(6, 3), new Complex(0, 3) }
            });
            var result = m2.Adjoint;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MatrixInnerProduct()
        {
            Assert.AreEqual(new Complex(25, -7), ComplexMatrix.InnerProduct(m4, m7));
        }

        [TestMethod]
        public void MatrixTensorProduct()
        {
            var expected = new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(-7, 9), new Complex(4, 19) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(-10, -2), new Complex(9, 7) },
                new Complex[] { new Complex(-7, 9), new Complex(4, 19), new Complex(-12, 4), new Complex(-8, 20) },
                new Complex[] { new Complex(-10, -2), new Complex(9, 7), new Complex(-8, -8), new Complex(4, 12) }
            });

            Assert.AreEqual(expected, ComplexMatrix.TensorProduct(m6, m8));
        }
    }
}
