using System;
using System.Text;

namespace Quantum.Math
{
    public class VectorSizeException : Exception
    {
        internal const string RANK_MESSAGE =
            "The rank of a vector must be positive.";
        internal const string ADDITION_MESSAGE =
            "Vectors must be the same size to add.";
        internal const string SUBTRACTION_MESSAGE =
            "Vectors must be the same size to subtract.";
        internal const string INNER_PRODUCT_MESSAGE =
            "Vectors must be the same size to take the inner product.";

        public VectorSizeException(string message) : base(message) { }
    }

    public class ComplexVector
    {
        #region Private Members

        private Complex[] vector;

        #endregion

        #region Properties

        public Complex this[int index]
        {
            get { return vector[index]; }
            set { vector[index] = value; }
        }

        public int Size
        {
            get { return vector.Length; }
        }

        public ComplexVector Inverse
        {
            get { return (new ComplexVector(this.Size) - this); }
        }

        public ComplexVector Conjugate
        {
            get
            {
                var result = new ComplexVector(this.Size);

                for (var i = 0; i < this.Size; i++)
                {
                    result[i] = this[i].Conjugate;
                }

                return result;
            }
        }

        public double Norm
        {
            get
            {
                // The inner product of a vector with itself is always real,
                // so the real part is the entire value
                return System.Math.Sqrt((InnerProduct(this, this)).Real);
            }
        }

        #endregion

        #region Constructors

        public ComplexVector(int size)
        {
            if (size < 0) throw new VectorSizeException(VectorSizeException.RANK_MESSAGE);

            var vector = new Complex[size];
            for (var i = 0; i < size; i++)
            {
                vector[i] = new Complex();
            }

            this.vector = vector;
        }

        public ComplexVector(Complex[] vector)
        {
            this.vector = vector;
        }

        #endregion

        #region Methods

        public ComplexVector ScalarProduct(Complex c)
        {
            var result = new Complex[this.Size];
            for (var i = 0; i < this.Size; i++)
            {
                result[i] = c * this[i];
            }

            return new ComplexVector(result);
        }

        #endregion

        #region Static Methods

        public static Complex InnerProduct(ComplexVector v1, ComplexVector v2)
        {
            if (v1.Size != v2.Size)
                throw new VectorSizeException(VectorSizeException.INNER_PRODUCT_MESSAGE);

            var result = new Complex();

            for (var i = 0; i < v1.Size; i++)
            {
                result += v1[i] * v2[i].Conjugate;
            }

            return result;
        }

        public static double Distance(ComplexVector v1, ComplexVector v2)
        {
            return (v1 - v2).Norm;
        }

        #endregion

        #region Overridden Methods

        public override bool Equals(object obj)
        {
            var other = obj as ComplexVector;
            if ((object)other == null) return false;

            if (this.Size != other.Size) return false;

            for (var i = 0; i < vector.Length; i++)
            {
                if (this[i] != other[i]) return false;
            }

            return true;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.Append("[ ");

            for (var i = 0; i < vector.Length; i++)
            {
                builder.Append(vector[i]);
                builder.Append(" ");
            }

            builder.Append("]");

            return builder.ToString();
        }

        #endregion

        #region Operators

        public static ComplexVector operator +(ComplexVector v1, ComplexVector v2)
        {
            if (v1.Size != v2.Size)
                throw new VectorSizeException(VectorSizeException.ADDITION_MESSAGE);
            
            var result = new Complex[v1.Size];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = v1[i] + v2[i];
            }

            return new ComplexVector(result);
        }

        public static ComplexVector operator -(ComplexVector v1, ComplexVector v2)
        {
            if (v1.Size != v2.Size)
                throw new VectorSizeException(VectorSizeException.SUBTRACTION_MESSAGE);

            var result = new Complex[v1.Size];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = v1[i] - v2[i];
            }

            return new ComplexVector(result);
        }

        public static bool operator ==(ComplexVector v1, ComplexVector v2)
        {
            return v1.Equals(v2);
        }

        public static bool operator !=(ComplexVector v1, ComplexVector v2)
        {
            return !v1.Equals(v2);
        }

        #endregion
    }
}
