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
    public partial class TeamInfo : UserControl
    {
        public TeamInfo()
        {
            InitializeComponent();
        }

        public void SetTeam(TeamData td)
        {
            try
            {
                this.labelName.Text = "";
                this.labelType.Text = "";
                this.labelSail.Text = "";
                this.labelPositionAt.Text = "";
                this.labelPosition.Text = "";
                this.labelSpeed.Text = "";
                this.labelDistanceToGo.Text = "";

                this.labelName.Text = td.name;
                this.labelName.ForeColor = Tools.InvertMeAColour(ColorTranslator.FromHtml("#" + td.colorHtml));
                this.labelType.Text = td.model;
                this.labelSail.Text = td.sail;
                if (td.LatestPosition != null)
                {
                    this.labelPositionAt.Text = td.LatestPosition.TimeStamp.ToString();
                    this.labelPosition.Text = td.LatestPosition.ToString();
                    this.labelSpeed.Text = td.LatestPosition.speed.ToString("F2") + " kn / " + td.LatestPosition.heading.ToString("F0") + " deg";
                    this.labelDistanceToGo.Text = td.LatestPosition.distToGo.ToString() + "nm";
                }
            }
            catch (Exception e)
            {
            }
        }
    }
}
