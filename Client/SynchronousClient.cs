using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace Client
{
    class SynchronousClient
    {
        private IPAddress ipAddress;
        private string message;
        /// <summary>
        /// Description: The client is built with a synchronous socket,
        /// so the execution of the client application is suspended until the
        /// server returns a response. The application sends a string
        /// to the server and then displays the string
        /// returned by the server on the console.
        /// 
        /// </summary>
        public void startClient()
        {
            // Data buffer for incoming data.
            byte[] bytes = new byte[1024];

            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket
                // This example uses port 11000 on the local computer.
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // Create a TCP/IP socket.
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(message);

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed test = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                }
                catch (ArgumentException ane)
                {
                    Console.WriteLine("ArgumentNullException: {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException: {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception: {0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine( "Exception: {0}", e.ToString());
            }
        }
        
        public void checkLan(Form1 form)
        {
            IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            byte[] bytes = new byte[0];

            foreach (IPAddress ip in ipHostEntry.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    bytes = ip.GetAddressBytes();
                    break;
                }
            }

            ArrayList masterList = new ArrayList();
            ArrayList[] threadList = new ArrayList[4];
            Thread[] thread = new Thread[4];

            thread[0] = new Thread( () => { threadList[0] = pinger( bytes, 0, 64 ); });
            thread[1] = new Thread( () => { threadList[1] = pinger( bytes, 65, 128 ); });
            thread[2] = new Thread( () => { threadList[2] = pinger( bytes, 129, 192 ); });
            thread[3] = new Thread( () => { threadList[3] = pinger( bytes, 193, 254 ); });

            for(int i = 0; i < 4; i++)
                thread[i].Start();

            for(int i = 0; i < 4; i++)
                thread[i].Join();

            for(int i = 0; i < 4; i++)
                for(int j = 0; j < threadList[i].Count; j++)
                    masterList.Add(threadList[i][j]);

            form.updateList(masterList);
        }

        private ArrayList pinger( byte[] ip, byte start, byte finish)
        {
            ArrayList list = new ArrayList();

            Thread.BeginCriticalRegion();
            byte[] byteIP = new byte[ip.Length];
            for (int i = 0; i < byteIP.Length; i++)
                byteIP[i] = ip[i];
            Thread.EndCriticalRegion();

            for(byte i = start; i <= finish; i++)
            {

                byteIP[ip.Length-1] = i;
                if (ping(byteIP))
                {
                    Thread.BeginCriticalRegion();
                    string lanIP = "";
                    for (int j = 0; j < ip.Length; j++)
                    {
                        if (j != ip.Length - 1)
                            lanIP += ip[j].ToString() + ".";
                        else
                            lanIP += ip[j].ToString();
                    }
                    list.Add(lanIP);
                    Thread.EndCriticalRegion();
                }
                if(i == 255)
                    break;
            }

            return list;
        }

        private bool ping(byte[] ip)
        {
            byte[] bytes = new byte[1024];
            bool status = false;

            try
            {
                IPAddress lanAddress = new IPAddress(ip);

                Ping ping = new Ping();
                PingReply pingRequest = ping.Send(lanAddress, 1000);

                if (pingRequest.Status != null)
                {
                    Console.WriteLine("Status: {0}, Time: {1}, Address: {2}", pingRequest.Status, pingRequest.RoundtripTime, pingRequest.Address);
                }
            }
            catch
            {
                Console.WriteLine("Timeout Error.");
            }

            return status;

            try
            {
                IPAddress lanAddress = new IPAddress(ip);
                byte[] lanBytes = lanAddress.GetAddressBytes();

                for (int i = 0; i < lanBytes.Length; i++)
                {
                    Console.Write(lanBytes[i] + ".");
                }

                Console.WriteLine();

                IPEndPoint ipEndPoint = new IPEndPoint(lanAddress, 11000);

                Socket sender = new Socket(lanAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    // connect to the ip address
                    //sender.Connect(ipEndPoint);

                    // Connect using a timeout ( 5 second )
                    IAsyncResult result = sender.BeginConnect(lanAddress, 11000, null, null);

                    status = result.AsyncWaitHandle.WaitOne(100, true);

                    if (!sender.Connected)
                    {
                        sender.Close();
                        Console.WriteLine("Failed to connect to server.");
                    }
                    else
                    {
                        sender.Disconnect(true);
                        sender.Close();
                        Console.WriteLine("Successfully connected to server.");
                    }

                    /*
                    byte[] msg = Encoding.ASCII.GetBytes("<EOF>");

                    // Send data to server
                    int bytesSent = sender.Send(msg);

                    // Receive data from server
                    int bytesRec = sender.Receive(bytes);

                    // Shut down socket for listening and sending on the client
                    sender.Shutdown(SocketShutdown.Both);

                    sender.Close();
                    status = true;
                    */
                }
                catch (ArgumentException ane)
                {
                    Console.WriteLine("ArgumentNullException: {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException: {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception: {0}", e.ToString());
                }
            }
            catch (Exception e) { }

            return status;
        }

        public void setMessage(string message)
        {
            this.message = message + "<EOF>";
        }

        public void setIPAddress(byte[] ip)
        {
            ipAddress = new IPAddress(ip);
        }

        // End of Class
    }
}
