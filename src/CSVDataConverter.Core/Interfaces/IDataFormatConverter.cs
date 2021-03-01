using System.Collections.Generic;
using System.Dynamic;

namespace CSVDataConverter.Core.Interfaces
{
    public interface IDataFormatConverter
    {
        string ConvertFromExpando(List<ExpandoObject> input);
        List<ExpandoObject> ConvertToExpando(string input);
    }
}
