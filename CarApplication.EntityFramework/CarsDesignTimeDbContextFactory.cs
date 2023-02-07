using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.EntityFramework
{
    public class CarsDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CarsDbContext>
    {
        public CarsDbContext CreateDbContext(string[] args = null)
        {
            return new CarsDbContext(new DbContextOptionsBuilder().UseSqlite("Data Source=Cars.db").Options);
        }
    }
}
