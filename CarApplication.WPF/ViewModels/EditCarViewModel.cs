using CarApplication.WPF.Commands;
using CarApplication.Domain.Models;
using CarApplication.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarApplication.WPF.ViewModels
{
    public class EditCarViewModel : ViewModelBase
    {
        public Guid CarId { get; }
        public CarDetailsFormViewModel CarDetailsFormViewModel { get; }

        public EditCarViewModel(Car car, CarsStore carsStore, ModalNavigationStore modalNavigationStore)
        {
            CarId = car.Id;

            ICommand submitCommand = new EditCarCommand(this, carsStore, modalNavigationStore);
            ICommand cancelCommand = new CloseModalCommand(modalNavigationStore);
            CarDetailsFormViewModel = new CarDetailsFormViewModel(submitCommand, cancelCommand)
            {
                ModelName = car.ModelName,
                Year = car.Year.ToString(),
                Country = car.Country,
                Type = car.Type
            };
        }
    }
}
