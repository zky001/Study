﻿using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class F1CTCS3ActionResponser : F1OrdinaryActionResponser
    {
        public F1CTCS3ActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_等级确认取消CTCS3));
        }
    }
}