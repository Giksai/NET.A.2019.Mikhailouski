using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace FourthDayLibrary
{
    public static class FourthDayLib
    {
        #region FirstTask
        private static void CheckArray(int[] input)
        {
            if (input == null) throw new ArgumentNullException();
            if (input.Length == 0) throw new ArgumentException("Input array length is zero");
            if (HasZerosInArray(input)) throw new ArgumentException("Having zeros in input array is forbidden");
        }
        /// <summary>
        /// Finds GCD of the given numbers using Euclid method
        /// </summary>
        /// <param name="timeElapsed">Parameter that holds method's elapsed time</param>
        /// <param name="input">Input numbers</param>
        public static int EuclidGCD(out long timeElapsed, params int[] input)
        {
            timeElapsed = 0;
            Stopwatch stpWatch = new Stopwatch();
            stpWatch.Start();

            CheckArray(input);
            if (input.Length == 1) return input[0];

            while (!AllEqual(input))
            {
                input[FindMaxValueIndex(input)] -= input[FindMinValueIndex(input)];
            }

            timeElapsed = stpWatch.ElapsedMilliseconds;
            stpWatch.Reset();

            return input[0];
        }
        private static bool AllEqual(int[] input) //Checks if all elements in the array are equal
        {
            int comparer = input[0];
            for (int i = 1; i < input.Length; i++)
                if (input[i] != comparer) return false;

            return true;
        }
        private static bool HasZerosInArray(int[] input) //Checks if there is zeros in given array
        {
            foreach (var element in input)
                if (element == 0) return true;

            return false;
        }
        private static int FindMaxValueIndex(int[] input) //Returns index of the largest element
        {
            int max = 0;
            foreach(var element in input)
                if (element > max) max = element;

            return Array.IndexOf(input, max);
        }
        private static int FindMinValueIndex(int[] input) //Returns index of the Lowest element
        {
            int min = int.MaxValue;
            foreach (var element in input)
                if (element < min) min = element;

            return Array.IndexOf(input, min);
        }

        /// <summary>
        /// Finds GCD of the given numbers using Stein method
        /// </summary>
        /// <param name="timeElapsed">Parameter that holds method's elapsed time</param>
        /// <param name="input">Input numbers</param>
        public static int SteinGCD(out long timeElapsed, params int[] input)
        {
            timeElapsed = 0;
            Stopwatch stpWatch = new Stopwatch();
            stpWatch.Start();

            CheckArray(input);
            if (input.Length == 1) return input[0];

            int currentGCD = SteinGCDBasic(input[0], input[1]);

            for (int i = 2; i < input.Length; i++)
               currentGCD = SteinGCDBasic(currentGCD, input[i]);

            timeElapsed = stpWatch.ElapsedMilliseconds;
            stpWatch.Reset();

            return currentGCD;
        }
        private static int SteinGCDBasic(int a, int b) //Finds GCD of two numbers using Stein method
        {
            if (a == 0) return b;
            if (b == 0) return a;
            if (a == b) return a;
            if (a == 1 || b == 1)  return 1;

            if ((a & 1) == 0) 
                return ((b & 1) == 0) ? SteinGCDBasic(a >> 1, b >> 1) << 1 : SteinGCDBasic(a >> 1, b);
            else 
                return ((b & 1) == 0) ? SteinGCDBasic(a, b >> 1) : SteinGCDBasic(b, a > b ? a - b : b - a);
        }
        #endregion

        #region SecondTask
        /// <summary>
        /// Converts given number to the binary representation
        /// </summary>
        /// <param name="input">Input number</param>
        public static string ToBinaryString(this double input)
        {
            DoubleToLongStruct convertStruct = new DoubleToLongStruct { Double64bits = input };
            long value = convertStruct.Long64bits;
            int bitsCount = 64;
            char[] result = new char[bitsCount];
            result[0] = value < 0 ? '1' : '0';
            for (int i = bitsCount - 2, j = 1; i >= 0; i--, j++)
            {
                result[j] = (value & (1L << i)) != 0 ? '1' : '0';
            } 

            return new string(result);
        }

        [StructLayout(LayoutKind.Explicit)]
        private struct DoubleToLongStruct
        {
            [FieldOffset(0)]
            private readonly long long64bits;

            [FieldOffset(0)]
            private double double64bits;

            public double Double64bits
            {
                set
                {
                    double64bits = value;
                }
            }

            public long Long64bits
            {
                get
                {
                    return long64bits;
                }
            }
        }
        #endregion
    }
}
