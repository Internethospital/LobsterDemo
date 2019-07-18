using Books.Models;
using Books.Winform.IView;
using efwplusWinform.Business;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Books.Winform.Viewform
{
    //Wcf界面的代码和Winform界面的代码是一模一样的
    public partial class frmBookManager : BaseFormBusiness, IfrmBookManager
    {
        public frmBookManager()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            InvokeController("Exit");
        }

        #region 界面接口

        public List<Book> bookList
        {
            set
            {
                gridBooks.DataSource = value;
            }
            get
            {
                return gridBooks.DataSource as List<Book>;
            }
        }

        private Book _book;
        public Book currBook
        {
            get
            {
                _book.BookName = txtBookName.Text;
                _book.BuyPrice = Convert.ToDecimal(txtPrice.Text);
                _book.BuyDate = txtDate.Value;
                _book.Flag = ckBack.Checked ? 1 : 0;
                return _book;
            }
            set
            {
                _book = value;
                txtBookName.Text = _book.BookName;
                txtPrice.Text = _book.BuyPrice.ToString();
                //txtDate.Value = _book.BuyDate;
                ckBack.Checked = _book.Flag == 0 ? false : true;
            }
        }

        public frmBookState viewState
        {
            set
            {
                switch (value)
                {
                    case frmBookState.默認:
                        btnAdd.Enabled = true;
                        btnAlter.Enabled = true;
                        btnDelete.Enabled = true;
                        btnClose.Enabled = true;
                        gridBooks.Enabled = true;

                        btnSave.Enabled = false;
                        btnCancel.Enabled = false;

                        txtBookName.Enabled = false;
                        txtPrice.Enabled = false;
                        txtDate.Enabled = false;
                        ckBack.Enabled = false;

                        break;
                    case frmBookState.編輯:
                        btnAdd.Enabled = false;
                        btnAlter.Enabled = false;
                        btnDelete.Enabled = false;
                        btnClose.Enabled = false;
                        gridBooks.Enabled = false;

                        btnSave.Enabled = true;
                        btnCancel.Enabled = true;

                        txtBookName.Enabled = true;
                        txtPrice.Enabled = true;
                        txtDate.Enabled = true;
                        ckBack.Enabled = true;
                        break;
                }
            }
        }

        public void clearForm()
        {
            txtBookName.Text = "";
            txtPrice.Text = "0.00";
            txtDate.Value = DateTime.Now;
            ckBack.Checked = false;

            txtBookName.Focus();
        }

        #endregion

        private void frmBookManager_OpenWindowBefore(object sender, EventArgs e)
        {
            InvokeController("GetBooks");
        }

        private void frmBookManager_ExitApplicationBefore(object sender, EventArgs e)
        {
            //MessageBox.Show("正在关闭界面");
        }

        private void frmBookManager_Load(object sender, EventArgs e)
        {
            InvokeController("GetBooks");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            InvokeController("AddBook");
        }

        private void btnAlter_Click(object sender, EventArgs e)
        {
            if (gridBooks.CurrentCell == null) return;
            InvokeController("AlterBook");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridBooks.CurrentCell == null) return;
            if (MessageBox.Show( "是否刪除此記錄？", "詢問", MessageBoxButtons.YesNo) == DialogResult.Yes)
                InvokeController("DeleteBook");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            InvokeController("SaveBook");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            InvokeController("CancelBook");
 
        }

        private void gridBooks_CurrentCellChanged(object sender, EventArgs e)
        {
            if (gridBooks.CurrentCell == null) return;
            InvokeController("SelectBook", gridBooks.CurrentCell.RowIndex);
        }
    }
}
