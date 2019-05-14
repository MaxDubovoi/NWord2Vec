using System;
using System.Collections.Generic;
using System.Text;
using Clusteriser.Contracts;
using Clusteriser.DTO;
using ExstentionMethods;
using System.Linq;

namespace Clusteriser
{
    public class Cluster : ICluster
    {
        private List<VectorDTO> vectors;
        private List<VectorDTO> centroids = new List<VectorDTO>();
        //public string Name { get; private set; }
        public int CentroidsNumber { get; private set; }
        private Cluster(List<float[]> _vectors, List<List<double>> _centroids)
        {
            SetVectors(_vectors);
            SetCentroids(_centroids);
            CentroidsNumber = _centroids.Count;
        }

        public static Cluster Compute(List<float[]> _vectors, int numberOfClusters)
        {
            KMeansPP kMeans = new KMeansPP(numberOfClusters, _vectors.ToListListDouble());
           var _centers = kMeans.runKMean();

            return new Cluster(_vectors,_centers);
        }
        public static double GetAccordance(List<float[]> vectorsList1 , List<float[]> vectorsList2)
        {
            Cluster textCluster = Compute(vectorsList1, vectorsList1.Count);
            Cluster queryCluster = Compute(vectorsList2, vectorsList2.Count);
            var scores = queryCluster.GetScores(textCluster);
            double sumScore = 0;
            foreach (Score item in scores)
            {
                sumScore += item.Points;
            }
            var avgScore = sumScore / scores.Count;
            return avgScore;
            //Console.WriteLine("AvgScore in % = {0}", avgScore * 100);
        }

        public List<Score> GetScores(Cluster textCluster)
        {
            var textCentroids = textCluster.GetCentroids();
            var maxDistances = new List<double>();
            var minDistances = new List<double>();
            var avgDistances = new List<double>();
            var scores = new List<Score>();

            foreach(VectorDTO queryVector in centroids)
            {
                double maxDistance = 0;
                double minDistance = 1000000000;
                double distanceSum = 0;
                double avgDistance = 0;
                foreach (VectorDTO outVector in textCentroids)
                { 
                    var currentDistance = outVector.Coordinates.Distance(queryVector.Coordinates);
                    distanceSum += currentDistance;
                    if (maxDistance< currentDistance)
                    {
                        maxDistance = currentDistance;
                    }
                    if (minDistance > currentDistance)
                    {
                        minDistance = currentDistance;
                    }
                }
                avgDistance = distanceSum / textCentroids.Count;
                maxDistances.Add(maxDistance);
                minDistances.Add(minDistance);
                avgDistances.Add(avgDistance);

                double points = ( avgDistance - minDistance ) / (maxDistance - minDistance);
                scores.Add(new Score(textCentroids, centroids, points));
                Console.WriteLine("Score {0} : {1}", scores.Count, points);
                Console.WriteLine("Max = {0}", maxDistance);
                Console.WriteLine("Avg = {0}", avgDistance);
                Console.WriteLine("Min = {0}", minDistance);
                Console.WriteLine("========================");
            }

            return scores;
        }

        public List<VectorDTO> GetVectors()
        {
            return vectors;
        }

        public void SetVectors(List<float[]> _floatVectors)
        {
            vectors = new List<VectorDTO>();
            foreach (List<double> row in _floatVectors.ToListListDouble())
            {                
                vectors.Add(new VectorDTO(row.ToArray()));
            }
        }

        public List<VectorDTO> GetCentroids()
        {
            return centroids;
        }
        private void SetCentroids(List<List<double>> _clusterCenters)
        {
            foreach(List<double> row in _clusterCenters)
            {
                centroids.Add(new VectorDTO(row.ToArray()));
            }
        }

    }
}
