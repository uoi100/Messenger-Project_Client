using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;


namespace Server
{
    /// <summary>
    /// Description: The server is built with a synchronoous socket,
    /// so execution of the server application is suspended while it waits
    /// for a connection from a client. The application receives a string
    /// from the client, displays the string on the console, and then
    /// echoes the string back to the client. The string from the client must
    /// contain the string "<EOF>" to signal the end of the message
    /// </summary>
    /// 

    class SynchronousServer
    {
        private IPAddress ipAddress;
        public string data = null;
        private Socket listener;
        private Socket handler;
        private volatile Boolean enabled;
        private byte[] bytes;

        /// <summary>
        /// Description: The server is listening on the socket for any client messages
        /// </summary>
        public void startListening()
        {
            // Data buffer for incoming data.
            bytes = new Byte[1024];
            enabled = true;
            // Establish the local endpoint for the socket.
            // Dns.GetHostName returns the name of the
            // host running the application.
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

            Console.WriteLine(ipAddress.ToString());
            // Create a TCP/IP Socket.
            listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and
            // listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.
                while (enabled)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.
                    handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.
                    while (true)
                    {

                        bytes = new byte[1024];
                        int bytesRec = handler.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                        if (data.IndexOf("<EOF>") > -1) { break; }
                    }

                    data = data.Substring(0, data.LastIndexOf("<EOF"));
                    // Show the data on the console.
                    Console.WriteLine("Text received: {0}", data);

                    // Echo the data back to the client.
                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.ToString());
            }

            Console.WriteLine("Server has stopped.");
            listener.Close();
        }

        /// <summary>
        /// Description: Set the ip address of the server
        /// </summary>
        /// <param name="ip">The ip address of the server that the client should connect to.</param>
        public void setIP(byte[] ip)
        {
            ipAddress = new IPAddress(ip);
        }

        /// <summary>
        /// Description: Stop the server
        /// </summary>
        public void stop()
        {
            enabled = false;
            try
            {
                // Create TCP/IP Socket
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any error.
                try
                {
                    sender.Connect(new IPEndPoint(ipAddress, 11000));

                    // encode the data string into a byte array
                    byte[] msg = Encoding.ASCII.GetBytes("<EOF>");

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive response from the remote device
                    int bytesRec = sender.Receive(bytes);

                    // Release the socket
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
            catch (Exception e) { }
        }
        // End Class
    }
}
