using v3.Context;
using v3.Customers.Services.Impl;
using v3.Customers.Services.Interfaces;
using v3.Customers.Utils;
using v3.Doctors.Services.Impl;
using v3.Doctors.Services.Interfaces;
using v3.Doctors.Utils;
using v3.MedicalSlots.Services.Impl;
using v3.MedicalSlots.Services.Interfaces;
using v3.MedicalSlots.Utils;
using v3.People.Services.Impl;
using v3.People.Services.Interfaces;

namespace v3.Common;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<MongoDbContext>();
        services.AddSingleton<IPersonRegistrationService, PersonRegistrationService>();
        services.AddSingleton<CustomerFinder>();
        services.AddSingleton<ICustomerRegistrationService, CustomerRegistrationService>();
        services.AddSingleton<ICustomerRetrievalService, CustomerRetrievalService>();
        services.AddSingleton<DoctorFinder>();
        services.AddSingleton<IDoctorHiringService, DoctorHiringService>();
        services.AddSingleton<IDoctorRehiringService, DoctorRehiringService>();
        services.AddSingleton<IDoctorTerminationService, DoctorTerminationService>();
        services.AddSingleton<IDoctorRetrievalService, DoctorRetrievalService>();
        services.AddSingleton<MedicalSlotFinder>();
        services.AddSingleton<IMedicalSlotRegistrationService, MedicalSlotRegistrationService>();
        services.AddSingleton<IMedicalSlotManagementService, MedicalSlotManagementService>();
        services.AddSingleton<PersonalDataChecker>();
        return services;
    }
}