using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.EntityFramework
{
    public class CarsDbContextFactory
    {
        private readonly DbContextOptions _options;

        public CarsDbContextFactory(DbContextOptions options)
        {
            _options = options;
        }

        public CarsDbContext Create()
        {
            return new CarsDbContext(_options);
        }
    }
}
