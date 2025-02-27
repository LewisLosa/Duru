using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duru.Core.Models;

namespace Duru.Tests.Core
{
    public class ReservationsTests
    {
        [Fact]
        public void Reservations_Properties_SetAndGet_Correctly()
        {
            // Arrange
            var reservation = new Reservation
            {
                ReservationId = 1,
                GuestId = 15,
                RoomId = 105,
                CheckIn = new DateTime(2023, 6, 10),
                CheckOut = new DateTime(2023, 6, 15),
                Status = "Confirmed",
                CreatedAt = new DateTime(2023, 5, 25),
                UpdatedAt = new DateTime(2023, 5, 26)
            };

            // Assert
            Assert.Equal(1, reservation.ReservationId);
            Assert.Equal(15, reservation.GuestId);
            Assert.Equal(105, reservation.RoomId);
            Assert.Equal(new DateTime(2023, 6, 10), reservation.CheckIn);
            Assert.Equal(new DateTime(2023, 6, 15), reservation.CheckOut);
            Assert.Equal("Confirmed", reservation.Status);
            Assert.Equal(new DateTime(2023, 5, 25), reservation.CreatedAt);
            Assert.Equal(new DateTime(2023, 5, 26), reservation.UpdatedAt);
        }

        [Fact]
        public void Reservations_DefaultValues_AreCorrect()
        {
            // Arrange
            var reservation = new Reservation();

            // Assert
            Assert.Equal(0, reservation.ReservationId);
            Assert.Equal(0, reservation.GuestId);
            Assert.Equal(0, reservation.RoomId);
            Assert.Equal(default, reservation.CheckIn);
            Assert.Equal(default, reservation.CheckOut);
            Assert.Equal(string.Empty, reservation.Status);
            Assert.Equal(default, reservation.CreatedAt);
            Assert.Equal(default, reservation.UpdatedAt);
        }

        [Fact]
        public void Reservations_StayDurationCalculation_IsCorrect()
        {
            // Arrange
            var reservation = new Reservation
            {
                CheckIn = new DateTime(2023, 6, 10),
                CheckOut = new DateTime(2023, 6, 15)
            };

            // Act
            var stayDuration = (reservation.CheckOut - reservation.CheckIn).TotalDays;

            // Assert
            Assert.Equal(5, stayDuration);
        }
    }
}
