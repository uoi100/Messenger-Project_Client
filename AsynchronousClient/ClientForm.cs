using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace AsynchronousClient
{
    public partial class ClientForm : Form
    {
        private Client client;
        private Thread threadA, threadB;
        private int count;
        public ClientForm()
        {
            client = new Client();
            count = 0;
            InitializeComponent();
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            threadA = new Thread(() => client.startClient());
            threadA.Start();
            count++;
        }
    }
}
