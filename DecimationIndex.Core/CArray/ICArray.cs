using System.Collections.Generic;

namespace DecimationIndex.Core.CArray
{
	public interface ICArray
	{
		/// <summary>
		/// Компоненты массива C
		/// </summary>
		IList<int> CList { get; }

		/// <summary>
		/// Компоненты массива C, значение функции g(r) которых равно выбранному r
		/// </summary>
		IList<int> C1List { get; }
		
		/// <summary>
		/// Массив минимальных элементов для каждой p-сопряженной группы
		/// </summary>
		IList<int> C2List { get; }
		
		/// <summary>
		/// Индексы децимации
		/// </summary>
		IList<int> C3List { get; }

		/// <summary>
		/// Индексы децимации в p-ичной системе счисления
		/// </summary>
		IList<string> CBasisList { get; }

		/// <summary>
		/// Основание системы счисления p
		/// </summary>
		int Basis { get; }
	}
}