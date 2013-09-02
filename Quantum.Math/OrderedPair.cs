namespace Quantum.Math
{
    public class OrderedPair
    {
        public int M { get; set; }
        public int N { get; set; }

        public OrderedPair(int m, int n)
        {
            this.M = m;
            this.N = n;
        }

        public override bool Equals(object obj)
        {
            var pair = obj as OrderedPair;
            if ((object)pair == null) return false;

            return (this.M == pair.M && this.N == pair.N);
        }

        public static bool operator ==(OrderedPair o1, OrderedPair o2)
        {
            return o1.Equals(o2);
        }

        public static bool operator !=(OrderedPair o1, OrderedPair o2)
        {
            return !o1.Equals(o2);
        }
    }
}
