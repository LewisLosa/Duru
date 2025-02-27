using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duru.Core.Models;

namespace Duru.Tests.Core
{
    public class ServicesTests
    {
        [Fact]
        public void Services_Properties_SetAndGet_Correctly()
        {
            // Arrange
            var service = new Service
            {
                ServiceId = 1,
                Name = "Room Cleaning",
                Description = "Daily room cleaning service",
                Price = 25.00,
                CreatedAt = new DateTime(2023, 7, 15)
            };

            // Assert
            Assert.Equal(1, service.ServiceId);
            Assert.Equal("Room Cleaning", service.Name);
            Assert.Equal("Daily room cleaning service", service.Description);
            Assert.Equal(25.00, service.Price);
            Assert.Equal(new DateTime(2023, 7, 15), service.CreatedAt);
        }

        [Fact]
        public void Services_DefaultValues_AreCorrect()
        {
            // Arrange
            var service = new Service();

            // Assert
            Assert.Equal(0, service.ServiceId);
            Assert.Equal(string.Empty, service.Name);
            Assert.Null(service.Description);
            Assert.Null(service.Price);
            Assert.Equal(default, service.CreatedAt);
        }
    }

}
