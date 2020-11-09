﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DecimationIndex.Core
{
	public class RVector
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

			if (!GetDividers(s).Contains(m))
			{
				throw new ArgumentOutOfRangeException(nameof(m),$"{nameof(m)} must be among the dividers of {nameof(s)} ");
			}

			_p = p;
			_m = m;
			_s = s;
			_n = s / m;

			GetVectorComponents();
		}

		/// <summary>
		/// Изначальный список целых чисел
		/// </summary>
		public List<int> InitialList { get; } = new List<int>();

		/// <summary>
		/// Список чисел с НОД = 1
		/// </summary>
		public List<int> FilteredList { get; } = new List<int>();

		/// <summary>
		/// Прореженный список по p-сопряженным элементам (вектор R)
		/// </summary>
		public List<int> ThinnedList { get; private set; }

		/// <summary>
		/// Вектор R в p-ичной системе счисления
		/// </summary>
		public List<string> PBasisList { get; private set; }

		/// <summary>
		/// Набор g(r) для всех значений вектора R
		/// </summary>
		public List<int> GofRVector { get; private set; }

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
				if (GetNod((int)Math.Pow(2, _m) - 1, number) == 1)
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
		/// Возвращает все делители числа s
		/// </summary>
		private List<int> GetDividers(int s)
		{
			var list = new List<int>();

			for (var i = 1; i * i <= s; i++)
			{
				if(s % i == 0)
					list.Add(i);
			}

			return list;
		}

		/// <summary>
		/// Рекурсивно находит наименьший общий делитель между двумя числами
		/// </summary>
		private int GetNod(int val1, int val2)
		{
			if (val2 == 0)
				return val1;
			else
				return GetNod(val2, val1 % val2);
		}

		/// <summary>
		/// Выполняет прореживание по p-сопряженным элементам
		/// </summary>
		private List<int> PConjugateThinning(List<int> list)
		{
			var thinnedList = new List<int>();

			foreach (var a in list)
			{
				var b = a * 3 % _period;
				var c = b * 3 % _period;

				var array = new[] {a, b, c};

				if(!thinnedList.Contains(array.Min()))
				{
					thinnedList.Add(array.Min());
				}
			}

			return thinnedList;
		}

		/// <summary>
		/// Вычисляет функцию g(r) для каждого значения в списке. Функция численно равна сумме позиций p-ичного представления числа r.
		/// </summary>
		private List<string> GetPBasisList(List<int> list, int p)
		{
			var result = new List<string>();

			foreach (var r in list)
			{
				var r_p = GetPBasis(r, p);

				result.Add(r_p);
			}

			return result;
		}

		/// <summary>
		/// Предтавляет число в p-чной системе счисления
		/// </summary>
		private string GetPBasis(int number, int p)
		{
			var result = string.Empty;

			while (number != 0)
			{
				int digit;
				
				if (number / p != 0)
					digit = number / p;
				else
					digit = number % p;

				if (digit >= 10)
					result += GetSymbol(digit);
				else
					result += digit.ToString();

				number /= p;
			}

			return result;
		}

		/// <summary>
		/// Возвращает букву для систем счисления больше 10-чной
		/// </summary>
		private char GetSymbol(int digit)
		{
			switch (digit)
			{
				case 10:
					return 'A';
				case 11:
					return 'B';
				case 12:
					return 'C';
				default:
					throw new ArgumentOutOfRangeException(nameof(digit),"Основание не может быть больше 13");
			}
		}

		/// <summary>
		/// Применяет функцию g(r) ко всем элементам списка
		/// </summary>
		private List<int> GetGofRVector(List<string> vector)
		{
			var result = new List<int>();

			foreach (var r in vector)
			{
				result.Add(GofR(r));
			}

			return result;
		}

		/// <summary>
		/// Вычисляет сумму позиций p-ичного представления числа r
		/// </summary>
		private int GofR(string r)
		{
			var sum = 0;

			foreach (var symbol in r)
			{
				sum += GetDigit(symbol);
			}

			return sum;
		}

		/// <summary>
		/// Возвращает число для систем счисления больше 10-чной
		/// </summary>
		private int GetDigit(char symbol)
		{
			if (int.TryParse(symbol.ToString(), out var digit))
			{
				return digit;
			}

			switch (symbol)
			{
				case 'A':
					return 10;
				case 'B':
					return 11;
				case 'C':
					return 12;
				default:
					throw new ArgumentOutOfRangeException(nameof(symbol),"Основание не может быть больше 13");
			}
		}
	}
}