using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace UnitTestMath
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMathServer()
        {
            //not
            string request = "Hello";
            string response = GetResponse(request);
            Assert.AreEqual();
        }

        private static string GetResponse(String request)
        {
            String server = "localhost";
            using (TcpClient client = new TcpClient(Server, Program.Port))
            {
                NetworkStream stream = client.GetStream();
                StreamWriter writer = new StreamWriter(stream);
                writer.WriteLine(request);
                writer.Flush();
                StreamReader reader = new StreamReader(Stream);
                String response = reader.ReadLine();
                return response;
            }
        }
    }
}
