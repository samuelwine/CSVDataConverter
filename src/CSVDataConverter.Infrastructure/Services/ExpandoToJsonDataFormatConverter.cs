using CSVDataConverter.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CSVDataConverter.Infrastructure.Services
{
    public class ExpandoToJsonDataFormatConverter : IDataFormatConverter
    {
        public string ConvertFromExpando(List<ExpandoObject> input)
        {
            return JsonSerializer.Serialize(input);
        }

        public List<ExpandoObject> ConvertToExpando(string[] input)
        {
            throw new NotImplementedException();
        }
    }
}
