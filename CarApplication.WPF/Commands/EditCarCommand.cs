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
    public class EditCarCommand : AsyncCommandBase
    {
        private readonly CarsStore _carsStore;
        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly EditCarViewModel _editCarViewModel;

        public EditCarCommand(EditCarViewModel editCarViewModel, CarsStore carsStore, ModalNavigationStore modalNavigationStore)
        {
            _editCarViewModel = editCarViewModel;
            _carsStore = carsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            CarDetailsFormViewModel formViewModel = _editCarViewModel.CarDetailsFormViewModel;

            formViewModel.ErrorMessage = null;
            formViewModel.IsSubmitting = true;

            try
            {
                Car car = new Car(
                    _editCarViewModel.CarId,
                    formViewModel.ModelName,
                    int.Parse(formViewModel.Year),
                    formViewModel.Country,
                    formViewModel.Type
                );

                // Edit car to database
                await _carsStore.Update(car);

                _modalNavigationStore.Close();
            }
            catch (Exception)
            {
                formViewModel.ErrorMessage = "Failed to edit car. Please try again later.";
            }
            finally
            {
                formViewModel.IsSubmitting = false;
            }
        }
    }
}
