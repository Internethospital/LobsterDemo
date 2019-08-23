using Books.Models;
using Books.Winform.IView;
using efwplusWinform.Business.AttributeInfo;
using efwplusWinform.Controller;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Books.Winform.Controller
{
    [WinformController(DefaultViewName = "frmBookManager",Memo ="基于efwplus框架增删改查示例")]//在菜单上显示
    [WinformView(Name = "frmBookManager", ViewTypeName = "Books.Winform.ViewForm.frmBookManager")]//控制器关联的界面
    public class BookDemoController : RestController
    {
        IfrmBookManager _ifrmbookmanager;
        public override void Init()
        {
            _ifrmbookmanager = (IfrmBookManager)DefaultView;
        }

        /// <summary>
        /// 获取书籍
        /// </summary>
        [WinformMethod]
        public void GetBooks()
        {
            _ifrmbookmanager.viewState = frmBookState.默認;
            //实例化RestRequest
            var request = new RestRequest("/demo/v1/book/getbookdata");
            //增加参数
            request.AddQueryParameter("page", "1");
            request.AddQueryParameter("limit", "10");
            request.AddQueryParameter("bookName", "");
            request.Method = Method.GET;
            //通过wcf服务调用bookWcfController控制器中的GetBooks方法
            RestResponseData retdata = InvokeService("Lobster.Service.Demo",request);
            List<Book> list = retdata.GetData<List<Book>>("data");
            _ifrmbookmanager.bookList = list;
        }
        /// <summary>
        /// 新增
        /// </summary>
        [WinformMethod]
        public void AddBook()
        {
            _ifrmbookmanager.viewState = frmBookState.編輯;
            Book book = new Book();
            book.BuyDate = DateTime.Now;
            _ifrmbookmanager.currBook = book;
            _ifrmbookmanager.clearForm();
        }
        /// <summary>
        /// 修改
        /// </summary>
        [WinformMethod]
        public void AlterBook()
        {
            _ifrmbookmanager.viewState = frmBookState.編輯;
        }

        //保存
        [WinformMethod]
        public void SaveBook()
        {
            //实例化RestRequest
            var request = new RestRequest("/demo/v1/book/savebook");
            request.Method = Method.POST;
            request.AddJsonBody(_ifrmbookmanager.currBook);
            var responseData = InvokeService("Lobster.Service.Demo", request);

            GetBooks();
        }
        /// <summary>
        /// 刪除
        /// </summary>
        [WinformMethod]
        public void DeleteBook()
        {
            //实例化RestRequest
            var request = new RestRequest("/demo/v1/book/deletebook");
            //增加参数
            request.AddParameter("Id", _ifrmbookmanager.currBook.Id);
            request.Method = Method.POST;
            var responseData = InvokeService("Lobster.Service.Demo", request);

            GetBooks();
        }
        /// <summary>
        /// 取消
        /// </summary>
        [WinformMethod]
        public void CancelBook()
        {
            _ifrmbookmanager.viewState = frmBookState.默認;
        }
        /// <summary>
        /// 選定網格書籍
        /// </summary>
        /// <param name="Index">網格行索引</param>
        [WinformMethod]
        public void SelectBook(int Index)
        {
            _ifrmbookmanager.currBook = _ifrmbookmanager.bookList[Index];
        }
    }
}

