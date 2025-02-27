using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duru.Core.Models;

namespace Duru.Tests.Core
{
    public class EmployeesTests
    {
        [Fact]
        public void Employee_Properties_SetAndGet_Correctly()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeId = 1,
                FirstName = "Eyüp",
                LastName = "Şengöz",
                Position = "Developer",
                Email = "eyupsengoz@losa.dev",
                Phone = "1234567890",
                HireDate = new DateTime(2023, 1, 15),
                Status = "Active",
                CreatedAt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
            };

            // Assert
            Assert.Equal(1, employee.EmployeeId);
            Assert.Equal("Eyüp", employee.FirstName);
            Assert.Equal("Şengöz", employee.LastName);
            Assert.Equal("Developer", employee.Position);
            Assert.Equal("eyupsengoz@losa.dev", employee.Email);
            Assert.Equal("1234567890", employee.Phone);
            Assert.Equal(new DateTime(2023, 1, 15), employee.HireDate);
            Assert.Equal("Active", employee.Status);
            Assert.Equal(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), employee.CreatedAt);
        }

        [Fact]
        public void Employee_DefaultValues_AreCorrect()
        {
            // Arrange
            var employee = new Employee();

            // Assert
            Assert.Equal(0, employee.EmployeeId);
            Assert.Equal(string.Empty, employee.FirstName);
            Assert.Equal(string.Empty, employee.LastName);
            Assert.Null(employee.Position);
            Assert.Null(employee.Email);
            Assert.Null(employee.Phone);
            Assert.Null(employee.HireDate);
            Assert.Equal(string.Empty, employee.Status);
            Assert.Equal(default, employee.CreatedAt);
        }
    }

}
