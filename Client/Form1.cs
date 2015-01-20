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
            client.startClient();
        }
    }
}
