﻿using CSVDataConverter.Core.Interfaces;
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

            ExpandoToXMLDataFormatConverter xmlConverter = new ExpandoToXMLDataFormatConverter();
            var xmlOutput = xmlConverter.ConvertFromExpando(list);
            Console.WriteLine(xmlOutput);

            ExpandoToJsonDataFormatConverter jsonConverter = new ExpandoToJsonDataFormatConverter();
            var jsonOutput = jsonConverter.ConvertFromExpando(list);
            Console.WriteLine(jsonOutput);
        }
    }
}
