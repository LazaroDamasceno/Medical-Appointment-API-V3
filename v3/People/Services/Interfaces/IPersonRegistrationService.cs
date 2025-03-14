﻿using System.ComponentModel.DataAnnotations;
using v3.People.Domain;
using v3.People.DTOs;

namespace v3.People.Services.Interfaces;

public interface IPersonRegistrationService
{
    Task<Person> Create(PersonRegistrationDto registrationDto);
}