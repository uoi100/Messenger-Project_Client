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

        /// <summary>
        /// Description: Start the server when clicked, the server is listening on a 
        /// separate thread so that you can still interact with the server form without it
        /// hanging due to the server listening to the socket.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Server_Click(object sender, EventArgs e)
        {
            string[] ipaddress = text_IPAddress.Text.Split('.');

            byte[] ipaddressv4 = new byte[4];

            for(int i = 0; i < 4; i++)
                ipaddressv4[i] = byte.Parse(ipaddress[i]);

            server.setIP(ipaddressv4);
            
            Thread thread = new Thread( () => server.startListening());
            thread.Start();
        }
    }
}
