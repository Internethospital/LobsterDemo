using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.CoreFrame;
using Core.Common.Data;
using Core.Common.Helper;
using Lobster.Web.Demo.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Lobster.Web.Demo.Controllers
{
    /// <summary>
    /// 书籍管理
    /// </summary>
    public class BookController : WebControllerBase
    {

        public ActionResult Book()
        {
            return View();
        }

        public ActionResult BookForm()
        {
            return View();
        }

        /// <summary>
        /// 获取应用数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object GetBookList()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("bookName", Request.Query["txtname"]);
            dic.Add("page", Request.Query["page"]);//当前页
            dic.Add("limit", Request.Query["limit"]);//每页多少条
            var response = RestHelper.GetResponseData("Lobster.Service.Demo", "/demo/v1/book/getbookdata", dic);
           
            if (response.Data != null)
            {
                var data = response.Data.GetData<dynamic>("data");
                var count = Convert.ToInt32(response.Data.GetData<string>("count"));
                if (count > 0)
                    return ToTableJson(count, data);
                else
                    return new Response(1, "无应用数据");
            }
            else
            {
                return new Response(1, "无应用数据");
            }
        }

        [HttpPost]
        public object SaveBook()
        {
            Book book = new Book();
            book.Id = Convert.ToInt32(Request.Form["Id"]);
            book.BookName = Request.Form["BookName"].ToString();
            book.BuyPrice = Convert.ToDecimal(Request.Form["BuyPrice"]);
            book.Flag= Convert.ToInt32(Request.Form["Flag"]);
            book.WorkId= Convert.ToInt32(Request.Form["WorkId"]);

            var request = new RestRequest();
            request.Resource = "/demo/v1/book/savebook";
            var response = RestHelper.ExecutePost<Response, Book>("Lobster.Service.Demo", request, book);
            return response;
        }

        [HttpPost]
        public object DeleteBook()
        {
            var dic = new Dictionary<string, object>();
            dic.Add("Id", Request.Form["Id"]);
            var response = RestHelper.GetResponseData("Lobster.Service.Demo", "/demo/v1/book/deletebook", dic, Method.POST);
            return response.Data;
        }
    }
}