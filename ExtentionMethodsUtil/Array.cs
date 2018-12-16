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
        public static List<List<double>> ToListListDouble(this List<float[]> list)
        {
            var outTempList = new List<List<double>>();
            int size = list.First().Length;

            foreach (float[] vector in list)
            {
                var innerTempList = new List<double>();
                for (var i = 0; i < size; i++)
                {
                    innerTempList.Add(vector[i]);
                }
                outTempList.Add(innerTempList);
                
            }
            return outTempList;
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
        public static double Distance(this double[] value1, double[] value2)
        {
            if (value1 == null) throw new ArgumentNullException("value1");
            if (value2 == null) throw new ArgumentNullException("value2");
            if (value1.Length != value2.Length) throw new ArgumentException("vector lengths do not match");

            return Math.Sqrt(value1.Subtract(value2).Select(x => x * x).Sum());
        }
        public static double[] Subtract(this double[] value1, double[] value2)
        {
            if (value1 == null) throw new ArgumentNullException("value1");
            if (value2 == null) throw new ArgumentNullException("value2");
            if (value1.Length != value2.Length) throw new ArgumentException("vector lengths do not match");

            var result = new double[value1.Length];
            for (var i = 0; i < value1.Length; i++)
            {
                result[i] = value1[i] - value2[i];
            }
            return result;
        }
    }
}
