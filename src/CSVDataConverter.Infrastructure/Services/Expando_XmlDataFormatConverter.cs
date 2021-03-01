using CSVDataConverter.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Xml.Linq;

namespace CSVDataConverter.Infrastructure.Services
{
    public class Expando_XmlDataFormatConverter : IDataFormatConverter
    {
        public string ConvertFromExpando(List<ExpandoObject> input)
        {
            XElement finalXmlString = new XElement("Root");
            XElement interimXmlString;

            foreach (var dataEntryObject in input)
            {
                foreach (var dataEntry in dataEntryObject as IDictionary<string, object>)
                {
                    if (dataEntry.Value.GetType() == typeof(Dictionary<string, object>))
                    {
                        interimXmlString = new XElement(dataEntry.Key);
                        foreach (var subDataEntry in dataEntry.Value as Dictionary<string, object>)
                        {
                            interimXmlString.Add(new XElement(subDataEntry.Key, subDataEntry.Value));
                        }
                    }
                    else
                    {
                        interimXmlString = new XElement(dataEntry.Key, dataEntry.Value);
                    }

                    finalXmlString.Add(interimXmlString);
                }
            }

            return finalXmlString.ToString();
        }

        public List<ExpandoObject> ConvertToExpando(string input)
        {
            throw new NotImplementedException();
        }
    }
}
