using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tracker.Data;

namespace Tracker.Gui.Controls
{
    public partial class Analytics : UserControl
    {
        #region delegates
        public delegate void MyBoatSelectionChanged();
        public event MyBoatSelectionChanged MyBoatChangedEvent;
        #endregion

        #region constructors
        public Analytics()
        {
            InitializeComponent();
        }
        #endregion

        #region methods
        private void InitializeCombo()
        {
            if(Holder.teams != null)
            {
                foreach (TeamData td in Holder.teams.Values.OrderBy(item => item.name))
                {
                    this.comboBox1.Items.Add(td);
                }

                this.comboBox1.SelectedIndexChanged -= new EventHandler(comboBox1_SelectedIndexChanged);

                if(Tracker.Properties.Settings.Default.MyTeam != -1 && Holder.teams.ContainsKey(Tracker.Properties.Settings.Default.MyTeam))
                    this.comboBox1.SelectedItem = Holder.teams[Tracker.Properties.Settings.Default.MyTeam];

                this.comboBox1.SelectedIndexChanged += new EventHandler(comboBox1_SelectedIndexChanged);
            }
        }

        private void InitializeScores()
        {
            List<ScoringItem> scores = new List<ScoringItem>();

            List<int> checkedTeams = new List<int>();
            foreach (string str in Holder.teamsSelected.Split(','))
            {
                if (str != "")
                    checkedTeams.Add(Convert.ToInt32(str));
            }

            
            int positionCount = 1;
            if(Holder.teams != null){
                foreach (TeamData td in Holder.teams.Values.Where(item => checkedTeams.Contains(item.id)).OrderBy(item => item.LatestPosition.distToGo))
                {
                    scores.Add(new ScoringItem(positionCount, td.id));
                    positionCount++;
                }
            }
            this.dataGridView1.DataSource = scores.ToArray();
        }

        void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tracker.Properties.Settings.Default.MyTeam = (this.comboBox1.SelectedItem as TeamData).id;
            this.UpdateDisplay();
            if (this.MyBoatChangedEvent != null)
                this.MyBoatChangedEvent();
            Tracker.Properties.Settings.Default.Save();
        }
        internal void UpdateDisplay()
        {
            this.InitializeCombo();
            this.InitializeScores();
        }
        #endregion

       
    }


    public class ScoringItem
    {
        private int position = -1;
        private int teamId = -1;

        public ScoringItem(int position, int teamId)
        {
            this.position = position;
            this.teamId = teamId;
        }

        public int Position 
        {
            get { return this.position; }
        }

        public String BoatName
        {
            get { return Holder.teams[this.teamId].name; }
        }

        public String DistanceToGo
        {
            get { return Holder.teams[this.teamId].LatestPosition.distToGo.ToString("F2"); }
        }

        public String ToGoDifference
        {
            get 
            {
                if (Holder.teams.ContainsKey(Tracker.Properties.Settings.Default.MyTeam))
                {
                    TeamPosition mine = Holder.teams[Tracker.Properties.Settings.Default.MyTeam].LatestPosition;
                    TeamPosition theirs = Holder.teams[this.teamId].LatestPosition;

                    return Math.Round(mine.distToGo - theirs.distToGo, 3).ToString("F2");
                }
                return "NaN";
            }
        }

        public string Distance
        {
            get
            {
                if (Holder.teams.ContainsKey(Tracker.Properties.Settings.Default.MyTeam))
                {
                    TeamPosition mine = Holder.teams[Tracker.Properties.Settings.Default.MyTeam].LatestPosition;
                    TeamPosition theirs = Holder.teams[this.teamId].LatestPosition;

                    return Tools.HaversineDistanceNauticalMiles(mine.latN, mine.lonE, theirs.latN, theirs.lonE).ToString("F2");
                }
                return "NaN";
            }
        }

        public string BearingT
        {
            get
            {
                if (Holder.teams.ContainsKey(Tracker.Properties.Settings.Default.MyTeam))
                {
                    TeamPosition mine = Holder.teams[Tracker.Properties.Settings.Default.MyTeam].LatestPosition;
                    TeamPosition theirs = Holder.teams[this.teamId].LatestPosition;

                    return Tools.HaversineHeadingDegrees(mine.latN, mine.lonE, theirs.latN, theirs.lonE).ToString("F0");
                }
                return "NaN";
            }
        }
    }
}
