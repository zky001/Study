﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CommonUtil.Util;
using Engine.TAX2.SS7C.Events;
using Engine.TAX2.SS7C.Model;
using Engine.TAX2.SS7C.Model.BtnStragy;
using Engine.TAX2.SS7C.Model.Domain.Constant;
using Engine.TAX2.SS7C.Resources.Keys;
using Engine.TAX2.SS7C.ViewModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using MMI.Facility.WPFInfrastructure.Interactivity;
using MMI.Facility.WPFInfrastructure.Interfaces;

namespace Engine.TAX2.SS7C.Controller
{
    [Export]
    public class SS7CController : ControllerBase<SS7CViewModel>
    {
        public IStateInterfaceFactory StateInterfaceFactory { private set; get; }

        //public ICommand NavigateToCommand { get; private set; }

        private StateInterfaceKey m_CurrentRootStateKey;

        private readonly IRegionManager m_RegionManager;

        private readonly IEventAggregator m_EventAggregator;

        private readonly Stack<StateInterfaceKey> m_NavigateHistory;

        public DelegateCommand<CommandParameter> LoadedCommand { private set; get; }

        public AsyncNavigateToByStateInterfaceKeyEvent AsyncNavigateToEvent { get; private set; }

        public ResetRunningTimeEvent ResetRunningTimeEvent { get; private set; }

        [ImportingConstructor]
        public SS7CController(IEventAggregator eventAggregator, IRegionManager regionManager, IStateInterfaceFactory stateInterfaceFactory) 
        {
            m_CurrentRootStateKey = StateInterfaceKey.Parser(StateKeys.Root);
            m_EventAggregator = eventAggregator;
            m_RegionManager = regionManager;
            StateInterfaceFactory = stateInterfaceFactory;
            m_NavigateHistory = new Stack<StateInterfaceKey>();
            LoadedCommand = new DelegateCommand<CommandParameter>(OnLoaded);
            ResetRunningTimeEvent = eventAggregator.GetEvent<ResetRunningTimeEvent>();
            AsyncNavigateToEvent = eventAggregator.GetEvent<AsyncNavigateToByStateInterfaceKeyEvent>();
            AsyncNavigateToEvent.Subscribe(s => NavigateTo(s.InterfaceKey), ThreadOption.UIThread, true);
        }

        /// <summary>Initalize</summary>
        public override void Initalize()
        {
            ViewModel.Model.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == PropertySupport.ExtractPropertyName<SS7CModel, bool>(s => s.IsVisible))
                {
                    if (ViewModel.Model.IsVisible)
                    {
                        AsyncNavigateToEvent.Publish(new AsyncNavigateToByStateInterfaceKeyEvent.Args(StateInterfaceKey.Root));
                        ResetRunningTimeEvent.Publish(null);
                    }
                }
            };

            SetValueWhenBootstrapper();
        }

        private void SetValueWhenBootstrapper()
        {
            if (GlobalParam.Instance.StartType == StartType.Bootstrapper)
            {
                ViewModel.Model.IsVisible = true;
            }
        }

        private void OnLoaded(CommandParameter obj)
        {
            NavigateToRoot();
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
                if (key.Parent.Paths.Count > 1)
                {
                    if (last != key.Parent)
                    {
                        m_NavigateHistory.Push(key.Parent);
                    }
                    else 
                    {
                        while (key.Parent.Paths.Count < last.Paths.Count)
                        {
                            m_NavigateHistory.Pop();
                            last = m_NavigateHistory.Peek();
                        }
                    }
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
            ViewModel.Model.UpdateCurrentState(StateInterfaceFactory.GetOrCreate(key));
        }

        public void NavigateToRoot()
        {
            NavigateTo(StateInterfaceKey.Root);
        }
    }
}