using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duru.Core.Models;

namespace Duru.Tests.Core
{
    public class PaymentsTests
    {
        [Fact]
        public void Payments_Properties_SetAndGet_Correctly()
        {
            // Arrange
            var payment = new Payment
            {
                PaymentId = 1,
                ReservationId = 201,
                Amount = 150.50,
                PaymentDate = new DateTime(2023, 5, 20),
                Method = "Credit Card",
                Status = "Completed"
            };

            // Assert
            Assert.Equal(1, payment.PaymentId);
            Assert.Equal(201, payment.ReservationId);
            Assert.Equal(150.50, payment.Amount);
            Assert.Equal(new DateTime(2023, 5, 20), payment.PaymentDate);
            Assert.Equal("Credit Card", payment.Method);
            Assert.Equal("Completed", payment.Status);
        }

        [Fact]
        public void Payments_DefaultValues_AreCorrect()
        {
            // Arrange
            var payment = new Payment();

            // Assert
            Assert.Equal(0, payment.PaymentId);
            Assert.Equal(0, payment.ReservationId);
            Assert.Equal(0, payment.Amount);
            Assert.Equal(default, payment.PaymentDate);
            Assert.Equal(string.Empty, payment.Method);
            Assert.Equal(string.Empty, payment.Status);
        }
    }    
}
