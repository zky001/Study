﻿using System.ComponentModel.Composition;
using Engine.TCMS.HXD3.Resource.Keys;

namespace Engine.TCMS.HXD3.Controller.BtnActionResponser
{
    [Export]
    public class ChangToRepairStateDistanceActionResponser : BtnActionResponserBase
    {
        /// <summary>
        /// 响应按键
        /// </summary>
        public override void ResponseClick()
        {
            NavigateTo(StateKeys.Root_检修状态_状态_累计距离);
        }
    }
}