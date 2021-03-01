using CSVDataConverter.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json;

namespace CSVDataConverter.Infrastructure.Services
{
    public class Expando_JsonDataFormatConverter : IDataFormatConverter
    {
        public string ConvertFromExpando(List<ExpandoObject> input)
        {
            return JsonSerializer.Serialize(input);
        }

        public List<ExpandoObject> ConvertToExpando(string input)
        {
            throw new NotImplementedException();
        }
    }
}
