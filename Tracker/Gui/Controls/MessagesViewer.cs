using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tracker.Gui.Controls
{
    public partial class MessagesViewer : UserControl
    {

        Messages messagesRef = null;

        public MessagesViewer()
        {
            InitializeComponent();
        }

        public void Init(Messages messages)
        {
            this.messagesRef = messages;
            this.listBox1.Items.Clear();
            foreach (Message mes in messages)
            {
                this.listBox1.Items.Add(mes);
            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.messagesRef.Clear();
            this.listBox1.Items.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.Parent is Form)
                (this.Parent as Form).Close();
        }
    }
}
