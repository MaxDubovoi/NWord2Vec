using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWord2Vec
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
        public static double[,] ToDouble(this List<float[]> list)
        {
            int size = list.First().Length;
            var result = new double[list.Count,list.First().Length];
            int listIterator = 0;
            foreach (float[] vector in list)
            {
                for(int i = 0; i<vector.Length; i++)
                {
                    result[listIterator,i] = vector[i];
                }
                    
                listIterator++;
            }
            return result;
        }
        public static double [] ToDouble(this List<float> list)
        {
            var listIterator = 0;
            var result = new double[list.Count];
            foreach (float item in list)
            {
                
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
        public static double[] ToDouble(this float[] vector)
        {
            if (vector == null) throw new ArgumentNullException("vector");

            var result = new double[vector.Length];
            for (var i = 0; i < vector.Length; i++)
            {
                result[i] = (double) vector[i];
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

        public static double Distance(this float[] value1, float[] value2)
        {
            if (value1 == null) throw new ArgumentNullException("value1");
            if (value2 == null) throw new ArgumentNullException("value2");
            if (value1.Length != value2.Length) throw new ArgumentException("vector lengths do not match");

            return Math.Sqrt(value1.Subtract(value2).Select(x => x * x).Sum());
        }


        public static WordVector GetByWord(this RealModel model, string word)
        {
            return model.Vectors.FirstOrDefault(x => x.Word == word);
        }

        public static IEnumerable<WordVector> Nearest(this RealModel model, float[] vector)
        {
            return model.Vectors.OrderBy(x => x.Vector.Distance(vector));
        }

        public static WordVector NearestSingle(this RealModel model, float[] vector)
        {
            return model.Vectors.OrderBy(x => x.Vector.Distance(vector)).First();
        }

        public static double Distance(this RealModel model, string word1, string word2)
        {
            var vector1 = model.GetByWord(word1);
            var vector2 = model.GetByWord(word2);
            if (vector1 == null) throw new ArgumentException(string.Format("cannot find word1 '{0}'", word1));
            if (vector2 == null) throw new ArgumentException(string.Format("cannot find word2 '{0}'", word2));
            return vector1.Vector.Distance(vector2.Vector);
        }

        public static IEnumerable<WordDistance> Nearest(this RealModel model, string word)
        {
            var vector = model.GetByWord(word);
            if (vector == null) throw new ArgumentException(string.Format("cannot find word '{0}'", word));
            return model.Vectors.Select(x => new WordDistance(x.Word, x.Vector.Distance(vector.Vector))).OrderBy(x => x.Distance).Where(x => x.Word != word);
        }

        public static double Distance(this WordVector word1, WordVector word2)
        {
            return word1.Vector.Distance(word2.Vector);
        }
        public static List<WordDistance> DistanceList(this List<WordVector> wordList, WordVector currentWord)
        {
            var distanceList = new List<WordDistance>();
            foreach(WordVector word in wordList)
            {
                distanceList.Add(new WordDistance(word.Word, word.Distance(currentWord), currentWord.Word));
            }
            return distanceList;
        }
        public static double AvarageDistance(this List<WordDistance> distances)
        {
            double sum = 0;
            foreach(WordDistance item in distances)
            {
                sum +=item.Distance;
            }
            return sum / distances.Count;
           
        }

        public static float[] Add(this WordVector word1, WordVector word2)
        {
            return word1.Vector.Add(word2.Vector);
        }

        public static float[] Subtract(this WordVector word1, WordVector word2)
        {
            return word1.Vector.Subtract(word2.Vector);
        }

        public static float[] Add(this float[] word1, WordVector word2)
        {
            return word1.Add(word2.Vector);
        }

        public static float[] Subtract(this float[] word1, WordVector word2)
        {
            return word1.Subtract(word2.Vector);
        }

        public static double Distance(this float[] word1, WordVector word2)
        {
            return word1.Distance(word2.Vector);
        }



    }

}
