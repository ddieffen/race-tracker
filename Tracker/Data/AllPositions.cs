using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Tracker.Data
{
    public class AllPositions : Dictionary<int, TeamListPositions>
    {
        #region attributes
        public DateTime timeStamp;
        #endregion
        #region constructors
        public AllPositions(XmlDocument xmlDoc)
        {
            foreach (XmlNode node in xmlDoc.SelectNodes("r/team"))
            {
                TeamListPositions tp = new TeamListPositions(node);
                this.Add(tp.teamId, tp);
            }
        }
        #endregion

    }
}
