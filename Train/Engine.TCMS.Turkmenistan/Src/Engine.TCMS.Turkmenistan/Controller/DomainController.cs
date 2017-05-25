﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Engine.TCMS.Turkmenistan.Event;
using Engine.TCMS.Turkmenistan.Model.BtnStragy;
using Engine.TCMS.Turkmenistan.ViewModel;
using Engine.TCMS.Turkmenistan.Resources.Keys;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Behaviors;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TCMS.Turkmenistan.Controller
{
    [Export]
    public class DomainController : ControllerBase<Lazy<DomainViewModel>>
    {
        public IStateInterfaceFactory StateInterfaceFactory { private set; get; }

        //public ICommand NavigateToCommand { get; private set; }

        private StateInterfaceKey m_CurrentRootStateKey;

        private readonly IRegionManager m_RegionManager;

        private readonly IEventAggregator m_EventAggregator;

        private readonly Stack<StateInterfaceKey> m_NavigateHistory;

        [ImportingConstructor]
        public DomainController(Lazy<DomainViewModel> viewModel, IEventAggregator eventAggregator, IRegionManager regionManager, IStateInterfaceFactory stateInterfaceFactory) : base(viewModel)
        {
            m_CurrentRootStateKey = StateInterfaceKey.Parser(StateKeys.Root_本车信息);
            m_EventAggregator = eventAggregator;
            m_RegionManager = regionManager;
            StateInterfaceFactory = stateInterfaceFactory;
            m_NavigateHistory = new Stack<StateInterfaceKey>();
            m_EventAggregator.GetEvent<NavigatorToState>().Subscribe(NavigateTo, ThreadOption.UIThread);
            m_EventAggregator.GetEvent<NavigatorToView>().Subscribe(NavigateToView, ThreadOption.UIThread);
        }

        private void NavigateToView(NavigatorToView.Args obj)
        {
            var memberInfo = Type.GetType(obj.ViewName);
            if (memberInfo != null) {
                var type = (ViewExportAttribute)memberInfo
                    .GetCustomAttributes(typeof(ViewExportAttribute), false)
                    .FirstOrDefault();
                if (type != null)
                {
                    m_RegionManager.RequestNavigate(type.RegionName, obj.ViewName);
                }
            }
        }

        private void NavigateTo(NavigatorToState.Args obj)
        {
            NavigateTo(obj.Params);
        }


        public void NavigateTo(string stateKey)
        {
            var key = StateInterfaceKey.Parser(stateKey);
            if (key.Paths.Count == 1)
            {
                m_CurrentRootStateKey = key;
                m_NavigateHistory.Clear();
                m_NavigateHistory.Push(key);
            }
            else if (m_NavigateHistory.Any())
            {
                var last = m_NavigateHistory.Peek();
                if (last != key.Parent && key.Parent.Paths.Count > 1)
                {
                    m_NavigateHistory.Push(key.Parent);
                }
            }
            else
            {
                m_NavigateHistory.Push(m_CurrentRootStateKey);
            }

            NavigateTo(key);
        }

        public void NavigateBack()
        {
            if (m_NavigateHistory.Any())
            {
                NavigateTo(m_NavigateHistory.Pop());
            }
        }

        private void NavigateTo(StateInterfaceKey key)
        {
            ViewModel.Value.Model.UpdateCurrentState(StateInterfaceFactory.GetOrCreate(key));
        }
    }
}