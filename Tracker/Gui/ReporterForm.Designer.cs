
namespace Tracker
{
    partial class ReporterForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.labelTo = new System.Windows.Forms.Label();
            this.labelToAddress = new System.Windows.Forms.Label();
            this.labelException = new System.Windows.Forms.Label();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.labelSubject = new System.Windows.Forms.Label();
            this.MailLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.MinimumSize = new System.Drawing.Size(400, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "TRACKER has crashed. Would you like to email an error report to the developers?";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.Location = new System.Drawing.Point(3, 73);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(41, 13);
            this.errorLabel.TabIndex = 1;
            this.errorLabel.Text = "Error :  ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 93);
            this.label2.MinimumSize = new System.Drawing.Size(400, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(400, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Please describe what actions were made before the error occured.";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.No;
            this.button2.Location = new System.Drawing.Point(248, 257);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 9;
            this.button2.Text = "No";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.button1.Location = new System.Drawing.Point(167, 257);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 7;
            this.button1.Text = "Yes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.Location = new System.Drawing.Point(5, 109);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.Size = new System.Drawing.Size(457, 112);
            this.descriptionTextBox.TabIndex = 6;
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(3, 29);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(26, 13);
            this.labelTo.TabIndex = 70;
            this.labelTo.Text = "To :";
            // 
            // labelToAddress
            // 
            this.labelToAddress.AutoSize = true;
            this.labelToAddress.Location = new System.Drawing.Point(56, 29);
            this.labelToAddress.Name = "labelToAddress";
            this.labelToAddress.Size = new System.Drawing.Size(103, 13);
            this.labelToAddress.TabIndex = 80;
            this.labelToAddress.Text = "ddieffen@gmail.com";
            // 
            // labelException
            // 
            this.labelException.AutoSize = true;
            this.labelException.Location = new System.Drawing.Point(56, 73);
            this.labelException.Name = "labelException";
            this.labelException.Size = new System.Drawing.Size(54, 13);
            this.labelException.TabIndex = 90;
            this.labelException.Text = "Exception";
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.buttonCopy.Location = new System.Drawing.Point(329, 257);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(133, 25);
            this.buttonCopy.TabIndex = 8;
            this.buttonCopy.Text = "Copy Message && Close";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 91;
            this.label3.Text = "Subject :  ";
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Location = new System.Drawing.Point(56, 51);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(61, 13);
            this.labelSubject.TabIndex = 92;
            this.labelSubject.Text = "Bug Report";
            // 
            // MailLabel
            // 
            this.MailLabel.ForeColor = System.Drawing.Color.Red;
            this.MailLabel.Location = new System.Drawing.Point(6, 224);
            this.MailLabel.Name = "MailLabel";
            this.MailLabel.Size = new System.Drawing.Size(456, 30);
            this.MailLabel.TabIndex = 93;
            // 
            // ReporterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 292);
            this.Controls.Add(this.MailLabel);
            this.Controls.Add(this.labelSubject);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.labelException);
            this.Controls.Add(this.labelToAddress);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReporterForm";
            this.Text = "Error Report";
            this.Load += new System.EventHandler(this.ReporterForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Label labelToAddress;
        private System.Windows.Forms.Label labelException;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.Label MailLabel;

    }
}