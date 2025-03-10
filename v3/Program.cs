using v3.Context;
using v3.Customers.Services.Impl;
using v3.Customers.Services.Interfaces;
using v3.Customers.Utils;
using v3.Doctors.Services.Impl;
using v3.Doctors.Services.Interfaces;
using v3.Doctors.Utils;
using v3.People.Services.Impl;
using v3.People.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<IPersonRegistrationService, PersonRegistrationService>();
builder.Services.AddSingleton<CustomerFinderUtil>();
builder.Services.AddSingleton<ICustomerRegistrationService, CustomerRegistrationService>();
builder.Services.AddSingleton<ICustomerRetrievalService, CustomerRetrievalService>();
builder.Services.AddSingleton<DoctorFinderUtil>();
builder.Services.AddSingleton<IDoctorHiringService, DoctorHiringService>();
builder.Services.AddSingleton<IDoctorRehiringService, DoctorRehiringService>();
builder.Services.AddSingleton<IDoctorTerminationService, DoctorTerminationService>();
builder.Services.AddSingleton<IDoctorRetrievalService, DoctorRetrievalService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "Medical Appointment API"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();