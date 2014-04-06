using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tracker.Data;

namespace Tracker
{
    public partial class Parameters : UserControl
    {
        #region delegates
        public delegate void Updating();
        public event Updating UpdatingEvent;
        #endregion

        #region constructors
        public Parameters()
        {
            InitializeComponent();

            this.checkBoxCourse.Checked = Tracker.Properties.Settings.Default.AutoCourseParams;
            this.checkBoxAll.Checked = Tracker.Properties.Settings.Default.AutoAllPositions;
            this.checkBoxLatest.Checked = Tracker.Properties.Settings.Default.AutoLatestPositions;
            this.checkBoxRace.Checked = Tracker.Properties.Settings.Default.AutoRaceParams;
            this.checkBoxTeams.Checked = Tracker.Properties.Settings.Default.AutoTeamsDef;
            this.checkBoxDons.Checked = Tracker.Properties.Settings.Default.UseDons;

            this.textBoxInterval.Text = (Tracker.Properties.Settings.Default.RefreshInterval /60 /1000).ToString();
            this.textBoxRoot.Text = Tracker.Properties.Settings.Default.RootLocation;

            this.checkBoxUseAllPositions.Checked = Tracker.Properties.Settings.Default.UseAllPositions;

            if(Holder.teams != null)
                this.label1LatestTeams.Text = Tools.UnixTimeStampToDateTime(Holder.teams.timeStamp).ToString();
            if (Holder.course != null)
                this.labelLatestCourse.Text = Tools.UnixTimeStampToDateTime(Holder.course.timeStamp).ToString();
            if (Holder.race != null)
                this.labelLatestRace.Text = Tools.UnixTimeStampToDateTime(Holder.race.timeStamp).ToString();
            if (Holder.latestPositionsUpdate != -1)
                this.labelLatest.Text = Tools.UnixTimeStampToDateTime(Holder.latestPositionsUpdate).ToString();
            if(Holder.allPositionsUpdate != -1)
                this.labelLatestAll.Text = Tools.UnixTimeStampToDateTime(Holder.allPositionsUpdate).ToString();
        }
        #endregion

        #region methods
        private void buttonApply_Click(object sender, EventArgs e)
        {
            bool closing = true;

            Tracker.Properties.Settings.Default.AutoCourseParams = this.checkBoxCourse.Checked ;
            Tracker.Properties.Settings.Default.AutoAllPositions = this.checkBoxAll.Checked ;
            Tracker.Properties.Settings.Default.AutoCourseParams = this.checkBoxCourse.Checked ;
            Tracker.Properties.Settings.Default.AutoRaceParams = this.checkBoxRace.Checked ;
            Tracker.Properties.Settings.Default.AutoTeamsDef = this.checkBoxTeams.Checked ;
            Tracker.Properties.Settings.Default.UseDons = this.checkBoxDons.Checked;
            try
            {
                int value = Convert.ToInt32(this.textBoxInterval.Text) * 1000 * 60;
                if (value > 0)
                    Tracker.Properties.Settings.Default.RefreshInterval = value;
                else
                {
                    MessageBox.Show("Interval value must be greater than zero");
                    closing = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid value for the refreshing interval.\r\n Please enter a numeric value in minutes", "Warning", MessageBoxButtons.OK);
                closing = false;
            }
            if (Tracker.Properties.Settings.Default.RootLocation != this.textBoxRoot.Text)
            {
                DialogResult res = MessageBox.Show("Modifying the server root location require to restart the application\r\n otherwise data from the previous and new race will be merged", "Warning", MessageBoxButtons.OKCancel);
                if (res == DialogResult.OK)
                    Tracker.Properties.Settings.Default.RootLocation = this.textBoxRoot.Text;
                else
                    closing = false;
            }

            Tracker.Properties.Settings.Default.UseAllPositions = this.checkBoxUseAllPositions.Checked;
            Tracker.Properties.Settings.Default.Save();

            if (this.UpdatingEvent != null)
                this.UpdatingEvent();

            if (this.Parent is Form && closing)
            {
                (this.Parent as Form).Close();

                this.Dispose();
            }
        }
        #endregion

    }
}
