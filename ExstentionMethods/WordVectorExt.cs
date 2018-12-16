using NWord2Vec;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExstentionMethods
{
    public static class WordVectorExt
    {
        public static float[] Add(this WordVector word1, WordVector word2)
        {
            return word1.Add(word2);
        }

        public static float[] Subtract(this WordVector word1, WordVector word2)
        {
            return word1.Subtract(word2);
        }
    }
}
