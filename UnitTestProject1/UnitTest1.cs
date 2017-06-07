using System;
using System.IO;
using System.Net.Sockets;
using MathNetTcpServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //be
            string request = "add 2 2";
            Assert.AreEqual("4", GetResponse(request));
            
            request = "subtract 3 2";
            Assert.AreEqual("1", GetResponse(request));

            request = "divide 2 2";
            Assert.AreEqual("1", GetResponse(request));

            request = "multiply 3 3";
            Assert.AreEqual("9", GetResponse(request));

            request = "pladder 3 3";
            Assert.AreEqual("use opperator: add/subtract/multiply/divide", GetResponse(request));


            request = "sasdadasf";
            Assert.AreEqual("use opperator and 2 numbers you want to use on it <add/subtract/multiply/divide> <number1> <number2>", GetResponse(request));

            request = "add a a";
            Assert.AreEqual("not a number", GetResponse(request));

            request = "add a1 a34253sdfsddghjgh";
            Assert.AreEqual("not a number",GetResponse(request));

            request = "add a1 as dfsgdfhgf a34253sdfsddghjgh";
            Assert.AreEqual("use opperator and 2 numbers you want to use on it <add/subtract/multiply/divide> <number1> <number2>", GetResponse(request));

            request = "";
            Assert.AreEqual("use opperator and 2 numbers you want to use on it <add/subtract/multiply/divide> <number1> <number2>", GetResponse(request));

            request = " ";
            Assert.AreEqual("use opperator and 2 numbers you want to use on it <add/subtract/multiply/divide> <number1> <number2>", GetResponse(request));

            request = "  ";
            Assert.AreEqual("not a number", GetResponse(request));

            request = "0";
            Assert.AreEqual("use opperator and 2 numbers you want to use on it <add/subtract/multiply/divide> <number1> <number2>", GetResponse(request));

            request = "llllllll llll";
            Assert.AreEqual("use opperator and 2 numbers you want to use on it <add/subtract/multiply/divide> <number1> <number2>", GetResponse(request));

        }

        private static string GetResponse(String request)
        {
            String server = "localhost";
            using (TcpClient client = new TcpClient(server, Program.Port))
            {
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(request);
                writer.Flush();
                StreamReader reader = new StreamReader(stream);
                String response = reader.ReadLine();
                return response;
            }

        }
    }
}
