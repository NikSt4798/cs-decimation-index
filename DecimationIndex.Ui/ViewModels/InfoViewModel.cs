using System.ComponentModel;
using System.Runtime.CompilerServices;
using DecimationIndex.Core.CArray;
using DecimationIndex.Core.RVector;
using DecimationIndex.Ui.Annotations;
using DecimationIndex.Ui.Helpers;

namespace DecimationIndex.Ui.ViewModels
{
	public class InfoViewModel : INotifyPropertyChanged, IInfoViewModel
	{
		public string InitialList { get; private set; }
		public string FilteredList { get; private set; }
		public string ThinnedList { get; private set; }
		public string PBasisList { get; private set; }
		public string GofRVector { get; private set; }

		public string CArray { get; private set; }
		public string C1Array { get; private set; }
		public string C2Array { get; private set; }
		public string C3Array { get; private set; }
		public string CBasisArray { get; private set; }
		public string CVolume { get; private set; }
		public string RVolume { get; private set; }
		
		public void InitVector(IRVector rVector)
		{
			InitialList = rVector.InitialList.GetListString("Изначальный вектор R: ");
			FilteredList = rVector.FilteredList.GetListString("НОД = 1 : ");
			ThinnedList = rVector.ThinnedList.GetListString("Прореженный вектор R: ");
			PBasisList = rVector.PBasisList.GetListString("Вектор R в p-ичной системе счисления: ");
			GofRVector = rVector.GofRVector.GetListString("Вектор функций g(r): ");
			RVolume = $"Объем вектора R: {rVector.ThinnedList.Count}";

			OnPropertyChanged(nameof(InitialList));
			OnPropertyChanged(nameof(FilteredList));
			OnPropertyChanged(nameof(ThinnedList));
			OnPropertyChanged(nameof(PBasisList));
			OnPropertyChanged(nameof(GofRVector));
			OnPropertyChanged(nameof(RVolume));
		}

		public void InitArray(ICArray cArray)
		{
			CArray = cArray.CList.GetListString("Массив C: ");
			C1Array = cArray.C1List.GetListString("Массив C1: ");
			C2Array = cArray.C2List.GetListString("Массив C2: ");
			C3Array = cArray.C3List.GetListString("Индексы децимации: ");
			CBasisArray = cArray.CBasisList.GetListString("Индексы децимации в p-ичной системе счисления: ");
			CVolume = $"Объем вектора индексов децимации М: {cArray.C3List.Count}";

			OnPropertyChanged(nameof(CArray));
			OnPropertyChanged(nameof(C1Array));
			OnPropertyChanged(nameof(C2Array));
			OnPropertyChanged(nameof(C3Array));
			OnPropertyChanged(nameof(CBasisArray));
			OnPropertyChanged(nameof(CVolume));
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