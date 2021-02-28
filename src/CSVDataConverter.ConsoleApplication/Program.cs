using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json;
using System.Xml.Linq;

namespace CSVDataConverter.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string csvString = "name,address";
            string[] stringArray = csvString.Split(",");
            dynamic interimObject = new ExpandoObject();
            ((IDictionary<string, object>)interimObject)[stringArray[0]] = "Dave";
            ((IDictionary<string, object>)interimObject)[stringArray[1]] = "Tewkesbury";

            //JSON serialization
            var jsonOutput = JsonSerializer.Serialize(interimObject);
            Console.WriteLine(jsonOutput);


            //XML serialization
            foreach (var item in interimObject as IDictionary<string, object>)
            {
                var xmlString = new XElement(item.Key, item.Value);
                Console.WriteLine(xmlString);
            }
        }
    }
}
