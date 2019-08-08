using System;
using Sorting;

namespace FirstTask
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                int[] inputArray = null;
                Console.WriteLine("Choose sorting method: \n" +
                    "1 - Merge sort \n" +
                    "2 - Quick sort");
                var method = Console.ReadKey();
                switch(method.KeyChar)
                {
                    case '1':
                        {
                            Console.WriteLine();
                            FillArray(ref inputArray);
                            Console.WriteLine("Original array:");
                            PrintArray(inputArray);
                            SortingMethods.MergeSort(inputArray);
                            Console.WriteLine("Sorted array: ");
                            PrintArray(inputArray);

                            break;
                        }
                    case '2':
                        {
                            Console.WriteLine();
                            FillArray(ref inputArray);
                            Console.WriteLine("Original array:");
                            PrintArray(inputArray);
                            SortingMethods.QuickSort(inputArray);
                            Console.WriteLine("Sorted array: ");
                            PrintArray(inputArray);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine();
                            Console.WriteLine("Wrong choice!");
                            continue;
                        }
                }
            }
        }

        /// <summary>
        /// Prints given array into console
        /// </summary>
        /// <param name="input">Array to be printed</param>
        private static void PrintArray(int[] input)
        {
            foreach (var i in input)
                Console.Write(i + " ");
            Console.WriteLine();
        }

        /// <summary>
        /// Fills given array with hope
        /// </summary>
        /// <param name="input">Array to be printed</param>
        private static void FillArray(ref int[] input)
        {
            Console.Write("Enter array length: ");
            int arrLength;
            try
            {
                arrLength = int.Parse(Console.ReadLine());
                if (arrLength < 1) throw new Exception();
            }
            catch
            {
                Console.WriteLine("Wrong input!");
                FillArray(ref input);
                return;
            }
            input = new int[arrLength];
            for (int i = 0; i < arrLength;)
            {
                Console.Write($"Array[{i}] = ");
                try
                {
                    input[i] = int.Parse(Console.ReadLine());
                    i++;
                }
                catch
                {
                    Console.WriteLine("Wrong input!");
                    continue;
                }
            }
        }
    }
}
