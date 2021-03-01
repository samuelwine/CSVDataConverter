using CSVDataConverter.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CSVDataConverter.Infrastructure.Services
{
    public class ExpandoToXMLDataFormatConverter : IDataFormatConverter
    {
        public string ConvertFromExpando(List<ExpandoObject> input)
        {
            XElement finalXmlString = new XElement("Root");
            XElement xmlString;

            foreach (var expando in input)
            {
                foreach (var item in expando as IDictionary<string, object>)
                {
                    if (item.Value.GetType() == typeof(Dictionary<string, object>))
                    {
                        xmlString = new XElement(item.Key);
                        foreach (var keyee in item.Value as Dictionary<string, object>)
                        {
                            xmlString.Add(new XElement(keyee.Key, keyee.Value));
                        }
                    }
                    else
                    {
                        xmlString = new XElement(item.Key, item.Value);
                    }
                    finalXmlString.Add(xmlString);
                }
            }

            return finalXmlString.ToString();
        }

        public string ConvertToExpando(string input)
        {
            throw new NotImplementedException();
        }
    }
}
