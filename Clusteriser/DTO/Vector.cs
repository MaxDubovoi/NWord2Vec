using System;
using System.Collections.Generic;
using System.Text;

namespace Clusteriser.DTO
{
    class Vector
    {
        private float[] coordinates;
        private string clusterName;

        public float Coordinates { get; private set; }
        public string ClusterName { get; set; }
        public Vector(float coordinates)
        {
            Coordinates = coordinates;
        }

    }
}
