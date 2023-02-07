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
    public class CarDetailsFormViewModel : ViewModelBase
    {
		private string _modelName;
		public string ModelName
		{
			get
			{
				return _modelName;
			}
			set
			{
				_modelName = value;
				OnPropertyChanged(nameof(ModelName));
				OnPropertyChanged(nameof(CanSubmit));
			}
		}

		private string _year;
		public string Year
		{
			get
			{
				return _year;
			}
			set
			{
				_year = value;
				OnPropertyChanged(nameof(Year));
			}
		}

		private string _country;
		public string Country
		{
			get
			{
				return _country;
			}
			set
			{
				_country = value;
				OnPropertyChanged(nameof(Country));
			}
		}

		private string _type;
        public string Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
				OnPropertyChanged(nameof(Type));
			}
		}

		private bool _isSubmitting;
		public bool IsSubmitting
		{
			get
			{
				return _isSubmitting;
			}
			set
			{
				_isSubmitting = value;
				OnPropertyChanged(nameof(IsSubmitting));
			}
		}
		public bool CanSubmit => !string.IsNullOrEmpty(ModelName);

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

        public ICommand SubmitCommand { get; }

		public ICommand CancelCommand { get; }

        public CarDetailsFormViewModel(ICommand submitCommand, ICommand cancelCommand)
        {
            SubmitCommand = submitCommand;
            CancelCommand = cancelCommand;
        }
    }
}
