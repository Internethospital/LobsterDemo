
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EfwControls.CustomControl
{
    /// <summary>
    /// 图标控件
    /// </summary>
    public class TextBoxImage: DevComponents.DotNetBar.Controls.TextBoxX
    {
        TextBoxImageShowCard showCard;
        public TextBoxImage()
        {
            Init();
        }

        public TextBoxImage(IContainer container)
        {
            container.Add(this);
            Init();
        } 

        private void Init()
        {
            this.BackColor = Color.White;
            this.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty;
            this.WatermarkColor = System.Drawing.SystemColors.ControlText;
            this.WatermarkImage = EfwControls.Properties.Resources.刷新;
            this.WatermarkText = "<选择图标>";
            this.ReadOnly = true;
            CreateDropDownButtonImage();
            this.ButtonCustomClick += new EventHandler(TextBoxEr_ButtonCustomClick);

            Action<Image, String,int> selectedImage= ((Image img,String name,int id) =>
            {
                //选定图标后
                this.WatermarkImage = img;
                this.WatermarkText = name;

                _selectedValue = id;
                _selectedText = name;
                _selectedImage = img;
            });
            showCard = new TextBoxImageShowCard(selectedImage);
            showCard.Visible = false;
        }
        /// <summary>
        /// 创建下拉按钮
        /// </summary>
        private void CreateDropDownButtonImage()
        {
            //创建下拉箭头按钮图像
            Bitmap img = new Bitmap(9, 5);
            Graphics grp = Graphics.FromImage(img);
            grp.SmoothingMode = SmoothingMode.HighQuality;
            grp.PixelOffsetMode = PixelOffsetMode.None;
            Pen pen = new Pen(Color.FromArgb(21, 66, 139), 1);
            grp.DrawLine(pen, 2, 1, 6, 1);
            grp.DrawLine(pen, 3, 2, 5, 2);
            grp.Dispose();
            img.SetPixel(4, 3, Color.FromArgb(21, 66, 139));
            this.ButtonCustom.Image = img;
            this.ButtonCustom.Visible = true;
            this.ButtonCustom.Enabled = true;
        }


        private void TextBoxEr_ButtonCustomClick(object sender, EventArgs e)
        {
            if (showCard.Visible)
            {
                showCard.Hide();
            }
            else
            {
                loadDefaultDataSource();
                SetSelectionCardLocation();
                showCard.Show();
                showCard.ShowImageList(_source, _typeData);
                BindParentEvent();//点击其他控件将隐藏ShowCard
            }
        }

        #region 设置ShowCard的显示位置
        /// <summary>
        /// 设置ShowCard的显示位置
        /// </summary>
        private void SetSelectionCardLocation()
        {
            /*
             * 以控件所在的窗体为参照对象定位选项卡位置
             */
            int x = this.Left;
            int y = this.Top + this.Height;

            System.Windows.Forms.Control ctrl = this.Parent;
            if (this.Parent == null)
                return;

            this.FindForm().AutoValidate = AutoValidate.Disable;
            if (!this.FindForm().Controls.Contains(showCard))
                this.FindForm().Controls.Add(showCard);

            //showCard.Width = ShowCardWidth < this.Width ? this.Width : ShowCardWidth;
            //showCard.Height = ShowCardHeight < 260 ? 260 : ShowCardHeight;

            Point location = new Point();
            location = this.Parent.PointToScreen(this.Location);
            location = this.FindForm().PointToClient(location);
            if (location.Y + showCard.Height < this.FindForm().Height - 40)
            {
                location.Y = location.Y + this.Height;
            }
            else
            {
                location.Y = location.Y - showCard.Height;
                if (location.Y < 0)
                {
                    showCard.Height = showCard.Height + location.Y;
                    location.Y = 0;
                }
            }
            showCard.Top = location.Y;
            Rectangle scrRect = Screen.GetBounds(this);
            if (location.X + showCard.Width < scrRect.Width)
            {
                showCard.Left = location.X;
            }
            else
            {
                showCard.Left = location.X + (scrRect.Width - (location.X + showCard.Width));
            }
            showCard.BringToFront();
        }
        #endregion

        #region 点击外部控件将隐藏ShowCard
        private void MouseDown_Hide(object sender, EventArgs e)
        {
            showCard.Hide();
        }

        private void EnumControls(Control ctrl, bool _isadd)
        {
            foreach (Control c in ctrl.Controls)
            {
                if (c.Visible == false) continue;
                if (c.Equals(showCard)) continue;//如果是弹出网格就不注册鼠标点击离开事件

                if (_isadd == true)
                    c.MouseDown += new MouseEventHandler(MouseDown_Hide);
                else
                    c.MouseDown -= new MouseEventHandler(MouseDown_Hide);
                EnumControls(c, _isadd);
            }
        }

        private void CleanParentEvent(Control _TargetControl)
        {
            Control ctrl = _TargetControl.FindForm();
            EnumControls(ctrl, false);
            ctrl.MouseDown -= new MouseEventHandler(MouseDown_Hide);
        }
        private void BindParentEvent()
        {
            Control _TargetControl = this;

            CleanParentEvent(_TargetControl);
            Control ctrl = _TargetControl.FindForm();
            EnumControls(ctrl, true);
            ctrl.MouseDown += new MouseEventHandler(MouseDown_Hide);
            //目标控件不注册鼠标点击事件
            _TargetControl.MouseDown -= new MouseEventHandler(MouseDown_Hide);
        }
        #endregion

        #region 自定义属性，加载数据源、获取选定图标、根据图标编码设置图标
        /// <summary>
        /// 加载默认数据源
        /// </summary>
        private void loadDefaultDataSource()
        {
            if (_source == null)
            {
                Action<ClientRequestData> _requestAction = ((ClientRequestData request) =>
                {
                    request.LoginRight = new EFWCoreLib.CoreFrame.Business.SysLoginRight(1);
                });

                ClientLink wcfClientLink = new ClientLink("MainFrame.Service");
                wcfClientLink.CreateConnection();
                ServiceResponseData retData = wcfClientLink.Request("IconController", "GetTypeData", _requestAction);
                _typeData = retData.GetData<DataTable>(0);
                retData = wcfClientLink.Request("IconController", "GetTextBoxImageDataSource", _requestAction);
                _source = retData.GetData<DataTable>(0);
                wcfClientLink.UnConnection();
            }
        }
        private Image getImage(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            System.Drawing.Image result = System.Drawing.Image.FromStream(ms);
            ms.Close();
            return result;
        }
        private int _selectedValue;
        [Description("选定图标编码")]
        public int SelectedValue
        {
            get { return _selectedValue; }
            set {
                _selectedValue = value;
                if (_source != null)
                {
                    DataRow[] drs= _source.Select("EnumValue='" + _selectedValue + "'");
                    if (drs.Length > 0)
                    {
                        //选定图标后
                        this.WatermarkImage = getImage((byte[])drs[0]["Format"]);
                        this.WatermarkText = drs[0]["IconTitle"].ToString();

                        _selectedText = drs[0]["IconTitle"].ToString();
                        _selectedImage = getImage((byte[])drs[0]["Format"]);
                    }
                }
            }
        }

        private string _selectedText;
        [Description("选定图标名称")]
        public string SelectedText
        {
            get { return _selectedText; }
        }

        private Image _selectedImage;
        [Description("选定图标")]
        public Image SelectedImage
        {
            get { return _selectedImage; }
        }

        //一次性将所有图标加载到控件，不做分页查询
        private DataTable _source;
        [Description("图标数据源，列名：Type,EnumValue,IconTitle,Format")]
        public DataTable ImageDataSource
        {
            set { _source = value; }
        }

        private DataTable _typeData;
        [Description("业务系统，列名：ID,Name")]
        public DataTable TypeData
        {
            get { return _typeData; }
            set { _typeData = value; }
        }

        #endregion

        #region 按键设定，上下键移动网格、翻页键网格翻页、Esc键隐藏、回车键选定、鼠标滚动网格上下移动
        //3.上下移动和翻页
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (this.Focused == true && showCard.Visible == true)
            {
                #region 上下键移动
                if (e.KeyCode == Keys.Up)
                {
                    showCard.GridMoveUpDown(0);
                    //e.Handled = true;
                    //return;
                }
                else if(e.KeyCode == Keys.Down)
                {
                    showCard.GridMoveUpDown(1);
                    //e.Handled = true;
                    //return;
                }
                #endregion

                #region page翻页
                if (e.KeyCode == Keys.PageUp)
                {
                    showCard.GridPageUpDown(1);
                }
                else if (e.KeyCode == Keys.PageDown)
                {
                    showCard.GridPageUpDown(0);
                }
                #endregion
            }

            base.OnKeyDown(e);
        }

        //4.Esc隐藏ShowCard，回车选择行记录，数字键选择记录
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            int keyAsc = (int)e.KeyChar;

            if (keyAsc == 27 || keyAsc == 13)
            {
                //Esc取消
                if (keyAsc == 27 && this.Focused)
                {
                    showCard.Hide();
                }
                //回车选择数据
                if (keyAsc == 13 && this.Focused && showCard.Visible == true)
                {
                    showCard.SelectedGrid();
                }
                //选好数据回车跳转到下一个控件
                else if (keyAsc == 13 && this.Focused)
                {
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;
                }
                e.Handled = false;
                base.OnKeyPress(e);
                return;
            }

            base.OnKeyPress(e);
        }
        //鼠标滚动
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (this.Focused == true && showCard.Visible == true)
            {
                int val = e.Delta;
                if (val > 0)//上滚
                {
                    showCard.GridMoveUpDown(0);
                }
                else//下滚
                {
                    showCard.GridMoveUpDown(1);
                }
            }
            base.OnMouseWheel(e);
        }

        #endregion

    }
}
