using CSVDataConverter.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSVDataConverter.UnitTests
{
    public class Expando_CsvDataFormatConverter_ConvertToExpando
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void GroupsHeadings(string input, int expected)
        {
            //Arrange
            var demoStringData = input;
            var dataFormatConverter = new Expando_CsvDataFormatConverter();

            //Act
            var expandoDataObject = dataFormatConverter.ConvertToExpando(demoStringData);

            //Assert
            int count = ((IDictionary<string, object>)expandoDataObject.First()).Count;
            Assert.Equal(expected, count);
        }

        public static IEnumerable<object[]> Data = new List<object[]>
        {
            new object[] { "Name,Surname,Address_Line1,Address_Line2" + Environment.NewLine + "Dave,Jenkins,1 Hall Road,Bradford", 3 },
            new object[] { "Name_FirstName,Name_Surname,Address_Line1,Address_Line2" + Environment.NewLine + "Dave,Jenkins,1 Hall Road,Bradford", 2 },
        };
    }
}
