using System.Collections.Generic;

namespace NWord2Vec
{
    public class RealModel
    {
        public int Words { get; private set; }
        public int Size { get; private set; }
        private List<WordVector> vectors;

        public IEnumerable<WordVector> Vectors => this.vectors;

        protected void AddVector(WordVector vector)
        {
            this.vectors.Add(vector);
        }

        public RealModel(int words, int size, List<WordVector> vectors)
        {
            this.Words = words;
            this.Size = size;
            this.vectors = vectors;
        }

        public static RealModel Load(string filename)
        {
            return new ModelReaderFactory().Manufacture(filename);
        }

        public static RealModel Load(IModelReader source)
        {
            return source.Open();
        }
    }
}
