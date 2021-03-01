using CSVDataConverter.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDataConverter.Infrastructure.Services
{
    public class UserOutputFormatSelectionService : IOutputFormatSelectionService
    {
        public IDataFormatConverter GetSelectedOutputFormat()
        {
            Console.WriteLine("Please select output format:\r\nPress 1 for Xml Output\r\nPress 2 for Json Output");
            var userFormatSelection = Console.ReadLine();

            switch (userFormatSelection)
            {
                case "1":
                    return new Expando_XmlDataFormatConverter();

                case "2":
                    return new Expando_JsonDataFormatConverter();

                default:
                    Console.WriteLine("Invalid Selection");
                    GetSelectedOutputFormat();
                    break;
            }

            return null;
        }
    }
}
