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
        public object GetBookList([FromQuery(Name = "page")]string page, [FromQuery(Name = "limit")]string limit, [FromQuery(Name = "txtname")]string bookName)
        {
            //实例化RestRequest
            var request = new RestRequest("/demo/v1/book/getbookdata");
            //增加参数
            request.AddQueryParameter("page", page);
            request.AddQueryParameter("limit", limit);
            request.AddQueryParameter("bookName", bookName);
            //执行请求
            var responseData = RestHelper.ExecuteGet<Response>("Lobster.Service.Demo", request);

            if (responseData != null)
            {
                var data = responseData.GetData<dynamic>("data");
                var count = Convert.ToInt32(responseData.GetData<string>("count"));
                if (count > 0)
                    return ToTableJson(count, data);
                else
                    return new Response(1, "无数据");
            }
            else
            {
                return new Response(1, "无数据");
            }
        }

        [HttpPost]
        public object SaveBook([FromForm]Book book)
        {
            //实例化RestRequest
            var request = new RestRequest("/demo/v1/book/savebook");
            //增加参数
            request.AddJsonBody(book);
            //执行请求
            var responseData = RestHelper.ExecutePost<Response>("Lobster.Service.Demo", request);
            return responseData;
        }

        [HttpPost]
        public object DeleteBook([FromForm]int Id)
        {
            //实例化RestRequest
            var request = new RestRequest("/demo/v1/book/deletebook");
            //增加参数
            request.AddParameter("Id", Id);
            //执行请求
            var responseData = RestHelper.ExecutePost<Response>("Lobster.Service.Demo", request);

            return responseData;
        }
    }
}