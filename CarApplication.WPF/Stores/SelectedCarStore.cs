using CarApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.WPF.Stores
{
    public class SelectedCarStore
    {
        private Car _selectedCar;
        private CarsStore _carsStore;

        public Car SelectedCar
        {
            get
            {
                return _selectedCar;
            }
            set
            {
                _selectedCar = value;
                SelectedCarChanged?.Invoke();
            }
        }

        public event Action SelectedCarChanged;  

        public SelectedCarStore(CarsStore carsStore)
        {
            _carsStore = carsStore;

            _carsStore.CarAdded += CarsStore_CarAdded;
            _carsStore.CarUpdated += CarsStore_CarUpdated;
        }

        private void CarsStore_CarAdded(Car car)
        {
            SelectedCar = car;
        }

        private void CarsStore_CarUpdated(Car car)
        {
            if (SelectedCar != null && car.Id == SelectedCar.Id)
            {
                SelectedCar = car;
            }
        }
    }
}
