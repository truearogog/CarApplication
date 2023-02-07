using CarApplication.Domain.Models;
using CarApplication.WPF.Stores;
using CarApplication.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.WPF.Commands
{
    public class OpenEditCarCommand : CommandBase
    {
        private readonly CarsStore _carsStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly Func<Car> _carSelector;

        public OpenEditCarCommand(Func<Car> carSelector, CarsStore carsStore, ModalNavigationStore modalNavigationStore)
        {
            _carSelector = carSelector;
            _carsStore = carsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            Car car = _carSelector();

            EditCarViewModel editCarViewModel = new EditCarViewModel(car, _carsStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = editCarViewModel;
        }
    }
}
