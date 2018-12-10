using System;
using System.Collections.Generic;
using System.Text;
using Clusteriser.Contracts;
using Clusteriser.DTO;
using Accord.MachineLearning;
using ExstentionMethods;
using System.Linq;

namespace Clusteriser
{
    public class Cluster : ICluster
    {
        private static List<VectorDTO> vectors;
        private List<VectorDTO> centroids;
        public string Name { get; private set; }
        public int CentroidsNumber { get; set; }
        private static int[] labels;
        private Cluster()
        {

        }

        public static ICluster Compute(List<float[]> _vectors, int numberOfClusters)
        {
            SetVectors(_vectors);
            KMeans kmeans = new KMeans(k: numberOfClusters);

            // Compute and retrieve the data centroids
            var clusters = kmeans.Learn(GetDoubleVectors());

            // Use the centroids to parition all the data
            labels = clusters.Decide(GetDoubleVectors());
            return new Cluster();
        }

        public List<Score> GetScores(ICluster cluster)
        {
            throw new NotImplementedException();
        }

        public List<VectorDTO> GetVectors()
        {
            return vectors;
        }
        private static double[][] GetDoubleVectors()
        {
            int size = vectors.First().Coordinates.Length;
            var result = new double[vectors.Count][];
            foreach (VectorDTO vector in vectors)
            {
                var listIterator = 0;
                for (var i = 0; i < size; i++)
                {
                    result[listIterator][i] = vector.Coordinates[i];
                }
                listIterator++;
            }
            return result;
        }

        public static void SetVectors(List<float[]> _floatVectors)
        {
            foreach (double[] item in _floatVectors.ToDouble())
            {
                vectors.Add(new VectorDTO(item));
            }
        }

        public List<VectorDTO> GetCentroids()
        {
            return centroids;
        }

        private void SetCentroids(KMeansClusterCollection kMeansClusters)
        {
            foreach (double[] item in kMeansClusters.Centroids)
            {
                centroids.Add(new VectorDTO(item));
            }
        }

    }
}
