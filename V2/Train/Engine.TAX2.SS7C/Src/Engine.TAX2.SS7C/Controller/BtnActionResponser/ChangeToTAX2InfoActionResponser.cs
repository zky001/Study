﻿using System;
using System.ComponentModel.Composition;
using Engine.TAX2.SS7C.Model.Domain.TrainState.TAX;
using Engine.TAX2.SS7C.Resources.Keys;

namespace Engine.TAX2.SS7C.Controller.BtnActionResponser
{
    [Export]
    public class ChangeToTAX2InfoActionResponser : ChangeToTAX2ActionResponserBase
    {
        public ChangeToTAX2InfoActionResponser()
        {
            StateKey = StateKeys.Root_机车设置_TAX2信息;
            TAXModelBase = new Lazy<TAXModelBase>(() =>  ViewModel.Value.TrainStateViewModel.TAX2ViewModel.CheckWalkViewModel.Model);
        }
    }
}