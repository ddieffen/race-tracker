using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Tracker.Data;
using System.Net;
using System.ComponentModel;

namespace Tracker
{

    public class Messages : List<Message>
    {
        int maxMessages = 100;

        public void Add(DateTime time, string message)
        {
            if (this.Count == maxMessages)
                this.RemoveAt(0);
            this.Add(new Message(time, message));
        }
    }

    public struct Message
    {
        DateTime time;
        string message;

        public Message(DateTime time, string message)
        {
            this.time = time;
            this.message = message;
        }

        public override string ToString()
        {
            return this.time.ToString() + " : " + this.message;
        }
    }

   

    public static class Presenter
    {
        #region delegates
        public delegate void Updating(string message);
        public static event Updating UpdatingEvent;
        #endregion

        #region attributes
        public static Messages messages = new Messages();
        public static long bytesTransfered = 0;
        #endregion

        #region load save
        public static int LoadData(string DataWorkingFolder, string SoftFolder)
        {
            if (new FileInfo(DataWorkingFolder + @"\contour.txt").Exists)
            {
                LoadContour(DataWorkingFolder + @"\contour.txt");
            }

            if (new FileInfo(DataWorkingFolder + @"\data.xml").Exists)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(DataWorkingFolder + @"\data.xml");
                LoadFromSavedData(xmlDoc);
                return 0;
            }

            return -1;
        }

        public static void LoadContour(string filename)
        {
            int counter = 0;
            string line;

            Holder.contours = new List<Contour>();

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader(filename);

            Contour currContour = new Contour();
            while ((line = file.ReadLine()) != null)
            {
                if (String.IsNullOrEmpty(line) == false && line[0] != '#')
                {
                    if (line[0] != '>')
                    {
                        string[] split = line.Split('\t');
                        ContourPoint cp = new ContourPoint(Convert.ToDouble(split[1]), Convert.ToDouble(split[0]));
                        currContour.points.Add(cp);
                    }
                    else if(line[0] == '>')
                    {
                        if (currContour.points.Count > 0)
                        {
                            Holder.contours.Add(currContour);
                            currContour = new Contour();
                        }
                    }
                }
                
            }

            file.Close();
        }

        public static void SaveData(string filename)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateNode("root");
            xmlDoc.AppendChild(rootNode);

            #region serverBase
            rootNode.AppendChild(xmlDoc.CreateNode("serverBaseName", xmlDoc.CreateAttr("url", Holder.serverBaseName)));
            rootNode.AppendChild(xmlDoc.CreateNode("sectionChecked", xmlDoc.CreateAttr("ids", Holder.sectionsSelected)));
            rootNode.AppendChild(xmlDoc.CreateNode("teamChecked", xmlDoc.CreateAttr("ids", Holder.teamsSelected)));
            
            #endregion
            if(Holder.course != null)
                rootNode.AppendChild(Holder.course.ToXmlNode(xmlDoc));
            if(Holder.race != null)
                rootNode.AppendChild(Holder.race.ToXmlNode(xmlDoc));
            if(Holder.teams != null)
                rootNode.AppendChild(Holder.teams.ToXmlNode(xmlDoc));

            string WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            WorkingDirectory += "\\tracker-yellowbrick\\" + Holder.serverBaseName.Replace("/", "").Replace(".", "").Replace(":", "");
            DirectoryInfo info = new DirectoryInfo(WorkingDirectory);
            if (info.Exists == false)
                Directory.CreateDirectory(WorkingDirectory);

            xmlDoc.Save(filename);
        }

        public static void LoadFromSavedData(XmlDocument xmlDoc)
        {
            if (xmlDoc != null)
            {
                Holder.dataIsBeeingUpdated = true;

                #region serverBase
                Holder.sectionsSelected = xmlDoc.SelectSingleNode("root/sectionChecked").Attributes["ids"].Value;
                Holder.teamsSelected = xmlDoc.SelectSingleNode("root/teamChecked").Attributes["ids"].Value;
                Holder.serverBaseName = xmlDoc.SelectSingleNode("root/serverBaseName").Attributes["url"].Value;
                #endregion

                if (xmlDoc.SelectSingleNode("root/course") != null)
                    Holder.course = new CourseSetup(xmlDoc.SelectSingleNode("root/course"));
                if (xmlDoc.SelectSingleNode("root/race") != null)
                    Holder.race = new RaceSetup(xmlDoc.SelectSingleNode("root/race"));
                if (xmlDoc.SelectSingleNode("root/teams") != null)
                    Holder.teams = new Teams(xmlDoc.SelectSingleNode("root/teams"));

                Holder.dataIsBeeingUpdated = false;
            }
            else
            {
                Presenter.messages.Add(DateTime.Now, "Trying to load data but XmlDocument sent is null");
            }
        }

        #endregion

        #region update from web

        public static int updateRace(string workingFolder)
        {
            if (UpdatingEvent != null)
                UpdatingEvent("Updating Race...");

            if (Tracker.Properties.Settings.Default.RootLocation[Tracker.Properties.Settings.Default.RootLocation.Length - 1] != '/')
                Tracker.Properties.Settings.Default.RootLocation += "/";

            string webrequest = Tracker.Properties.Settings.Default.RootLocation + "RaceSetup/";
            string fileLocation = workingFolder + "\\RaceSetup.xml";

            XmlDocument RaceDocument = GetFileFromWeb(webrequest, fileLocation);
            if (RaceDocument != null)
            {
                Holder.dataIsBeeingUpdated = true;
                if (Holder.race == null)
                {
                    Holder.race = new RaceSetup(RaceDocument);
                    Holder.race.timeStamp = Tools.DateTimeToUnixTime(DateTime.Now);
                    Holder.dataIsBeeingUpdated = false;
                    return 0;
                }
                else
                {
                    Holder.dataIsBeeingUpdated = false;
                    return Holder.race.UpdateFromWebData(RaceDocument);
                }
            }
            else
            {
                messages.Add(DateTime.Now, "Failed to get the file " + webrequest + " from the web");
                return -1;
            }
        }

        public static int updateCourse(string workingFolder)
        {
            if (UpdatingEvent != null)
                UpdatingEvent("Updating Course...");

            if (Tracker.Properties.Settings.Default.RootLocation[Tracker.Properties.Settings.Default.RootLocation.Length - 1] != '/')
                Tracker.Properties.Settings.Default.RootLocation += "/";

            string webrequest = Tracker.Properties.Settings.Default.RootLocation + "CourseSetup/";
            string fileLocation = workingFolder + "\\CourseSetup.xml";

            XmlDocument CourseDocument = GetFileFromWeb(webrequest, fileLocation);
            if (CourseDocument != null)
            {
                Holder.dataIsBeeingUpdated = true;
                if (Holder.course == null)
                {
                    Holder.course = new CourseSetup(CourseDocument);
                    Holder.course.timeStamp = Tools.DateTimeToUnixTime(DateTime.Now);
                    Holder.dataIsBeeingUpdated = false;
                    return 0;
                }
                else
                {
                    Holder.dataIsBeeingUpdated = false;
                    return Holder.course.UpdateFromWebData(CourseDocument);
                }
            }
            else
            {
                messages.Add(DateTime.Now, "Failed to get the file " + webrequest + " from the web");
                return -1;
            }
        }

        public static int updateTeamsData(string workingFolder)
        {
            if (UpdatingEvent != null)
                UpdatingEvent("Updating Team Data...");

            if (Tracker.Properties.Settings.Default.RootLocation[Tracker.Properties.Settings.Default.RootLocation.Length - 1] != '/')
                Tracker.Properties.Settings.Default.RootLocation += "/";

            string webrequest = Tracker.Properties.Settings.Default.RootLocation + "TeamSetup/";
            string fileLocation = workingFolder + "\\TeamSetup.xml";

            XmlDocument RaceDocument = GetFileFromWeb(webrequest, fileLocation);
            if (RaceDocument != null)
            {
                Holder.dataIsBeeingUpdated = true;
                if (Holder.teams == null)
                {
                    Holder.teams = new Teams(RaceDocument);
                    Holder.teams.timeStamp = Tools.DateTimeToUnixTime(DateTime.Now);
                }
                else
                {
                    Holder.teams.UpdateTeamsStatus(RaceDocument);
                    Holder.teams.timeStamp = Tools.DateTimeToUnixTime(DateTime.Now);
                }
                Holder.dataIsBeeingUpdated = false;
                return 0;
            }
            else
            {
                messages.Add(DateTime.Now, "Failed to get the file " + webrequest + " from the web");
                return -1;
            }
        }

        public static int updateLatestPositions(string workingFolder)
        {
            if (UpdatingEvent != null)
                UpdatingEvent("Updating Latest Positions...");

            if (Tracker.Properties.Settings.Default.RootLocation[Tracker.Properties.Settings.Default.RootLocation.Length - 1] != '/')
                Tracker.Properties.Settings.Default.RootLocation += "/";

            if (Holder.teams != null)
            {
                string webrequest = Tracker.Properties.Settings.Default.RootLocation + "LatestPositions/";
                string fileLocation = workingFolder + "\\LatestPositions.xml";

                XmlDocument RaceDocument = GetFileFromWeb(webrequest, fileLocation);
                if (RaceDocument != null)
                {
                    LatestPositions rs = new LatestPositions(RaceDocument);
                    Holder.dataIsBeeingUpdated = true;
                    foreach (KeyValuePair<int, TeamListPositions> tpkey in rs)
                    {
                        if (Holder.teams.ContainsKey(tpkey.Key))
                        {
                            foreach (double timestamp in tpkey.Value.Keys)
                            {
                                if (Holder.teams[tpkey.Key].positions.ContainsKey(timestamp) == false)
                                    Holder.teams[tpkey.Key].positions.Add(timestamp, tpkey.Value[timestamp]);
                                else
                                {

                                }
                            }
                        }
                    }

                    Holder.latestPositionsUpdate = Tools.DateTimeToUnixTime(DateTime.Now);
                    Holder.dataIsBeeingUpdated = false;
                    return 0;
                }
                else
                {
                    messages.Add(DateTime.Now, "Failed to get the file " + webrequest + " from the web");
                    return -1;
                }
            }
            else
            {
                messages.Add(DateTime.Now, "Failed to update LatestPositions, teams are not loaded properly");
                return -1;
            }
        }

       /// <summary>
       /// Gets the latest positions in a smalled format than XML
       /// </summary>
       /// <param name="bclass">If set to -1 will iterate over all classes, otherwise download the specific class only</param>
       /// <returns>0 is success</returns>
        public static int updateLatestPositionDon(int bclass = -1)
        {
            try
            {
                if (UpdatingEvent != null)
                    UpdatingEvent("Updating Latest Positions...");

                if (Tracker.Properties.Settings.Default.RootLocation[Tracker.Properties.Settings.Default.RootLocation.Length - 1] != '/')
                    Tracker.Properties.Settings.Default.RootLocation += "/";

                if (Holder.teams != null)
                {
                    int[] teams = new int[1] { bclass };
                    if (bclass == -1)
                    {
                        if (Holder.race != null && Holder.race.sections != null && Holder.race.sections.Keys.Count > 0)
                        {
                            List<int> checkedSections = new List<int>();
                            foreach (string str in Holder.sectionsSelected.Split(','))
                            {
                                if (str != "")
                                    checkedSections.Add(Convert.ToInt32(str));
                            }
                            teams = checkedSections.ToArray();
                        }
                    }

                    string baseUrl = System.Web.HttpUtility.UrlEncode(Tracker.Properties.Settings.Default.RootLocation);
                    baseUrl = baseUrl.Replace("%2f", "%2F");
                    baseUrl = baseUrl.Replace("%3a", "%3A");
                    baseUrl = baseUrl.Substring(baseUrl.Length - 3, 3) == "%2F" ? baseUrl.Substring(0, baseUrl.Length -3) : baseUrl;

                    foreach (int teamID in teams)
                    {
                        string url = "http://www.drapers.us/RaceTracking/index.cgi?BaseAdr=" + baseUrl + "&Class=" + teamID.ToString() + "&xxx=Go&Quick=Y";
                        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                        webRequest.Method = WebRequestMethods.Http.Get;
                        webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                        webRequest.KeepAlive = true;
                        webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                        webRequest.Headers.Add("Accept-Encoding", "gzip,deflate");

                        HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                        bytesTransfered += webResponse.ContentLength;

                        string tempData = "";
                        using (Stream stream = webResponse.GetResponseStream())
                        {
                            StreamReader sr = new StreamReader(stream);
                            tempData = sr.ReadToEnd();
                        }

                        string[] lines = tempData.Split('\n');

                        LatestPositions rs = new LatestPositions(lines);
                        Holder.dataIsBeeingUpdated = true;
                        foreach (KeyValuePair<int, TeamListPositions> tpkey in rs)
                        {
                            if (Holder.teams.ContainsKey(tpkey.Key))
                            {
                                foreach (double timestamp in tpkey.Value.Keys)
                                {
                                    TeamPosition previous = null; ;
                                    if (Holder.teams[tpkey.Key].positions.Count > 0)
                                        previous = Holder.teams[tpkey.Key].positions.OrderBy(item => item.Key).Last().Value;

                                    if (Holder.teams[tpkey.Key].positions.ContainsKey(timestamp) == false)
                                    {
                                        TeamPosition latestPos = tpkey.Value[timestamp];
                                        latestPos.CalculateFromPrevious(previous);
                                        Holder.teams[tpkey.Key].positions.Add(timestamp, latestPos);
                                    }
                                    

                                }
                            }
                        }
                    }

                    Holder.latestPositionsUpdate = Tools.DateTimeToUnixTime(DateTime.Now);
                    Holder.dataIsBeeingUpdated = false;
                    return 0;
                }
                else
                {
                    messages.Add(DateTime.Now, "Failed to update LatestPositions, teams are not loaded properly");
                    return -1;
                }
            }
            catch { return -1; }
        }

        public static int updateAllPositions(string workingFolder)
        {
            if (UpdatingEvent != null)
                UpdatingEvent("Updating All Positions...");

            if (Tracker.Properties.Settings.Default.RootLocation[Tracker.Properties.Settings.Default.RootLocation.Length - 1] != '/')
                Tracker.Properties.Settings.Default.RootLocation += "/";

            if (Holder.teams != null)
            {
                if (Tracker.Properties.Settings.Default.UseAllPositions)
                {
                    string webrequest = Tracker.Properties.Settings.Default.RootLocation + "AllPositions/";
                    string fileLocation = workingFolder + "\\AllPositions.xml";

                    XmlDocument RaceDocument = GetFileFromWeb(webrequest, fileLocation);
                    if (RaceDocument != null)
                    {
                        AllPositions rs = new AllPositions(RaceDocument);
                        Holder.dataIsBeeingUpdated = true;
                        foreach (KeyValuePair<int, TeamListPositions> tpkey in rs)
                        {
                            if (Holder.teams.ContainsKey(tpkey.Key))
                            {
                                foreach (double timestamp in tpkey.Value.Keys)
                                {
                                    if (Holder.teams[tpkey.Key].positions.ContainsKey(timestamp) == false)
                                        Holder.teams[tpkey.Key].positions.Add(timestamp, tpkey.Value[timestamp]);
                                    else
                                    {

                                    }
                                }
                            }
                        }

                        Holder.allPositionsUpdate = Tools.DateTimeToUnixTime(DateTime.Now);
                        Holder.dataIsBeeingUpdated = false;
                        return 0;
                    }
                    else
                    {
                        messages.Add(DateTime.Now, "Failed to get the file " + webrequest + " from the web");
                        return -1;
                    }
                }
                else
                    return 0;
            }
            else
            {
                messages.Add(DateTime.Now, "Failed to update AllPositions, teams are not loaded properly");
                return -1;
            }
        }


        private static XmlDocument GetFileFromWeb(string webLocation, string fileLocation)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(webLocation);
                request.Timeout = 10000;
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                request.AutomaticDecompression = DecompressionMethods.GZip;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                bytesTransfered += response.ContentLength;
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);

                FileInfo file = new FileInfo(fileLocation);

                FileStream fileStream = File.Create(file.FullName);
                byte[] buffer = new byte[1000];
                int length;
                while ((length = reader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                    fileStream.Write(buffer, 0, length);
                fileStream.Close();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(file.FullName);

                reader.Close();
                dataStream.Close();
                response.Close();

                return xmlDoc;
            }
            catch(Exception e)
            {
                messages.Add(DateTime.Now, e.Message);
                return null;
            }
        }

        #endregion

    }
}
