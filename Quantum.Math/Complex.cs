using System.Text;

namespace Quantum.Math
{
    public class Complex
    {
        #region Properties

        public double Real { get; set; }
        public double Imaginary { get; set; }

        public int Precision { get; set; }

        public Complex Conjugate
        {
            get { return new Complex(this.Real, 0 - this.Imaginary); }
        }

        public double Modulus
        {
            get { return System.Math.Sqrt(this.ModulusSquared); }
        }

        public double ModulusSquared
        {
            get { return (this.Real * this.Real) + (this.Imaginary * this.Imaginary); }
        }

        public double Phase
        {
            get
            {
                var phase = System.Math.Atan(this.Imaginary / this.Real);
                if (this.Real < 0) phase += System.Math.PI;
                return phase;
            }
        }

        public Complex Inverse
        {
            get { return new Complex(0 - this.Real, 0 - this.Imaginary); }
        }

        #endregion

        #region Constructors

        public Complex() : this(0, 0) { }

        public Complex(double real, double imaginary) : this(real, imaginary, Representation.Cartesian) { }

        public Complex(double m, double n, Representation rep, int precision = 10)
        {
            if (rep == Representation.Cartesian)
            {
                this.Real = m;
                this.Imaginary = n;
            }
            else
            {
                this.Real = m * System.Math.Cos(n);
                this.Imaginary = m * System.Math.Sin(n);
            }

            this.Precision = precision;
        }

        #endregion

        #region Methods

        public Complex Pow(int n)
        {
            var modulus = System.Math.Pow(this.Modulus, n);
            var phase = this.Phase * n;
            return new Complex(modulus, phase, Representation.Polar);
        }

        public Complex[] Root(int n)
        {
            var modulus = System.Math.Pow(this.Modulus, 1.0 / n);
            var phase = this.Phase / n;

            var result = new Complex[n];

            for (var i = 0; i < n; i++)
            {
                result[i] = new Complex(modulus, 2 * System.Math.PI * i / n + phase, Representation.Polar);
            }

            return result;
        }

        #endregion

        #region Overridden Methods

        public override bool Equals(object obj)
        {
            var other = obj as Complex;
            if ((object)other == null) return false;

            return (System.Math.Round(this.Real, this.Precision) == System.Math.Round(other.Real, this.Precision) &&
                System.Math.Round(this.Imaginary, this.Precision) == System.Math.Round(other.Imaginary, this.Precision));
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (this.Real != 0)
            {
                builder.Append(this.Real);
                if (this.Imaginary > 0) builder.Append("+");
            }

            if (this.Imaginary != 0)
            {
                builder.Append(this.Imaginary);
                builder.Append("i");
            }

            if (this.Real == 0 && this.Imaginary == 0)
            {
                builder.Append("0");
            }

            return builder.ToString();
        }

        #endregion

        #region Operators

        public static Complex operator +(Complex c1, Complex c2)
        {
            return new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        }

        public static Complex operator *(Complex c1, Complex c2)
        {
            var real = (c1.Real * c2.Real) - (c1.Imaginary * c2.Imaginary);
            var imaginary = (c1.Real * c2.Imaginary) + (c1.Imaginary * c2.Real);
            return new Complex(real, imaginary);
        }

        public static Complex operator -(Complex c1, Complex c2)
        {
            return new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        }

        public static Complex operator /(Complex c1, Complex c2)
        {
            var real = (c1.Real * c2.Real) + (c1.Imaginary * c2.Imaginary);
            var imaginary = (c1.Imaginary * c2.Real) - (c1.Real * c2.Imaginary);
            return new Complex(real / c2.ModulusSquared, imaginary / c2.ModulusSquared);
        }

        public static bool operator ==(Complex c1, Complex c2)
        {
            return c1.Equals(c2);
        }

        public static bool operator !=(Complex c1, Complex c2)
        {
            return !c1.Equals(c2);
        }

        #endregion

        #region Casting Operators

        public static implicit operator Complex(int i)
        {
            return new Complex(i, 0);
        }

        public static implicit operator Complex(double d)
        {
            return new Complex(d, 0);
        }

        #endregion
    }
}