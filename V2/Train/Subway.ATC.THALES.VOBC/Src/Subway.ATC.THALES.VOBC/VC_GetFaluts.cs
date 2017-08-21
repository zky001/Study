﻿
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using ES.Facility.Common;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;

namespace Subway.ATC.THALES.VOBC
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class VC_GetFaluts : baseClass
    {
        private int readTxtID = 0;
        private List<FaultInfo> _listFault;

        #region 属性

        /// <summary>
        /// 获取当前故障列表属性
        /// </summary>
        public static List<FaultInfo> CurrentFaults { get; private set; }

        /// <summary>
        /// 获取所有发生故障列表属性
        /// </summary>
        public static List<FaultInfo> AllFaults { get; private set; }

        /// <summary>
        /// 读取或设置当前最高优先级的故障属性
        /// </summary>
        public static FaultInfo CurrentFault { get; set; }

        static VC_GetFaluts()
        {
            CurrentFaults = new List<FaultInfo>();
            AllFaults = new List<FaultInfo>();
        }

        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共视图-获取故障";
        }

        public override bool init(ref int nErrorObjectIndex)
        {
            LoadFaultInfo();   
            return true;
        }

        private void LoadFaultInfo()
        {
            _listFault = new List<FaultInfo>(); //本地故障列表
            var file = Path.Combine(AppPaths.ConfigDirectory, "故障信息.txt");
            var all = File.ReadAllLines(file, Encoding.Default);
            foreach (var cStr in all)
            {
                string[] split = cStr.Split(new char[] {'\t'});
                var fault = new FaultInfo()
                {
                    Logic = Convert.ToInt32(split[0]),
                    Code = split[1],
                    Display = split[4],
                    Grade = Convert.ToInt32(split[3])
                };
                _listFault.Add(fault);
            }
        }
        #endregion

        #region 界面绘制
        public override void paint(Graphics dcGs)
        {
            getFaultValue();

            
        }

        /// <summary>
        /// 获取故障网络值
        /// </summary>
        private void getFaultValue()
        {
            //遍历本地故障列表
            _listFault.ForEach(a =>
            {
                if (BoolList[a.Logic])//发生相应故障
                {
                    if (CurrentFaults == null || CurrentFaults.Count == 0)
                    {
                        FaultInfo fault = new FaultInfo()
                        {
                            Code = a.Code,
                            Display = a.Display,
                            Grade = a.Grade,
                            Logic = a.Logic
                        };
                        CurrentFaults.Insert(0, fault);
                        AllFaults.Insert(0, fault);
                    }
                    else if (CurrentFaults.Find(b => a.Logic == b.Logic) == null)//当前故障中不存在该故障，当前列表添加相应故障，记录发生时间
                    {
                        FaultInfo fault = new FaultInfo()
                        {
                            Code = a.Code,
                            Display = a.Display,
                            Grade = a.Grade,
                            Logic = a.Logic
                        };
                        CurrentFaults.Insert(0, fault);
                        AllFaults.Insert(0, fault);
                    }
                }
                else//故障正常
                {
                    if (CurrentFaults == null || CurrentFaults.Count == 0)
                    {
                        return;
                    }

                    FaultInfo fault = CurrentFaults.Find(b => a.Logic == b.Logic);
                    if (fault != null)//当前故障列表中已有该故障，记录故障结束时间，当前故障列表移除该故障
                    {
                        fault.OverTime = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + " " + DateTime.Now.ToLongTimeString();
                        CurrentFaults.Remove(fault);
                    }
                }
            });

            //当前最高等级故障
            if (CurrentFaults == null || CurrentFaults.Count == 0)
            {
                CurrentFault = null;
                return;
            }

            //获取当前最高优先级故障
            FaultInfo faultClone = CurrentFaults[0];
            for (int i = 0; i < CurrentFaults.Count; i++)
            {
                if (faultClone.Grade < CurrentFaults[i].Grade)
                {
                    faultClone = CurrentFaults[i];
                }
            }
            CurrentFault = faultClone;
        }
        #endregion
    }
}