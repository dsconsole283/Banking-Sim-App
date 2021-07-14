using BankingLibrary;
using System;
using Xunit;

namespace BankLibrary.Tests
{
    public class ClientModelTests
    {
        [Theory]
        [InlineData("afdv2334534", "xxxxxxxxxxx")]
        [InlineData("afd", "xxx")]
        [InlineData("afdv2334534&*@#$%^%#^", "xxxxxxxxxxxxxxxxxxxxx")]
        [InlineData("2334534", "xxxxxxx")]
        public void PassWordViewShouldReturnExpectedResults(string password, string expected)
        {
            //Arrange
            ClientModel client = new ClientModel();

            //Act
            client.Password = password;
            string actual = client.Password;
            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
