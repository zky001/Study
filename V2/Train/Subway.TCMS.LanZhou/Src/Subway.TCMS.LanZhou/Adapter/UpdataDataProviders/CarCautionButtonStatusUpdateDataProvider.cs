﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using MMI.Facility.Interface.Data;
using Subway.TCMS.LanZhou.Config;
using Subway.TCMS.LanZhou.Config.LineInformation;
using Subway.TCMS.LanZhou.Extension;
using Subway.TCMS.LanZhou.Model.Domain.Car.CarParts;
using Subway.TCMS.LanZhou.Model.Domain.Constant;
using Subway.TCMS.LanZhou.ViewModel;

namespace Subway.TCMS.LanZhou.Adapter.UpdateDataProviders
{
    [Export(typeof(IUpdateDataProvider))]
    class CarCautionButtonStatusUpdateDataProvider : UpdateDataProviderBase
    {
        [ImportingConstructor]
        public CarCautionButtonStatusUpdateDataProvider(IEventAggregator eventAggregator, DomainViewModel viewModel)
            : base(eventAggregator, viewModel)
        {
        }

        public override void UpdateDatas(object sender, CommunicationDataChangedArgs args)
        {
            var d = ViewModel.TrainViewModel.Model.CarModels.Where(w => w.LineInformationDatas != null).Select(s => s.LineInformationDatas.Value.CautionButtonStatus).Where(w => !string.IsNullOrWhiteSpace(w.ItemConfig.CautionButtonNormal));
            ;

            foreach (var it in d)
            {
                it.State = GetGroundConnectState(args, it);
            }
        }

        private BranchInformationStatus GetGroundConnectState(CommunicationDataChangedArgs args, CarItem<BranchInformationStatus, CautionButtonStatusConfig> it)
        {
            if (DataService.ReadService.GetInBoolOf(it.ItemConfig.CautionButtonLineInf))
            {
                return BranchInformationStatus.Branch;
            }
            return BranchInformationStatus.Normal;
        }
    }
}