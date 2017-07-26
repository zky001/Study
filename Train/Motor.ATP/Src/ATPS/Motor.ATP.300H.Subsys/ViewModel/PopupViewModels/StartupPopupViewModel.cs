﻿using System.ComponentModel.Composition;
using Motor.ATP.Infrasturcture.Resources.Strings;

//.PopupView;

namespace Motor.ATP._300H.Subsys.ViewModel.PopupViewModels
{
    [Export]
    public class StartupPopupViewModel : EnsureEventPopupViewModelBase
    {
        public StartupPopupViewModel()
        {
            TitleContent = PopupViewStringKeys.StringTitleEnsureStartUp;
            EnsurceContent = PopupViewStringKeys.StringEnsureStartUpContent;
        }
    }
}