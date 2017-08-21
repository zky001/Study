﻿/*--------------------------------------------------------------------------------------------------
 *
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：lih
 * 创建时间：2015-08-06
 *
 * -------------------------------------------------------------------------------------------------
 *
 * 功能描述：维护-No.5-能耗信息
 *
 *-------------------------------------------------------------------------------------------------*/

using ES.JCTMS.Common;
using ES.JCTMS.Common.Control;
using MMI.Facility.DataType;
using MMI.Facility.Interface.Attribute;
using SS4B_TMS.Common;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SS4B_TMS.LCUInfo
{
    /// <summary>
    ///     功能描述：维护-No.5-能耗信息
    ///     创建人：lih
    ///     创建时间：2015-08-06
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V805MaintenanceEnergyConsumptionInfo : baseClass
    {
        /// <summary>
        ///     界面绘制
        /// </summary>
        /// <param name="dcGs"></param>
        public override void paint(Graphics dcGs)
        {
            //_listbox.Paint(dcGs);
            //dcGs.DrawString(_strInfos[0], _chineseFont, Brushs.BlackBrush, _strRects[0], FontInfo.SF_LC);
            //dcGs.DrawString(_strInfos[6], _chineseFont, Brushs.BlackBrush, _strRects[1], FontInfo.SF_LC);

            //dcGs.DrawString(_totalStr, _digitFont, Brushs.BlackBrush, _totalRect, FontInfo.SF_LC);

            //base.paint(dcGs);
        }

        private ListBox<EnergyInfo> m_Listbox;
        private Rectangle[] m_StrRects;
        private string[] m_StrInfos;
        private string[] m_RecentMonthStrs;
        private Font m_ChineseFont = new Font("宋体", 18);
        private Font m_DigitFont = new Font("Arial", 18);
        private int m_I = 0;
        private Rectangle m_TotalRect;
        private string m_TotalStr;

        /// <summary>
        ///     获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "维护-能耗信息";
        }

        /// <summary>
        ///     获取组件类型名称
        /// </summary>
        /// <returns>组件类型名称</returns>
        /// <summary>
        ///     初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            m_StrRects = new Rectangle[2];
            m_StrRects[0] = new Rectangle(30, 127, 116, 25);
            m_StrRects[1] = new Rectangle(35, 412, 114, 30);

            m_TotalRect = new Rectangle(129, 412, 90, 30);

            m_StrInfos = new string[7] { "单位:kwh", "能耗类别", "累计", "辅助能耗", "牵引能耗", "再生能耗", "总能耗:" };

            OnGetRecentSixMonth();

            m_Listbox = new ListBox<EnergyInfo>(new RectangleF(25, 152, 740, 237), new List<EnergyInfo>(),
                new ListBoxHeader
                {
                    Text = m_StrInfos[1],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14, FontStyle.Regular),
                    Height = 60,
                    Width = 100,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "EnergyStyle",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = m_RecentMonthStrs[0],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    Height = 60,
                    Width = 80,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "Month1Value",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = m_RecentMonthStrs[1],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    Height = 60,
                    Width = 80,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "Month2Value",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = m_RecentMonthStrs[2],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    Height = 60,
                    Width = 80,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "Month3Value",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = m_RecentMonthStrs[3],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    Height = 60,
                    Width = 80,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "Month4Value",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = m_RecentMonthStrs[4],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    Height = 60,
                    Width = 80,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "Month5Value",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = m_RecentMonthStrs[5],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    Height = 60,
                    Width = 80,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "Month6Value",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                },
                new ListBoxHeader
                {
                    Text = m_StrInfos[2],
                    TextBrush = Brushs.BlackBrush,
                    HeaderFont = new Font("宋体", 14),
                    DataFont = new Font("Arial", 14f),
                    Height = 60,
                    Width = 144,
                    BackgroundBrush = Brushes.Transparent,
                    TProperty = "RowTotal",
                    SF_Data = FontInfo.SfCc,
                    SF_Header = FontInfo.SfCc
                }
                );
            m_Listbox.BackgroundBrush = Brushes.Transparent;
            m_Listbox.GridBrush = Brushes.Black;
            m_Listbox.RowCount = 3;

            var assist = new EnergyInfo { EnergyStyle = m_StrInfos[3] };
            var traction = new EnergyInfo { EnergyStyle = m_StrInfos[4] };
            var regenerate = new EnergyInfo { EnergyStyle = m_StrInfos[5] };
            m_Listbox.DataList.Add(assist);
            m_Listbox.DataList.Add(traction);
            m_Listbox.DataList.Add(regenerate);

            var assistTotal = OnGetRowTotal(assist);
            var tractionTotal = OnGetRowTotal(traction);
            var regenerateTotal = OnGetRowTotal(regenerate);

            var total = assistTotal + tractionTotal + regenerateTotal;
            if (total > 0.0)
            {
                m_TotalStr = total.ToString("0.00");
            }

            return true;
        }

        /// <summary>
        ///     获取从当前日期倒推6个月的年月字符串
        /// </summary>
        /// <returns></returns>
        private bool OnGetRecentSixMonth()
        {
            m_RecentMonthStrs = new string[6] { "", "", "", "", "", "" };

            var curYear = DateTime.Now.Year;
            var curMonth = DateTime.Now.Month;
            for (var j = 0; j < m_RecentMonthStrs.Length; j++)
            {
                if (curMonth < 10)
                {
                    m_RecentMonthStrs[j] = string.Format("{0}-0{1}", curYear, curMonth);
                }
                else
                {
                    m_RecentMonthStrs[j] = string.Format("{0}-{1}", curYear, curMonth);
                }

                curMonth--;
                if (curMonth <= 0)
                {
                    curMonth = 12;
                    curYear--;
                }
            }

            return true;
        }

        /// <summary>
        ///     累计六个月
        /// </summary>
        /// <param name="temp"></param>
        private double OnGetRowTotal(EnergyInfo temp)
        {
            var sum = 0.0;
            double temp1, temp2, temp3, temp4, temp5, temp6 = 0.0;
            double.TryParse(temp.Month1Value, out temp1);
            double.TryParse(temp.Month2Value, out temp2);
            double.TryParse(temp.Month3Value, out temp3);
            double.TryParse(temp.Month4Value, out temp4);
            double.TryParse(temp.Month5Value, out temp5);
            double.TryParse(temp.Month6Value, out temp6);
            sum = temp1 + temp2 + temp3 + temp4 + temp5 + temp6;

            if (sum > 0.0)
            {
                temp.RowTotal = sum.ToString("0.00");
            }

            return sum;
        }
    }
}