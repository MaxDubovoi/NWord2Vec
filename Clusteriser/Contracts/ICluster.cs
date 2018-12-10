using Clusteriser.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clusteriser.Contracts
{
    interface ICluster
    {
        void Compute();
        void SetVectors(List<Vector> vectors);
        List<Vector> GetVectors();
        string GetName();
        List<Vector> GetCentroids();
        List<Score> GetScores(ICluster cluster);

    }
}
