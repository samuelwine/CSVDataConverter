using CSVDataConverter.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSVDataConverter.UnitTests
{
    public class Expando_CsvDataFormatConverter_ConvertToExpando
    {
        [Fact]
        public void GroupsHeadingsByUnderscore()
        {
            //Arrange
            var demoStringData = "Name,Surname,Address_Line1,Address_Line2" + Environment.NewLine + "Dave,Jenkins,1 Hall Road,Bradford";
            var dataFormatConverter = new Expando_CsvDataFormatConverter();

            //Act
            var expandoDataObject = dataFormatConverter.ConvertToExpando(demoStringData);

            //Assert
            int count = ((IDictionary<string, object>)expandoDataObject.First()).Count;
            Assert.Equal(3, count);
        }

        [Fact]
        public void CreatesMultipleGroups()
        {
            //Arrange
            var demoStringData = "Name_FirstName,Name_Surname,Address_Line1,Address_Line2" + Environment.NewLine + "Dave,Jenkins,1 Hall Road,Bradford";
            var dataFormatConverter = new Expando_CsvDataFormatConverter();

            //Act
            var expandoDataObject = dataFormatConverter.ConvertToExpando(demoStringData);

            //Assert
            int count = ((IDictionary<string, object>)expandoDataObject.First()).Count;
            Assert.Equal(2, count);
        }
    }
}
