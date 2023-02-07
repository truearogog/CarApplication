using CarApplication.WPF.Stores;
using CarApplication.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarApplication.WPF.Commands
{
    public class OpenAddCarCommand : CommandBase
    {
        private readonly CarsStore _carsStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        public OpenAddCarCommand(CarsStore carsStore, ModalNavigationStore modalNavigationStore)
        {
            _carsStore = carsStore;
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object? parameter)
        {
            AddCarViewModel addCarViewModel = new AddCarViewModel(_carsStore, _modalNavigationStore);
            _modalNavigationStore.CurrentViewModel = addCarViewModel;
        }
    }
}
