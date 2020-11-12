using DecimationIndex.Core.RVector;
using System.Collections.Generic;

namespace DecimationIndex.Ui.ViewModels
{
	public class RVectorViewModel
	{
		public RVectorViewModel(IRVector rVector)
		{
			InitialList = GetListString(rVector.InitialList, "Изначальный вектор R: ");
			FilteredList = GetListString(rVector.FilteredList, "НОД = 1 : ");
			ThinnedList = GetListString(rVector.ThinnedList, "Прореженный вектор R: ");
			PBasisList = GetListString(rVector.PBasisList, "Вектор R в p-ичной системе счисления: ");
			GofRVector = GetListString(rVector.GofRVector, "Вектор функций g(r): ");

			CArray = GetListString(rVector.CList, "Массив C: ");
			C1Array = GetListString(rVector.C1List, "Массив C1: ");
			C2Array = GetListString(rVector.C2List, "Массив C2: ");
			C3Array = GetListString(rVector.C3List, "Массив C3: ");
		}

		public string InitialList { get; }
		public string FilteredList { get; }
		public string ThinnedList { get; }
		public string PBasisList { get; }
		public string GofRVector { get; }

		public string CArray { get; }
		public string C1Array { get; }
		public string C2Array { get; }
		public string C3Array { get; }

		private static string GetListString<T>(IEnumerable<T> list, string predicate)
		{
			var result = predicate + "{ ";

			result += string.Join(", ", list);

			result += "}";

			return result;
		}
	}
}