﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;

namespace Tracker
{
    public static class Tools
    {
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static double DateTimeToUnixTime(DateTime time)
        {
            return (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static XmlAttribute CreateAttr(this XmlDocument xmlDoc, string name, object value)
        {
            XmlAttribute attribute = xmlDoc.CreateAttribute(name);
            if (value == null)
                attribute.Value = "";
            else
                attribute.Value = value.ToString();
            return attribute;
        }
       
        public static XmlNode CreateNode(this XmlDocument xmlDoc, string name, params XmlNode[] children)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, xmlDoc.NamespaceURI);
            foreach (XmlNode child in children)
            {
                if (child is XmlAttribute)
                    node.Attributes.Append(child as XmlAttribute);
                else
                    node.AppendChild(child);
            }
            return node;
        }

        const int RGBMAX = 255;
        public static Color InvertMeAColour(Color ColourToInvert)
        {
            return Color.FromArgb(RGBMAX - ColourToInvert.R,
              RGBMAX - ColourToInvert.G, RGBMAX - ColourToInvert.B);
        }

        /// <summary>
        /// Calculate the distance between two points on earth given their lat lon
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        public static double HaversineDistanceNauticalMiles(double lat1, double lon1, double lat2, double lon2)
        {
            //double angularDistance = Math.Acos(Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(lon1 - lon2));
            //double angulatDistance2 = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin((lat1-lat2)/2),2)+Math.Cos(lat1)*Math.Cos(lat2)*Math.Pow(Math.Sin((lon1-lon2)/2),2)));
            //double radAngle = angularDistance * 2 * Math.PI / 360;
            //double earthRadiusNauticalMiles = (3443.92 + 3432.37) / 2; //WGS84 equator-polar average in nautcal miles
            //double distance = radAngle * earthRadiusNauticalMiles;


            //double a = Math.Pow(Math.Sin((lat1 * 0.0174532925 - lat2 * 0.0174532925) / 2), 2) + (Math.Cos(lat1) * Math.Cos(lat2 * 0.0174532925) * Math.Pow(Math.Sin(lon1 * 0.0174532925 - lon2 * 0.0174532925), 2));
            //double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            //double d = 6371 * c;

            double x = (lon2 * 0.0174532925 - lon1 * 0.0174532925) * Math.Cos((lat1 * 0.0174532925 + lat2 * 0.0174532925) / 2);
            double y = (lat2 * 0.0174532925 - lat1 * 0.0174532925);
            double dis = Math.Sqrt(x * x + y * y) * (3443.92 + 3432.37) / 2;

            //return Math.Round(distance, 3);

            return dis;


        }

        /// <summary>
        /// Calculate the heading in degrees relatively to the terrestrial north betwwen two lat lon on earth
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        public static double HaversineHeadingDegrees(double lat1, double lon1, double lat2, double lon2)
        {
            double earthRadiusNauticalMiles = (3443.92 + 3432.37) / 2; //WGS84 equator-polar average in nautcal miles

            double angularDistanceX = Math.Acos(Math.Sin(lat1) * Math.Sin(lat1) + Math.Cos(lat1) * Math.Cos(lat1) * Math.Cos(lon1 - lon2));
            double radAngleX = angularDistanceX * 2 * Math.PI / 360;
            double distanceX = radAngleX * earthRadiusNauticalMiles * (lon2 - lon1) / Math.Abs(lon1 - lon2);

            double angularDistanceY = Math.Acos(Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(lon1 - lon1));
            double radAngleY = angularDistanceY * 2 * Math.PI / 360;
            double distanceY = radAngleY * earthRadiusNauticalMiles * (lat2 - lat1) / Math.Abs(lat1 - lat2);

            double angularDistanceR = Math.Acos(Math.Sin(lat1) * Math.Sin(lat2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Cos(lon1 - lon2));
            double radAngleR = angularDistanceR * 2 * Math.PI / 360;
            double distanceR = radAngleR * earthRadiusNauticalMiles;

            double normRatio = 1 / distanceR;

            double angle = 90 - (Math.Atan2(distanceY * normRatio, distanceX * normRatio) * 360 / (2 * Math.PI));

            return Math.Round(angle, 0);
        }

    }
}
