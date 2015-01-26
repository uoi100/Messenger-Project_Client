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
using System.Collections;

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
            setIP();
            client.setMessage(text_msg.Text);
            client.startClient();
        }

        private void setIP()
        {
            string[] ip = text_IPAddress.Text.Split('.');
            byte[] bytes = new byte[4];

            for (int i = 0; i < 4; i++)
            {
                bytes[i] = byte.Parse(ip[i]);
            }
            client.setIPAddress(bytes);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => client.checkLan(this));

            thread.Start();
        }

        delegate void setCallBack(ArrayList list);

        public void updateList(ArrayList addressList)
        {
            if (this.listBox1.InvokeRequired)
            {
                setCallBack d = new setCallBack(updateList);
                this.Invoke(d, new object[] { addressList });
            }
            else
            {
                Console.WriteLine(" Printing List... ");

                for (int i = 0; i < addressList.Count; i++)
                {
                    string ipAddress = (string)addressList[i];
                    Console.WriteLine(ipAddress);
                    listBox1.Items.Add(ipAddress);
                }

                Console.WriteLine(" End Printing List... ");
            }
        }
    }
}
