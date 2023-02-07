using CarApplication.WPF.Stores;
using CarApplication.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.WPF.Commands
{
    public class LoadCarsCommand : AsyncCommandBase
    {
        private readonly CarsViewModel _carsViewModel;
        private readonly CarsStore _carsStore;

        public LoadCarsCommand(CarsViewModel carsViewModel, CarsStore carsStore)
        {
            _carsViewModel = carsViewModel;
            _carsStore = carsStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            _carsViewModel.ErrorMessage = null;
            _carsViewModel.IsLoading = true;

            try
            {
                await _carsStore.Load();
            }
            catch (Exception ex)
            {
                _carsViewModel.ErrorMessage = "Failed to load cars. Please restart the application.";
            }
            finally
            {
                _carsViewModel.IsLoading = false;
            }
        }
    }
}
