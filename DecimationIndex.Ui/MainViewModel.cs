using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using DecimationIndex.Core;
using DecimationIndex.Ui.Annotations;

namespace DecimationIndex.Ui
{
	public class MainViewModel : INotifyPropertyChanged
	{
		public MainViewModel()
		{
			SelectedPValue = AvailablePValues.First();
			SValue = 2;
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

		private void Validate()
		{
			AvailableMValues.Clear();

			var dividers = MathHelpers.GetDividers(SValue);

			foreach (var value in dividers)
			{
				AvailableMValues.Add(value);
			}

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