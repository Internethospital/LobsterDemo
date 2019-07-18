using DevComponents.DotNetBar;
using efwplusWinform.Common;
using efwplusWinform.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace efwplusWinform.Business
{
    public partial class BaseFormBusiness : Form, IBaseView
    {
        public BaseFormBusiness()
        {
            InitializeComponent();
            //this.Font = new Font("宋体", 9F);
        }


        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        #region IBaseView 成员

        //public event ControllerEventHandler ControllerEvent;

        private ControllerEventHandler _InvokeController;
        public ControllerEventHandler InvokeController
        {
            get
            {
                return _InvokeController;
            }
            set
            {
                _InvokeController = value;
            }
        }
        private string _frmName;
        public string frmName
        {
            get
            {
                return _frmName;
            }
            set
            {
                _frmName = value;
            }
        }
        #endregion


        public virtual void doBarCode(string barCode)
        {

        }

        [Description("打开窗体之前")]
        public event EventHandler OpenWindowBefore;

        [Description("关闭窗体之后")]
        public event EventHandler CloseWindowAfter;

        [Description("退出程序之前")]
        public event EventHandler ExitApplicationBefore;

        public void ExecOpenWindowBefore(object sender, EventArgs e)
        {
            if (OpenWindowBefore != null)
                OpenWindowBefore(sender, e);
        }

        public void ExecCloseWindowAfter(object sender, EventArgs e)
        {
            if (CloseWindowAfter != null)
                CloseWindowAfter(sender, e);
        }

        public void ExecExitApplicationBefore(object sender, EventArgs e)
        {
            if (ExitApplicationBefore != null)
                ExitApplicationBefore(sender, e);
        }

        private Dictionary<string, int> dicIndex = new Dictionary<string, int>();
        /// <summary>
        /// 绑定网格控件的选定索引
        /// </summary>
        /// <param name="grids"></param>
        public void bindGridSelectIndex(params DataGridView[] grids)
        {
            foreach (DataGridView grid in grids)
            {
                if (dicIndex.ContainsKey(grid.Name))
                    continue;
                grid.Click += new EventHandler(delegate (object sender, EventArgs e)
                {
                    if ((sender as DataGridView).CurrentCell != null)
                        dicIndex[(sender as DataGridView).Name] = (sender as DataGridView).CurrentCell.RowIndex;
                    else
                        dicIndex[(sender as DataGridView).Name] = -1;
                });
            }
        }
        /// <summary>
        /// 设置网格控件的上次选定行
        /// </summary>
        /// <param name="grids"></param>
        public void setGridSelectIndex(params DataGridView[] grids)
        {
            foreach (DataGridView grid in grids)
            {
                if (dicIndex.ContainsKey(grid.Name) == false) continue;
                int rowindex = dicIndex[grid.Name];
                int colindex = 0;
                if (rowindex != -1 && rowindex <= (grid.RowCount - 1))
                {
                    for (int i = 0; i < grid.Columns.Count; i++)
                    {
                        if (grid.Columns[i].Visible)
                        {
                            colindex = i;
                            break;
                        }
                    }
                    grid.CurrentCell = grid[colindex, rowindex];
                }
            }
        }
        /// <summary>
        /// 设置网格控件的指定行
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rowindex"></param>
        public void setGridSelectIndex(DataGridView grid, int rowindex)
        {
            int colindex = 0;
            if (rowindex > -1 && rowindex <= (grid.RowCount - 1))
            {
                for (int i = 0; i < grid.Columns.Count; i++)
                {
                    if (grid.Columns[i].Visible)
                    {
                        colindex = i;
                        break;
                    }
                }
                dicIndex[grid.Name] = rowindex;
                grid.CurrentCell = grid[colindex, rowindex];
            }
        }

        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                //case CodeBarInput.WM_IDCARD_INFO:
                //    {
                //        byte[] cInput = new byte[101];
                //        System.Runtime.InteropServices.Marshal.Copy(m.WParam, cInput, 0, 100);
                //        String sInput = System.Text.Encoding.Default.GetString(cInput);
                //        if (sInput.Length > 0)
                //        {
                //        }
                //    }
                //    break;
                //case CodeBarInput.WM_RFID_INFO:
                //    {
                //        byte[] cInput = new byte[101];
                //        System.Runtime.InteropServices.Marshal.Copy(m.WParam, cInput, 0, 100);
                //        String sInput = System.Text.Encoding.Default.GetString(cInput);
                //        if (sInput.Length > 0)
                //        {
                //        }
                //    }
                //    break;
                //case CodeBarInput.WM_SCANNER_INPUT:
                //    {
                //        byte[] cInput = new byte[101];
                //        System.Runtime.InteropServices.Marshal.Copy(m.WParam, cInput, 0, 100);
                //        String sInput = System.Text.Encoding.Default.GetString(cInput);
                //        string barcode = Convert.ToString(sInput).Trim('\0');

                //        if (this.barMainContainer.SelectedDockContainerItem.Tag is EfwControls.CustomControl.BaseFormEx)
                //        {
                //            (this.barMainContainer.SelectedDockContainerItem.Tag as EfwControls.CustomControl.BaseFormEx).doBarCode(barcode);
                //        }
                //    }
                //    break;
                case WindowsAPI.WM_ASYN_INPUT:
                    InvokeController("AsynInitCompleted");
                    InvokeController("AsynInitCompletedForm",frmName);
                    break;
                default:
                    base.DefWndProc(ref m);//调用基类函数处理非自定义消息。
                    break;
            }
        }

        /// <summary>
        /// 简单提示，右下角提示
        /// </summary>
        /// <param name="text"></param>
        public void MessageBoxShowSimple(string text)
        {
            string CaptionText = "";//默认空
            //InvokeController("MessageBoxShowSimple", text);
            DevComponents.DotNetBar.Balloon b = new DevComponents.DotNetBar.Balloon();
            Rectangle r = Screen.GetWorkingArea(this);
            b.Size = new Size(280, 120);
            b.Location = new Point(r.Right - b.Width, r.Bottom - b.Height);
            b.AlertAnimation = eAlertAnimation.BottomToTop;
            b.Style = eBallonStyle.Office2007Alert;
            b.CaptionText = CaptionText;
            b.Font = new Font("宋体", 11f);
            //b.Padding = new System.Windows.Forms.Padding(5);
            b.Text = text;
            //b.AlertAnimation=eAlertAnimation.TopToBottom;
            //b.AutoResize();
            b.AutoClose = true;
            b.AutoCloseTimeOut = 5;
            //b.Owner=this;

            b.Show(false);
        }
        /// <summary>
        /// 异步调用
        /// </summary>
        public void AsynInvoked(Func<Object> beginInvoke, Action<Object> endInvoke)
        {
            try {
                if (beginInvoke != null)
                {
                    ToastNotification.Show(this,
                    "数据正在处理中，请等待...",
                    efwplusWinform.Common.CommonResource.load,
                    0,
                    (eToastGlowColor)(eToastGlowColor.Blue),
                    (eToastPosition)(eToastPosition.TopCenter));

                    this.Enabled = false;

                    using (BackgroundWorker bw = new BackgroundWorker())
                    {
                        bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
                        bw.DoWork += new DoWorkEventHandler(bw_DoWork);
                        bw.RunWorkerAsync(new object[] { beginInvoke, endInvoke });
                    }
                }
            }
            catch(Exception e)
            {
                ToastNotification.Close(this);
                this.Enabled = true;

                throw e;
            }
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;//这里只是简单的把参数当做结果返回，当然您也可以在这里做复杂的处理后，再返回自己想要的结果(这里的操作是在另一个线程上完成的)
            Func<Object> beginInvoke = (Func<Object>)args[0];
            object result = beginInvoke();
            e.Result = new object[] { args[1], result };
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ToastNotification.Close(this);
            this.Enabled = true;

            object[] rst= (object[])e.Result;
            Action<Object> endInvoke = (Action<Object>)rst[0];
            object result = rst[1];
            endInvoke(result);
            //这时后台线程已经完成，并返回了主线程，所以可以直接使用UI控件了
        }
    }

    /// <summary>
    /// 控制器委托
    /// </summary>
    /// <param name="eventname">方法名称</param>
    /// <param name="objs">参数数组</param>
    /// <returns></returns>
    [Serializable]
    public delegate object ControllerEventHandler(string funname, params object[] objs);
}
