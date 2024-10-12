using AutoMapper;
using kts_travels.SharedServices.Application.Factories.Interfaces;
using kts_travels.SharedServices.Application.Factories;
using kts_travels.SharedServices.Application.Services;
using kts_travels.SharedServices.Application.Services.Interfaces;

using kts_travels.SharedServices.Infrastructure.Persistence;
using kts_travels.SharedServices.Infrastructure.Persistence.Repositories;
using kts_travels.SharedServices.Application.Configuration;
using kts_travels.SharedServices.Application.Utilities;
using Microsoft.EntityFrameworkCore;
using kts_travels.SharedServices.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
    });
// Register Services
builder.Services.AddScoped<ITripLogService, TripLogService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IVehicleSummariesService, VehicleSummariesService>();
builder.Services.AddScoped<IVehicleSummariesFactory, VehicleSummariesFactory>();
builder.Services.AddScoped<IExcelImportService, ExcelImportService>();
builder.Services.AddScoped<ITripLogFactory, TripLogFactory>();
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register Repositories
builder.Services.AddScoped<ITripLogRepository, TripLogRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleSummariesRepository, VehicleSummariesRepository>();



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
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
