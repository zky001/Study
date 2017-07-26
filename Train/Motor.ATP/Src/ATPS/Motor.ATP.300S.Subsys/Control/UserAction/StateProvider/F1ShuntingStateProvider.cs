﻿using System.ComponentModel;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300S.Subsys.Control.UserAction.StateProvider
{
    public class F1ShuntingStateProvider : DriverSelectableItemStateProviderBase
    {
        public override void Initalize(IDriverSelectableItem item)
        {
            base.Initalize(item);
            UpdateEnable();
            ATP.RegionFStateProvier.ShuntingStateProvider.PropertyChanged += ShuntingStateProviderOnPropertyChanged;
        }

        private void ShuntingStateProviderOnPropertyChanged(object sender,
            PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateState();
        }

        private void UpdateEnable()
        {
            Enabled = ATP.RegionFStateProvier.ShuntingStateProvider.Enabled;
        }


        public override void UpdateState()
        {
            UpdateEnable();
            UpdateContent();
        }

        private void UpdateContent()
        {
            ChangedContent = ATP.RegionFStateProvier.ShuntingStateProvider.EnterOrQuitState == EnterOrQuit.Enter ? string.Empty : "�˳�����";
        }
    }
}