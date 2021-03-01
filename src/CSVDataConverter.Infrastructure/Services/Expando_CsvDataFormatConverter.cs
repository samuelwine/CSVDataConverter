﻿using CSVDataConverter.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDataConverter.Infrastructure.Services
{
    public class Expando_CsvDataFormatConverter : IDataFormatConverter
    {
        public string ConvertFromExpando(List<ExpandoObject> input)
        {
            throw new NotImplementedException();
        }

        public List<ExpandoObject> ConvertToExpando(string[] input)
        {
            var expandoList = new List<ExpandoObject>(); 

            string[] headings = input.Take(1).Single().Split(",");

            var data = input.Skip(1);
            foreach (var dataEntry in data) 
            {
                dynamic dataEntryObject = new ExpandoObject();

                string[] dataEntryParts = dataEntry.Split(",");  
                for (int i = 0; i < dataEntryParts.Length; i++)
                {
                    if (headings[i].Contains('_'))
                    {
                        var propertyNameParts = headings[i].Split("_");
                        IDictionary<string, object> value;

                        if (!((IDictionary<string, object>)dataEntryObject).ContainsKey(propertyNameParts[0]))
                        {
                            value = new Dictionary<string, object>();
                            value.Add(propertyNameParts[1], dataEntryParts[i]);
                            ((IDictionary<string, object>)dataEntryObject)[propertyNameParts[0]] = value;
                        }
                        else
                        {
                            ((Dictionary<string, object>)((IDictionary<string, object>)dataEntryObject)[propertyNameParts[0]]).Add(propertyNameParts[1], dataEntryParts[i]);
                        }
                    }
                    else
                    {
                        ((IDictionary<string, object>)dataEntryObject)[headings[i]] = dataEntryParts[i];
                    }
                }

                expandoList.Add(dataEntryObject);
            }

            return expandoList;
        }
    }
}
