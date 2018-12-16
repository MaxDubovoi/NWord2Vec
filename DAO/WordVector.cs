﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWord2Vec
{
    public class WordVector
    {
        public WordVector(string word, float[] vector)
        {
            this.Word = word;
            this.Vector = vector;
        }

        public string Word { get; private set; }
        public float[] Vector { get; private set; }

        public override string ToString()
        {
            return this.Word;
        }
    }
}
