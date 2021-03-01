using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVDataConverter.Core.Interfaces
{
    public interface IDataFormatConverter
    {
        string ConvertFromExpando(List<ExpandoObject> input);
        List<ExpandoObject> ConvertToExpando(string[] input);
    }
}
