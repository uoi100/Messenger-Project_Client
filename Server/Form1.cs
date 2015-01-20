using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Server
{

    /// <summary>
    /// Description: Front-end controller of the synchronous server. Results are printed in the
    /// console.
    /// </summary>
    public partial class Form1 : Form
    {
        private SynchronousServer server;
        public Form1()
        {
            server = new SynchronousServer();
            InitializeComponent();
        }

        private void btn_Server_Click(object sender, EventArgs e)
        {
            server.startListening();
        }
    }
}
