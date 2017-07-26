﻿using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using MMI.Facility.Interface.Data.Config;
using MMI.Facility.Interface.Project;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP._300T.Subsys.Model;

namespace Motor.ATP._300T.Subsys.WPFView.Shell
{
    public partial class ShellForm : ProjectFormBase
    {
        public ShellForm()
        {
            InitializeComponent();
        }

        public ShellForm(SubsystemInitParam initParam, IATP atp)
            : this()
        {
            var dataPackage = initParam.DataPackage;
            IAppConfig appconfig = initParam.AppConfig;
            RegionManager.SetRegionManager(elementHost1.HostContainer, ServiceLocator.Current.GetInstance<IRegionManager>());

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/Motor.ATP.300T.Subsys;component/Resources/ATP300Resource.xaml")
            });

            UserControl shellView;
            if (GlobalParam.Instance.InitParam.AppConfig.ActureFormConfig.IsOutterFrameVisible)
            {
                shellView = ServiceLocator.Current.GetInstance<ShellView>();
            }
            else
            {
                shellView = ServiceLocator.Current.GetInstance<ShellContentView>();
            }

            shellView.DataContext = atp;
            elementHost1.Child = shellView;

            AppName = appconfig.AppName;
            AppConfig = appconfig;
            DataPackage = dataPackage;

            elementHost1.HostContainer.MouseEnter += (sender, args) => OnMouseEnter(args);
            elementHost1.HostContainer.MouseLeave += (sender, args) => OnMouseLeave(args);

        }
    }
}
