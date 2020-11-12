using System;
using System.Collections.Generic;

namespace DecimationIndex.Core
{
	public static class MathHelpers
	{
		/// <summary>
		/// Возвращает все делители числа s
		/// </summary>
		public static List<int> GetDividers(this int s)
		{
			var list = new List<int>();

			for (var i = 1; i <= s; i++)
			{
				if(s % i == 0)
					list.Add(i);
			}

			return list;
		}

		/// <summary>
		/// Рекурсивно находит наименьший общий делитель между двумя числами
		/// </summary>
		public static int GetNod(int val1, int val2)
		{
			if (val2 == 0)
				return val1;
			else
				return GetNod(val2, val1 % val2);
		}
		
		/// <summary>
		/// Возвращает букву для систем счисления больше 10-чной
		/// </summary>
		public static char GetSymbol(this int digit)
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
		/// Возвращает число для систем счисления больше 10-чной
		/// </summary>
		public static int GetDigit(this char symbol)
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
		
		/// <summary>
		/// Предтавляет число в p-чной системе счисления
		/// </summary>
		public static string GetPBasis(this int number, int p)
		{
			var result = string.Empty;

			while (number != 0)
			{
				var digit = number % p;

				if (digit >= 10)
					result += digit.GetSymbol();
				else
					result += digit.ToString();

				number /= p;
			}

			return result;
		}

		/// <summary>
		/// Вычисляет сумму позиций p-ичного представления числа r
		/// </summary>
		public static int GofR(this string r)
		{
			var sum = 0;

			foreach (var symbol in r)
			{
				sum += symbol.GetDigit();
			}

			return sum;
		}
	}
}