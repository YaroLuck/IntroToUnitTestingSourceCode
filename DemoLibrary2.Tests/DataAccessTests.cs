using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DemoLibrary;
using DemoLibrary.Models;

namespace DemoLibrary2.Tests
{
    public class DataAccessTests
    {
        [Fact]
        public void AddPersonToPeopleList_ShouldWork()
        {
            PersonModel newPerson = new PersonModel { FirstName = "Tim", LastName = "Corey" };
            List<PersonModel> people = new List<PersonModel>();

            DataAccess.AddPersonToPeopleList(people, newPerson);

            Assert.True(people.Count == 1);
            Assert.Contains<PersonModel>(newPerson, people);
        }

        [Theory]
        [InlineData("Tim", "", "LastName")]
        [InlineData("", "Corey", "FirstName")]
        public void AddPersonToPeopleList_ShouldFail(string firstName, string lastName, string param)
        {
            PersonModel newPerson = new PersonModel { FirstName = firstName, LastName = lastName };
            List<PersonModel> people = new List<PersonModel>();

            Assert.Throws<ArgumentException>(param, () => DataAccess.AddPersonToPeopleList(people, newPerson));
        }

        [Fact]
        public void ConvertModelsToCSV_ShouldConvert()
        {
            //Arrange
            PersonModel newPerson = new PersonModel { FirstName = "Tim", LastName = "Corey" };
            List<PersonModel> people = new List<PersonModel>();
            people.Add(newPerson);

            //Act
            List<string> output = DataAccess.ConvertModelsToCSV(people);
            //Assert
            Assert.Contains("Tim,Corey", output);
            Assert.True(output.Count == 1);
        }

        [Theory]
        [InlineData("Tim", "Corey", "Tim,Corey")]
        [InlineData("Jim", "Bim", "Jim,Bim")]
        public void SplitStringToList_ShouldSplit(string firstName, string lastName, string dataInContent)
        {
            //Arrange
            List<PersonModel> output = new List<PersonModel>();
            PersonModel person = new PersonModel();
            string[] content = new string[1];

            //Act
            content[0] = dataInContent;
            DataAccess.SplitStringToList(output, content);
            person = output.FirstOrDefault();
            //Assert
            Assert.True(person.FirstName == firstName);
            Assert.True(person.LastName == lastName);
            Assert.True(person.FullName == $"{firstName} {lastName}");
        }

        [Fact]
        public void SplitStringToList_ShouldWork()
        {
            //Arrange
            List<PersonModel> output = new List<PersonModel>();
            string[] content = new string[2];

            //Act
            content[0] = "Tim,Corey";
            content[1] = "Jim,Bim";
            DataAccess.SplitStringToList(output, content);

            //Assert
            Assert.True(output.Count == 2);
        }
    }
}
