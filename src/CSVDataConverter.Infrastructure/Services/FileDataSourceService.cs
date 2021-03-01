using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVDataConverter.Core.Interfaces;


namespace CSVDataConverter.Infrastructure.Services
{
    public class FileDataSourceService : IDataSourceService
    {
        public string GetDataAsString()
        {
            Console.WriteLine("Please enter file path:");
            var filepath = Console.ReadLine();
            string data = null;

            try
            {
                data = File.ReadAllText(filepath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"The system has been unable to process the file. System Error Message: {e.Message}");
            }

            return data;
        }
    }
}
