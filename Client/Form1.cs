using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{

    /// <summary>
    /// Front-End Controller for the synchronous class,
    /// results are printed in the console.
    /// </summary>
    public partial class Form1 : Form
    {
        private SynchronousClient client;
        public Form1()
        {
            client = new SynchronousClient();
            InitializeComponent();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            client.setMessage(text_msg.Text);
            client.startClient();
        }
    }
}
