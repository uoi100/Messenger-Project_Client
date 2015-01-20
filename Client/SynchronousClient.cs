﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class SynchronousClient
    {
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

            byte[] ip = {142,232,49,157};
            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket
                // This example uses port 11000 on the local computer.
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddress = new IPAddress(ip);
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
                    byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

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


        // End of Class
    }
}
