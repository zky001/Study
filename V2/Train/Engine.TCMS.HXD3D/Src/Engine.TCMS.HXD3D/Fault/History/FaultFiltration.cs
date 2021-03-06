﻿using System;
using MMI.Facility.Interface.Attribute;

namespace Engine.TCMS.HXD3D.Fault.History
{
    [GksDataType(DataType.isMMIObjectClass)]
    internal class FaultFiltration : FaultHistoryBase
    {
        protected override void Initalize()
        {
            base.Initalize();

            Filter = d => d.MsgOverTime == DateTime.MinValue;
        }
    }
}