using DecimationIndex.Core.RVector;
using DecimationIndex.Ui.Helpers;

namespace DecimationIndex.Ui.ViewModels
{
	public class RVectorViewModel
	{
		public RVectorViewModel(IRVector rVector)
		{
			InitialList = rVector.InitialList.GetListString("Изначальный вектор R: ");
			FilteredList = rVector.FilteredList.GetListString("НОД = 1 : ");
			ThinnedList = rVector.ThinnedList.GetListString("Прореженный вектор R: ");
			PBasisList = rVector.PBasisList.GetListString("Вектор R в p-ичной системе счисления: ");
			GofRVector = rVector.GofRVector.GetListString("Вектор функций g(r): ");
		}

		public string InitialList { get; }
		public string FilteredList { get; }
		public string ThinnedList { get; }
		public string PBasisList { get; }
		public string GofRVector { get; }
	}
}