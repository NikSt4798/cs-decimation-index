using System.Collections.Generic;
using System.Runtime.InteropServices;
using DecimationIndex.Core;

namespace DecimationIndex.Ui.ViewModels
{
	public class RVectorViewModel
	{
		public RVectorViewModel(RVector rVector)
		{
			InitialList = GetListString(rVector.InitialList, "Изначальный вектор R: ");
			FilteredList = GetListString(rVector.FilteredList, "Нод = 1 : ");
			ThinnedList = GetListString(rVector.ThinnedList, "Прореженный вектор R: ");
			PBasisList = GetListString(rVector.PBasisList, "Вектор R в p-ичной системе счисления: ");
			GofRVector = GetListString(rVector.GofRVector, "Вектор функций g(r): ");
		}

		public string InitialList { get; }
		public string FilteredList { get; }
		public string ThinnedList { get; }
		public string PBasisList { get; }
		public string GofRVector { get; }

		private static string GetListString<T>(IEnumerable<T> list, string predicate)
		{
			var result = predicate + "{ ";

			result += string.Join(", ", list);

			result += "}";

			return result;
		}
	}
}