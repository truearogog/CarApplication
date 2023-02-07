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
    public class DeleteCarCommand : AsyncCommandBase
    {
        private readonly CarsStore _carsStore;
        private readonly Func<Car> _carSelector;
        private readonly Action<bool>? _isDeletingSetter;
        private readonly Action<string>? _errorMessageSetter;

        public DeleteCarCommand(Func<Car> carSelector, CarsStore carsStore, Action<bool> isDeletingSetter = null, Action<string> errorMessageSetter = null)
        {
            _carSelector = carSelector;
            _carsStore = carsStore;
            _isDeletingSetter = isDeletingSetter;
            _errorMessageSetter = errorMessageSetter;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            Car car = _carSelector();

            _errorMessageSetter?.Invoke(null);
            _isDeletingSetter?.Invoke(true);

            try
            {
                await _carsStore.Delete(car.Id);
            }
            catch (Exception)
            {
                _errorMessageSetter?.Invoke("Failed to delete a car. Please try again later.");
            }
            finally
            {
                _isDeletingSetter?.Invoke(false);
            }
        }
    }
}
