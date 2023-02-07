using CarApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.Domain.Queries
{
    public interface IGetAllCarsQuery
    {
        Task<IEnumerable<Car>> Execute();
    }
}
