using System;
using System.Collections.Generic;
using System.Text;

namespace Clusteriser.DTO
{
    public class Score
    {
        private double points;
        private List<VectorDTO> _textCentroids;
        private List<VectorDTO> _queryCentroid;

        public double Points
        {
            get { return points; }
            set { points = value; }
        }

        public Score(List<VectorDTO> textCentroid, List<VectorDTO> queryCentroid, double points)
        {
            _textCentroids = textCentroid;
            _queryCentroid = queryCentroid;
            Points = points;
        }



    }
}
