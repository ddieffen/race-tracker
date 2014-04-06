using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;
using Tracker.Data;

namespace Tracker.Gui.Controls
{
    public partial class BoatSpeeds : UserControl
    {
        public BoatSpeeds()
        {
            InitializeComponent();

            GraphPane myPane = this.zedGraphControl1.GraphPane;

            // Set the titles
            myPane.Title.Text = "Selected Teams Speeds";
            myPane.XAxis.Title.Text = "Date";
            myPane.XAxis.Type = AxisType.Date;
            myPane.XAxis.Scale.MajorUnit = DateUnit.Hour;
            myPane.YAxis.Title.Text = "Speed";
            myPane.Legend.IsVisible = true;

            this.zedGraphControl1.IsShowPointValues = true;
        }

        public void UpdateSpeeds()
        {
            GraphPane myPane = this.zedGraphControl1.GraphPane;

            myPane.CurveList.Clear();
            this.zedGraphControl1.AxisChange();

            List<int> checkedTeams = new List<int>();
            foreach (string str in Holder.teamsSelected.Split(','))
            {
                if (str != "")
                    checkedTeams.Add(Convert.ToInt32(str));
            }
            if (Holder.teams != null)
            {
                foreach (TeamData team in Holder.teams.Values.Where(item => checkedTeams.Contains(item.id)).OrderBy(item => item.LatestPosition.distToGo))
                {
                    PointPairList ppl = new PointPairList();

                    foreach (TeamPosition tp in team.positions.Values.OrderBy(item => item.timestamp))
                    {
                        DateTime dt = Tools.UnixTimeStampToDateTime(tp.timestamp);
                        ppl.Add(new XDate(dt), tp.speed, 0, team.name + "\r\n" + tp.ToNiceString());
                    }

                    LineItem myCurve = myPane.AddCurve(team.name, ppl, Tools.InvertMeAColour(ColorTranslator.FromHtml("#" + team.colorHtml)), SymbolType.Circle);
                    myCurve.Symbol.IsVisible = true;

                }
            }
            // Fill the background of the chart rect and pane
            myPane.Chart.Fill = new Fill(Color.White, Color.White, 45.0f);
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.Fill = new Fill(Color.White, Color.LightYellow, 45.0f);

            this.zedGraphControl1.AxisChange();
            this.zedGraphControl1.Refresh();
        }
    }
}
