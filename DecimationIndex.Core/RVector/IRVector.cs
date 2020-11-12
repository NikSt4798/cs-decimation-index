﻿using System.Collections.Generic;

namespace DecimationIndex.Core.RVector
{
	public interface IRVector
	{
		/// <summary>
		/// Изначальный список целых чисел
		/// </summary>
		IList<int> InitialList { get; }

		/// <summary>
		/// Список чисел с НОД = 1
		/// </summary>
		IList<int> FilteredList { get; }

		/// <summary>
		/// Прореженный список по p-сопряженным элементам (вектор R)
		/// </summary>
		IList<int> ThinnedList { get; }

		/// <summary>
		/// Вектор R в p-ичной системе счисления
		/// </summary>
		IList<string> PBasisList { get; }

		/// <summary>
		/// Набор g(r) для всех значений вектора R
		/// </summary>
		IList<int> GofRVector { get; }

		IList<int> CList { get; }

		IList<int> C1List { get; }

		IList<int> C2List { get; }

		IList<int> C3List { get; }
	}
}