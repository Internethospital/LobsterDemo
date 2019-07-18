using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace EfwControls.CustomControl
{
    internal partial class TextBoxImageShowCard : UserControl
    {
        private Action<Image, String,int> SelectedImage;//选定图标后回调方法
        private DataTable ImageDataSource;//图标数据源
        public TextBoxImageShowCard(Action<Image, String,int> _selectedImages)
        {
            InitializeComponent();
            SelectedImage = _selectedImages;
            txtSearchChar.KeyDown += TxtSearchChar_KeyDown;
            txtSearchChar.KeyPress += TxtSearchChar_KeyPress;
            txtSearchChar.MouseWheel += TxtSearchChar_MouseWheel;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        //点击选定
        private void gridImage_Click(object sender, EventArgs e)
        {
            SelectedGrid();
        }
        
        //按业务系统过滤图标
        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataView dv = new DataView(ImageDataSource);
            dv.RowFilter = "Type = " + cbType.SelectedValue.ToString();
            pagerImage.DataSource = dv.ToTable();
        }
        //按文本检索过滤图标
        private void txtSearchChar_TextChanged(object sender, EventArgs e)
        {
            string key = txtSearchChar.Text;
            DataView dv = new DataView(ImageDataSource);
            if (string.IsNullOrEmpty(key)==false)
            {
                dv.RowFilter = "(Convert(EnumValue, 'System.String') = '" + key + "' or IconTitle like '%" + key + "%')";
            }
            pagerImage.DataSource = dv.ToTable();
        }

        private void gridImage_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (gridImage.Columns[e.ColumnIndex].DataPropertyName == "Format")
            {
                e.Value = getImage((byte[])e.Value);
            }
        }
        private Image getImage(byte[] data)
        {
            MemoryStream ms = new MemoryStream(data);
            System.Drawing.Image result = System.Drawing.Image.FromStream(ms);
            ms.Close();
            return result;
        }

        #region 公共方法，显示网格图标、选定网格、上移下移、翻页
        /// <summary>
        /// 选定网格
        /// </summary>
        public void SelectedGrid()
        {
            if (gridImage.CurrentCell == null) return;

            if (SelectedImage != null)
            {
                DataTable dt = gridImage.DataSource as DataTable;
                Image image = getImage((byte[])dt.Rows[gridImage.CurrentCell.RowIndex]["Format"]);
                string name = dt.Rows[gridImage.CurrentCell.RowIndex]["IconTitle"].ToString();
                int id = Convert.ToInt32(dt.Rows[gridImage.CurrentCell.RowIndex]["EnumValue"]);
                SelectedImage(image, name, id);

                this.Hide();
            }
        }
        /// <summary>
        /// 显示图标
        /// </summary>
        /// <param name="imageDataSource"></param>
        /// <param name="typeData"></param>
        public void ShowImageList(DataTable imageDataSource, DataTable typeData)
        {
            ImageDataSource = imageDataSource;
            pagerImage.DataSource = imageDataSource.Copy();

            cbType.DisplayMember = "Name";
            cbType.ValueMember = "ID";
            cbType.DataSource = typeData;
            cbType.SelectedIndex = 0;
        }
        /// <summary>
        /// 网格上下移动
        /// </summary>
        /// <param name="type">0 下移 1 上移</param>
        public void GridMoveUpDown(int type)
        {
            if (gridImage.CurrentCell == null) return;
            int row = gridImage.CurrentCell.RowIndex;
            int col = gridImage.CurrentCell.ColumnIndex;
            if (type == 0)
            {
                if (row > 0)
                    gridImage.CurrentCell = gridImage[col, row - 1];
            }
            else if (type == 1)
            {
                if (row < gridImage.Rows.Count - 1)
                    gridImage.CurrentCell = gridImage[col, row + 1];
            }
        }
        /// <summary>
        /// 网格翻页
        /// </summary>
        /// <param name="type">0下一页 1上一页</param>
        public void GridPageUpDown(int type)
        {
            if (type == 0)
            {
                pagerImage.pageNo += 1;
            }
            else if (type == 1)
            {
                pagerImage.pageNo -= 1;
            }
        }
        #endregion

        #region 按键设定，上下键移动网格、翻页键网格翻页、Esc键隐藏、回车键选定、鼠标滚动网格上下移动
        //3.上下移动和翻页
        private void TxtSearchChar_KeyDown(object sender, KeyEventArgs e)
        {
            #region 上下键移动
            if (e.KeyCode == Keys.Up)
            {
                GridMoveUpDown(0);
                //e.Handled = true;
                //return;
            }
            else if (e.KeyCode == Keys.Down)
            {
                GridMoveUpDown(1);
                //e.Handled = true;
                //return;
            }
            #endregion

            #region page翻页
            if (e.KeyCode == Keys.PageUp)
            {
                GridPageUpDown(1);
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                GridPageUpDown(0);
            }
            #endregion

            base.OnKeyDown(e);
        }

        //4.Esc隐藏ShowCard，回车选择行记录，数字键选择记录
        private void TxtSearchChar_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyAsc = (int)e.KeyChar;

            if (keyAsc == 27 || keyAsc == 13)
            {
                //Esc取消
                if (keyAsc == 27)
                {
                    Hide();
                }
                //回车选择数据
                if (keyAsc == 13)
                {
                    SelectedGrid();
                }
                //选好数据回车跳转到下一个控件
                else if (keyAsc == 13)
                {
                    //SendKeys.Send("{Tab}");
                    //e.Handled = true;
                }
                //e.Handled = false;
                //base.OnKeyPress(e);
                //return;
            }
            base.OnKeyPress(e);
        }
        //鼠标滚动
        private void TxtSearchChar_MouseWheel(object sender, MouseEventArgs e)
        {
            int val = e.Delta;
            if (val > 0)//上滚
            {
                GridMoveUpDown(0);
            }
            else//下滚
            {
                GridMoveUpDown(1);
            }
            base.OnMouseWheel(e);
        }

        #endregion

    }
}
