using Books.Models;
using efwplusWinform.Controller;
using System.Collections.Generic;

namespace Books.Winform.IView
{
    public interface IfrmBookManager : IBaseView
    {
        //给网格加载数据
        List<Book> bookList { get; set; }
        //当前维护的书籍
        Book currBook { get; set; }
        //界面狀態
        frmBookState viewState { set; }
        //新增清空表單
        void clearForm();
    }

    public enum frmBookState
    {
        默認,編輯
    }
}
