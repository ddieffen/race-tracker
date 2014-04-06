using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Win32;

namespace Tracker
{
    /// <summary>
    /// A form for displaying an error that crashed GREET to a user and ask for a description
    /// what they were doing that caused the crash.
    /// </summary>
    public partial class ReporterForm : Form
    {
        const string emailAddress = "ddieffen@gmail.com";
        const string subject = "BUG report for Tracker";
        string version;
        string[] rejectedRegistryStrings;
        Exception exception;

        /// <summary>
        /// Creates a form for displaying an error that crashed GREET to a user and ask for a 
        /// description what caused the crash
        /// </summary>
        public ReporterForm()
        {
            InitializeComponent();
            version = Application.ProductVersion;
            rejectedRegistryStrings = new string[] { "gmail", "1" };
            object key = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Classes\SOFTWARE\Google\Gmail", "MailtoRegistered", "none");
            string defaultMailClient = "";
            if(key != null)
                defaultMailClient = key.ToString();

            if (defaultMailClient != null && this.CheckMailClient(defaultMailClient) == false)
            {
                MailLabel.Text = "Tracker does not support sending error reports through web-based email. Please copy and paste the message manually into your email, or change your default mail client.";
                button1.Enabled = false;
                button2.Enabled = false;
            }
            else
            {
                MailLabel.Hide();
                button1.Enabled = true;
                button2.Enabled = true;
                descriptionTextBox.Size = new Size(457, 140);
            }
        }

        public bool CheckMailClient(string defaultMailClient)
        {
            foreach (string s in rejectedRegistryStrings)
            {
                if (defaultMailClient.Contains(s))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Sets the exception text of the form
        /// </summary>
        /// <param name="exceptionText"></param>
        public ReporterForm(Exception e)
            : this()
        {
            this.exception = e;
            this.labelToAddress.Text = emailAddress;
            this.labelSubject.Text = subject;
            this.labelException.Text = e.Message;
        }

        /// <summary>
        /// Returns the body of the mail.
        /// </summary>
        /// <returns></returns>
        private string getBody()
        {
            string body = "";
            if (this.descriptionTextBox.Text != "")
                body = "User description : \n" + this.descriptionTextBox.Text + "\n\n";
            body += "Exception Message :" + Environment.NewLine + exception.Message + "\n\n" +
                "Exception Source : \n" + exception.Source + "\n\n" +
                "Deployement version : \n" + version + "\n\n" +
                "Exception Stack Trace : \n" + exception.StackTrace.Replace("&", "") + "\n\n";
            return body;

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            Process.Start("mailto:" + emailAddress + "?subject=" + subject + "&body="
                   + getBody().Replace("\n", "%0A").Replace("\"", "%22"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            String message = "To: " + emailAddress + Environment.NewLine + Environment.NewLine;
            message += getBody();
            Clipboard.SetText(message);
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ReporterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
