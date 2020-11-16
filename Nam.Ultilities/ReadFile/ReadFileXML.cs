using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Nam.Ultilities.ReadFile
{
    public static class ReadFileXML<T>
    {
        public static List<T> getServerNodeXMLRangeValueDB(string fileName, string nodesXML = "SERVERNODE")
        {
            try
            {
                XmlDocument docXML = new XmlDocument();
                docXML.Load(fileName);
                XmlNodeList nodeList = docXML.GetElementsByTagName(nodesXML);
                List<T> obj = new List<T>();
                foreach (XmlNode node in nodeList)
                {
                    string json = JsonConvert.SerializeXmlNode(node, Newtonsoft.Json.Formatting.None);
                    T nodeobj = JsonConvert.DeserializeObject<T>(json.Replace("@", ""));
                    obj.Add(nodeobj);
                }
                return obj;
            }
            catch
            {
                return default(List<T>);
            }
        }

    }
}
