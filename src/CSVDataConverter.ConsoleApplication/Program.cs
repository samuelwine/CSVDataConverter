using CSVDataConverter.Core.Interfaces;
using CSVDataConverter.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
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

            var csvData = serviceProvider.GetService<ICSVDataSourceService>().GetCSVDataAsStringArray();

            string[] properties = csvData.Take(1).Single().Split(",");
            var dataLines = csvData.Skip(1);
            var list = new List<ExpandoObject>();
            foreach (var dataline in dataLines)
            {
                string[] bits = dataline.Split(",");
                dynamic interimObject = new ExpandoObject();
                for (int i = 0; i < bits.Length; i++)
                {
                    ((IDictionary<string, object>)interimObject)[properties[i]] = bits[i];
                }
                list.Add(interimObject);
            }

            //JSON serialization
            var jsonOutput = JsonSerializer.Serialize(list);
            Console.WriteLine(jsonOutput);


            //XML serialization
            foreach (var expando in list)
                foreach (var item in expando as IDictionary<string, object>)
                {
                    var xmlString = new XElement(item.Key, item.Value);
                    Console.WriteLine(xmlString);
                }
        }
    }
}
