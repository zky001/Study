using System.Linq;
using General.CIR.Models.Units;

namespace General.CIR.Commands.ScreenBtnResponse
{
	public class SerchNumber2Response : BtnResponseBase
	{
		

		public override void ClickUp()
		{
			SerchItem serchItem = ViewModel.DispatchCommandViewModel.AllSerchItems.FirstOrDefault(f=>f.Index==2);
			bool flag = serchItem != null;
			if (flag)
			{
				ViewModel.DispatchCommandViewModel.SelectSerchItem = serchItem;
			}
		}

		public override void ClickDown()
		{
		}
	}
}