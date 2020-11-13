using System.Collections.Generic;

namespace DecimationIndex.Ui.Helpers
{
	internal static class ArrayHelper
	{
		public static string GetListString<T>(this IList<T> list, string predicate)
		{
			var result = predicate + "{ ";

			result += string.Join(", ", list);

			result += "}";

			return result;
		}
	}
}