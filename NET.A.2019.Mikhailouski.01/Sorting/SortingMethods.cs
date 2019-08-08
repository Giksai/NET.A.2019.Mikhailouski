using System;

namespace Sorting
{
    public static class SortingMethods
    {
        /// <summary>
        /// Method performs Merge sort, which recursively divides input array into
        /// smaller length arrays and then merges them into one sorted array
        /// </summary>
        /// <param name="array">Input array</param>
        public static void MergeSort(int[] array)
        {
            if (array == null) return;
            if (array.Length == 0) return;
            DivideArrays(array, 0, array.Length - 1);
        }
        /// <summary>
        /// Method performs quick sort, which recursively finds pivot element,
        /// divides array into two arrays, which consists of elements lower and
        /// higher to pivot and then swaps elements to perform sorting
        /// </summary>
        /// <param name="array">Input array</param>
        public static void QuickSort(int[] array)
        {
            if (array == null) return;
            if (array.Length == 0) return;
            DivideArray(array, 0, array.Length - 1);
        }

        #region Quick sort functioning
        /// <summary>
        /// Divides input array into two arrays, which contain elements that
        /// are higher and lower than the pivot element
        /// </summary>
        /// <param name="input">Input array</param>
        /// <param name="minIndex">Lowest index of the input array</param>
        /// <param name="maxIndex">Highest index of the input array</param>
        /// <returns></returns>
        private static int[] DivideArray(int[] input, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return input;
            }
            var pivotIndex = FindPivotIndex(input, minIndex, maxIndex);
            DivideArray(input, minIndex, pivotIndex - 1);
            DivideArray(input, pivotIndex + 1, maxIndex);
            return input;
        }

        /// <summary>
        /// Returns pivot index and swaps elements
        /// </summary>
        /// <param name="array">Input array</param>
        /// <param name="minIndex">Left index</param>
        /// <param name="maxIndex">Right index</param>
        /// <returns></returns>
        private static int FindPivotIndex(int[] array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i] < array[maxIndex])
                {
                    pivot++;
                    SwapElements(ref array[pivot], ref array[i]);
                }
            }
            pivot++;
            SwapElements(ref array[pivot], ref array[maxIndex]);
            return pivot;
        }

        /// <summary>
        /// Swaps two elements
        /// </summary>
        /// <param name="first">Reference to the first element</param>
        /// <param name="second">Reference to the second element</param>
        private static void SwapElements(ref int first, ref int second)
        {
            var tempElement = first;
            first = second;
            second = tempElement;
        }
        #endregion

        #region Merge sort functioning
        /// <summary>
        /// Divides array into two equal parts, then merges them
        /// </summary>
        /// <param name="input">Input array</param>
        /// <param name="minIndex">Lowest index of the input array</param>
        /// <param name="maxIndex">Highest index of the input array</param>
        /// <returns></returns>
        private static int[] DivideArrays(int[] input, int minIndex, int maxIndex)
        {
            if (minIndex < maxIndex)
            {
                var middleIndex = (minIndex + maxIndex) / 2;
                DivideArrays(input, minIndex, middleIndex);
                DivideArrays(input, middleIndex + 1, maxIndex);
                MergeArrays(input, minIndex, middleIndex, maxIndex);
            }
            return input;
        }

        /// <summary>
        /// Merges parts of the array
        /// </summary>
        /// <param name="input">Input array</param>
        /// <param name="minIndex">Left part index</param>
        /// <param name="middleIndex">Middle index</param>
        /// <param name="maxIndex">Right part index</param>
        private static void MergeArrays(int[] input, int minIndex, int middleIndex, int maxIndex)
        {
            var left = minIndex;
            var right = middleIndex + 1;
            var tempArray = new int[maxIndex - minIndex + 1];
            var index = 0;
            while ((left <= middleIndex) && (right <= maxIndex))
            {
                if (input[left] < input[right])
                {
                    tempArray[index] = input[left];
                    left++;
                }
                else
                {
                    tempArray[index] = input[right];
                    right++;
                }
                index++;
            }
            for (var i = left; i <= middleIndex; i++)
            {
                tempArray[index] = input[i];
                index++;
            }
            for (var i = right; i <= maxIndex; i++)
            {
                tempArray[index] = input[i];
                index++;
            }
            for (var i = 0; i < tempArray.Length; i++)
            {
                input[minIndex + i] = tempArray[i];
            }
        }
        #endregion
    }
}
