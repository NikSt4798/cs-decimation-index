using DecimationIndex.Core.CArray;
using DecimationIndex.Core.RVector;

namespace DecimationIndex.Ui.ViewModels
{
	public interface IInfoViewModel
	{
		void InitVector(IRVector rVector);
		void InitArray(ICArray cArray);
	}
}