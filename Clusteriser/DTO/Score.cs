using System;
using System.Collections.Generic;
using System.Text;

namespace Clusteriser.DTO
{
    public class Score
    {
        private string textClusterName;

        public string TextClusterName
        {
            get { return textClusterName; }
            set { textClusterName = value; }
        }

        private string queryClusterName;

        public string QueryClusterName
        {
            get { return queryClusterName; }
            set { queryClusterName = value; }
        }

        private double points;

        public double Points
        {
            get { return points; }
            set { points = value; }
        }

        public Score(string textName, string queryName, double points)
        {
            TextClusterName = textName;
            QueryClusterName = queryName;
            Points = points;
        }


    }
}
