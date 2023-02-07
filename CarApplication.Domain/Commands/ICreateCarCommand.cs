using CarApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.Domain.Commands
{
    public interface ICreateCarCommand
    {
        Task Execute(Car car);
    }
}
