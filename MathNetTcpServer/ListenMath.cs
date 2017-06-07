using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace MathNetTcpServer
{
    class MathServer
    {
        public int Port { get; set; }
        public IPAddress LocalAddress { get; private set; }
        
        public MathServer(int port)
        {
            Port = port;
            LocalAddress = IPAddress.Loopback;
        }

        public void Start()
        {
            TcpListener server = new TcpListener(LocalAddress, Port);
            server.Start();
            Trace.WriteLine("Math server started on " + LocalAddress + "port " + Port);
            while (true)
            {
                Trace.WriteLine("Waiting for connection ...");
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");
                Task.Run(() => DoIt(client));

            }
        }

        private static void DoIt(TcpClient client )
        {
            try
            {
                NetworkStream stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                String request = reader.ReadLine();
                // math stuff here (which makes this different from eccho server since
                // it should do more with the request than just returning it)

                // makes the variable response can be used later on
                string response = "";
                

                
                
                // splits a string into pieces when a space character is encountered,
                // [0] should be the math opperator
                // [1] should be digit 1 that you want to add with the other 
                // [2] same as previous
                string[] del = request.Split(' ');
                if (del.Length != 3)
                {
                    response = "use opperator and 2 numbers you want to use on it <add/subtract/multiply/divide> <number1> <number2>";
                    Trace.WriteLine("request is invalid, use math opperator and 2 numbers only", request);
                }

                else
                {
                    try
                    {
                        Double tal1 = Double.Parse(del[1]);
                        Double tal2 = Double.Parse(del[2]);

                        switch (del[0])
                        {
                            case "add":
                                response = Convert.ToString(tal1 + tal2);
                                Trace.WriteLine(request, response);
                                break;

                            case "subtract":
                                response = Convert.ToString(tal1 - tal2);
                                Trace.WriteLine(request, response);
                                break;

                            case "multiply":
                                response = Convert.ToString(tal1 * tal2);
                                Trace.WriteLine(request, response);
                                break;

                            case "divide":
                                response = Convert.ToString(tal1 / tal2);
                                Trace.WriteLine(request, response);
                                break;

                            //plz type either: add/subtract/multiply/divide and two digits separated with spaces e.g 'add 2 2'
                            default:
                                response = "use opperator: add/subtract/multiply/divide";
                                Trace.WriteLine("request is invalid, use math opperator and 2 numbers only", request);
                                break;
                        }
                    }
                    catch (FormatException)
                    {
                        response = "not a number";
                        Trace.WriteLine("request is invalid, use numbers, not letters!" ,request);
                    }

                        
                    }
                

                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(response);
                writer.Flush();
            }
            catch (IOException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            finally
            {
                client.Close();
            }

        }
    }
}
