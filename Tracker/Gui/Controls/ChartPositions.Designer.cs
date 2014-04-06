namespace Tracker.Gui.Controls
{
    partial class ChartPositions
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.checkedListBox2 = new System.Windows.Forms.CheckedListBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.teamInfo1 = new Tracker.Gui.Controls.TeamInfo();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.checkBoxContour = new System.Windows.Forms.CheckBox();
            this.checkBoxPOIS = new System.Windows.Forms.CheckBox();
            this.checkBoxRumLine = new System.Windows.Forms.CheckBox();
            this.checkBoxTeamsTrace = new System.Windows.Forms.CheckBox();
            this.checkBoxCenterSelected = new System.Windows.Forms.CheckBox();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(698, 613);
            this.splitContainer2.SplitterDistance = 161;
            this.splitContainer2.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(698, 161);
            this.splitContainer1.SplitterDistance = 444;
            this.splitContainer1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(442, 159);
            this.panel1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.checkedListBox2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkedListBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 159F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(442, 159);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // checkedListBox2
            // 
            this.checkedListBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox2.FormattingEnabled = true;
            this.checkedListBox2.Location = new System.Drawing.Point(224, 3);
            this.checkedListBox2.Name = "checkedListBox2";
            this.checkedListBox2.Size = new System.Drawing.Size(215, 153);
            this.checkedListBox2.TabIndex = 4;
            this.checkedListBox2.SelectedIndexChanged += new System.EventHandler(this.checkedListBox2_SelectedIndexChanged);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(3, 3);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(215, 153);
            this.checkedListBox1.TabIndex = 3;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.teamInfo1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(248, 159);
            this.panel2.TabIndex = 0;
            // 
            // teamInfo1
            // 
            this.teamInfo1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teamInfo1.Location = new System.Drawing.Point(0, 0);
            this.teamInfo1.Name = "teamInfo1";
            this.teamInfo1.Size = new System.Drawing.Size(248, 159);
            this.teamInfo1.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.zedGraphControl1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.panel3);
            this.splitContainer3.Size = new System.Drawing.Size(696, 446);
            this.splitContainer3.SplitterDistance = 483;
            this.splitContainer3.TabIndex = 1;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl1.IsSynchronizeXAxes = true;
            this.zedGraphControl1.IsSynchronizeYAxes = true;
            this.zedGraphControl1.Location = new System.Drawing.Point(0, 0);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(483, 446);
            this.zedGraphControl1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.linkLabel1);
            this.panel3.Controls.Add(this.checkBoxContour);
            this.panel3.Controls.Add(this.checkBoxPOIS);
            this.panel3.Controls.Add(this.checkBoxRumLine);
            this.panel3.Controls.Add(this.checkBoxTeamsTrace);
            this.panel3.Controls.Add(this.checkBoxCenterSelected);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(209, 446);
            this.panel3.TabIndex = 0;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(9, 153);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(198, 13);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "\"http://www.ngdc.noaa.gov/mgg/coast/\"";
            this.linkLabel1.Text = "http://www.ngdc.noaa.gov/mgg/coast/";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // checkBoxContour
            // 
            this.checkBoxContour.AutoSize = true;
            this.checkBoxContour.Location = new System.Drawing.Point(12, 133);
            this.checkBoxContour.Name = "checkBoxContour";
            this.checkBoxContour.Size = new System.Drawing.Size(98, 17);
            this.checkBoxContour.TabIndex = 4;
            this.checkBoxContour.Text = "Show Contours";
            this.checkBoxContour.UseVisualStyleBackColor = true;
            this.checkBoxContour.CheckedChanged += new System.EventHandler(this.checkBoxContour_CheckedChanged);
            // 
            // checkBoxPOIS
            // 
            this.checkBoxPOIS.AutoSize = true;
            this.checkBoxPOIS.Location = new System.Drawing.Point(12, 110);
            this.checkBoxPOIS.Name = "checkBoxPOIS";
            this.checkBoxPOIS.Size = new System.Drawing.Size(138, 17);
            this.checkBoxPOIS.TabIndex = 3;
            this.checkBoxPOIS.Text = "Show Points of Interrest";
            this.checkBoxPOIS.UseVisualStyleBackColor = true;
            this.checkBoxPOIS.CheckedChanged += new System.EventHandler(this.checkBoxPOIS_CheckedChanged);
            // 
            // checkBoxRumLine
            // 
            this.checkBoxRumLine.AutoSize = true;
            this.checkBoxRumLine.Location = new System.Drawing.Point(12, 87);
            this.checkBoxRumLine.Name = "checkBoxRumLine";
            this.checkBoxRumLine.Size = new System.Drawing.Size(101, 17);
            this.checkBoxRumLine.TabIndex = 2;
            this.checkBoxRumLine.Text = "Show Rum Line";
            this.checkBoxRumLine.UseVisualStyleBackColor = true;
            this.checkBoxRumLine.CheckedChanged += new System.EventHandler(this.checkBoxRumLine_CheckedChanged);
            // 
            // checkBoxTeamsTrace
            // 
            this.checkBoxTeamsTrace.AutoSize = true;
            this.checkBoxTeamsTrace.Location = new System.Drawing.Point(12, 64);
            this.checkBoxTeamsTrace.Name = "checkBoxTeamsTrace";
            this.checkBoxTeamsTrace.Size = new System.Drawing.Size(114, 17);
            this.checkBoxTeamsTrace.TabIndex = 1;
            this.checkBoxTeamsTrace.Text = "Show Boats Trace";
            this.checkBoxTeamsTrace.UseVisualStyleBackColor = true;
            this.checkBoxTeamsTrace.CheckedChanged += new System.EventHandler(this.checkBoxTrace_CheckedChanged);
            // 
            // checkBoxCenterSelected
            // 
            this.checkBoxCenterSelected.AutoSize = true;
            this.checkBoxCenterSelected.Location = new System.Drawing.Point(12, 41);
            this.checkBoxCenterSelected.Name = "checkBoxCenterSelected";
            this.checkBoxCenterSelected.Size = new System.Drawing.Size(142, 17);
            this.checkBoxCenterSelected.TabIndex = 0;
            this.checkBoxCenterSelected.Text = "Center on Selected Boat";
            this.checkBoxCenterSelected.UseVisualStyleBackColor = true;
            this.checkBoxCenterSelected.CheckedChanged += new System.EventHandler(this.checkBoxCenterSelected_CheckedChanged);
            // 
            // ChartPositions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer2);
            this.Name = "ChartPositions";
            this.Size = new System.Drawing.Size(698, 613);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.CheckedListBox checkedListBox2;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Panel panel2;
        private TeamInfo teamInfo1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox checkBoxPOIS;
        private System.Windows.Forms.CheckBox checkBoxRumLine;
        private System.Windows.Forms.CheckBox checkBoxTeamsTrace;
        private System.Windows.Forms.CheckBox checkBoxCenterSelected;
        private System.Windows.Forms.CheckBox checkBoxContour;
        private System.Windows.Forms.LinkLabel linkLabel1;

    }
}
