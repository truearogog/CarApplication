using CarApplication.Domain.Models;
using CarApplication.Domain.Queries;
using CarApplication.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.EntityFramework.Queries
{
    public class GetAllCarsQuery : IGetAllCarsQuery
    {
        private readonly CarsDbContextFactory _contextFactory;

        public GetAllCarsQuery(CarsDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Car>> Execute()
        {
            using (var context = _contextFactory.Create())
            {
                IEnumerable<CarDto> carDtos = await context.Cars.ToListAsync();

                return carDtos.Select(x => new Car(x.Id, x.ModelName, x.Year, x.Country, x.Type));
            }
        }
    }
}
