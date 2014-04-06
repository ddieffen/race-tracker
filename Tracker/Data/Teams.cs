using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Tracker.Data
{
    public class Teams : Dictionary<int, TeamData>
    {
        #region attributes
        public double timeStamp = -1;
        #endregion
        #region constructors
        public Teams(XmlDocument xmlDoc)
        {
            XmlNode teamNode = xmlDoc.SelectSingleNode("r/teams");
            this.FillNodeTeamsNode(teamNode);
           
        }

        public Teams()
        {
        }

        public Teams(XmlNode xmlNode)
        {
            this.FillNodeTeamsNode(xmlNode);
        }

        public void FillNodeTeamsNode(XmlNode teamsNode)
        {
            if (teamsNode != null)
            {
                foreach (XmlNode node in teamsNode.SelectNodes("team"))
                {
                    TeamData td = new TeamData(node);
                    this.Add(td.id, td);
                }
            }
        }
        #endregion
        #region methods
        internal void UpdateTeamsStatus(XmlDocument TeamsDocument)
        {
            XmlNode teamsNode = TeamsDocument.SelectSingleNode("r/teams");
            foreach (XmlNode node in teamsNode.SelectNodes("team"))
            {
                TeamData td = new TeamData(node);
                if (this.ContainsKey(td.id))
                    this[td.id].status = td.status;
                else
                {
                    IEnumerable<TeamData> answer =
                    from n in Holder.teams.Values
                    where  n.name == td.name
                    select n;
                    if (answer.Count<TeamData>() >= 0)
                    {
                        foreach (TeamData toComp in answer)
                        {
                            if (toComp.sail == td.sail)
                                this.Remove(toComp.id); //remove duplicated or updated id team
                        }
                    }

                    this.Add(td.id, td);
                }
            }   
        }
        internal XmlNode ToXmlNode(XmlDocument xmlDoc)
        {
            XmlNode teamsNode = xmlDoc.CreateNode("teams");
            

            foreach (TeamData td in Holder.teams.Values)
            {
                XmlNode teamNode = xmlDoc.CreateNode("team", xmlDoc.CreateAttr("id", td.id)
                    , xmlDoc.CreateAttr("name", td.name), xmlDoc.CreateAttr("trackC", td.colorHtml)
                    , xmlDoc.CreateAttr("model", td.model), xmlDoc.CreateAttr("status", td.status)
                    , xmlDoc.CreateAttr("sail", td.sail));
                teamsNode.AppendChild(teamNode);

                XmlNode teamSections = xmlDoc.CreateNode("tags");
                teamNode.AppendChild(teamSections);
                foreach (int secId in td.sections)
                {
                    teamSections.AppendChild(xmlDoc.CreateNode("tag", xmlDoc.CreateAttr("id", secId)));
                }

                XmlNode teamPositions = xmlDoc.CreateNode("positions");
                teamNode.AppendChild(teamPositions);
                foreach (TeamPosition tp in td.positions.Values)
                {
                    XmlNode teamPosition = xmlDoc.CreateNode("pos", xmlDoc.CreateAttr("a", tp.latN)
                        , xmlDoc.CreateAttr("o", tp.lonE), xmlDoc.CreateAttr("c", tp.heading)
                        , xmlDoc.CreateAttr("s", tp.speed), xmlDoc.CreateAttr("w", tp.timestamp)
                        , xmlDoc.CreateAttr("d", tp.distToGo));
                    teamPositions.AppendChild(teamPosition);
                }
            }

            return teamsNode;

        }
        #endregion

        
    }

    public class TeamData
    {
        #region attributes
        public int id = -1;
        public string name = "";
        public string model = "";
        public string sail = "";
        public string status = "";
        public List<int> sections;
        public string colorHtml = "";
        public Dictionary<double, TeamPosition> positions = new Dictionary<double, TeamPosition>();
        #endregion
        #region constructors
        public TeamData(XmlNode node)
        {
            this.id = Convert.ToInt32(node.Attributes["id"].Value);
            if(node.Attributes["name"] != null)
                this.name = node.Attributes["name"].Value;
            if (node.Attributes["model"] != null)
                this.model = node.Attributes["model"].Value;
            if(node.Attributes["sail"] != null)
                this.sail = node.Attributes["sail"].Value;
            if(node.Attributes["status"]!=null)
                this.status = node.Attributes["status"].Value;
            if(node.Attributes["trackC"] != null)
                this.colorHtml = node.Attributes["trackC"].Value;

            this.sections = new List<int>();
            foreach (XmlNode tag in node.SelectNodes("tags/tag"))
            {
                this.sections.Add(Convert.ToInt32(tag.Attributes["id"].Value));
            }
            foreach (XmlNode tag in node.SelectNodes("positions/pos"))
            {
                TeamPosition pos = new TeamPosition(tag);
                if(this.positions.ContainsKey(pos.timestamp) == false)
                    this.positions.Add(pos.timestamp, pos);
            }
        }
        #endregion
        #region methods
        public override string ToString()
        {
            return this.name;
        }
        public TeamPosition LatestPosition
        { 
            get
            {
                double maxTimeStamp = -1;
                foreach (double time in this.positions.Keys)
                    maxTimeStamp = Math.Max(time, maxTimeStamp);
                
                if (maxTimeStamp != -1)
                    return this.positions[maxTimeStamp];
                else
                    return new TeamPosition();
            }
        }
        #endregion
    }
}
