using Duru.Core.Services;
using Duru.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Core.Business
{
    public class ReservationManager
    {
        private readonly IReservationService _reservationService;
        private readonly IRoomService _roomService;
        private readonly IGuestService _guestService;
        private readonly IPaymentService _paymentService;

        public ReservationManager(
            IReservationService reservationService,
            IRoomService roomService,
            IGuestService guestService,
            IPaymentService paymentService)
        {
            _reservationService = reservationService;
            _roomService = roomService;
            _guestService = guestService;
            _paymentService = paymentService;
        }

        public async Task<ReservationViewModel?> CreateReservationAsync(ReservationViewModel reservation)
        {
            // Business rule validations
            if (reservation.CheckIn >= reservation.CheckOut)
            {
                throw new ArgumentException("Check-out date must be after check-in date");
            }

            if (reservation.CheckIn.Date < DateTime.Now.Date)
            {
                throw new ArgumentException("Check-in date cannot be in the past");
            }

            // Check if room is available for the requested dates
            bool isRoomAvailable = await _reservationService.IsRoomAvailableAsync(
                reservation.RoomId,
                reservation.CheckIn,
                reservation.CheckOut);

            if (!isRoomAvailable)
            {
                throw new InvalidOperationException("Room is not available for the selected dates");
            }

            // Set initial status
            reservation.Status = "Pending";
            reservation.CreatedAt = DateTime.Now;
            reservation.UpdatedAt = DateTime.Now;

            // Create the reservation
            var model = reservation.ToModel();
            int id = await _reservationService.CreateReservationAsync(model);

            if (id <= 0)
            {
                return null;
            }

            // Get the created reservation
            var createdReservation = await _reservationService.GetReservationByIdAsync(id);
            return createdReservation?.ToViewModel();
        }

        public async Task<bool> CancelReservationAsync(int reservationId)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);

            if (reservation == null)
            {
                throw new ArgumentException("Reservation not found");
            }

            // Check if cancellation is allowed based on business rules
            if (reservation.CheckIn.Date <= DateTime.Now.Date)
            {
                throw new InvalidOperationException("Cannot cancel a reservation on or after check-in date");
            }

            reservation.Status = "Cancelled";
            reservation.UpdatedAt = DateTime.Now;

            return await _reservationService.UpdateReservationAsync(reservation);
        }

        public async Task<bool> CheckInAsync(int reservationId)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);

            if (reservation == null)
            {
                throw new ArgumentException("Reservation not found");
            }

            // Business rules for check-in
            if (reservation.Status != "Confirmed" && reservation.Status != "Pending")
            {
                throw new InvalidOperationException($"Cannot check in reservation with status: {reservation.Status}");
            }

            // Update reservation status
            reservation.Status = "CheckedIn";
            reservation.UpdatedAt = DateTime.Now;

            // Update room status
            var room = await _roomService.GetRoomByIdAsync(reservation.RoomId);
            if (room != null)
            {
                room.Status = "Occupied";
                await _roomService.UpdateRoomAsync(room);
            }

            return await _reservationService.UpdateReservationAsync(reservation);
        }

        public async Task<bool> CheckOutAsync(int reservationId)
        {
            var reservation = await _reservationService.GetReservationByIdAsync(reservationId);

            if (reservation == null)
            {
                throw new ArgumentException("Reservation not found");
            }

            // Business rules for check-out
            if (reservation.Status != "CheckedIn")
            {
                throw new InvalidOperationException($"Cannot check out reservation with status: {reservation.Status}");
            }

            // Check for outstanding payments
            var payments = await _paymentService.GetPaymentsByReservationIdAsync(reservationId);
            double totalPaid = payments.Sum(p => p.Amount);

            // Calculate total amount (simplified for this example)
            double totalAmount = 0; // This would be calculated based on room rates and additional services

            if (totalPaid < totalAmount)
            {
                throw new InvalidOperationException("Cannot check out with outstanding balance");
            }

            // Update reservation status
            reservation.Status = "CheckedOut";
            reservation.UpdatedAt = DateTime.Now;

            // Update room status
            var room = await _roomService.GetRoomByIdAsync(reservation.RoomId);
            if (room != null)
            {
                room.Status = "Cleaning";
                await _roomService.UpdateRoomAsync(room);
            }

            return await _reservationService.UpdateReservationAsync(reservation);
        }
    }
}
