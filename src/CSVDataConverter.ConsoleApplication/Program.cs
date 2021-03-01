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
            ServiceProvider serviceProvider = ConfigureServices();

            var inputConverter = serviceProvider.GetService<IInputFormatSelectionService>().GetSelectedInputFormat();
            if (inputConverter is null) return;

            var data = serviceProvider.GetService<IDataSourceService>().GetDataAsString();
            if (data is null) return;

            var expandoObjectsCollection = inputConverter.ConvertToExpando(data);

            var outputConverter = serviceProvider.GetService<IOutputFormatSelectionService>().GetSelectedOutputFormat();
            if (outputConverter is null) return;

            var output = outputConverter.ConvertFromExpando(expandoObjectsCollection);
            Console.WriteLine(output);
        }

        private static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                            .AddSingleton<IDataSourceService, FileDataSourceService>()
                            .AddSingleton<IOutputFormatSelectionService, OutputFormatSelectionService>()
                            .AddSingleton<IInputFormatSelectionService, InputFormatSelectionService>()
                            .BuildServiceProvider();
        }
    }
}
