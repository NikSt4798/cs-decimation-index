using System;
using System.Collections.Generic;
using System.Linq;

namespace DecimationIndex.Core.RVector
{
	public sealed class RVector : IRVector
	{
		private int _m;

		public RVector(int p, int m)
		{
			Basis = p;
			_m = m;

			GetVectorComponents();
		}

		
		public int Basis { get; }

		public IList<int> InitialList { get; } = new List<int>();

		public IList<int> FilteredList { get; } = new List<int>();

		public IList<int> Vector { get; private set; }

		public IList<string> PBasisVector { get; private set; }

		public IList<int> GofRVector { get; private set; }

		public IList<int> ThinnedList => Vector;

		public IList<string> PBasisList => Vector.Select(x => x.GetPBasis(Basis)).ToList();

		private void GetVectorComponents()
		{
			//Получаем целые числа в интервале от 1 до p^m-2
			for (var i = 1; i <= (int)Math.Pow(Basis, _m) - 1; i++)
			{
				InitialList.Add(i);
			}

			//Оставляем только те, у которых НОД(2^m-1) = 1
			foreach (var number in InitialList)
			{
				if (MathHelpers.GetNod((int)Math.Pow(Basis, _m) - 1, number) == 1)
				{
					FilteredList.Add(number);
				}
			}

			//Выполняем прореживание по p-сопряженным элементам
			Vector = PConjugateThinning(FilteredList);

			//Переводим все значения r в p-ичную запись
			PBasisVector = GetPBasisList(Vector, Basis);

			//Применяем функцию g(r) ко всем компонентам вектора
			GofRVector = GetGofRVector(PBasisVector);
		}
		
		/// <summary>
		/// Выполняет прореживание по p-сопряженным элементам
		/// </summary>
		private IList<int> PConjugateThinning(IEnumerable<int> list)
		{
			var thinnedList = new List<int>();

			foreach (var a in list)
			{
				var array = new int[_m];

				array[0] = a;

				for (var i = 1; i < _m; i++)
				{
					array[i] = array[i-1]*Basis % ((int)Math.Pow(Basis, _m) - 1);
				}

				if(a == array.Min())
				{
					thinnedList.Add(array.Min());
				}
			}

			return thinnedList;
		}

		/// <summary>
		/// Вычисляет функцию g(r) для каждого значения в списке. Функция численно равна сумме позиций p-ичного представления числа r.
		/// </summary>
		private IList<string> GetPBasisList(IEnumerable<int> list, int p)
		{
			var result = new List<string>();

			foreach (var r in list)
			{
				var r_p = r.GetPBasis(p);

				result.Add(r_p);
			}

			return result;
		}

		/// <summary>
		/// Применяет функцию g(r) ко всем элементам списка
		/// </summary>
		private IList<int> GetGofRVector(IEnumerable<string> vector)
		{
			var result = new List<int>();

			foreach (var r in vector)
			{
				result.Add(r.GofR());
			}

			return result;
		}
	}
}