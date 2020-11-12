using System.Collections.Generic;

namespace DecimationIndex.Core
{
	public class MathHelpers
	{
		/// <summary>
		/// Возвращает все делители числа s
		/// </summary>
		public static List<int> GetDividers(int s)
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
	}
}