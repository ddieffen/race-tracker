using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Tracker.Data
{
    public class RaceSetup
    {
        #region attributes
        public string raceName = "";
        public double frequency = 5;
        public Dictionary<int, Section> sections = new Dictionary<int,Section>();
        public double timeStamp = -1;
        #endregion
        #region constructors
        public RaceSetup(XmlDocument xmlDoc)
        {
            XmlNode raceNode = xmlDoc.SelectSingleNode("r/race");
            this.FillFromRaceNode(raceNode);
        }

        public RaceSetup()
        { }

        public RaceSetup(XmlNode xmlNode)
        {
            this.FillFromRaceNode(xmlNode);
        }

        public void FillFromRaceNode(XmlNode raceNode)
        {
            if (raceNode != null)
            {
                this.raceName = raceNode.Attributes["title"].Value;
                this.frequency = Convert.ToDouble(raceNode.Attributes["frequency"].Value);
                this.sections = new Dictionary<int, Section>();
                foreach (XmlNode section in raceNode.SelectNodes("tags/tag"))
                {
                    Section ns = new Section(section);
                    this.sections.Add(ns.sectionId, ns);
                }
            }
        }
        #endregion
        #region methods
        internal XmlNode ToXmlNode(XmlDocument xmlDoc)
        {
            XmlNode raceNode = xmlDoc.CreateNode("race", xmlDoc.CreateAttr("timestamp", Holder.race.timeStamp), xmlDoc.CreateAttr("title", Holder.race.raceName), xmlDoc.CreateAttr("frequency", Holder.race.frequency));


            //Sections in the race
            XmlNode sectionsNode = raceNode.AppendChild(xmlDoc.CreateNode("tags"));
            raceNode.AppendChild(sectionsNode);
            foreach (Section sec in Holder.race.sections.Values)
            {
                XmlNode sectionNode = xmlDoc.CreateNode("tag", xmlDoc.CreateAttr("name", sec.sectionName), xmlDoc.CreateAttr("id", sec.sectionId));
                sectionsNode.AppendChild(sectionNode);
            }

            return raceNode;
        }
        internal int UpdateFromWebData(XmlDocument RaceDocument)
        {
            XmlNode raceNode = RaceDocument.SelectSingleNode("r/race");
            this.FillFromRaceNode(raceNode);
            return 0;
        }
        #endregion

       
    }

    public class Section
    {
        #region attributes
        public int sectionId = -1;
        public string sectionName = "";
        #endregion
        #region constructors
        public Section(XmlNode node)
        {
            this.sectionId = Convert.ToInt32(node.Attributes["id"].Value);
            this.sectionName = node.Attributes["name"].Value;
        }
        #endregion
        #region methods
        public override string ToString()
        {
            return this.sectionName;
        }
        #endregion
    }
}
