using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using DecimationIndex.Core;
using DecimationIndex.Core.CArray;
using DecimationIndex.Core.RVector;
using DecimationIndex.Ui.Commands;
using DecimationIndex.Ui.Properties;
using DecimationIndex.Ui.ViewModels;

namespace DecimationIndex.Ui
{
	public class MainViewModel : INotifyPropertyChanged
	{
		public MainViewModel()
		{
			SValue = 2;
			SelectedPValue = AvailablePValues.First();

			CalculateCommand = new RelayCommand(Execute);
		}

		public RelayCommand CalculateCommand { get; set; }

		private RVectorViewModel _rVectorViewModel;
		public RVectorViewModel RVectorViewModel
		{
			get => _rVectorViewModel;
			set
			{
				_rVectorViewModel = value;
				OnPropertyChanged(nameof(RVectorViewModel));
			}
		}

		private CArrayViewModel _cArrayViewModel;
		public CArrayViewModel CArrayViewModel
		{
			get => _cArrayViewModel;
			set
			{
				_cArrayViewModel = value;
				OnPropertyChanged(nameof(CArrayViewModel));
			}
		}

		public IReadOnlyCollection<int> AvailablePValues { get; } = new[] {2, 3, 5, 7, 11, 13};

		private int _selectedPValue;
		public int SelectedPValue
		{
			get => _selectedPValue;
			set
			{
				_selectedPValue = value;
				OnPropertyChanged(nameof(SelectedPValue));
				Validate();
			}
		}

		public ObservableCollection<int> AvailableMValues { get; } = new ObservableCollection<int>();

		private int _selectedMValue;
		public int SelectedMValue
		{
			get => _selectedMValue;
			set
			{
				_selectedMValue = value;
				OnPropertyChanged(nameof(SelectedMValue));
				
				if(SelectedMValue != 0)
					NValue = SValue / SelectedMValue;
			}
		}

		public ObservableCollection<int> AvailableRValues { get; } = new ObservableCollection<int>();

		private int _selectedRValue;
		public int SelectedRValue
		{
			get => _selectedRValue;
			set
			{
				_selectedRValue = value;
				OnPropertyChanged(nameof(SelectedRValue));

				CArrayViewModel = new CArrayViewModel(new CArray(SelectedPValue, SelectedMValue, NValue, SelectedRValue));
			}
		}

		private int _sValue;
		public int SValue
		{
			get => _sValue;
			set
			{
				_sValue = value;
				OnPropertyChanged(nameof(SValue));
				Validate();
			}
		}


		private int _nValue;
		public int NValue
		{
			get => _nValue;
			set
			{
				_nValue = value;
				OnPropertyChanged(nameof(NValue));
			}
		}
		

		private Visibility _sWarningVisibility;
		public Visibility SWarningVisibility
		{
			get => _sWarningVisibility;
			set
			{
				_sWarningVisibility = value;
				OnPropertyChanged(nameof(SWarningVisibility));
			}
		}

		private Visibility _rVisibility = Visibility.Hidden;
		public Visibility RVisibility
		{
			get => _rVisibility;
			set
			{
				_rVisibility = value;
				OnPropertyChanged(nameof(RVisibility));
			}
		}
		
		private void Execute(object obj)
		{
			var rVector = new RVector(SelectedPValue, SelectedMValue, SValue);

			RVectorViewModel = new RVectorViewModel(rVector);

			AvailableRValues.Clear();

			foreach (var item in rVector.ThinnedList)
			{
				AvailableRValues.Add(item);
			}

			SelectedRValue = AvailableRValues.First();
			RVisibility = Visibility.Visible;
		}

		private void Validate()
		{
			AvailableMValues.Clear();

			var dividers = MathHelpers.GetDividers(SValue);

			foreach (var value in dividers)
			{
				AvailableMValues.Add(value);
			}

			SelectedMValue = AvailableMValues.First();

			var period = Math.Pow(SelectedPValue, SValue) - 1;

			SWarningVisibility = period < 10000000 ? Visibility.Collapsed : Visibility.Visible;
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}