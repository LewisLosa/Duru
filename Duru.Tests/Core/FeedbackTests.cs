using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duru.Core.Models;

namespace Duru.Tests.Core
{
    public class FeedbackTests
    {
        [Fact]
        public void Feedback_Properties_SetAndGet_Correctly()
        {
            // Arrange
            var feedback = new Feedback
            {
                FeedbackId = 1,
                ReservationId = 2,
                Rating = 5,
                Comment = "Excellent service!",
                CreatedAt = new DateTime(2023, 2, 15)
            };

            // Assert
            Assert.Equal(1, feedback.FeedbackId);
            Assert.Equal(2, feedback.ReservationId);
            Assert.Equal(5, feedback.Rating);
            Assert.Equal("Excellent service!", feedback.Comment);
            Assert.Equal(new DateTime(2023, 2, 15), feedback.CreatedAt);
        }

        [Fact]
        public void Feedback_DefaultValues_AreCorrect()
        {
            // Arrange
            var feedback = new Feedback();

            // Assert
            Assert.Equal(0, feedback.FeedbackId);
            Assert.Equal(0, feedback.ReservationId);
            Assert.Equal(0, feedback.Rating);
            Assert.Null(feedback.Comment);
            Assert.Equal(default, feedback.CreatedAt);
        }
    }
}
