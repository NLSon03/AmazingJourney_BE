using AmazingJourney.Application.DTOs;
using AmazingJourney_BE.AmazingJourney.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmazingJourney.Application.Interfaces
{
    public interface IBookingService
    {
        // Lấy danh sách tất cả các booking
        Task<IEnumerable<BookingDTO>> GetAllBookingsAsync();

        // Lấy thông tin booking theo ID
        Task<BookingDTO> GetBookingByIdAsync(int id);

        // Lấy danh sách booking của một người dùng theo UserId
        Task<IEnumerable<BookingDTO>> GetBookingsByUserIdAsync(int userId);

        // Tạo mới một booking
        Task<BookingDTO> CreateBookingAsync(BookingCreateDTO bookingCreateDto, int userId);

        // Cập nhật thông tin booking
        Task<BookingDTO> UpdateBookingAsync(int id, BookingDTO bookingUpdateDto);

        // Xóa booking theo ID
        Task<bool> DeleteBookingAsync(int id);
    }
}
