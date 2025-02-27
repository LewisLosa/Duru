using Duru.Core.Models;
using Duru.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duru.Core.Services
{
    public static class ModelMapper
    {
        public static EmployeeViewModel ToViewModel(this Employee employee)
        {
            return new EmployeeViewModel
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Position = employee.Position,
                Email = employee.Email,
                Phone = employee.Phone,
                HireDate = employee.HireDate,
                Status = employee.Status,
                CreatedAt = employee.CreatedAt
            };
        }

        public static Employee ToModel(this EmployeeViewModel viewModel)
        {
            return new Employee
            {
                EmployeeId = viewModel.EmployeeId,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Position = viewModel.Position,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                HireDate = viewModel.HireDate,
                Status = viewModel.Status,
                CreatedAt = viewModel.CreatedAt
            };
        }

        public static RoomViewModel ToViewModel(this Room room)
        {
            return new RoomViewModel
            {
                RoomId = room.RoomId,
                RoomNumber = room.RoomNumber,
                RoomTypeId = room.RoomTypeId,
                Status = room.Status,
                Floor = room.Floor
            };
        }

        public static Room ToModel(this RoomViewModel viewModel)
        {
            return new Room
            {
                RoomId = viewModel.RoomId,
                RoomNumber = viewModel.RoomNumber,
                RoomTypeId = viewModel.RoomTypeId,
                Status = viewModel.Status,
                Floor = viewModel.Floor
            };
        }

        public static GuestViewModel ToViewModel(this Guest guest)
        {
            return new GuestViewModel
            {
                GuestId = guest.GuestId,
                FirstName = guest.FirstName,
                LastName = guest.LastName,
                Email = guest.Email,
                Phone = guest.Phone,
                Address = guest.Address,
                CreatedAt = guest.CreatedAt
            };
        }

        public static Guest ToModel(this GuestViewModel viewModel)
        {
            return new Guest
            {
                GuestId = viewModel.GuestId,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Address = viewModel.Address,
                CreatedAt = viewModel.CreatedAt
            };
        }

        public static ReservationViewModel ToViewModel(this Reservation reservation)
        {
            return new ReservationViewModel
            {
                ReservationId = reservation.ReservationId,
                GuestId = reservation.GuestId,
                RoomId = reservation.RoomId,
                CheckIn = reservation.CheckIn,
                CheckOut = reservation.CheckOut,
                Status = reservation.Status,
                CreatedAt = reservation.CreatedAt,
                UpdatedAt = reservation.UpdatedAt
            };
        }

        public static Reservation ToModel(this ReservationViewModel viewModel)
        {
            return new Reservation
            {
                ReservationId = viewModel.ReservationId,
                GuestId = viewModel.GuestId,
                RoomId = viewModel.RoomId,
                CheckIn = viewModel.CheckIn,
                CheckOut = viewModel.CheckOut,
                Status = viewModel.Status,
                CreatedAt = viewModel.CreatedAt,
                UpdatedAt = viewModel.UpdatedAt
            };
        }
    }
}
