using System;
using System.Xml;
using KafkaNet;
using KafkaNet.Protocol;
using KafkaNet.Model;
using System.Collections.Generic;

namespace ReadPDML
{
    class Program
    {
        static void Main(string[] args)
        {
            string topic = "s1ap";
            XmlDocument doc = new XmlDocument();
            Message msg;
            doc.Load(@"D:\USIM_2018_08_21.pdml");
            Uri uri = new Uri("http://localhost:9092");
            var options = new KafkaOptions(uri);
            var router = new BrokerRouter(options);
            var client = new Producer(router);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                foreach (XmlNode node1 in node.ChildNodes)
                {
                    if(node1.InnerXml.Contains("frame.interface_id"))
                        foreach (XmlNode node2 in node1.Attributes)
                        {
                            if (node2.InnerText.Contains("Frame"))
                            {
                                string text = node2.InnerText.Split(':')[0];
                                msg = new Message(text);
                                client.SendMessageAsync(topic, new List<Message> { msg }).Wait();
                                Console.Write(text +" \n");
                            }
                        }
                    //if (node1.InnerXml.Contains("nas_eps.nas_msg_emm_type"))
                    //{
                    //    foreach (XmlNode node2 in node1.ChildNodes)
                    //    {
                    //        if (node2.InnerXml.Contains("NAS EPS Mobility Management Message Type"))
                    //        {
                    //            if (node2.HasChildNodes)
                    //            {
                    //                foreach (XmlNode n in node2)
                    //                {
                    //                    string text = n.InnerXml.Split(':')[1];
                    //                    Console.Write(text + " \t");
                    //                }
                    //            }
                    //            else
                    //            {
                    //                string text = node2.InnerXml.Split(':')[1];
                    //                Console.Write(text + " \t");
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
