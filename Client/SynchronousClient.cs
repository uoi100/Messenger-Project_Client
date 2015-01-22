using System;
using System.Net;
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

        public ArrayList checkLan()
        {
            ArrayList addressList = new ArrayList();
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

            for (byte i = 0; i <= 255; i++)
            {
                bytes[bytes.Length - 1] = i;
                if (ping(bytes))
                {
                    addressList.Add(bytes.ToString());
                }
            }

            return addressList;
        }

        private ArrayList pinger( byte[] ip, int 1)
        {

        }

        private bool ping(byte[] ip)
        {
            byte[] bytes = new byte[1024];
            bool status = false;

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
