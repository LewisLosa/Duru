using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Duru.Core.Models;

namespace Duru.Tests.Core
{
    public class MaintenanceRequestsTests
    {
        [Fact]
        public void MaintenanceRequests_Properties_SetAndGet_Correctly()
        {
            // Arrange
            var request = new MaintenanceRequest
            {
                MaintenanceRequestId = 1,
                RoomId = 101,
                EmployeeId = 5,
                Description = "Broken lamp",
                Status = "Pending",
                CreatedAt = new DateTime(2023, 4, 5),
                UpdatedAt = new DateTime(2023, 4, 6)
            };

            // Assert
            Assert.Equal(1, request.MaintenanceRequestId);
            Assert.Equal(101, request.RoomId);
            Assert.Equal(5, request.EmployeeId);
            Assert.Equal("Broken lamp", request.Description);
            Assert.Equal("Pending", request.Status);
            Assert.Equal(new DateTime(2023, 4, 5), request.CreatedAt);
            Assert.Equal(new DateTime(2023, 4, 6), request.UpdatedAt);
        }

        [Fact]
        public void MaintenanceRequests_DefaultValues_AreCorrect()
        {
            // Arrange
            var request = new MaintenanceRequest();

            // Assert
            Assert.Equal(0, request.MaintenanceRequestId);
            Assert.Equal(0, request.RoomId);
            Assert.Equal(0, request.EmployeeId);
            Assert.Null(request.Description);
            Assert.Equal(string.Empty, request.Status);
            Assert.Equal(default, request.CreatedAt);
            Assert.Equal(default, request.UpdatedAt);
        }
    }
}
