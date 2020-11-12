using System;
using System.Collections.Generic;
using System.Linq;

namespace DecimationIndex.Core.CArray
{
	public sealed class CArray : ICArray
	{
		private readonly int _p;
		private readonly int _m;
		private readonly int _n;
		private readonly int _r;
		private readonly int _s;

		public CArray(int p, int m, int n, int r)
		{
			_p = p;
			_m = m;
			_n = n;
			_r = r;
			_s = m * n;

			GetCArray();
		}

		public IList<string> CList { get; private set; }
		public IList<string> C1List { get; private set; }
		public IList<string> C2List { get; private set; }
		public IList<string> C3List { get; private set; }

		private void GetCArray()
		{
			CList = FormCArray();
			C1List = FormC1Array(CList);
			C2List = FormC2Array(C1List);
			C3List = FormC3Array(C2List);
		}

		private IList<string> FormCArray()
		{
			int l;

			if (_p == 2)
			{
				l = (int) ((Math.Pow(_p, _s) - 1) / (Math.Pow(2, _m) - 1) + 1);

				var array1 = new int[l];

				for (var i = 0; i < l; i++)
				{
					array1[i] = (int)(_r + 2 * i * (Math.Pow(2, _m) - 1));
				}

				return array1.Select(element => element.GetPBasis(_p)).ToList();
			}
			else
			{
				l = (int) ((Math.Pow(_p, _s) - 1) / (Math.Pow(_p, _m) - 1) + 1);

				var array2 = new int[l];

				for (var i = 0; i < l; i++)
				{
					array2[i] = (int)(_r + i * (Math.Pow(_p, _m) - 1));
				}

				return array2.Select(element => element.GetPBasis(_p)).ToList();
			}
		}

		private IList<string> FormC1Array(IEnumerable<string> array)
		{
			var result = new List<string>();
			var g_r = _r.GetPBasis(_p).GofR();

			foreach (var r in array)
			{
				if(r.GofR() == g_r)
					result.Add(r);
			}

			return result;
		}

		private IList<string> FormC2Array(IEnumerable<string> list)
		{
			var result = new List<string>();

			foreach (var value in list)
			{
				var array = new int[_s];

				array[0] = value.First().GetDigit();

				for (var i = 1; i < _s; i++)
				{
					array[i] = (int)(i * (_p % (Math.Pow(_p, _s) - 1)));
				}

				result.Add(array.Min().GetSymbol().ToString());
			}

			return result;
		}

		private IList<string> FormC3Array(IEnumerable<string> array)
		{
			var result = new List<string>();

			foreach (var value in array)
			{
				if(!result.Contains(value))
					result.Add(value);
			}

			return result;
		}
	}
}