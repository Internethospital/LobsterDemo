using EFWCoreLib.WinformFrame.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LoginWeb.Winform.IView
{
    public interface IfrmBrowserPage: IBaseView
    {
        /// <summary>
        /// 页面标题改变
        /// </summary>
        Action<string> PageTitleChanged { get; set; }
        /// <summary>
        /// 设置URL
        /// </summary>
        /// <param name="url"></param>
        void SetUrl(string url);

        /// <summary>
        /// 获取URL
        /// </summary>
        /// <returns></returns>
        string GetUrl();

        /// <summary>
        /// 显示或者隐藏工具栏 true显示 false隐藏
        /// </summary>
        /// <param name="val"></param>
        void ShowOrHideTools(bool val);

        /// <summary>
        /// 显示模式，Window弹窗
        /// </summary>
        bool IsWindowMode { get; set; }

        /// <summary>
        /// 界面的哈希值
        /// </summary>
        string ViewHashCode { get; }
    }
}
