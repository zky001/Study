﻿#region 文件说明
/*--------------------------------------------------------------------------------------------------
 * 
 * Copyright(C) 成都运达科技股份有限公司
 * 创建人：唐林
 * 创建时间：2014-7-1
 * 
 * -------------------------------------------------------------------------------------------------
 * 
 * 功能描述：公共组件-No.1-切换视图按钮
 * 
 *-------------------------------------------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using LightRail.Ethiopia.TMS.Common;
using LightRail.Ethiopia.TMS.Control;
using LightRail.Ethiopia.TMS.Control.Common;

namespace LightRail.Ethiopia.TMS.HVAC
{
    /// <summary>
    /// 功能描述：公共组件-No.1-切换视图按钮
    /// 创建人：唐林
    /// 创建时间：2014-7-1
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    public class V7_HVAC_MainView : baseClass
    {
        #region 私有变量
        private ViewState _currentViewState;                      //当前视图
        private List<Image> _resource_Image = new List<Image>();    //图片资源
        private List<Button> _btns_TabView = new List<Button>();//按钮列表
        private Button _btn_Quit;
        #endregion

        #region 框架初始化函数
        /// <summary>
        /// 获取组件描述信息
        /// </summary>
        /// <returns>组件描述信息</returns>
        public override string GetInfo()
        {
            return "公共试图-视图切换按钮";
        }


        /// <summary>
        /// 初始化函数：导入图片资源与创建自定义控件
        /// </summary>
        /// <param name="nErrorObjectIndex"></param>
        /// <returns></returns>
        public override bool init(ref int nErrorObjectIndex)
        {
            UIObj.ParaList.ForEach(a =>
            {
                using (FileStream fs = new FileStream(Path.Combine(RecPath,a), FileMode.Open))
                {
                    _resource_Image.Add(Image.FromStream(fs));
                }
            });

            String[] _strs_TabView = new String[] { "HVAC", "HVAC Paremset", "HVAC Test" };
            float temp = 6;
            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button(
                    _strs_TabView[i],
                    new RectangleF(temp, 552, _resource_Image[i * 2].Width, 38),
                    i,
                    new ButtonStyle()
                    {
                        FontStyle = new FontStyle_ES()
                        {
                            Font = new Font("Verdana", 9, FontStyle.Bold),
                            TextBrush = new SolidBrush(Color.Yellow),
                            StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                        },
                        Background = _resource_Image[i * 2],
                        DownImage = _resource_Image[i * 2 + 1]
                    },
                    false
                    );
                temp += (_resource_Image[i * 2].Width + 13);
                btn.MouseDownEvent += btn_MouseDownEvent;
                btn.ClickEvent += btn_ClickEvent;
                _btns_TabView.Add(btn);
            }
            _btns_TabView[0].IsReplication = false;
            ((ButtonStyle)_btns_TabView[0].Style).FontStyle.TextBrush = new SolidBrush(Color.Black);

            _btn_Quit = new Button(
                "QUIT",
                new RectangleF(722, 552, 69, 38),
                0,
                new ButtonStyle()
                {
                    FontStyle = new FontStyle_ES()
                    {
                        Font = new Font("Verdana", 9, FontStyle.Bold),
                        TextBrush = new SolidBrush(Color.Yellow),
                        StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                    },
                    Background = _resource_Image[6],
                    DownImage = _resource_Image[7]
                }
                );
            _btn_Quit.MouseDownEvent += btn_Quit_MouseDownEvent;
            _btn_Quit.ClickEvent += btn_Quit_ClickEvent;

            return true;
        }

        /// <summary>
        /// 获取到当前运行视图：根据当前视图设置按钮状态
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                _btns_TabView.FindAll(a => a.ID != nParaB-701).ForEach(b =>
                {
                    b.IsReplication = true;
                    ((ButtonStyle)b.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);

                });
                _currentViewState = (ViewState)nParaB;
                switch ((ViewState)nParaB)
                {
                    case ViewState.HVAC_State:
                        _btns_TabView[0].IsReplication = false;
                        ((ButtonStyle)_btns_TabView[0].Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
                        break;
                    case ViewState.HVAC_Paremset:
                        _btns_TabView[1].IsReplication = false;
                        ((ButtonStyle)_btns_TabView[1].Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
                        break;
                    case ViewState.HVAC_Test:
                        _btns_TabView[2].IsReplication = false;
                        ((ButtonStyle)_btns_TabView[2].Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
                        break;
                }
            }
        }

        public override void paint(Graphics dcGs)
        {
            _btns_TabView.ForEach(a => a.Paint(dcGs));
            _btn_Quit.Paint(dcGs);

            dcGs.DrawLine(new Pen(new SolidBrush(Color.White)), new PointF(0, 548), new PointF(800, 548));

            base.paint(dcGs);
        }

        #endregion

        #region 鼠标事件
        public override bool mouseDown(Point nPoint)
        {
            _btns_TabView.ToList().ForEach(a => a.MouseDown(nPoint));
            _btn_Quit.MouseDown(nPoint);

            return base.mouseDown(nPoint);
        }

        public override bool mouseUp(Point nPoint)
        {
            _btns_TabView.ToList().ForEach(a => a.MouseUp(nPoint));
            _btn_Quit.MouseUp(nPoint);

            return base.mouseUp(nPoint);
        }

        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Quit_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Quit_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            append_postCmd(CmdType.ChangePage, 1, 0, 0);
            ((ButtonStyle)((Button)sender).Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);
        }

        /// <summary>
        /// 菜单切换按钮鼠标按下事件响应函数：改变按钮字体颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MouseDownEvent(object sender, ClickEventArgs<int> e)
        {
            ((ButtonStyle)_btns_TabView[e.Message].Style).FontStyle.TextBrush = new SolidBrush(Color.Black);
        }

        /// <summary>
        /// 菜单切换按钮点击事件响应函数：实现视图的切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ClickEvent(object sender, ClickEventArgs<int> e)
        {
            _btns_TabView.FindAll(a => a.ID != e.Message).ForEach(b =>
            {
                b.IsReplication = true;
                ((ButtonStyle)b.Style).FontStyle.TextBrush = new SolidBrush(Color.Yellow);

            });
            _btns_TabView[e.Message].IsReplication = false;

            append_postCmd(
                CmdType.ChangePage, 
                701+e.Message,
                0,
                0
                );
        }
        #endregion
    }
}
