using CarApplication.WPF.Commands;
using CarApplication.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarApplication.WPF.ViewModels
{
    public class AddCarViewModel : ViewModelBase
    {
        public CarDetailsFormViewModel CarDetailsFormViewModel { get; }

        public AddCarViewModel(CarsStore carsStore, ModalNavigationStore modalNavigationStore)
        {
            ICommand submitCommand = new AddCarCommand(this, carsStore, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            CarDetailsFormViewModel = new CarDetailsFormViewModel(submitCommand, cancelCommand);
        }
    }
}
