using AutoMapper;
using kts_travels.Configuration;
using kts_travels.Data;
using kts_travels.Services;
using kts_travels.Services.IServices;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Register AutoMapper with the specified profile
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IDieselEntryService, DieselEntryService>();
builder.Services.AddScoped<IExcelImportService, ExcelImportService>();

builder.Services.AddControllers();
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
