using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duru.Core.Models;

namespace Duru.Tests.Core
{
    public class RoomsTests
    {
        [Fact]
        public void Rooms_Properties_SetAndGet_Correctly()
        {
            // Arrange
            var room = new Room
            {
                RoomId = 1,
                RoomNumber = "501",
                RoomTypeId = 2,
                Status = "Available",
                Floor = 5
            };

            // Assert
            Assert.Equal(1, room.RoomId);
            Assert.Equal("501", room.RoomNumber);
            Assert.Equal(2, room.RoomTypeId);
            Assert.Equal("Available", room.Status);
            Assert.Equal(5, room.Floor);
        }

        [Fact]
        public void Rooms_DefaultValues_AreCorrect()
        {
            // Arrange
            var room = new Room();

            // Assert
            Assert.Equal(0, room.RoomId);
            Assert.Equal(string.Empty, room.RoomNumber);
            Assert.Null(room.RoomTypeId);
            Assert.Null(room.Status);
            Assert.Null(room.Floor);
        }
    }
}
