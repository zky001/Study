﻿using System.Windows;

namespace Engine.TPX21F.HXN5B.WPFApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {        /// <summary>引发 <see cref="E:System.Windows.Application.Startup" /> 事件。</summary>
             /// <param name="e">一个包含事件数据的 <see cref="T:System.Windows.StartupEventArgs" />。</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var boot = new HXN5BTPX21FMefBootstrapper();
            boot.Run();
        }
    }
}