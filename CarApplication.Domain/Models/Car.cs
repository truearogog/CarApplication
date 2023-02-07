using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.Domain.Models
{
    public class Car
    {
        public Guid Id { get; }
        public string ModelName { get; }
        public int Year { get; }
        public string Country { get; }
        public string Type { get; }

        public Car(Guid id, string modelName, int year, string country, string type)
        {
            Id = id;
            ModelName = modelName;
            Year = year;
            Country = country;
            Type = type;
        }
    }
}
