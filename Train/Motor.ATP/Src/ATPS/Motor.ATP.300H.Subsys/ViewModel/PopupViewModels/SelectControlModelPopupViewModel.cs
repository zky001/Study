using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Resources.Strings;
using Motor.ATP._300H.Subsys.Constant;

//.PopupView;

namespace Motor.ATP._300H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class SelectControlModelPopupViewModel : DriverPopupViewModelBase
    {
        public SelectControlModelPopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleSelectControlModel;
            PopupViewName = PopupContentViewNames.SelectControlModelView;
        }
    }
}