using System.Collections.Generic;

namespace DecimationIndex.Core.CArray
{
	public interface ICArray
	{
		/// <summary>
		/// Компоненты массива C
		/// </summary>
		IList<string> CList { get; }

		/// <summary>
		/// Компоненты массива C, значение функции g(r) которых равно выбранному r
		/// </summary>
		IList<string> C1List { get; }
		
		/// <summary>
		/// Массив минимальных элементов для каждой p-сопряженной группы
		/// </summary>
		IList<string> C2List { get; }
		
		/// <summary>
		/// Наименьшие уникальные элементы массива С2
		/// </summary>
		IList<string> C3List { get; }
	}
}