using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SecondDayTaskLib
{
    public static class SecondDay
    {
        #region FirstTask
        /// <summary>
        /// Inserts bits from second parameter into the first parameter
        /// starting from position i to j
        /// </summary>
        /// <param name="into">Bits to be inserted to</param>
        /// <param name="from">Bits to insert</param>
        /// <param name="i">Starting position</param>
        /// <param name="j">Ending position</param>
        /// <returns></returns>
        public static int InsertNumber(int into, int from, byte i, byte j)
        {
            if (i > j) throw new ArgumentException("Parameter j is less than parameter i");

            from = from << i;                                                                   //Align bits to  sarting position

            BitArray intoBits = new BitArray(new []{ into });                                   //Convert integers to bit arrays
            BitArray fromBits = new BitArray(new []{ from });

            for(int pos = i; pos <= j; pos++)                                                   //insert bits
                intoBits[pos] = fromBits[pos];

            int[] result = new int[1];                                                          //Convert bit array back to integer
            intoBits.CopyTo(result, 0);
            return result[0];
        }
        /// <summary>
        /// Prints binary form of the given integer
        /// </summary>
        /// <param name="num">Input integer</param>
        public static void PrintBitArray(int num)
        {
            BitArray array = new BitArray(new[] { num });
            foreach (bool bit in array)
            {
                if (bit) Console.Write("1");
                else Console.Write("0");
            }
            Console.WriteLine();
        }
        #endregion

        #region SecondTask
        /// <summary>
        /// Method finds closest largest number, which consists of digits of the given number
        /// </summary>
        /// <param name="input">Input number</param>
        /// <param name="elapsedTime">Time spent on evaluating biggest closest number, in miliseconds</param>
        /// <returns></returns>
        public static int FindNextBiggerNumber(int input, out short elapsedTime)
        {
            short currMiliseconds = (short)DateTime.Now.Millisecond;
            elapsedTime = 0;

            int[] numberArray = new int[input.ToString().Length];

            for (int i = 0; i < numberArray.Length; i++)                    //Convert input number to array of single digits
                numberArray[i] = int.Parse(input.ToString()[i].ToString());

            if (HasNoBiggerNumber(numberArray))                             //Check if given number has closest largest number
                return -1;

            for (int i = numberArray.Length - 1; i >= 0; i--)               //
                if (numberArray[i] > numberArray[i - 1])
                {
                    Swap(ref numberArray[i], ref numberArray[i - 1]);
                    break;
                }

            StringBuilder stringBuilder = new StringBuilder();
            foreach (int i in numberArray)                                  //Convert digits array to single number
                stringBuilder.Append(i);

            if (DateTime.Now.Millisecond < currMiliseconds)
                elapsedTime = (short)(1000 - currMiliseconds + DateTime.Now.Millisecond);
            else
                elapsedTime = (short)(DateTime.Now.Millisecond - currMiliseconds);

            return int.Parse(stringBuilder.ToString());
        }
        /// <summary>
        /// Swaps values of the two given integers
        /// </summary>
        /// <param name="a">First integer</param>
        /// <param name="b">Second integer</param>
        private static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        /// <summary>
        /// Checks if there is a closest largest number to given integer
        /// </summary>
        /// <param name="input">Input array</param>
        /// <returns></returns>
        private static bool HasNoBiggerNumber(int[] input)
        {
            int a = 0;                                                      //Amount of decreased digits

            for (int i = 0; i < input.Length - 1; i++)                      //Checks if next digit is smaller than previous
                if (input[i] >= input[i + 1])
                    a++;

            if (a == input.Length - 1)                                      //If all digits in given number decreases
                return true;                                                //Then there is no closest largest number

            return false;
        }
        #endregion

        #region FourthTask
        /// <summary>
        /// Finds numbers which have at least one digit, equal to the first number in the given list
        /// </summary>
        /// <param name="inArray">Input array</param>
        /// <returns></returns>
        public static int[] FilterDigit(int[] inArray)
        {
            if (inArray == null || inArray.Length == 0) throw new ArgumentException();
            if (inArray[0] > 9 || inArray[0] < -9) throw new ArgumentException();

            List<int> input = new List<int>(inArray);

            for (int i = 1; i < input.Count;)
            {
                if(input[i].ToString().Length == 1)
                {
                    if (input[i] == input[0])
                    {
                        i++;
                        continue;
                    }
                    else
                    {
                        input.RemoveAt(i);
                        continue;
                    }
                }
                for (int h = 0; h < input[i].ToString().Length; h++)
                {
                    if (int.Parse(input[i].ToString()[h].ToString()) == input[0])
                    {
                        i++;
                        break;
                    }
                    if (h == input[i].ToString().Length - 1)
                    {
                        input.RemoveAt(i);
                        break;
                    }
                }
            }
            input.RemoveAt(0);
            return input.ToArray();
        }
        #endregion

        #region FifthTask
        /// <summary>
        /// Finds root of specified degree of the input number
        /// </summary>
        /// <param name="input">Input number</param>
        /// <param name="degree">Degree</param>
        /// <param name="precision">Precision</param>
        public static double FindNthRoot(double input, UInt32 degree, double precision = 0.000001)
        {
            if (precision < 1e-15 || precision > 1)
                throw new ArgumentOutOfRangeException(nameof(precision));

            if (input < 0 && degree % 2 == 0)
                throw new ArgumentException();

            if (Math.Abs(input) < precision)
                throw new ArgumentException();

            double x0, x1 = input;

            do
            {
                x0 = x1;
                x1 = x0 - (Math.Pow(x0, degree) - input) / (Math.Pow(x0, degree - 1) * degree);
            } while (Math.Abs(x1 - x0) > precision);

            return Math.Round(x1, 5);
        }
        #endregion
    }
}
