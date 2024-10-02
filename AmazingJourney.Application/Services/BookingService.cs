using AmazingJourney.Application.DTOs;
using AmazingJourney.Application.Interfaces;
using AmazingJourney.Infrastructure.Data;
using AmazingJourney_BE.AmazingJourney.Domain.Entities;
using AmazingJourney_BE.AmazingJourney.DTOs;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AmazingJourney.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRoomService _roomService; 

        public BookingService(ApplicationDbContext context, IMapper mapper, IRoomService roomService)
        {
            _context = context;
            _mapper = mapper;
            _roomService = roomService;
        }

        public async Task<IEnumerable<BookingDTO>> GetAllBookingsAsync()
        {
            var bookings = await _context.Bookings.ToListAsync();
            return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
        }

        public async Task<BookingDTO> GetBookingByIdAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            return booking == null ? null : _mapper.Map<BookingDTO>(booking);
        }

        public async Task<IEnumerable<BookingDTO>> GetBookingsByUserIdAsync(int userId)
        {
            var bookings = await _context.Bookings
                                          .Where(b => b.UserId == userId)
                                          .ToListAsync();
            return _mapper.Map<IEnumerable<BookingDTO>>(bookings);
        }

        public async Task<BookingDTO> CreateBookingAsync(BookingCreateDTO bookingCreateDto, int userId)
        {
            var room = await _roomService.GetRoomByIdAsync(bookingCreateDto.RoomId);
            if (room == null || !room.Availability)
            {
                throw new Exception("Phòng không khả dụng.");
            }

            var booking = _mapper.Map<Booking>(bookingCreateDto);
            booking.UserId = userId;
            booking.TotalPrice = (decimal)(booking.CheckOut - booking.CheckIn).TotalDays * room.PricePerNight;
            booking.Status = "Pending"; 
            booking.CreatedAt = DateTime.Now;
            booking.UpdatedAt = DateTime.Now;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookingDTO>(booking);
        }

        public async Task<BookingDTO> UpdateBookingAsync(int id, BookingDTO bookingDto)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                throw new Exception("Booking không tồn tại.");
            }

            // Cập nhật thông tin booking từ bookingDto
            booking.CheckIn = bookingDto.CheckIn;
            booking.CheckOut = bookingDto.CheckOut;
            booking.TotalPrice = bookingDto.TotalPrice;
            booking.Status = bookingDto.Status;
            booking.UpdatedAt = DateTime.Now;

            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            return _mapper.Map<BookingDTO>(booking);
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
                return false;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
