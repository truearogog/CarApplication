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
    public class AddCarCommand : AsyncCommandBase
    {
        private readonly AddCarViewModel _addCarViewModel;
        private readonly CarsStore _carsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public AddCarCommand(AddCarViewModel addCarViewModel, CarsStore carsStore, ModalNavigationStore modalNavigationStore)
        {
            _addCarViewModel = addCarViewModel;
            _carsStore = carsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            CarDetailsFormViewModel formViewModel = _addCarViewModel.CarDetailsFormViewModel;

            formViewModel.ErrorMessage = null;
            formViewModel.IsSubmitting = true;

            try
            {
                Car car = new Car(
                    Guid.NewGuid(),
                    formViewModel.ModelName,
                    int.Parse(formViewModel.Year),
                    formViewModel.Country,
                    formViewModel.Type
                );

                // Add car to database
                await _carsStore.Add(car);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to add car. Please try again later.";
            }
            finally
            {
                formViewModel.IsSubmitting = false;
            }
        }
    }
}
