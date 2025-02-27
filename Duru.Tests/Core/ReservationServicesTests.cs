using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duru.Core.Models;

namespace Duru.Tests.Core
{
    public class ReservationServicesTests
    {
        [Fact]
        public void ReservationServices_Properties_SetAndGet_Correctly()
        {
            // Arrange
            var reservationService = new ReservationService
            {
                ReservationServiceId = 1,
                ReservationId = 301,
                ServiceId = 10,
                Quantity = 2,
                TotalPrice = 100.00
            };

            // Assert
            Assert.Equal(1, reservationService.ReservationServiceId);
            Assert.Equal(301, reservationService.ReservationId);
            Assert.Equal(10, reservationService.ServiceId);
            Assert.Equal(2, reservationService.Quantity);
            Assert.Equal(100.00, reservationService.TotalPrice);
        }

        [Fact]
        public void ReservationServices_DefaultValues_AreCorrect()
        {
            // Arrange
            var reservationService = new ReservationService();

            // Assert
            Assert.Equal(0, reservationService.ReservationServiceId);
            Assert.Equal(0, reservationService.ReservationId);
            Assert.Equal(0, reservationService.ServiceId);
            Assert.Equal(0, reservationService.Quantity);
            Assert.Equal(0, reservationService.TotalPrice);
        }
    }
}
