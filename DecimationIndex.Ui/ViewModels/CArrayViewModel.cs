using DecimationIndex.Core;
using DecimationIndex.Core.CArray;
using DecimationIndex.Ui.Items;
using System.Collections.Generic;

namespace DecimationIndex.Ui.ViewModels
{
	public class CArrayViewModel
	{
		public CArrayViewModel(ICArray cArray)
		{

			foreach (var value in cArray.C3List)
			{
				var vector = new Vector
				{
					Value = value,
					BasisValue = value.GetPBasis(cArray.Basis)
				};

				DecimationIndices.Add(vector);
			}
		}

		public IList<Vector> DecimationIndices { get; } = new List<Vector>();
	}
}