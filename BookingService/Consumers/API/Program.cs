using Application.Ports;
using Data;
using Microsoft.EntityFrameworkCore;
using API.Middlewares;
using Domain.Guests.Ports;
using Application.Guests;
using Data.Guests;
using Application.Bookings.Ports;
using Application.Bookings;
using Domain.Bookings.Ports;
using Domain.Bookings;
using Application.Guests.Ports;
using Domain.Guests;
using Domain.Rooms.Ports;
using Application.Rooms.Ports;
using Application.Rooms;
using Data.Rooms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region
builder.Services.AddScoped<IBookingManager, BookingManager>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddScoped<IGuestManager, GuestManager>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();

builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomManager, RoomManager>();
#endregion

#region

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<HotelDbContext>(options => options.UseNpgsql(connectionString));

#endregion

builder.Services.AddExceptionHandler<ErrorHandlingMiddleware>();
builder.Services.AddProblemDetails();

builder.Services.AddAutoMapper(typeof(GuestMapping));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();
