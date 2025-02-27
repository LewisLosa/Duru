using System.Diagnostics;
using Duru.Data;
using Duru.Data.Entities;
using Duru.Data.Repositories;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace Duru.Tests
{
    public class RepositoryTests : IDisposable
    {
        private readonly string _dbPath;
        private readonly string _connectionString;
        private readonly SqliteDbContext _dbContext;
        private readonly RoomRepository _roomRepository;
        private readonly EmployeeRepository _employeeRepository;
        private readonly GuestRepository _guestRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly PaymentRepository _paymentRepository;
        private readonly FeedbackRepository _feedbackRepository;
        private readonly MaintenanceRequestRepository _maintenanceRequestRepository;
        private readonly ServiceRepository _serviceRepository;
        private readonly RoomTypeRepository _roomTypeRepository;

        public RepositoryTests()
        {
            Batteries.Init();

            _dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test_repositories.db");
            _connectionString = $"Data Source={_dbPath};Mode=ReadWriteCreate;Cache=Shared";
            _dbContext = new SqliteDbContext(_connectionString);

            // Initialize all repositories
            _roomRepository = new RoomRepository(_dbContext);
            _employeeRepository = new EmployeeRepository(_dbContext);
            _guestRepository = new GuestRepository(_dbContext);
            _reservationRepository = new ReservationRepository(_dbContext);
            _paymentRepository = new PaymentRepository(_dbContext);
            _feedbackRepository = new FeedbackRepository(_dbContext);
            _maintenanceRequestRepository = new MaintenanceRequestRepository(_dbContext);
            _serviceRepository = new ServiceRepository(_dbContext);
            _roomTypeRepository = new RoomTypeRepository(_dbContext);

            if (File.Exists(_dbPath))
            {
                try
                {
                    File.Delete(_dbPath);
                }
                catch (IOException ex)
                {
                    Debug.WriteLine($"[WARN] Could not delete database file: {ex.Message}");
                }
            }

            // Initialize database
            DatabaseInitializer.InitializeAsync(_dbContext).GetAwaiter().GetResult();
        }

        [Fact]
        public async Task GuestRepository_CrudOperations_ShouldSucceed()
        {
            // Arrange
            var guest = new Guest
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "1234567890",
                Address = "123 Main St",
                CreatedAt = DateTime.Now
            };

            Debug.WriteLine("=== GUEST REPOSITORY TEST ===");
            Debug.WriteLine($"Adding Guest: {guest.FirstName} {guest.LastName}, Email: {guest.Email}, Phone: {guest.Phone}");

            // Act - Insert
            var insertedId = await _guestRepository.InsertAsync(guest);

            // Assert - Insert
            Debug.WriteLine($"Inserted Guest ID: {insertedId}");
            Assert.True(insertedId > 0);

            // Act - Get by ID
            var retrievedGuest = await _guestRepository.GetByIdAsync(insertedId);

            // Assert - Get by ID
            Debug.WriteLine($"Retrieved Guest: {retrievedGuest.FirstName} {retrievedGuest.LastName}, Email: {retrievedGuest.Email}");
            Assert.NotNull(retrievedGuest);
            Assert.Equal("John", retrievedGuest.FirstName);
            Assert.Equal("Doe", retrievedGuest.LastName);

            // Act - Update
            retrievedGuest.FirstName = "Jane";
            retrievedGuest.Email = "jane.doe@example.com";
            Debug.WriteLine($"Updating Guest to: {retrievedGuest.FirstName} {retrievedGuest.LastName}, Email: {retrievedGuest.Email}");
            var updateResult = await _guestRepository.UpdateAsync(retrievedGuest);

            // Assert - Update
            Debug.WriteLine($"Update Result: {updateResult} row(s) affected");
            Assert.Equal(1, updateResult);

            // Act - Get all
            var allGuests = await _guestRepository.GetAllAsync();

            // Assert - Get all
            Debug.WriteLine($"Retrieved {allGuests.Count()} guests in total");
            Assert.NotEmpty(allGuests);

            // Act - Get updated
            var updatedGuest = await _guestRepository.GetByIdAsync(insertedId);

            // Assert - Get updated
            Debug.WriteLine($"Retrieved Updated Guest: {updatedGuest.FirstName} {updatedGuest.LastName}, Email: {updatedGuest.Email}");
            Assert.Equal("Jane", updatedGuest.FirstName);
            Assert.Equal("jane.doe@example.com", updatedGuest.Email);

            // Act - Delete
            Debug.WriteLine($"Deleting Guest with ID: {insertedId}");
            var deleteResult = await _guestRepository.DeleteAsync(insertedId);

            // Assert - Delete
            Debug.WriteLine($"Delete Result: {deleteResult} row(s) affected");
            Assert.Equal(1, deleteResult);

            // Act - Verify deletion
            var deletedGuest = await _guestRepository.GetByIdAsync(insertedId);

            // Assert - Verify deletion
            Debug.WriteLine($"Guest after deletion: {(deletedGuest == null ? "null (as expected)" : "still exists!")}");
            Assert.Null(deletedGuest);
        }

        [Fact]
        public async Task RoomTypeRepository_CrudOperations_ShouldSucceed()
        {
            // Arrange
            var roomType = new RoomType
            {
                Name = "Deluxe Suite",
                Description = "Luxury room with ocean view",
                Capacity = 2,
                Price = 199.99
            };

            Debug.WriteLine("=== ROOM TYPE REPOSITORY TEST ===");
            Debug.WriteLine($"Adding Room Type: {roomType.Name}, Capacity: {roomType.Capacity}, Price: ${roomType.Price}");

            // Act - Insert
            var insertedId = await _roomTypeRepository.InsertAsync(roomType);

            // Assert - Insert
            Debug.WriteLine($"Inserted Room Type ID: {insertedId}");
            Assert.True(insertedId > 0);

            // Act - Get by ID
            var retrievedRoomType = await _roomTypeRepository.GetByIdAsync(insertedId);

            // Assert - Get by ID
            Debug.WriteLine($"Retrieved Room Type: {retrievedRoomType.Name}, Price: ${retrievedRoomType.Price}");
            Assert.NotNull(retrievedRoomType);
            Assert.Equal("Deluxe Suite", retrievedRoomType.Name);
            Assert.Equal(199.99, retrievedRoomType.Price);

            // Act - Update
            retrievedRoomType.Name = "Executive Suite";
            retrievedRoomType.Price = 249.99;
            Debug.WriteLine($"Updating Room Type to: {retrievedRoomType.Name}, Price: ${retrievedRoomType.Price}");
            var updateResult = await _roomTypeRepository.UpdateAsync(retrievedRoomType);

            // Assert - Update
            Debug.WriteLine($"Update Result: {updateResult} row(s) affected");
            Assert.Equal(1, updateResult);

            // Act - Get updated
            var updatedRoomType = await _roomTypeRepository.GetByIdAsync(insertedId);

            // Assert - Get updated
            Debug.WriteLine($"Retrieved Updated Room Type: {updatedRoomType.Name}, Price: ${updatedRoomType.Price}");
            Assert.Equal("Executive Suite", updatedRoomType.Name);
            Assert.Equal(249.99, updatedRoomType.Price);

            // Act - Delete
            Debug.WriteLine($"Deleting Room Type with ID: {insertedId}");
            var deleteResult = await _roomTypeRepository.DeleteAsync(insertedId);

            // Assert - Delete
            Debug.WriteLine($"Delete Result: {deleteResult} row(s) affected");
            Assert.Equal(1, deleteResult);
        }

        [Fact]
        public async Task ServiceRepository_CrudOperations_ShouldSucceed()
        {
            // Arrange
            var service = new Service
            {
                Name = "Room Cleaning",
                Description = "Daily room cleaning service",
                Price = 25.0,
                CreatedAt = DateTime.Now
            };

            Debug.WriteLine("=== SERVICE REPOSITORY TEST ===");
            Debug.WriteLine($"Adding Service: {service.Name}, Price: ${service.Price}, Description: {service.Description}");

            // Act - Insert
            var insertedId = await _serviceRepository.InsertAsync(service);

            // Assert - Insert
            Debug.WriteLine($"Inserted Service ID: {insertedId}");
            Assert.True(insertedId > 0);

            // Act - Get by ID
            var retrievedService = await _serviceRepository.GetByIdAsync(insertedId);

            // Assert - Get by ID
            Debug.WriteLine($"Retrieved Service: {retrievedService.Name}, Price: ${retrievedService.Price}");
            Assert.NotNull(retrievedService);
            Assert.Equal("Room Cleaning", retrievedService.Name);

            // Act - Update
            retrievedService.Price = 30.0;
            Debug.WriteLine($"Updating Service Price to: ${retrievedService.Price}");
            var updateResult = await _serviceRepository.UpdateAsync(retrievedService);

            // Assert - Update
            Debug.WriteLine($"Update Result: {updateResult} row(s) affected");
            Assert.Equal(1, updateResult);

            // Act - Get all
            var allServices = await _serviceRepository.GetAllAsync();

            // Assert - Get all
            Debug.WriteLine($"Retrieved {allServices.Count()} services in total");
            Assert.NotEmpty(allServices);

            // Act - Delete
            Debug.WriteLine($"Deleting Service with ID: {insertedId}");
            var deleteResult = await _serviceRepository.DeleteAsync(insertedId);

            // Assert - Delete
            Debug.WriteLine($"Delete Result: {deleteResult} row(s) affected");
            Assert.Equal(1, deleteResult);
        }

        [Fact]
        public async Task ReservationRepository_CrudAndSpecialQueries_ShouldSucceed()
        {
            Debug.WriteLine("=== RESERVATION REPOSITORY TEST ===");

            // Arrange - Create guest and room first
            var guest = new Guest
            {
                FirstName = "Alice",
                LastName = "Smith",
                Email = "alice.smith@example.com",
                CreatedAt = DateTime.Now
            };
            Debug.WriteLine($"Adding Guest: {guest.FirstName} {guest.LastName}, Email: {guest.Email}");
            var guestId = await _guestRepository.InsertAsync(guest);
            Debug.WriteLine($"Inserted Guest ID: {guestId}");

            var room = new Room
            {
                RoomNumber = "501",
                Status = "Available",
                Floor = 5
            };
            Debug.WriteLine($"Adding Room: {room.RoomNumber}, Floor: {room.Floor}, Status: {room.Status}");
            var roomId = await _roomRepository.InsertAsync(room);
            Debug.WriteLine($"Inserted Room ID: {roomId}");

            var checkIn = DateTime.Now.AddDays(5);
            var checkOut = DateTime.Now.AddDays(10);
            var reservation = new Reservation
            {
                GuestId = guestId,
                RoomId = roomId,
                CheckIn = checkIn,
                CheckOut = checkOut,
                Status = "Confirmed",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            Debug.WriteLine($"Adding Reservation: Guest ID: {reservation.GuestId}, Room ID: {reservation.RoomId}");
            Debug.WriteLine($"Check-in: {reservation.CheckIn.ToShortDateString()}, Check-out: {reservation.CheckOut.ToShortDateString()}, Status: {reservation.Status}");

            // Act - Insert reservation
            var insertedId = await _reservationRepository.InsertAsync(reservation);

            // Assert - Insert
            Debug.WriteLine($"Inserted Reservation ID: {insertedId}");
            Assert.True(insertedId > 0);

            // Act - Get by ID
            var retrievedReservation = await _reservationRepository.GetByIdAsync(insertedId);

            // Assert - Get by ID
            Debug.WriteLine($"Retrieved Reservation: Guest ID: {retrievedReservation.GuestId}, Room ID: {retrievedReservation.RoomId}, Status: {retrievedReservation.Status}");
            Assert.NotNull(retrievedReservation);
            Assert.Equal(guestId, retrievedReservation.GuestId);
            Assert.Equal(roomId, retrievedReservation.RoomId);

            // Act - Get by guest ID
            var reservationsByGuest = await _reservationRepository.GetReservationsByGuestIdAsync(guestId);

            // Assert - Get by guest ID
            Debug.WriteLine($"Retrieved {reservationsByGuest.Count()} reservations for Guest ID: {guestId}");
            Assert.NotEmpty(reservationsByGuest);
            Assert.Contains(reservationsByGuest, r => r.Id == insertedId);

            // Act - Get by room ID
            var reservationsByRoom = await _reservationRepository.GetReservationsByRoomIdAsync(roomId);

            // Assert - Get by room ID
            Debug.WriteLine($"Retrieved {reservationsByRoom.Count()} reservations for Room ID: {roomId}");
            Assert.NotEmpty(reservationsByRoom);
            Assert.Contains(reservationsByRoom, r => r.Id == insertedId);

            // Act - Update
            retrievedReservation.Status = "CheckedIn";
            retrievedReservation.UpdatedAt = DateTime.Now;
            Debug.WriteLine($"Updating Reservation Status to: {retrievedReservation.Status}");
            var updateResult = await _reservationRepository.UpdateAsync(retrievedReservation);

            // Assert - Update
            Debug.WriteLine($"Update Result: {updateResult} row(s) affected");
            Assert.Equal(1, updateResult);

            // Act - Cleanup
            Debug.WriteLine("Cleaning up test data...");
            await _reservationRepository.DeleteAsync(insertedId);
            await _guestRepository.DeleteAsync(guestId);
            await _roomRepository.DeleteAsync(roomId);
        }

        [Fact]
        public async Task FeedbackRepository_CrudAndSpecialQueries_ShouldSucceed()
        {
            Debug.WriteLine("=== FEEDBACK REPOSITORY TEST ===");

            // Arrange - Create guest, room and reservation first
            var guest = new Guest
            {
                FirstName = "Robert",
                LastName = "Johnson",
                Email = "robert@example.com",
                CreatedAt = DateTime.Now
            };
            Debug.WriteLine($"Adding Guest: {guest.FirstName} {guest.LastName}, Email: {guest.Email}");
            var guestId = await _guestRepository.InsertAsync(guest);
            Debug.WriteLine($"Inserted Guest ID: {guestId}");

            var room = new Room
            {
                RoomNumber = "601",
                Status = "Available",
                Floor = 6
            };
            Debug.WriteLine($"Adding Room: {room.RoomNumber}, Floor: {room.Floor}, Status: {room.Status}");
            var roomId = await _roomRepository.InsertAsync(room);
            Debug.WriteLine($"Inserted Room ID: {roomId}");

            var checkIn = DateTime.Now.AddDays(-10);
            var checkOut = DateTime.Now.AddDays(-5);
            var reservation = new Reservation
            {
                GuestId = guestId,
                RoomId = roomId,
                CheckIn = checkIn,
                CheckOut = checkOut,
                Status = "Completed",
                CreatedAt = DateTime.Now.AddDays(-15),
                UpdatedAt = DateTime.Now.AddDays(-5)
            };
            Debug.WriteLine($"Adding Reservation: Guest ID: {reservation.GuestId}, Room ID: {reservation.RoomId}");
            Debug.WriteLine($"Check-in: {reservation.CheckIn.ToShortDateString()}, Check-out: {reservation.CheckOut.ToShortDateString()}, Status: {reservation.Status}");
            var reservationId = await _reservationRepository.InsertAsync(reservation);
            Debug.WriteLine($"Inserted Reservation ID: {reservationId}");

            var feedback = new Feedback
            {
                ReservationId = reservationId,
                Rating = 5,
                Comment = "Excellent stay, very comfortable",
                CreatedAt = DateTime.Now
            };
            Debug.WriteLine($"Adding Feedback: Reservation ID: {feedback.ReservationId}, Rating: {feedback.Rating}, Comment: \"{feedback.Comment}\"");

            // Act - Insert feedback
            var insertedId = await _feedbackRepository.InsertAsync(feedback);

            // Assert - Insert
            Debug.WriteLine($"Inserted Feedback ID: {insertedId}");
            Assert.True(insertedId > 0);

            // Act - Get by ID
            var retrievedFeedback = await _feedbackRepository.GetByIdAsync(insertedId);

            // Assert - Get by ID
            Debug.WriteLine($"Retrieved Feedback: Rating: {retrievedFeedback.Rating}, Comment: \"{retrievedFeedback.Comment}\"");
            Assert.NotNull(retrievedFeedback);
            Assert.Equal(5, retrievedFeedback.Rating);

            // Act - Get by reservation ID
            var feedbackByReservation = await _feedbackRepository.GetFeedbacksByReservationIdAsync(reservationId);

            // Assert - Get by reservation ID
            Debug.WriteLine($"Retrieved {feedbackByReservation.Count()} feedbacks for Reservation ID: {reservationId}");
            Assert.NotEmpty(feedbackByReservation);
            Assert.Contains(feedbackByReservation, f => f.Id == insertedId);

            // Act - Update
            retrievedFeedback.Rating = 4;
            retrievedFeedback.Comment = "Very good stay, but room service was slow";
            Debug.WriteLine($"Updating Feedback: Rating: {retrievedFeedback.Rating}, Comment: \"{retrievedFeedback.Comment}\"");
            var updateResult = await _feedbackRepository.UpdateAsync(retrievedFeedback);

            // Assert - Update
            Debug.WriteLine($"Update Result: {updateResult} row(s) affected");
            Assert.Equal(1, updateResult);

            // Act - Delete and cleanup
            Debug.WriteLine("Cleaning up test data...");
            await _feedbackRepository.DeleteAsync(insertedId);
            await _reservationRepository.DeleteAsync(reservationId);
            await _guestRepository.DeleteAsync(guestId);
            await _roomRepository.DeleteAsync(roomId);
        }

        [Fact]
        public async Task MaintenanceRequestRepository_CrudAndSpecialQueries_ShouldSucceed()
        {
            Debug.WriteLine("=== MAINTENANCE REQUEST REPOSITORY TEST ===");

            // Arrange - Create employee and room first
            var employee = new Employee
            {
                FirstName = "Michael",
                LastName = "Brown",
                Position = "Maintenance Staff",
                Email = "michael@example.com",
                Status = "Active",
                CreatedAt = DateTime.Now,
                HireDate = DateTime.Now.AddYears(-2)
            };
            Debug.WriteLine($"Adding Employee: {employee.FirstName} {employee.LastName}, Position: {employee.Position}, Email: {employee.Email}");
            var employeeId = await _employeeRepository.InsertAsync(employee);
            Debug.WriteLine($"Inserted Employee ID: {employeeId}");

            var room = new Room
            {
                RoomNumber = "701",
                Status = "Maintenance",
                Floor = 7
            };
            Debug.WriteLine($"Adding Room: {room.RoomNumber}, Floor: {room.Floor}, Status: {room.Status}");
            var roomId = await _roomRepository.InsertAsync(room);
            Debug.WriteLine($"Inserted Room ID: {roomId}");

            var maintenanceRequest = new MaintenanceRequest
            {
                RoomId = roomId,
                EmployeeId = employeeId,
                Description = "Air conditioner not working",
                Status = "Pending",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            Debug.WriteLine($"Adding Maintenance Request: Room ID: {maintenanceRequest.RoomId}, Employee ID: {maintenanceRequest.EmployeeId}");
            Debug.WriteLine($"Description: \"{maintenanceRequest.Description}\", Status: {maintenanceRequest.Status}");

            // Act - Insert maintenance request
            var insertedId = await _maintenanceRequestRepository.InsertAsync(maintenanceRequest);

            // Assert - Insert
            Debug.WriteLine($"Inserted Maintenance Request ID: {insertedId}");
            Assert.True(insertedId > 0);

            // Act - Get by ID
            var retrievedRequest = await _maintenanceRequestRepository.GetByIdAsync(insertedId);

            // Assert - Get by ID
            Debug.WriteLine($"Retrieved Maintenance Request: Description: \"{retrievedRequest.Description}\", Status: {retrievedRequest.Status}");
            Assert.NotNull(retrievedRequest);
            Assert.Equal("Air conditioner not working", retrievedRequest.Description);

            // Act - Get by room ID
            var requestsByRoom = await _maintenanceRequestRepository.GetMaintenanceRequestsByRoomIdAsync(roomId);

            // Assert - Get by room ID
            Debug.WriteLine($"Retrieved {requestsByRoom.Count()} maintenance requests for Room ID: {roomId}");
            Assert.NotEmpty(requestsByRoom);
            Assert.Contains(requestsByRoom, r => r.Id == insertedId);

            // Act - Get by employee ID
            var requestsByEmployee = await _maintenanceRequestRepository.GetMaintenanceRequestsByEmployeeIdAsync(employeeId);

            // Assert - Get by employee ID
            Debug.WriteLine($"Retrieved {requestsByEmployee.Count()} maintenance requests for Employee ID: {employeeId}");
            Assert.NotEmpty(requestsByEmployee);
            Assert.Contains(requestsByEmployee, r => r.Id == insertedId);

            // Act - Get by status
            var requestsByStatus = await _maintenanceRequestRepository.GetMaintenanceRequestsByStatusAsync("Pending");

            // Assert - Get by status
            Debug.WriteLine($"Retrieved {requestsByStatus.Count()} maintenance requests with Status: Pending");
            Assert.NotEmpty(requestsByStatus);
            Assert.Contains(requestsByStatus, r => r.Id == insertedId);

            // Act - Update
            retrievedRequest.Status = "InProgress";
            retrievedRequest.UpdatedAt = DateTime.Now;
            Debug.WriteLine($"Updating Maintenance Request Status to: {retrievedRequest.Status}");
            var updateResult = await _maintenanceRequestRepository.UpdateAsync(retrievedRequest);

            // Assert - Update
            Debug.WriteLine($"Update Result: {updateResult} row(s) affected");
            Assert.Equal(1, updateResult);

            // Act - Get updated
            var updatedRequest = await _maintenanceRequestRepository.GetByIdAsync(insertedId);

            // Assert - Get updated
            Debug.WriteLine($"Retrieved Updated Maintenance Request: Status: {updatedRequest.Status}");
            Assert.Equal("InProgress", updatedRequest.Status);

            // Act - Delete and cleanup
            Debug.WriteLine("Cleaning up test data...");
            await _maintenanceRequestRepository.DeleteAsync(insertedId);
            await _roomRepository.DeleteAsync(roomId);
            await _employeeRepository.DeleteAsync(employeeId);
        }

        [Fact]
        public async Task PaymentRepository_CrudAndSpecialQueries_ShouldSucceed()
        {
            Debug.WriteLine("=== PAYMENT REPOSITORY TEST ===");

            // Arrange - Create guest, room and reservation first
            var guest = new Guest
            {
                FirstName = "Sarah",
                LastName = "Wilson",
                Email = "sarah@example.com",
                CreatedAt = DateTime.Now
            };
            Debug.WriteLine($"Adding Guest: {guest.FirstName} {guest.LastName}, Email: {guest.Email}");
            var guestId = await _guestRepository.InsertAsync(guest);
            Debug.WriteLine($"Inserted Guest ID: {guestId}");

            var room = new Room
            {
                RoomNumber = "801",
                Status = "Available",
                Floor = 8
            };
            Debug.WriteLine($"Adding Room: {room.RoomNumber}, Floor: {room.Floor}, Status: {room.Status}");
            var roomId = await _roomRepository.InsertAsync(room);
            Debug.WriteLine($"Inserted Room ID: {roomId}");

            var checkIn = DateTime.Now.AddDays(15);
            var checkOut = DateTime.Now.AddDays(20);
            var reservation = new Reservation
            {
                GuestId = guestId,
                RoomId = roomId,
                CheckIn = checkIn,
                CheckOut = checkOut,
                Status = "Confirmed",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            Debug.WriteLine($"Adding Reservation: Guest ID: {reservation.GuestId}, Room ID: {reservation.RoomId}");
            Debug.WriteLine($"Check-in: {reservation.CheckIn.ToShortDateString()}, Check-out: {reservation.CheckOut.ToShortDateString()}, Status: {reservation.Status}");
            var reservationId = await _reservationRepository.InsertAsync(reservation);
            Debug.WriteLine($"Inserted Reservation ID: {reservationId}");

            var payment = new Payment
            {
                ReservationId = reservationId,
                Amount = 450.75,
                PaymentDate = DateTime.Now,
                Method = "CreditCard",
                Status = "Completed"
            };
            Debug.WriteLine($"Adding Payment: Reservation ID: {payment.ReservationId}, Amount: ${payment.Amount}, Method: {payment.Method}, Status: {payment.Status}");

            // Act - Insert payment
            var insertedId = await _paymentRepository.InsertAsync(payment);

            // Assert - Insert
            Debug.WriteLine($"Inserted Payment ID: {insertedId}");
            Assert.True(insertedId > 0);

            // Act - Get by ID
            var retrievedPayment = await _paymentRepository.GetByIdAsync(insertedId);

            // Assert - Get by ID
            Debug.WriteLine($"Retrieved Payment: Amount: ${retrievedPayment.Amount}, Method: {retrievedPayment.Method}, Status: {retrievedPayment.Status}");
            Assert.NotNull(retrievedPayment);
            Assert.Equal(450.75, retrievedPayment.Amount);
            Assert.Equal("CreditCard", retrievedPayment.Method);

            // Act - Get by reservation ID
            var paymentsByReservation = await _paymentRepository.GetPaymentsByReservationIdAsync(reservationId);

            // Assert - Get by reservation ID
            Debug.WriteLine($"Retrieved {paymentsByReservation.Count()} payments for Reservation ID: {reservationId}");
            Assert.NotEmpty(paymentsByReservation);
            Assert.Contains(paymentsByReservation, p => p.Id == insertedId);

            // Act - Update
            retrievedPayment.Status = "Refunded";
            Debug.WriteLine($"Updating Payment Status to: {retrievedPayment.Status}");
            var updateResult = await _paymentRepository.UpdateAsync(retrievedPayment);

            // Assert - Update
            Debug.WriteLine($"Update Result: {updateResult} row(s) affected");
            Assert.Equal(1, updateResult);

            // Act - Get updated
            var updatedPayment = await _paymentRepository.GetByIdAsync(insertedId);

            // Assert - Get updated
            Debug.WriteLine($"Retrieved Updated Payment: Status: {updatedPayment.Status}");
            Assert.Equal("Refunded", updatedPayment.Status);

            // Act - Delete and cleanup
            Debug.WriteLine("Cleaning up test data...");
            await _paymentRepository.DeleteAsync(insertedId);
            await _reservationRepository.DeleteAsync(reservationId);
            await _guestRepository.DeleteAsync(guestId);
            await _roomRepository.DeleteAsync(roomId);
        }

        public void Dispose()
        {
            try
            {
                SqliteConnection.ClearAllPools();

                GC.Collect();
                GC.WaitForPendingFinalizers();

                if (File.Exists(_dbPath))
                {
                    File.Delete(_dbPath);
                }
            }
            catch (IOException ex)
            {
                Debug.WriteLine($"An error occurred when deleting database file: {ex.Message}");
            }
        }
    }
}