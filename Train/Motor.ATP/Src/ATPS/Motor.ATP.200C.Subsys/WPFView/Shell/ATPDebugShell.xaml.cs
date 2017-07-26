﻿using System.Windows;
using Motor.ATP.Infrasturcture.Monitor;
using Motor.ATP._200C.Subsys.Model;

namespace Motor.ATP._200C.Subsys.WPFView.Shell
{
    /// <summary>
    /// ATPDebugShell.xaml 的交互逻辑
    /// </summary>
    public partial class ATPDebugShell 
    {
        private readonly ATP200CDomainModel m_Domain;

        public ATPDebugShell()
        {
            InitializeComponent();

            m_Domain = new ATP200CDomainModel(null);

            DataContext = m_Domain;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var monitor = new Window()
            {
                Width = 400,
                Height = 300,
                Owner = this,
                Content = new DomainView() {DataContext = DataContext}
            };
            monitor.Show();
        }
    }
}
