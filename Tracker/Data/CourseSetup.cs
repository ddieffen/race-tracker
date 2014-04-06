using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Tracker.Data
{
    public class CourseSetup
    {
        #region attributes
        public Dictionary<int, Waypoint> waypoints = new Dictionary<int,Waypoint>();
        public List<Waypoint> pois = new List<Waypoint>();
        public double timeStamp = -1;
        #endregion
        #region constructors
        public CourseSetup(XmlDocument xmlDoc)
        {
            this.FillFromCourseNode(xmlDoc.SelectSingleNode("r/course"));
        }

        public CourseSetup()
        { }

        public CourseSetup(XmlNode xmlNode)
        {
            this.FillFromCourseNode(xmlNode);
        }

        public void FillFromCourseNode(XmlNode node)
        {
            if (node != null)
            {
                this.waypoints.Clear();
                foreach (XmlNode wpnode in node.SelectNodes("waypoints/wp"))
                {
                    Waypoint wp = new Waypoint(wpnode);
                    this.waypoints.Add(wp.order, wp);
                }
                this.pois.Clear();
                foreach (XmlNode poisnode in node.SelectNodes("pois/poi"))
                {
                    Waypoint wp = new Waypoint(poisnode);
                    this.pois.Add(wp);
                }
            }
        }
        #endregion
        #region methods
        internal int UpdateFromWebData(XmlDocument CourseDocument)
        {
            this.FillFromCourseNode(CourseDocument.SelectSingleNode("r/course"));
            return 0;
        }
        public XmlNode ToXmlNode(XmlDocument xmlDoc)
        {
            XmlNode courseNode = xmlDoc.CreateNode("course", xmlDoc.CreateAttr("timestamp", Holder.course.timeStamp));

            //Points of interrest
            XmlNode poisNode = courseNode.AppendChild(xmlDoc.CreateNode("pois"));
            courseNode.AppendChild(poisNode);
            foreach (Waypoint wp in Holder.course.pois)
            {
                XmlNode waypoint = xmlDoc.CreateNode("poi", xmlDoc.CreateAttr("lat", wp.latN), xmlDoc.CreateAttr("lon", wp.lonE), xmlDoc.CreateAttr("name", wp.name), xmlDoc.CreateAttr("order", wp.order), xmlDoc.CreateAttr("drawMark", wp.showLabelName));
                poisNode.AppendChild(waypoint);
            }

            //waypoints race
            XmlNode waypointsNode = courseNode.AppendChild(xmlDoc.CreateNode("waypoints"));
            courseNode.AppendChild(waypointsNode);
            foreach (Waypoint wp in Holder.course.waypoints.Values)
            {
                XmlNode waypoint = xmlDoc.CreateNode("wp", xmlDoc.CreateAttr("lat", wp.latN), xmlDoc.CreateAttr("lon", wp.lonE), xmlDoc.CreateAttr("name", wp.name), xmlDoc.CreateAttr("order", wp.order), xmlDoc.CreateAttr("drawMark", wp.showLabelName));
                waypointsNode.AppendChild(waypoint);
            }

            return courseNode;
        }
        #endregion

        
    }


    public class Waypoint
    {
        #region attributes
        public string name = "";
        public double latN = -1;
        public double lonE = -1;
        public bool showLabelName = true;
        public int order = -1;
        #endregion
        #region constructors
        public Waypoint(XmlNode xmlNode)
        {
            if (xmlNode.Attributes["name"] != null)
                this.name = xmlNode.Attributes["name"].Value;
            if (xmlNode.Attributes["title"] != null)
                this.name = xmlNode.Attributes["title"].Value;

            this.latN = Convert.ToDouble(xmlNode.Attributes["lat"].Value);
            this.lonE = Convert.ToDouble(xmlNode.Attributes["lon"].Value);
            if(xmlNode.Attributes["order"]!=null)
                this.order = Convert.ToInt32(xmlNode.Attributes["order"].Value);

            if (xmlNode.Attributes["type"] != null && xmlNode.Attributes["type"].Value == "label")
                this.showLabelName = true;
            else
                this.showLabelName = false;

        }
        #endregion
    }


}
