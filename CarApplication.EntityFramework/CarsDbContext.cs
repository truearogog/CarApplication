using CarApplication.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.EntityFramework
{
    public class CarsDbContext : DbContext
    {
        public DbSet<CarDto> Cars { get; set; }

        public CarsDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
