using System;
using Quantum.Emulate;
using Quantum.Math;

namespace Quantum.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            //var deutschMatrix = new ComplexMatrix(new Complex[][] {
            //    new Complex[] { new Complex(1, 0), new Complex(1, 0) },
            //    new Complex[] { new Complex(0, 0), new Complex(0, 0) }
            //});

            //var gate = ((new HadamardGate()).Combine(new HadamardGate()))
            //    .Compose(Gate.FromFunction(deutschMatrix))
            //    .Compose((new HadamardGate()).Combine(new IdentityGate(1)));

            //var register = new Register(new Qubit[] { new Qubit(false), new Qubit(true) });

            //register.ApplyGate(gate);
            //var result = register.Measure(0) ? "Balanced" : "Constant";

            //Console.WriteLine(deutschMatrix);
            //Console.WriteLine(result);
            //Console.WriteLine();

            //var deutschJozsaMatrix = new ComplexMatrix(new Complex[][] {
            //    new Complex[] { new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(1, 0) },
            //    new Complex[] { new Complex(1, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0) }
            //});

            //var gate = ((new HadamardGate()).Combine(new HadamardGate()).Combine(new HadamardGate()).Combine(new HadamardGate()))
            //    .Compose(Gate.FromFunction(deutschJozsaMatrix))
            //    .Compose((new HadamardGate()).Combine(new HadamardGate()).Combine(new HadamardGate()).Combine(new IdentityGate(1)));

            //var register = new Register(new Qubit[] { new Qubit(false), new Qubit(false), new Qubit(false), new Qubit(true) });

            //register.ApplyGate(gate);
            //var bitArray = register.Measure();

            //var topBits = 0;
            //topBits += bitArray[0] ? 4 : 0;
            //topBits += bitArray[1] ? 2 : 0;
            //topBits += bitArray[2] ? 1 : 0;

            //var result = "Constant";
            //if (topBits != 0) result = "Balanced";

            //Console.WriteLine(deutschJozsaMatrix);
            //Console.WriteLine(topBits);
            //Console.WriteLine(result);
            //Console.WriteLine();

            var needle = new ComplexMatrix(new Complex[][] {
                new Complex[] { new Complex(1, 0), new Complex(1, 0), new Complex(1, 0), new Complex(1, 0), new Complex(1, 0), new Complex(1, 0), new Complex(0, 0), new Complex(1, 0) },
                new Complex[] { new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(0, 0), new Complex(1, 0), new Complex(0, 0) }
            });

            var register = new Register(new Qubit[] { new Qubit(false), new Qubit(false), new Qubit(false), new Qubit(true), new Qubit(true), new Qubit(true) });

            var superpose = (new HadamardGate()).Combine(new HadamardGate()).Combine(new HadamardGate()).Combine(new IdentityGate(3));
            register.ApplyGate(superpose);

            // Invert phase, invert about mean Sqrt(2^N) times (N = 3)
            var minusIPlus2A = new ComplexMatrix(new OrderedPair(8, 8));
            for (var m = 0; m < 8; m++)
            {
                for (var n = 0; n < 8; n++)
                {
                    if (m == n) minusIPlus2A[m][n] = new Complex(-1 + (2 / 8), 0);
                    else minusIPlus2A[m][n] = new Complex(2 / 8, 0);
                }
            }

            var phaseInvertGate = (new IdentityGate(3)).Combine(new HadamardGate()).Combine(new IdentityGate(2))
                .Compose((Gate.FromFunction(needle)).Combine(new IdentityGate(2)));
            register.ApplyGate(phaseInvertGate);
            var invertAboutMeanGate = (new Gate(minusIPlus2A)).Combine(new IdentityGate(3));
            register.ApplyGate(invertAboutMeanGate);

            phaseInvertGate = (new IdentityGate(4)).Combine(new HadamardGate()).Combine(new IdentityGate(1))
                .Compose((Gate.FromFunction(needle, 1)).Combine(new IdentityGate(1)));
            register.ApplyGate(phaseInvertGate);
            invertAboutMeanGate = (new Gate(minusIPlus2A)).Combine(new IdentityGate(3));
            register.ApplyGate(invertAboutMeanGate);

            phaseInvertGate = (new IdentityGate(5)).Combine(new HadamardGate())
                .Compose((Gate.FromFunction(needle, 2)));
            register.ApplyGate(phaseInvertGate);
            invertAboutMeanGate = (new Gate(minusIPlus2A)).Combine(new IdentityGate(3));
            register.ApplyGate(invertAboutMeanGate);

            var result = register.Measure();

            //var modFn = new ComplexMatrix(new OrderedPair(4, 16));
            //var a = 3;

            //for (var x = 0; x < modFn.Size.N; x++)
            //{
            //    modFn[(int)System.Math.Pow(a, x) % 4][x] = new Complex(1, 0);
            //}

            //var register = new Register(new Qubit[] { new Qubit(false), new Qubit(false), new Qubit(false), new Qubit(false), new Qubit(false), new Qubit(false) });

            //var gate = ((new HadamardGate()).Combine(new HadamardGate()).Combine(new HadamardGate()).Combine(new HadamardGate()).Combine(new IdentityGate(2)))
            //    .Compose(Gate.FromFunction(modFn));

            //var modResult = 0;
            //register.ApplyGate(gate);
            //modResult += register.Measure(4) ? 2 : 0;
            //modResult += register.Measure(5) ? 1 : 0;

            //var unity = new Complex(1, 0);
            //var mthRoot = unity.Root(16)[15];

            //var fourier = new ComplexMatrix(new OrderedPair(16, 16));
            //for (var i = 0; i < 16; i++)
            //{
            //    for (var j = 0; j < 16; j++)
            //    {
            //        if (i == 0 || j == 0) fourier[i][j] = new Complex(1, 0) / 4;
            //        else fourier[i][j] = mthRoot.Pow(i * j) / 4;
            //    }
            //}

            //var fourierGate = (new Gate(fourier)).Combine(new IdentityGate(2));

            //register.ApplyGate(fourierGate);
            //var measurement = register.Measure();

            //var result = 0;
            //result += measurement[3] ? 1 : 0;
            //result += measurement[2] ? 2 : 0;
            //result += measurement[1] ? 4 : 0;
            //result += measurement[0] ? 8 : 0;

            //var modFn = new ComplexMatrix(new OrderedPair(8, 64));
            //var a = 3;

            //for (var x = 0; x < modFn.Size.N; x++)
            //{
            //    var mod = (int)System.Math.Pow(a, x) % 6;
            //    if (mod < 0) mod += 6;
            //    modFn[mod][x] = new Complex(1, 0);
            //}

            //var register = new Register(new Qubit[] { new Qubit(false), new Qubit(false), new Qubit(false), new Qubit(false), new Qubit(false), new Qubit(false), new Qubit(false), new Qubit(false), new Qubit(false) });

            //var gate = ((new HadamardGate()).Combine(new HadamardGate()).Combine(new HadamardGate()).Combine(new HadamardGate()).Combine(new HadamardGate()).Combine(new HadamardGate()).Combine(new IdentityGate(3)))
            //    .Compose(Gate.FromFunction(modFn));

            //var modResult = 0;
            //register.ApplyGate(gate);
            //modResult += register.Measure(6) ? 4 : 0;
            //modResult += register.Measure(7) ? 2 : 0;
            //modResult += register.Measure(8) ? 1 : 0;

            //var unity = new Complex(1, 0);
            //var mthRoot = unity.Root(64)[63];

            //var fourier = new ComplexMatrix(new OrderedPair(64, 64));
            //for (var i = 0; i < 64; i++)
            //{
            //    for (var j = 0; j < 64; j++)
            //    {
            //        if (i == 0 || j == 0) fourier[i][j] = new Complex(1, 0) / 8;
            //        else fourier[i][j] = mthRoot.Pow(i * j) / 8;
            //    }
            //}

            //var fourierGate = (new Gate(fourier)).Combine(new IdentityGate(3));

            //register.ApplyGate(fourierGate);
            //var measurement = register.Measure();

            //var result = 0;
            //result += measurement[5] ? 1 : 0;
            //result += measurement[4] ? 2 : 0;
            //result += measurement[3] ? 4 : 0;
            //result += measurement[2] ? 8 : 0;
            //result += measurement[1] ? 16 : 0;
            //result += measurement[0] ? 32 : 0;

            //var divisor = 16; // x / 2^m = lambda / period (r)

            //var period = divisor / GCD(result, divisor);

            //var potentialFactor1 = GCD((int)System.Math.Pow(3, period) - 1, 4);
            //var potentialFactor2 = GCD((int)System.Math.Pow(3, period) + 1, 4);
            
            Console.Write("Press enter to exit.");
            Console.ReadLine();
        }

        static int GCD(int value1, int value2)
        {
            while (value1 != 0 && value2 != 0)
            {
                if (value1 > value2) value1 -= value2;
                else value2 -= value1;
            }

            if (value1 == 0) return value2;
            else return value1;
        }

        static void InvertPhase(Register register, ComplexMatrix fn)
        {
            register.SetQubitValue(3, true);

            var gate = (new IdentityGate(3)).Combine(new HadamardGate())
                .Compose(Gate.FromFunction(fn));

            register.ApplyGate(gate);
        }

        static void InvertAboutMean(Register register)
        {
            var minusIPlus2A = new ComplexMatrix(new OrderedPair(8, 8));
            for (var m = 0; m < 8; m++)
            {
                for (var n = 0; n < 8; n++)
                {
                    if (m == n) minusIPlus2A[m][n] = new Complex(-1 + (2 / 8), 0);
                    else minusIPlus2A[m][n] = new Complex(2 / 8, 0);
                }
            }

            var gate = (new Gate(minusIPlus2A)).Combine(new IdentityGate(1));
            register.ApplyGate(gate);
        }
    }
}
