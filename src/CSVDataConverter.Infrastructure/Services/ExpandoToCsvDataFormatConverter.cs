using CSVDataConverter.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDataConverter.Infrastructure.Services
{
    public class ExpandoToCsvDataFormatConverter : IDataFormatConverter
    {
        public string ConvertFromExpando(List<ExpandoObject> input)
        {
            throw new NotImplementedException();
        }

        public List<ExpandoObject> ConvertToExpando(string[] input)
        {
            string[] propertyNames = input.Take(1).Single().Split(",");
            var dataLines = input.Skip(1);
            var list = new List<ExpandoObject>();
            foreach (var dataline in dataLines)
            {
                string[] bits = dataline.Split(",");
                dynamic interimObject = new ExpandoObject();
                for (int i = 0; i < bits.Length; i++)
                {
                    if (propertyNames[i].Contains('_'))
                    {
                        var propertyNameParts = propertyNames[i].Split("_");
                        IDictionary<string, object> value;

                        if (!((IDictionary<string, object>)interimObject).ContainsKey(propertyNameParts[0]))
                        {
                            value = new Dictionary<string, object>();
                            value.Add(propertyNameParts[1], bits[i]);
                            ((IDictionary<string, object>)interimObject)[propertyNameParts[0]] = value;
                        }
                        else
                        {
                            ((Dictionary<string, object>)((IDictionary<string, object>)interimObject)[propertyNameParts[0]]).Add(propertyNameParts[1], bits[i]);
                        }
                    }
                    else
                    {
                        ((IDictionary<string, object>)interimObject)[propertyNames[i]] = bits[i];
                    }
                }
                list.Add(interimObject);
            }
            return list;
        }
    }
}
