using System;
using System.Collections.Generic;
using System.Text;

namespace Clusteriser.DTO
{
    class VectorDTO
    {
        private double[] coordinates;
        private string clusterName;

        public double[] Coordinates { get; private set; }
        public string ClusterName { get; set; }
        public VectorDTO(double[] coordinates)
        {
            Coordinates = coordinates;
        }

    }
}
