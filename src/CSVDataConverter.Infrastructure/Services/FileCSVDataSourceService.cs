using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVDataConverter.Core.Interfaces;


namespace CSVDataConverter.Infrastructure.Services
{
    public class FileCSVDataSourceService : ICSVDataSourceService
    {
        public string[] GetCSVDataAsStringArray()
        {
            Console.WriteLine("Please enter file path:");
            var filepath = Console.ReadLine();
            var data = File.ReadAllLines(filepath);
            return data;
        }
    }
}
