using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.EntityFramework.DTOs
{
    public class CarDto
    {
        [Key]
        public Guid Id { get; set; }
        public string ModelName { get; set; }
        public int Year { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
    }
}
