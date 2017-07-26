﻿using MMI.Facility.Interface;
using MMI.Facility.Interface.Data.Config;
using Motor.ATP.Infrasturcture.Interface.Service;

namespace Motor.ATP.Infrasturcture.Model.Service
{
    public class InterfaceAdapterService : IInterfaceAdapterService
    {

        public IReadOnlyDictionary<string, int> InBoolDictionary { private set; get; }

        public IReadOnlyDictionary<string, int> InFloatDictionary { private set; get; }

        public IReadOnlyDictionary<string, int> OutBoolDictionary { private set; get; }

        public IReadOnlyDictionary<string, int> OutFloatDictionary { private set; get; }

        public void Initalize(IProjectIndexDescriptionConfig descriptionConfig)
        {
            InBoolDictionary = descriptionConfig.InBoolDescriptionDictionary;
            InFloatDictionary = descriptionConfig.InFloatDescriptionDictionary;
            OutBoolDictionary = descriptionConfig.OutBoolDescriptionDictionary;
            OutFloatDictionary = descriptionConfig.OutFloatDescriptionDictionary;
        }
    }
}