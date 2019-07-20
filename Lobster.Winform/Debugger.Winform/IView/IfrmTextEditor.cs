using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Debugger.Winform.IView
{
    public interface IfrmTextEditor : IBaseView
    {
        
        /// <summary>
        /// 设置工具栏是否显示
        /// </summary>
        bool VisibleTool { set; }
        /// <summary>
        /// 编辑状态 true：编辑中 false：已保存
        /// </summary>
        bool EditState { get; set; }
        /// <summary>
        /// 导入文本内容
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        bool LoadText(string text);

        Action<string> EditStateEvent { get; set; }
        /// <summary>
        /// 打开文件
        /// </summary>
        void OpenFile();
        /// <summary>
        /// 保存文件
        /// </summary>
        void SaveFile();
        /// <summary>
        /// 剪切
        /// </summary>
        void CutText();
        /// <summary>
        /// 复制
        /// </summary>
        void CopyText();
        /// <summary>
        /// 粘贴
        /// </summary>
        void PasteText();
        /// <summary>
        /// 删除
        /// </summary>
        void DeleteText();
        /// <summary>
        /// 撤销
        /// </summary>
        void UndoText();
        /// <summary>
        /// 重做
        /// </summary>
        void RedoText();
    }
}
