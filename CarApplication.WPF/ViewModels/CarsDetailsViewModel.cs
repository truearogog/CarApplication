using CarApplication.WPF.Commands;
using CarApplication.Domain.Models;
using CarApplication.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarApplication.WPF.ViewModels
{
    public class CarsDetailsViewModel : ViewModelBase
    {
        private readonly SelectedCarStore _selectedCarStore;

        public Car SelectedCar => _selectedCarStore.SelectedCar;

        public bool HasSelectedCar => SelectedCar != null;
        public string ModelName => SelectedCar?.ModelName ?? "Unknown";
        public string Year => SelectedCar?.Year.ToString() ?? "Unknown";
        public string Country => SelectedCar?.Country ?? "Unknown";
        public string Type => SelectedCar?.Type ?? "Unknown";

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

        public CarsDetailsViewModel(CarsStore carsStore, SelectedCarStore selectedCarStore, ModalNavigationStore modalNavigationStore)
        {
            _selectedCarStore = selectedCarStore;
            _selectedCarStore.SelectedCarChanged += SelectedCarStore_SelectedCarChanged;

            EditCommand = new OpenEditCarCommand(() => SelectedCar, carsStore, modalNavigationStore);
            DeleteCommand = new DeleteCarCommand(() => SelectedCar, carsStore, x => IsDeleting = x, x => ErrorMessage = x);
        }

        protected override void Dispose()
        {
            _selectedCarStore.SelectedCarChanged -= SelectedCarStore_SelectedCarChanged;

            base.Dispose();
        }

        private void SelectedCarStore_SelectedCarChanged()
        {
            OnPropertyChanged(nameof(HasSelectedCar));
            OnPropertyChanged(nameof(ModelName));
            OnPropertyChanged(nameof(Year));
            OnPropertyChanged(nameof(Country));
            OnPropertyChanged(nameof(Type));
        }
    }
}
