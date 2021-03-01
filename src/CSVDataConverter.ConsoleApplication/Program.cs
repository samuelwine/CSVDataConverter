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
            ServiceProvider serviceProvider = ConfigureServices();

            var data = serviceProvider.GetService<ICSVDataSourceService>().GetCSVDataAsStringArray();
            if (data is null) return;

            var inputConverter = serviceProvider.GetService<IInputFormatSelectionService>().GetSelectedInputFormat();
            var expandoList = inputConverter.ConvertToExpando(data);

            var outputConverter = serviceProvider.GetService<IOutputFormatSelectionService>().GetSelectedOutputFormat();
            var output = outputConverter.ConvertFromExpando(expandoList);
            Console.WriteLine(output);
        }

        private static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                            .AddSingleton<ICSVDataSourceService, FileCSVDataSourceService>()
                            .AddSingleton<IOutputFormatSelectionService, OutputFormatSelectionService>()
                            .AddSingleton<IInputFormatSelectionService, InputFormatSelectionService>()
                            .BuildServiceProvider();
        }
    }
}
