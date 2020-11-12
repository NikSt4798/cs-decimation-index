using System;
using System.Collections.Generic;
using System.Linq;

namespace DecimationIndex.Core.RVector
{
	public sealed class RVector : IRVector
	{
		private static readonly int[] AvailableP = {2, 3, 5, 7, 11, 13};
		
		private int _p;
		private int _m;
		private int _n;
		private int _s;

		private int _period => (int) Math.Pow(_p, _s) - 1;
		
		public RVector(int p, int m, int s)
		{
			if (!AvailableP.Contains(p))
			{
				throw new ArgumentOutOfRangeException(nameof(p),$"{nameof(p)} must be in range '2, 3, 5, 7, 11, 13' ");
			}
			
			if (Math.Pow(p, s) - 1 > 10000000)
			{
				throw new ArgumentOutOfRangeException(nameof(s),$"{nameof(p)}^{nameof(s)} - 1 must be less than 10000000 ");
			}

			if (!s.GetDividers().Contains(m))
			{
				throw new ArgumentOutOfRangeException(nameof(m),$"{nameof(m)} must be among the dividers of {nameof(s)} ");
			}

			_p = p;
			_m = m;
			_s = s;
			_n = s / m;

			GetVectorComponents();
		}

		
		public IList<int> InitialList { get; } = new List<int>();

		public IList<int> FilteredList { get; } = new List<int>();

		public IList<int> ThinnedList { get; private set; }

		public IList<string> PBasisList { get; private set; }

		public IList<int> GofRVector { get; private set; }

		public IList<int> CList { get; private set; }

		public IList<int> C1List { get; private set; }

		public IList<int> C2List { get; private set; }

		public IList<int> C3List { get; private set; }

		private void GetVectorComponents()
		{
			//Получаем целые числа в интервале от 1 до p^m-2
			for (var i = 1; i <= (int)Math.Pow(_p, _m) - 2; i++)
			{
				InitialList.Add(i);
			}

			//Оставляем только те, у которых НОД(2^m-1) = 1
			foreach (var number in InitialList)
			{
				if (MathHelpers.GetNod((int)Math.Pow(_p, _m) - 1, number) == 1)
				{
					FilteredList.Add(number);
				}
			}

			//Выполняем прореживание по p-сопряженным элементам
			ThinnedList = PConjugateThinning(FilteredList);

			//Переводим все значения r в p-ичную запись
			PBasisList = GetPBasisList(ThinnedList, _p);

			//Применяем функцию g(r) ко всем компонентам вектора
			GofRVector = GetGofRVector(PBasisList);
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
					array[i] = array[i-1]*_p % ((int)Math.Pow(_p, _m) - 1);
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