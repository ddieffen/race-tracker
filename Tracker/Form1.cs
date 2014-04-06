using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Tracker.Data;
using System.IO;
using System.Net;
using System.Net.Cache;
using Tracker.Gui.Controls;

namespace Tracker
{
    public partial class Form1 : Form
    {
        #region attributes
        public string WorkingDirectory;
        public BackgroundWorker UpdaterWorker = new BackgroundWorker();
        public BackgroundWorker SavingWorker = new BackgroundWorker();

        int updateResult = 0;
        int countDown = 300;
        #endregion

        #region constructors
        public Form1()
        {
            InitializeComponent();

            this.Text = "Yellowbrick Data Reader " + Application.ProductVersion.ToString();

            this.setEnvironment();

            
        }

        public void setEnvironment()
        {
            this.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Holder.serverBaseName = Tracker.Properties.Settings.Default.RootLocation;
            this.WorkingDirectory += "\\tracker-yellowbrick\\" + Holder.serverBaseName.Replace("/", "").Replace(".", "").Replace(":", "");
            DirectoryInfo info = new DirectoryInfo(this.WorkingDirectory);
            if (info.Exists == false)
                Directory.CreateDirectory(this.WorkingDirectory);

            Presenter.UpdatingEvent += new Presenter.Updating(SetStatusThreadSafe);
            this.UpdaterWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(UpdaterWorer_RunWorkerCompleted);
            this.UpdaterWorker.DoWork += new DoWorkEventHandler(UpdaterWorer_DoWork);
            this.SavingWorker.DoWork += new DoWorkEventHandler(SavingWorker_DoWork);
            this.analytics1.MyBoatChangedEvent += new Analytics.MyBoatSelectionChanged(analytics1_MyBoatChangedEvent);
            this.chartPositions1.MySelectionChangedEvent += new ChartPositions.MyBoatSelectionChanged(chartPositions1_MySelectionChangedEvent);
            this.countDown = Tracker.Properties.Settings.Default.RefreshInterval / 1000;

            int result = Presenter.LoadData(this.WorkingDirectory, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\tracker-yellowbrick\\");
            if (result == -1)
                this.UpdaterWorker.RunWorkerAsync(true);
            else if (result == 0)
            {
                this.chartPositions1.UpdateDisplay();
                this.analytics1.UpdateDisplay();
                this.boatSpeeds1.UpdateSpeeds();
                this.SetStatusThreadSafe("Ready");
                this.UpdaterWorker.RunWorkerAsync(false);
            }
        }

        void chartPositions1_MySelectionChangedEvent()
        {
            this.analytics1.UpdateDisplay();
            this.boatSpeeds1.UpdateSpeeds();
        }

        void analytics1_MyBoatChangedEvent()
        {
            this.chartPositions1.UpdateDisplay();
            this.boatSpeeds1.UpdateSpeeds();
        }

        void SavingWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Presenter.SaveData(this.WorkingDirectory + @"\data.xml");
            Tracker.Properties.Settings.Default.Save();
        }

        void UpdaterWorer_DoWork(object sender, DoWorkEventArgs e)
        {
            bool firstTimeLoading = false;
            if(e.Argument != null &&  e.Argument is bool)
                firstTimeLoading = (bool)e.Argument;

            string dataDir = this.WorkingDirectory + @"\webdata";

            DirectoryInfo info = new DirectoryInfo(dataDir);
            if (info.Exists == false)
                Directory.CreateDirectory(dataDir);

            int result = 0;
            if (Tracker.Properties.Settings.Default.AutoRaceParams || firstTimeLoading)
                result += Presenter.updateRace(dataDir);
            if (Tracker.Properties.Settings.Default.AutoCourseParams || firstTimeLoading)
                result += Presenter.updateCourse(dataDir);
            if (Tracker.Properties.Settings.Default.AutoTeamsDef || firstTimeLoading)
                result += Presenter.updateTeamsData(dataDir);
            if (Tracker.Properties.Settings.Default.AutoLatestPositions || firstTimeLoading)
            {
                if(Tracker.Properties.Settings.Default.UseDons)
                    result += Presenter.updateLatestPositionDon();
                else
                    result += Presenter.updateLatestPositions(dataDir);
            }
            if (Tracker.Properties.Settings.Default.UseAllPositions && (Tracker.Properties.Settings.Default.AutoAllPositions || firstTimeLoading))
                result += Presenter.updateAllPositions(dataDir);

            this.updateResult = result;
        }

        void UpdaterWorer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (this.updateResult == 0)
            {
                this.SetLastUpdateTextThreadSafe("Last Updated on " + DateTime.Now.ToString());
                this.SetStatusThreadSafe("Ready");
                this.chartPositions1.UpdateDisplay();
                this.analytics1.UpdateDisplay();
                this.boatSpeeds1.UpdateSpeeds();

                this.SavingWorker.RunWorkerAsync();
            }
            else
            {
                if (Holder.race == null && Holder.course == null && Holder.teams == null)
                    this.SetStatusThreadSafe("Error : No data can be downloaded, check your connexion and the root server adress");
                else
                    this.SetStatusThreadSafe("Error : Faild to update, see messages log for errors");
            }
        }
        #endregion

        #region event methods
        private delegate void UpdateStripText(string message);
        public void SetStatusThreadSafe(string message)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdateStripText(this.SetStatusThreadSafe), message);
            else
            {
                if (message.Contains("Error"))
                    this.toolStripStatusLabel1.ForeColor = Color.Red;
                else
                    this.toolStripStatusLabel1.ForeColor = Color.Black;
                this.toolStripStatusLabel1.Text = message;
            }
        }

        private delegate void UpdateCountDownText(string message);
        public void SetCountDownTextThreadSafe(string message)
        {
            if (this.InvokeRequired)
                this.Invoke(new UpdateCountDownText(this.SetCountDownTextThreadSafe), message);
            else
            {
                this.toolStripStatusLabel2.Text = message;
            }
        }

        private delegate void LastUpdateDownText(string message);
        public void SetLastUpdateTextThreadSafe(string message)
        {
            if (this.InvokeRequired)
                this.Invoke(new LastUpdateDownText(this.SetLastUpdateTextThreadSafe), message);
            else
            {
                this.toolStripStatusLabel3.Text = message;
            }
        }


        private void editParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form paramsForm = new Form();
            Parameters ctrlP = new Parameters();
            paramsForm.Size = new Size(ctrlP.MinimumSize.Width + 20, ctrlP.MinimumSize.Height + 40);
            ctrlP.Dock = DockStyle.Fill;
            paramsForm.Controls.Add(ctrlP);
            paramsForm.Show();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Presenter.SaveData(this.WorkingDirectory + @"/data.xml");
            Tracker.Properties.Settings.Default.Save();
        }

        private void Update_Tick(object sender, EventArgs e)
        {
            

            countDown--;
            this.SetCountDownTextThreadSafe("Next update in : " + new TimeSpan(0, 0, this.countDown).ToString());
            if (countDown == 0)
            {
                this.UpdaterWorker.RunWorkerAsync();
                this.countDown = Tracker.Properties.Settings.Default.RefreshInterval / 1000;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.SavingWorker.RunWorkerAsync();
        }
        #endregion

        private void messagesLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form messagesForm = new Form();
            MessagesViewer mViewer = new MessagesViewer();
            mViewer.Init(Presenter.messages);
            messagesForm.Size = new Size(mViewer.MinimumSize.Width + 20, mViewer.MinimumSize.Height + 40);
            mViewer.Dock = DockStyle.Fill;
            messagesForm.Controls.Add(mViewer);
            messagesForm.AutoSize = true;
            messagesForm.Show();
        }

    }
}
