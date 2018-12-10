using System;
using System.Collections.Generic;
using System.Linq;

namespace ExstentionMethods
{
    public static class Array
    {
        public static double[][] ToDouble(this List<float[]> list)
        {
            int size = list.First().Length;
            var result = new double[list.Count][];
            foreach (float[] vector in list)
            {
                var listIterator = 0;
                for (var i = 0; i < size; i++)
                {
                    result[listIterator][i] = vector[i];
                }
                listIterator++;
            }
            return result;
        }
        public static double[] ToDouble(this List<float> list)
        {
            var result = new double[list.Count];
            foreach (float item in list)
            {
                var listIterator = 0;

                result[listIterator] = item;
                listIterator++;
            }
            return result;
        }
    }
}
