using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.CoreFrame;
using Core.Common.Data;
using Core.Common.Helper;
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
            string bookName =Request.Query["txtname"]; 
            int page = Convert.ToInt32(Request.Query["page"]); //当前页码
            int limit = Convert.ToInt32(Request.Query["limit"]);//每页数据量
            var dic = new Dictionary<string, object>();
            dic.Add("page", page);
            dic.Add("limit", limit);
            dic.Add("bookName", bookName);
            var response = RestHelper.GetResponseData("Lobster.Service.Demo", "demo/v1/Book/GetBookData", dic);
           
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
    }
}