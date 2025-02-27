using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duru.Core.Models;

namespace Duru.Tests.Core
{
    public class GuestsTests
    {
        [Fact]
        public void Guests_Properties_SetAndGet_Correctly()
        {
            // Arrange
            var guest = new Guest
            {
                GuestId = 1,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Phone = "9876543210",
                Address = "123 Main St, City",
                CreatedAt = new DateTime(2023, 3, 10)
            };

            // Assert
            Assert.Equal(1, guest.GuestId);
            Assert.Equal("Jane", guest.FirstName);
            Assert.Equal("Smith", guest.LastName);
            Assert.Equal("jane.smith@example.com", guest.Email);
            Assert.Equal("9876543210", guest.Phone);
            Assert.Equal("123 Main St, City", guest.Address);
            Assert.Equal(new DateTime(2023, 3, 10), guest.CreatedAt);
        }

        [Fact]
        public void Guests_DefaultValues_AreCorrect()
        {
            // Arrange
            var guest = new Guest();

            // Assert
            Assert.Equal(0, guest.GuestId);
            Assert.Equal(string.Empty, guest.FirstName);
            Assert.Equal(string.Empty, guest.LastName);
            Assert.Null(guest.Email);
            Assert.Null(guest.Phone);
            Assert.Null(guest.Address);
            Assert.Equal(default, guest.CreatedAt);
        }
    }
}
