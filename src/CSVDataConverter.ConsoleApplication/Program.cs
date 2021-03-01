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
                .AddSingleton<IOutputFormatSelectionService, UserOutputFormatSelectionService>()
                .BuildServiceProvider();

            var data = serviceProvider.GetService<ICSVDataSourceService>().GetCSVDataAsStringArray();
            if (data is null) return;

            Expando_CsvDataFormatConverter csvConverter = new Expando_CsvDataFormatConverter();
            var expandoList = csvConverter.ConvertToExpando(data);

            var dataConverter = serviceProvider.GetService<IOutputFormatSelectionService>().GetSelectedOutputFormat();
            var output = dataConverter.ConvertFromExpando(expandoList);
            Console.WriteLine(output);
        }
    }
}
