using Data;
using Domain.Bookings.Entities;
using Domain.Bookings.Enums;
using Domain.Bookings.Ports;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Bookings
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext _context;

        public BookingRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<Booking> GetByIdAsync(int id)
        {
            return await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Guest)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Booking>> GetAllAsync()
        {
            return await _context.Bookings
                .Include(b => b.Room)
                .Include(b => b.Guest)
                .ToListAsync();
        }

        public async Task<Booking> Create(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
            return booking;
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Booking booking)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasBookingsForGuest(int guestId)
        {
            return await _context.Bookings.AnyAsync(b => b.GuestId == guestId);
        }

        public async Task<bool> IsRoomOccupied(int roomId, DateTime start, DateTime end)
        {
            return await _context.Bookings.AnyAsync(b =>
                b.RoomId == roomId &&
                b.Status != Status.Canceled &&
                b.Status != Status.Finished &&
                (start < b.End.AddHours(3) && end > b.Start));
        }
    }
}
