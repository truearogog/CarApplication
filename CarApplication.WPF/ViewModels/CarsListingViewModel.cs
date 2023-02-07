using CarApplication.WPF.Commands;
using CarApplication.Domain.Models;
using CarApplication.WPF.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Runtime.ConstrainedExecution;

namespace CarApplication.WPF.ViewModels
{
    public class CarsListingViewModel : ViewModelBase
    {
        private readonly CarsStore _carsStore;
        private readonly SelectedCarStore _selectedCarStore;
        private readonly ModalNavigationStore _modalNavigationStore;

        private readonly ObservableCollection<CarsListingItemViewModel> _carsListingItemViewModels;
        public IEnumerable<CarsListingItemViewModel> CarsListingItemViewModels => _carsListingItemViewModels;

        private CarsListingItemViewModel _selectedCarsListingItemViewModel;

        public CarsListingItemViewModel SelectedCarsListingItemViewModel
        {
            get
            {
                return _selectedCarsListingItemViewModel;
            }
            set
            {
                _selectedCarsListingItemViewModel = value;
                OnPropertyChanged(nameof(SelectedCarsListingItemViewModel));

                _selectedCarStore.SelectedCar = _selectedCarsListingItemViewModel?.Car;
            }
        }

        public ICommand LoadCarsCommand { get; }

        public CarsListingViewModel(CarsStore carsStore, SelectedCarStore selectedCarStore, ModalNavigationStore modalNavigationStore)
        {
            _carsStore = carsStore;
            _selectedCarStore = selectedCarStore;
            _modalNavigationStore = modalNavigationStore;
            _carsListingItemViewModels = new ObservableCollection<CarsListingItemViewModel>();

            _carsStore.CarsLoaded += CarsStore_CarsLoaded;
            _carsStore.CarAdded += CarsStore_CarAdded;
            _carsStore.CarUpdated += CarsStore_CarUpdated;
            _carsStore.CarDeleted += CarsStore_CarDeleted;
        }

        protected override void Dispose()
        {
            _carsStore.CarsLoaded -= CarsStore_CarsLoaded;
            _carsStore.CarAdded -= CarsStore_CarAdded;
            _carsStore.CarUpdated -= CarsStore_CarUpdated;
            _carsStore.CarDeleted -= CarsStore_CarDeleted;

            base.Dispose();
        }

        private void CarsStore_CarsLoaded()
        {
            _carsListingItemViewModels.Clear();

            foreach (var item in _carsStore.Cars)
            {
                AddCar(item);
            }
        }

        private void CarsStore_CarAdded(Car car)
        {
            AddCar(car);
        }

        private void CarsStore_CarUpdated(Car car)
        {
            var carsViewModel = _carsListingItemViewModels.FirstOrDefault(x => x.Car.Id.Equals(car.Id));

            if (carsViewModel != null)
            {
                carsViewModel.Update(car);
            }
        }

        private void CarsStore_CarDeleted(Guid id)
        {
            var carsViewModel = _carsListingItemViewModels.FirstOrDefault(x => x.Car.Id.Equals(id));

            if (carsViewModel != null)
            {
                _carsListingItemViewModels.Remove(carsViewModel);

                var firstOrNullCarsViewModel = _carsListingItemViewModels.FirstOrDefault();
                _selectedCarStore.SelectedCar = firstOrNullCarsViewModel?.Car;
            }
        }

        private void AddCar(Car car)
        {
            CarsListingItemViewModel itemViewModel = new CarsListingItemViewModel(car, _carsStore, _modalNavigationStore);
            _carsListingItemViewModels.Add(itemViewModel);
        }
    }
}
