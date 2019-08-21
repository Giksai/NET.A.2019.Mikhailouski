using System;

namespace SixthDayLib
{
    public static class Sorting
    {
        public static void SortRowsBySum(this int[][] input)
        {
            if (input == null) throw new ArgumentNullException();
            input.CheckArray();

            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < input[i].Length; j++)
                    if (input[i].Sum() < input[j].Sum())
                        Swap(ref input[i], ref input[j]);
        }

        public static void SortRowsByMaxElement(this int[][] input)
        {
            if (input == null) throw new ArgumentNullException();
            input.CheckArray();

            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < input[i].Length; j++)
                    if (input[i].MaxElement() < input[j].MaxElement())
                        Swap(ref input[i], ref input[j]);
        }

        public static void SortRowsByMinElement(this int[][] input)
        {
            if (input == null) throw new ArgumentNullException();
            input.CheckArray();

            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < input[i].Length; j++)
                    if (input[i].MinElement() < input[j].MinElement())
                        Swap(ref input[i], ref input[j]);
        }

        private static void CheckArray(this int[][] input)
        {
            foreach (var element in input)
                if (element == null) throw new ArgumentNullException();
        }

        private static void Swap(ref int[] inp1, ref int[] inp2)
        {
            int[] buff = inp1;
            inp1 = inp2;
            inp2 = buff;
        }
        private static int MaxElement(this int[] input)
        {
            int max = int.MinValue;

            foreach (int item in input)
                if (item > max)
                    max = item;

            return max;
        }
        private static int MinElement(this int[] input)
        {
            int min = int.MaxValue;

            foreach (int item in input)
                if (item < min)
                    min = item;

            return min;
        }
        private static int Sum(this int[] input)
        {
            int sum = 0;

            foreach (int element in input)
                sum += element;

            return sum;
        }
    }
}
