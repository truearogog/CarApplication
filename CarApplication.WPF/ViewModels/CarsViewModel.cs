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
    public class CarsViewModel : ViewModelBase
    {
        public CarsListingViewModel CarsListingViewModel { get; }
        public CarsDetailsViewModel CarsDetailsViewModel { get; }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
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

        public ICommand LoadCarsCommand { get; }
        public ICommand AddCarCommand { get; }

        public CarsViewModel(CarsStore carsStore, SelectedCarStore selectedCarStore, ModalNavigationStore modalNavigationStore)
        {
            CarsListingViewModel = new CarsListingViewModel(carsStore, selectedCarStore, modalNavigationStore);
            CarsDetailsViewModel = new CarsDetailsViewModel(carsStore, selectedCarStore, modalNavigationStore);

            LoadCarsCommand = new LoadCarsCommand(this, carsStore);
            AddCarCommand = new OpenAddCarCommand(carsStore, modalNavigationStore);
        }

        public static CarsViewModel LoadViewModel(CarsStore carsStore, SelectedCarStore selectedCarStore, ModalNavigationStore modalNavigationStore)
        {
            CarsViewModel viewModel = new CarsViewModel(carsStore, selectedCarStore, modalNavigationStore);

            viewModel.LoadCarsCommand.Execute(null);

            return viewModel;
        }
    }
}
