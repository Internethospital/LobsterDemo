using Core.Common.CoreFrame;
using Core.Common.Data;
using Core.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Lobster.Service.Demo.Dao;
using System;

namespace Lobster.Service.Demo.Controllers
{
    /// <summary>
    /// 应用管理
    /// </summary>
    [ApiVersion("1.0")]
    [Route("demo/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class BookController : ApiControllerBase
    {
        private readonly IApiHelper _apiHelper;//api辅助

        /// <summary>
        /// 应用管理
        /// </summary>
        /// <param name="apiHelper"></param>
        public BookController(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        /// <summary>
        /// 获取书籍
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object GetBookData(string bookName,int page,int limit)
        {
            //string bookName = Request.Query["bookName"];
            //int page = Convert.ToInt32(Request.Query["page"]);
            //int limit = Convert.ToInt32(Request.Query["limit"]);

            var response = new Response();
            PageInfo pageinfo = new PageInfo(limit, page);
            pageinfo.OrderBy = new string[] { "Id" };
            var data = _apiHelper.NewDao<BookDao>().GetBookData(bookName, ref pageinfo);

            response.AddData("data", data);
            response.AddData("count", pageinfo.totalRecord);
            return response;
        }
        /// <summary>
        /// 保存书籍
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object SaveBook()
        {
            var response = new Response();
            return response;
        }

        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object DeleteBook()
        {
            var response = new Response();
            return response;
        }
    }
}