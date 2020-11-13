using DecimationIndex.Core.CArray;
using DecimationIndex.Ui.Helpers;

namespace DecimationIndex.Ui.ViewModels
{
	public class CArrayViewModel
	{
		public CArrayViewModel(ICArray cArray)
		{
			CArray =  cArray.CList.GetListString("Массив C: ");
			C1Array = cArray.C1List.GetListString("Массив C1: ");
			C2Array = cArray.C2List.GetListString("Массив C2: ");
			C3Array = cArray.C3List.GetListString("Индексы децимации: ");
			CBasisArray = cArray.CBasisList.GetListString("Индексы децимации в p-ичной системе счисления: ");

			MValue = $"Объем М массива : {cArray.C3List.Count}";
		}

		public string CArray { get; }
		public string C1Array { get; }
		public string C2Array { get; }
		public string C3Array { get; }
		public string CBasisArray { get; }
		public string MValue { get; }
	}
}