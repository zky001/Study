﻿using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction
{
    public class EmptyStateProvider : DriverSelectableItemStateProviderBase
    {
        public override void UpdateState()
        {
            Enabled = false;
        }
    }
}