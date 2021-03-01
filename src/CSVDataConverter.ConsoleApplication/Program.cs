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

            ExpandoToCsvDataFormatConverter csvConverter = new ExpandoToCsvDataFormatConverter();
            var expandoList = csvConverter.ConvertToExpando(data);

            ExpandoToXmlDataFormatConverter xmlConverter = new ExpandoToXmlDataFormatConverter();
            var xmlOutput = xmlConverter.ConvertFromExpando(expandoList);
            Console.WriteLine(xmlOutput);

            ExpandoToJsonDataFormatConverter jsonConverter = new ExpandoToJsonDataFormatConverter();
            var jsonOutput = jsonConverter.ConvertFromExpando(expandoList);
            Console.WriteLine(jsonOutput);
        }
    }
}
