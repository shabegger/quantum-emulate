using System;
using System.Text;

namespace Quantum.Math
{
    public class MatrixSizeException : Exception
    {
        internal const string RANK_MESSAGE =
            "All rows of a matrix must have the same rank.";
        internal const string ADDITION_MESSAGE =
            "Matrices must be the same size to add.";
        internal const string SUBTRACTION_MESSAGE =
            "Matrices must be the same size to subtract.";
        internal const string MULTIPLICATION_MESSAGE =
            "N of matrix 1 must equal M of matrix 2.";
        internal const string SQUARE_MESSAGE =
            "Must be a square matrix.";

        public MatrixSizeException(string message) : base(message) { }
    }

    public class ComplexMatrix
    {
        #region Private Members

        private ComplexVector[] matrix;

        #endregion

        #region Properties

        public ComplexVector this[int index]
        {
            get { return matrix[index]; }
            set { matrix[index] = value; }
        }

        public OrderedPair Size
        {
            get
            {
                var m = matrix.Length;
                var n = (m > 0) ? matrix[0].Size : 0;
                return new OrderedPair(m, n);
            }
        }

        public ComplexMatrix Inverse
        {
            get { return (new ComplexMatrix(this.Size) - this); }
        }

        public ComplexMatrix Transpose
        {
            get
            {
                var result = new ComplexMatrix(new OrderedPair(this.Size.N, this.Size.M));

                for (var m = 0; m < this.Size.M; m++)
                {
                    for (var n = 0; n < this.Size.N; n++)
                    {
                        result[n][m] = this[m][n];
                    }
                }

                return result;
            }
        }

        public ComplexMatrix Conjugate
        {
            get
            {
                var result = new ComplexMatrix(new OrderedPair(this.Size.M, this.Size.N));

                for (var m = 0; m < this.Size.M; m++)
                {
                    for (var n = 0; n < this.Size.N; n++)
                    {
                        result[m][n] = this[m][n].Conjugate;
                    }
                }

                return result;
            }
        }

        public ComplexMatrix Adjoint
        {
            get
            {
                var result = new ComplexMatrix(new OrderedPair(this.Size.N, this.Size.M));

                for (var m = 0; m < this.Size.M; m++)
                {
                    for (var n = 0; n < this.Size.N; n++)
                    {
                        result[n][m] = this[m][n].Conjugate;
                    }
                }

                return result;
            }
        }

        public Complex Trace
        {
            get
            {
                if (this.Size.M != this.Size.N)
                    throw new MatrixSizeException(MatrixSizeException.SQUARE_MESSAGE);

                var result = new Complex();

                for (var i = 0; i < this.Size.M; i++)
                {
                    result += this[i][i];
                }

                return result;
            }
        }

        public bool IsHermitian
        {
            get
            {
                return this == this.Adjoint;
            }
        }

        public bool IsUnitary
        {
            get
            {
                if (this.Size.M != this.Size.N)
                    throw new MatrixSizeException(MatrixSizeException.SQUARE_MESSAGE);

                var possibleIdentity = this.Adjoint * this;

                var complex0 = new Complex(0, 0);
                var complex1 = new Complex(1, 0);

                for (var m = 0; m < possibleIdentity.Size.M; m++)
                {
                    for (var n = 0; n < possibleIdentity.Size.N; n++)
                    {
                        if (m == n)
                        {
                            if (possibleIdentity[m][n] != complex1) return false;
                        }
                        else
                        {
                            if (possibleIdentity[m][n] != complex0) return false;
                        }
                    }
                }

                return true;
            }
        }

        #endregion

        #region Constructors

        public ComplexMatrix(OrderedPair size)
        {
            var matrix = new ComplexVector[size.M];
            for (var i = 0; i < size.M; i++)
            {
                matrix[i] = new ComplexVector(size.N);
            }
            this.matrix = matrix;
        }

        public ComplexMatrix(ComplexVector[] matrix)
        {
            var size = -1;
            for (var i = 0; i < matrix.Length; i++)
            {
                if (size == -1) size = matrix[i].Size;
                else if (size != matrix[i].Size)
                {
                    throw new MatrixSizeException(MatrixSizeException.RANK_MESSAGE);
                }
            }
            this.matrix = matrix;
        }

        public ComplexMatrix(Complex[][] matrix)
        {
            var vectors = new ComplexVector[matrix.Length];
            var size = -1;

            for (var i = 0; i < matrix.Length; i++)
            {
                vectors[i] = new ComplexVector(matrix[i]);

                if (size == -1) size = vectors[i].Size;
                else if (size != vectors[i].Size)
                {
                    throw new MatrixSizeException(MatrixSizeException.RANK_MESSAGE);
                }
            }

            this.matrix = vectors;
        }

        #endregion

        #region Methods

        public ComplexMatrix ScalarProduct(Complex c)
        {
            var result = new ComplexVector[this.Size.M];
            for (var m = 0; m < this.Size.M; m++)
            {
                result[m] = this[m].ScalarProduct(c);
            }

            return new ComplexMatrix(result);
        }

        public ComplexVector Row(int i)
        {
            return this[i];
        }

        public ComplexVector Column(int i)
        {
            var column = new Complex[this.Size.M];

            for (var m = 0; m < this.Size.M; m++)
            {
                column[m] = this[m][i];
            }

            return new ComplexVector(column);
        }

        public void Normalize()
        {
            var magnitude = 0.0;

            for (var m = 0; m < this.Size.M; m++)
            {
                for (var n = 0; n < this.Size.N; n++)
                {
                    magnitude += this[m][n].ModulusSquared;
                }
            }

            magnitude = System.Math.Sqrt(magnitude);

            for (var m = 0; m < this.Size.M; m++)
            {
                for (var n = 0; n < this.Size.N; n++)
                {
                    this[m][n] = this[m][n] / magnitude;
                }
            }
        }

        #endregion

        #region Static Methods

        public static Complex InnerProduct(ComplexMatrix m1, ComplexMatrix m2)
        {
            return (m1.Adjoint * m2).Trace;
        }

        public static ComplexMatrix TensorProduct(ComplexMatrix m1, ComplexMatrix m2)
        {
            var size = new OrderedPair(m1.Size.M * m2.Size.M, m1.Size.N * m2.Size.N);
            var result = new ComplexMatrix(size);

            for (var m = 0; m < size.M; m++)
            {
                for (var n = 0; n < size.N; n++)
                {
                    result[m][n] = m1[m / m2.Size.M][n / m2.Size.N] * m2[m % m2.Size.M][n % m2.Size.N];
                }
            }

            return result;
        }

        #endregion

        #region Overridden Methods

        public override bool Equals(object obj)
        {
            var other = obj as ComplexMatrix;
            if ((object)other == null) return false;

            if (this.Size != other.Size) return false;

            for (var m = 0; m < this.Size.M; m++)
            {
                for (var n = 0; n < this.Size.N; n++)
                {
                    if (this[m][n] != other[m][n]) return false;
                }
            }

            return true;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            for (var i = 0; i < matrix.Length; i++)
            {
                builder.Append(matrix[i]);
                builder.AppendLine();
            }

            return builder.ToString();
        }

        #endregion

        #region Operators

        public static ComplexMatrix operator +(ComplexMatrix m1, ComplexMatrix m2)
        {
            if (m1.Size != m2.Size)
                throw new MatrixSizeException(MatrixSizeException.ADDITION_MESSAGE);

            var result = new ComplexVector[m1.Size.M];
            for (var m = 0; m < m1.Size.M; m++)
            {
                result[m] = m1[m] + m2[m];
            }

            return new ComplexMatrix(result);
        }

        public static ComplexMatrix operator -(ComplexMatrix m1, ComplexMatrix m2)
        {
            if (m1.Size != m2.Size)
                throw new MatrixSizeException(MatrixSizeException.SUBTRACTION_MESSAGE);

            var result = new ComplexVector[m1.Size.M];
            for (var m = 0; m < m1.Size.M; m++)
            {
                result[m] = m1[m] - m2[m];
            }

            return new ComplexMatrix(result);
        }

        public static ComplexMatrix operator *(ComplexMatrix m1, ComplexMatrix m2)
        {
            if (m1.Size.N != m2.Size.M)
                throw new MatrixSizeException(MatrixSizeException.MULTIPLICATION_MESSAGE);

            var result = new ComplexMatrix(new OrderedPair(m1.Size.M, m2.Size.N));
            for (var m = 0; m < m1.Size.M; m++)
            {
                for (var n = 0; n < m2.Size.N; n++)
                {
                    for (var i = 0; i < m1.Size.N; i++)
                    {
                        result[m][n] += m1[m][i] * m2[i][n];
                    }
                }
            }

            return result;
        }

        public static bool operator ==(ComplexMatrix m1, ComplexMatrix m2)
        {
            return m1.Equals(m2);
        }

        public static bool operator !=(ComplexMatrix m1, ComplexMatrix m2)
        {
            return !m1.Equals(m2);
        }

        #endregion

        #region Casting Operators

        public static explicit operator ComplexMatrix(ComplexVector v)
        {
            return (new ComplexMatrix(new ComplexVector[] { v })).Transpose;
        }

        #endregion
    }
}
