﻿using WingsOn.Domain;
using WingsOn.Domain.Entities;

namespace WingsOn.Dal.Repositories;

public class AirportRepository : RepositoryBase<Airport>
{
    public AirportRepository()
    {
        Repository.AddRange(new []
        {
            new Airport
            {
                Id = 60,
                Code = "OQO",
                City = "Zwijnaarde",
                Country = "Cuba"
            },
            new Airport
            {
                Id = 98,
                Code = "GJE",
                City = "Ottignies",
                Country = "Iran"
            },
            new Airport
            {
                Id = 13,
                Code = "CZR",
                City = "Okigwe",
                Country = "Mali"
            },
            new Airport
            {
                Id = 80,
                Code = "ANH",
                City = "Chiniot",
                Country = "Algeria"
            }
        });
    }
}