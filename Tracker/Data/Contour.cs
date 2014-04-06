using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tracker.Data
{
    public class Contour
    {
        public List<ContourPoint> points = new List<ContourPoint>();
    }

    public struct ContourPoint
    {
        public double latN;
        public double lonE;

        public ContourPoint(double lat, double lon)
        {
            this.latN = lat;
            this.lonE = lon;
        }
    
    }
}
