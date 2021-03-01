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
            if (data is null) return;

            Expando_CsvDataFormatConverter csvConverter = new Expando_CsvDataFormatConverter();
            var expandoList = csvConverter.ConvertToExpando(data);

            Expando_XmlDataFormatConverter xmlConverter = new Expando_XmlDataFormatConverter();
            var xmlOutput = xmlConverter.ConvertFromExpando(expandoList);
            Console.WriteLine(xmlOutput);

            Expando_JsonDataFormatConverter jsonConverter = new Expando_JsonDataFormatConverter();
            var jsonOutput = jsonConverter.ConvertFromExpando(expandoList);
            Console.WriteLine(jsonOutput);
        }
    }
}
