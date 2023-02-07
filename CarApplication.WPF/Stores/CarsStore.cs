using CarApplication.Domain.Commands;
using CarApplication.Domain.Models;
using CarApplication.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.WPF.Stores
{
    public class CarsStore
    {
        private readonly IGetAllCarsQuery _getAllCarsQuery;
        private readonly ICreateCarCommand _createCarCommand;
        private readonly IUpdateCarCommand _updateCarCommand;
        private readonly IDeleteCarCommand _deleteCarCommand;

        private readonly List<Car> _cars;
        public IEnumerable<Car> Cars => _cars;

        public event Action CarsLoaded;
        public event Action<Car> CarAdded;
        public event Action<Car> CarUpdated;
        public event Action<Guid> CarDeleted;

        public CarsStore(IGetAllCarsQuery getAllCarsQuery, ICreateCarCommand createCarCommand, IUpdateCarCommand updateCarCommand, IDeleteCarCommand deleteCarCommand)
        {
            _getAllCarsQuery = getAllCarsQuery;
            _createCarCommand = createCarCommand;
            _updateCarCommand = updateCarCommand;
            _deleteCarCommand = deleteCarCommand;

            _cars = new List<Car>();
        }

        public async Task Load()
        {
            IEnumerable<Car> cars = await _getAllCarsQuery.Execute();

            _cars.Clear();
            _cars.AddRange(cars);

            CarsLoaded?.Invoke();
        }

        public async Task Add(Car car)
        {
            await _createCarCommand.Execute(car);

            _cars.Add(car);

            CarAdded?.Invoke(car);
        }

        public async Task Update(Car car)
        {
            await _updateCarCommand.Execute(car);

            int currentIndex = _cars.FindIndex(x => x.Id.Equals(car.Id));

            if (currentIndex != -1)
            {
                _cars[currentIndex] = car;
            }
            else
            {
                _cars.Add(car);
            }

            CarUpdated?.Invoke(car);
        }

        public async Task Delete(Guid id)
        {
            await _deleteCarCommand.Execute(id);

            _cars.RemoveAll(x => x.Id.Equals(id));

            CarDeleted?.Invoke(id);
        }
    }
}
