using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVDataConverter.Core.Interfaces;


namespace CSVDataConverter.Infrastructure.Services
{
    public class InputFormatSelectionService : IInputFormatSelectionService
    {
        public IDataFormatConverter GetSelectedInputFormat()
        {
            Console.WriteLine("Please select input format:\r\nPress 1 for Csv");
            var userFormatSelection = Console.ReadLine();

            switch (userFormatSelection)
            {
                case "1":
                    return new Expando_CsvDataFormatConverter();

                default:
                    Console.WriteLine("Invalid Selection");
                    GetSelectedInputFormat();
                    break;
            }

            return null;
        }
    }
}
