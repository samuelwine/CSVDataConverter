using CSVDataConverter.Core.Interfaces;
using CSVDataConverter.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace CSVDataConverter.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ICSVDataSourceService, FileCSVDataSourceService>()
                .BuildServiceProvider();

            var data = serviceProvider.GetService<ICSVDataSourceService>().GetCSVDataAsStringArray();

            string[] propertyNames = data.Take(1).Single().Split(",");
            var dataLines = data.Skip(1);
            var list = new List<ExpandoObject>();
            foreach (var dataline in dataLines)
            {
                string[] bits = dataline.Split(",");
                dynamic interimObject = new ExpandoObject();
                for (int i = 0; i < bits.Length; i++)
                {
                    if (propertyNames[i].Contains('_'))
                    {
                        var propertyNameParts = propertyNames[i].Split("_");
                        IDictionary<string, object> value;

                        if (!((IDictionary<string, object>)interimObject).ContainsKey(propertyNameParts[0]))
                        {
                            value = new Dictionary<string, object>();
                            value.Add(propertyNameParts[1], bits[i]);
                            ((IDictionary<string, object>)interimObject)[propertyNameParts[0]] = value;
                        }
                        else
                        {
                            ((Dictionary<string, object>)((IDictionary<string, object>)interimObject)[propertyNameParts[0]]).Add(propertyNameParts[1], bits[i]);
                        }
                    }
                    else
                    {
                        ((IDictionary<string, object>)interimObject)[propertyNames[i]] = bits[i];
                    }
                }
                list.Add(interimObject);
            }

            //JSON serialization
            var jsonOutput = JsonSerializer.Serialize(list);
            Console.WriteLine(jsonOutput);


            //XML serialization
            XElement finalXmlString = new XElement("Root");
            XElement xmlString;
            
            foreach (var expando in list)
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
            Console.WriteLine(finalXmlString);
        }
    }
}
