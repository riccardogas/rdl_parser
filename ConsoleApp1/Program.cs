using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Data;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream("c:\\tmp\\myreport.rdlc", FileMode.Open))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fs);
                var nsmgr = new XmlNamespaceManager(doc.NameTable);
                nsmgr.AddNamespace("a", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition");
                nsmgr.AddNamespace("rd", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner");
                

                XmlNodeList nodes = doc.SelectNodes("/a:Report/a:DataSources/a:DataSource",nsmgr);

                foreach (XmlNode node in nodes)
                {
                    var x = node.Attributes["Name"].Value;
                    var connpropnode = node.SelectNodes("a:ConnectionProperties", nsmgr);
                    foreach (XmlNode item in connpropnode)
                    {
                        var DataProviderNode = item.SelectSingleNode("a:ConnectString", nsmgr).InnerText;
                        Console.WriteLine(DataProviderNode);
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
