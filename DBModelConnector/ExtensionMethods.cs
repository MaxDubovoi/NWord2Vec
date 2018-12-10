using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBModelConnector
{
    public static class ExtensionMethods
    {
        public static float[] Add(this float[] value1, float[] value2)
        {
            if (value1 == null) throw new ArgumentNullException("value1");
            if (value2 == null) throw new ArgumentNullException("value2");
            if (value1.Length != value2.Length) throw new ArgumentException("vector lengths do not match");

            var result = new float[value1.Length];
            for (var i = 0; i < value1.Length; i++)
            {
                result[i] = value1[i] + value2[i];
            }
            return result;
        }

        public static float[] Subtract(this float[] value1, float[] value2)
        {
            if (value1 == null) throw new ArgumentNullException("value1");
            if (value2 == null) throw new ArgumentNullException("value2");
            if (value1.Length != value2.Length) throw new ArgumentException("vector lengths do not match");

            var result = new float[value1.Length];
            for (var i = 0; i < value1.Length; i++)
            {
                result[i] = value1[i] - value2[i];
            }
            return result;
        }
        public static float[] Multiply(this float[] vector, float scalarValue)
        {
            if (vector == null) throw new ArgumentNullException("vector");

            var result = new float[vector.Length];
            for (var i = 0; i < vector.Length; i++)
            {
                result[i] = vector[i] * scalarValue;
            }
            return result;
        }
        public static float Sum(this float[] vector)
        {
            float result = 0;
            for (var i = 0; i < vector.Length; i++)
            {
                result +=  vector[i];
            }
            return result;
        }
        public static double[][] ToDouble(this List<float[]> list)
        {
            int size = list.First().Length;
            var result = new double[list.Count][];
            foreach(float[] vector in list)
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
        public static double [] ToDouble(this List<float> list)
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
        public static float[] ToFloat(this double[] vector)
        {
            if (vector == null) throw new ArgumentNullException("vector");

            var result = new float[vector.Length];
            for (var i = 0; i < vector.Length; i++)
            {
                result[i] = (float)vector[i];
            }
            return result;
        }
        public static float[] Pow(this float[] vector, float value)
        {
            var result = new float[vector.Length];
            for (var i = 0; i < vector.Length; i++)
            {
                result[i] = (float)Math.Pow(vector[i],value);
            }
            return result;
        }
    }

}
