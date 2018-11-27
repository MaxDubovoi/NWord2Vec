using System.Collections.Generic;
using System.Linq;
using LinearSystems;

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
        public float[] GetCentralVector()//TODO: Solve problem with SLAR
        {
            List<float[]> leftSLAR = new List<float[]>();
            List<float> rightSLAR = new List<float>();
            double[] solution;
            int index = 0;
            int maxIterations = 1000;
            double tolerance = 0.00001;
            while (leftSLAR.Count < Size)
            {
                
                var a = vectors.ElementAt(index);
                foreach (WordVector b in vectors)
                {
                    if(a.Equals(b))
                    {
                        continue;
                    }
                    else
                    {
                        leftSLAR.Add(a.Vector.Subtract(b.Vector).Multiply(2));
                        rightSLAR.Add(a.Vector.Pow(2).Subtract(b.Vector.Pow(2)).Sum());
                    }
                    if (leftSLAR.Count == b.Vector.Length)
                        break;
                }
                index++;
            }
            var linearSystem = new ClassicalIterativeMethods();
            solution = new double[rightSLAR.Count];

            var haveSolution = linearSystem.Jacobi(leftSLAR.Count, maxIterations, tolerance ,leftSLAR.ToDouble(), rightSLAR.ToDouble(), solution);
            return solution.ToFloat();
        }
        public List<WordDistance> GetWordDistances(WordVector word)
        {
            return vectors.DistanceList(word);
        }
    }
}
