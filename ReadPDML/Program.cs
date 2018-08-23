using System;
using System.Xml;

namespace ReadPDML
{
    class Program
    {
        static void Main(string[] args)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(@"D:\USIM_2018_08_21.pdml");
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                foreach (XmlNode node1 in node.ChildNodes)
                {
                    if(node1.InnerXml.Contains("frame.interface_id"))
                    foreach (XmlNode node2 in node1.Attributes)
                    {
                        if (node2.InnerText.Contains("Frame"))
                        {
                            string text = node2.InnerText.Split(',')[0];
                            Console.WriteLine(text);
                        }
                    }
                }
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
