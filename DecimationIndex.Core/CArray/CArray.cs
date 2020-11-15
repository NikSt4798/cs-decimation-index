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
			
			CList = FormCArray();
			C1List = FormC1Array(CList);
			C2List = FormC2Array(C1List);
			C3List = FormC3Array(C2List);
			CBasisList = FormCBasisArray(C3List);
		}

		public IList<int> CList { get; }
		public IList<int> C1List { get; }
		public IList<int> C2List { get; }
		public IList<int> C3List { get; }
		public IList<string> CBasisList { get; }

		public int Basis => _p;

		private IList<int> FormCArray()
		{
			int l;

			if (_p == 2)
			{
				l = (int) ((Math.Pow(_p, _s) - 1) / (Math.Pow(2, _m) - 1));

				var array1 = new int[l];

				for (var i = 0; i < l; i++)
				{
					array1[i] = (int)(_r + 2 * i * (Math.Pow(2, _m) - 1));
				}

				return array1.ToList();
			}
			else
			{
				l = (int) ((Math.Pow(_p, _s) - 1) / (Math.Pow(_p, _m) - 1));

				var array2 = new int[l];

				for (var i = 0; i < l; i++)
				{
					array2[i] = (int)(_r + i * (Math.Pow(_p, _m) - 1));
				}

				return array2.ToList();
			}
		}

		private IList<int> FormC1Array(IEnumerable<int> array)
		{
			var result = new List<int>();
			var g_r = _r.GetPBasis(_p).GofR();

			foreach (var r in array)
			{
				if(r.GetPBasis(_p).GofR() == g_r)
					result.Add(r);
			}

			return result;
		}

		private IList<int> FormC2Array(IEnumerable<int> list)
		{
			var result = new List<int>();

			foreach (var value in list)
			{
				var array = new int[_s];

				array[0] = value;

				for (var i = 1; i < _s; i++)
				{
					array[i] = array[i-1]*_p % ((int)Math.Pow(_p, _s) - 1);
				}
				
				result.Add(array.Min());
			}

			return result;
		}

		private IList<int> FormC3Array(IEnumerable<int> array)
		{
			var result = new List<int>();

			foreach (var value in array)
			{
				if(!result.Contains(value))
					result.Add(value);
			}

			result.Sort();

			return result;
		}

		private IList<string> FormCBasisArray(IEnumerable<int> array)
		{
			var result = new List<string>();

			foreach (var value in array)
			{
				result.Add(value.GetPBasis(_p));
			}

			return result;
		}
	}
}