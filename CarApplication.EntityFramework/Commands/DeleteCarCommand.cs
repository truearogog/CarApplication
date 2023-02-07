using CarApplication.Domain.Commands;
using CarApplication.Domain.Models;
using CarApplication.EntityFramework.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.EntityFramework.Commands
{
    public class DeleteCarCommand : IDeleteCarCommand
    {
        private readonly CarsDbContextFactory _contextFactory;

        public DeleteCarCommand(CarsDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task Execute(Guid id)
        {
            using (var context = _contextFactory.Create())
            {
                CarDto carDto = new CarDto()
                {
                    Id = id
                };

                context.Cars.Remove(carDto);
                await context.SaveChangesAsync();
            }
        }
    }
}
