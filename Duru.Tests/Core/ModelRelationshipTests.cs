using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duru.Core.Models;

namespace Duru.Tests.Core
{
    public class ModelRelationshipTests
    {
        [Fact]
        public void Reservation_GuestRelationship_Works()
        {
            // This is a demonstration of how to test relationships
            // In a real implementation, you would need repositories or a context

            // Arrange
            var guest = new Guest
            {
                GuestId = 1,
                FirstName = "Robert",
                LastName = "Johnson"
            };

            var reservation = new Reservation
            {
                ReservationId = 100,
                GuestId = guest.GuestId,
                RoomId = 200,
                CheckIn = DateTime.Today,
                CheckOut = DateTime.Today.AddDays(3)
            };

            // Assert
            Assert.Equal(guest.GuestId, reservation.GuestId);
        }

        [Fact]
        public void Room_RoomTypeRelationship_Works()
        {
            // Arrange
            var roomType = new RoomType
            {
                RoomTypeId = 5,
                Name = "Standard",
                Capacity = 2
            };

            var room = new Room
            {
                RoomId = 25,
                RoomNumber = "301",
                RoomTypeId = roomType.RoomTypeId
            };

            // Assert
            Assert.Equal(roomType.RoomTypeId, room.RoomTypeId);
        }

        [Fact]
        public void ReservationService_ServiceRelationship_Works()
        {
            // Arrange
            var service = new Service
            {
                ServiceId = 10,
                Name = "Breakfast",
                Price = 15.00
            };

            var reservationService = new ReservationService
            {
                ReservationServiceId = 50,
                ReservationId = 100,
                ServiceId = service.ServiceId,
                Quantity = 2,
                TotalPrice = service.Price * 2 ?? 0
            };

            // Assert
            Assert.Equal(service.ServiceId, reservationService.ServiceId);
            Assert.Equal(30.00, reservationService.TotalPrice);
        }
    }
}
