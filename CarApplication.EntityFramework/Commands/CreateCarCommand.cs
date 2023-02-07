using CarApplication.Domain.Commands;
using CarApplication.Domain.Models;
using CarApplication.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.EntityFramework.Commands
{
    public class CreateCarCommand : ICreateCarCommand
    {
        private readonly CarsDbContextFactory _contextFactory;

        public CreateCarCommand(CarsDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Car car)
        {
            using (var context = _contextFactory.Create())
            {
                CarDto carDto = new CarDto()
                {
                    Id = car.Id,
                    ModelName = car.ModelName,
                    Year = car.Year,
                    Country = car.Country,
                    Type = car.Type,
                };

                context.Cars.Add(carDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
