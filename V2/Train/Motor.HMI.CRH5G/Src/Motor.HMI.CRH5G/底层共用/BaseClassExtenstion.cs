﻿using System;
using MMI.Facility.Interface;

namespace Motor.HMI.CRH5G.底层共用
{
    public static class BaseClassExtenstion
    {
        public static baseClass FindNeighbourObject(this baseClass obj, Type type)
        {
            return ObjectManager.Instance.FindObjcet(obj, type);
        }

        public static T FindNeighbourObject<T>(this baseClass obj) where T : CRH5GBase
        {
            return ObjectManager.Instance.FindObjcet<T>(obj);
        }
    }
}