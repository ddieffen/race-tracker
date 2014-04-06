using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Tracker.Data
{
    public class LatestPositions : Dictionary<int, TeamListPositions>
    {
        #region attributes
        public DateTime timeStamp;
        #endregion
        #region constructors
        public LatestPositions(XmlDocument xmlDoc)
        {
            foreach (XmlNode node in xmlDoc.SelectNodes("r/team"))
            {
                TeamListPositions tp = new TeamListPositions(node);
                this.Add(tp.teamId, tp);
            }
        }

        public LatestPositions(string[] lines)
        {
            for (int i = 2; i < lines.Count()-2; i++)
            {
                try
                {
                    TeamListPositions tp = new TeamListPositions(lines[i]);
                    this.Add(tp.teamId, tp);
                }
                catch 
                {
                    Presenter.messages.Add(DateTime.Now, "Failed to parse the latest positions for :" + lines[i]);
                }
            }
        
        }
        #endregion
    }

    /// <summary>
    /// Class used to store a list of positions for a team
    /// </summary>
    public class TeamListPositions : Dictionary<double,TeamPosition>
    {
        #region attributes
        public int teamId = -1;

        #endregion
        #region constructors
        public TeamListPositions(XmlNode node)
        {
            this.teamId = Convert.ToInt32(node.Attributes["id"].Value);
            foreach (XmlNode pos in node.SelectNodes("pos"))
            {
                TeamPosition tp = new TeamPosition(pos);
                if(this.ContainsKey(tp.timestamp) == false)
                    this.Add(tp.timestamp, tp);
            }
        }

        public TeamListPositions(string p)
        {
            string[] split = p.Split(';');
            this.teamId = Convert.ToInt32(split[0]);
            double time = Convert.ToDouble(split[1]);
            if (this.ContainsKey(time) == false)
            {
                TeamPosition tp = new TeamPosition(time, split[2], split[3]);

                IEnumerable<TeamPosition> previous = this.Values.Where(item => item.TimeStamp < tp.TimeStamp).OrderBy(item => item.TimeStamp);
                if (previous != null && previous.Count() > 0)
                {
                    TeamPosition prev = previous.Last();
                    tp.CalculateFromPrevious(prev);
                }

                tp.UpdateDistanceToGo();
                this.Add(time, tp);
            }
        }
        #endregion
    }

    /// <summary>
    /// Class used to store a single position of a team
    /// </summary>
    public class TeamPosition
    {
        #region attributes
        public double latN = -1;
        public double lonE = -1;
        /// <summary>
        /// In knots
        /// </summary>
        public double speed = -1;
        /// <summary>
        /// In degrees T
        /// </summary>
        public double heading = -1;
        /// <summary>
        /// In nautical miles
        /// </summary>
        public double distToGo = -1;
        /// <summary>
        /// POSIX seconds
        /// </summary>
        public double timestamp = 0;

        #endregion
        #region constructors
        public TeamPosition(XmlNode node)
        {
            this.latN = Convert.ToDouble(node.Attributes["a"].Value);
            this.lonE = Convert.ToDouble(node.Attributes["o"].Value);
            this.heading = Convert.ToDouble(node.Attributes["c"].Value);
            this.speed = Convert.ToDouble(node.Attributes["s"].Value);
            this.distToGo = Convert.ToDouble(node.Attributes["d"].Value);
            this.timestamp = Convert.ToDouble(node.Attributes["w"].Value);
        }

        public TeamPosition(double time, string lat, string lon, string heading = "-1", string speed = "-1", string distToGo = "-1")
        {
            this.latN = Convert.ToDouble(lat);
            this.lonE = Convert.ToDouble(lon);
            this.heading = Convert.ToDouble(heading);
            this.speed = Convert.ToDouble(speed);
            this.distToGo = Convert.ToDouble(distToGo);
            this.timestamp = time;
        }

        /// <summary>
        /// Calculates speed, heading from the previous team positions passed as an argument
        /// </summary>
        /// <param name="previousTp">Previous TP used to average speed and heading</param>
        public void CalculateFromPrevious(TeamPosition previousTp)
        {
            if (previousTp != null)
            {
                double distance = Tools.HaversineDistanceNauticalMiles(this.latN, this.lonE, previousTp.latN, previousTp.lonE);
                double time = (this.timestamp - previousTp.timestamp) / 3600;
                if (time != 0 && this.speed == -1)
                    this.speed = distance / time;
                if(this.heading == -1)
                    this.heading = 180 + Tools.HaversineHeadingDegrees(this.latN, this.lonE, previousTp.latN, previousTp.lonE);
            }
        }

        /// <summary>
        /// If distance to go is -1 update it using the waypoints and current location
        /// </summary>
        public void UpdateDistanceToGo()
        {
            if (this.distToGo == -1 && Holder.course != null && Holder.course.waypoints != null && Holder.course.waypoints.Count > 0)
            {
                List<Waypoint> toPassThough = new List<Waypoint>();
                Waypoint end = Holder.course.waypoints.OrderBy(item => item.Key).Last().Value;
                double toGo = Tools.HaversineDistanceNauticalMiles(this.latN, this.lonE, end.latN, end.lonE);

                foreach (Waypoint wp in Holder.course.waypoints.Values)
                {
                    double waypointToEnd = Tools.HaversineDistanceNauticalMiles(wp.latN, wp.lonE, end.latN, end.lonE);
                    if (waypointToEnd < toGo)
                        toPassThough.Add(wp);
                }

                toPassThough = toPassThough.OrderBy(item => item.order).ToList();
                distToGo = 0;
                for (int i = 1; i < toPassThough.Count; i++)
                {
                    distToGo += Tools.HaversineDistanceNauticalMiles(toPassThough[i - 1].latN, toPassThough[i - 1].lonE, toPassThough[i].latN, toPassThough[i].lonE);
                }
                distToGo += Tools.HaversineDistanceNauticalMiles(this.latN, this.lonE, toPassThough[0].latN, toPassThough[0].lonE);
            }
        }

        public TeamPosition()
        {
            
        }
        #endregion
        #region methods
        public override string ToString()
        {
            return "Lat: " + this.latN.ToString() + " N / Lon: " + this.lonE.ToString() + " E";
        }

        public string ToNiceString()
        {
            return Tools.UnixTimeStampToDateTime(this.timestamp).ToString() + "\r\n" +
                "Lat: " + this.latN.ToString() + " N / Lon: " + this.lonE.ToString() + " E \r\n"
                + "Speed: " + this.speed + " kn / Heading: " + this.heading + " deg \r\n" + 
                "Distance to go : " + this.distToGo + " nm";
        }

        public DateTime TimeStamp
        {
            get 
            {
                return Tools.UnixTimeStampToDateTime(this.timestamp);
            }
        }
        #endregion
    }
}
