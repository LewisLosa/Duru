using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duru.Core.Models;

namespace Duru.Tests.Core
{
    public class RoomTypesTests
    {
        [Fact]
        public void RoomTypes_Properties_SetAndGet_Correctly()
        {
            // Arrange
            var roomType = new RoomType
            {
                RoomTypeId = 1,
                Name = "Deluxe Suite",
                Description = "Luxury room with ocean view",
                Capacity = 4,
                Price = 299.99
            };

            // Assert
            Assert.Equal(1, roomType.RoomTypeId);
            Assert.Equal("Deluxe Suite", roomType.Name);
            Assert.Equal("Luxury room with ocean view", roomType.Description);
            Assert.Equal(4, roomType.Capacity);
            Assert.Equal(299.99, roomType.Price);
        }

        [Fact]
        public void RoomTypes_DefaultValues_AreCorrect()
        {
            // Arrange
            var roomType = new RoomType();

            // Assert
            Assert.Equal(0, roomType.RoomTypeId);
            Assert.Equal(string.Empty, roomType.Name);
            Assert.Null(roomType.Description);
            Assert.Null(roomType.Capacity);
            Assert.Null(roomType.Price);
        }
    }
}
