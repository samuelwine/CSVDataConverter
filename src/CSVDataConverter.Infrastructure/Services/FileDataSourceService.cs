using System;
using System.IO;
using CSVDataConverter.Core.Interfaces;


namespace CSVDataConverter.Infrastructure.Services
{
    public class FileDataSourceService : IDataSourceService
    {
        public string GetDataAsString()
        {
            Console.WriteLine("Please enter file path:");
            //var filepath = Console.ReadLine();
            var filepath = "..\\..\\..\\csvinput.csv";
            Console.WriteLine($"using filepath {filepath}");
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
