using Clusteriser.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clusteriser.Contracts
{
    public interface ICluster
    {
        List<Score> GetScores(Cluster cluster);
        //Cluster Compute(List<float[]> _vectors, int numberOfClusters);

    }
}
