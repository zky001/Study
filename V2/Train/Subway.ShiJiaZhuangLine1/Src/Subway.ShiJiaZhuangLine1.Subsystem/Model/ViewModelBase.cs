﻿using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Subway.ShiJiaZhuangLine1.Interface.Model;

namespace Subway.ShiJiaZhuangLine1.Subsystem.Model
{
    public class ViewModelBase : NotificationObject
    {
        protected IRegionManager RegionManager { private set; get; }

        protected IMMI Parent { private set; get; }

        protected ViewModelBase(IMMI parent)
        {
            Parent = parent;
            RegionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
        }
    }
}