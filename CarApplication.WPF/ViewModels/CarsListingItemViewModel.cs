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
    public class CarsListingItemViewModel : ViewModelBase
    {
        public Car Car { get; private set; }

        public string ModelName => Car.ModelName;
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        private bool _isDeleting;
        public bool IsDeleting
        {
            get
            {
                return _isDeleting;
            }
            set
            {
                _isDeleting = value;
                OnPropertyChanged(nameof(IsDeleting));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                OnPropertyChanged(nameof(HasErrorMessage));
            }
        }
        public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);

        public CarsListingItemViewModel(Car car, CarsStore carsStore, ModalNavigationStore modalNavigationStore)
        {
            Car = car;

            EditCommand = new OpenEditCarCommand(() => Car, carsStore, modalNavigationStore);
            DeleteCommand = new DeleteCarCommand(() => Car, carsStore, x => IsDeleting = x, x => ErrorMessage = x);
        }

        public void Update(Car car)
        {
            Car = car;

            OnPropertyChanged(nameof(ModelName));
        }
    }
}
