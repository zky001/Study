﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MMI.Facility.WPFInfrastructure.Behaviors;
using Urban.Phillippine.View.Constant;

namespace Urban.Phillippine.View.Views.PopUp
{
    /// <summary>
    /// FaultInfomation.xaml 的交互逻辑
    /// </summary>
    [ViewExport(RegionName = RegionNames.PopUpRegion)]
    public partial class FaultInfomation : UserControl
    {
        public FaultInfomation()
        {
            InitializeComponent();
        }
    }
}
