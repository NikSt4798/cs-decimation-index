using DecimationIndex.Core;
using DecimationIndex.Core.RVector;
using DecimationIndex.Ui.Items;
using System.Collections.Generic;

namespace DecimationIndex.Ui.ViewModels
{
	public class RVectorViewModel
	{
		public RVectorViewModel(IRVector rVector)
		{
			foreach (var value in rVector.Vector)
			{
				var vector = new Vector
				{
					Value = value,
					BasisValue = value.GetPBasis(rVector.Basis)
				};

				RVector.Add(vector);
			}
		}

		public List<Vector> RVector { get; } = new List<Vector>();
	}
}