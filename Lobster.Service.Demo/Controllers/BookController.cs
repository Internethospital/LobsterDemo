using Core.Common.CoreFrame;
using Core.Common.Data;
using Core.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Lobster.Service.Demo.Dao;
using System;
using Lobster.Service.Demo.Entity;
using Microsoft.AspNetCore.Cors;
using qcloudsms_csharp;

namespace Lobster.Service.Demo.Controllers
{
    /// <summary>
    /// 书籍管理
    /// </summary>
    [ApiVersion("1.0")]
    [EnableCors("AllowSameDomain")]//启用跨域
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
        public ActionResult<Response> GetBookData(int page, int limit,string bookName)
        {
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
        public ActionResult<Response> SaveBook([FromBody]Book book)
        {
            var response = new Response();

            var result = NewDao<AbstractDao>().Save<Book>(book);
            response.AddData("result", result);
            
            return response;
        }

        /// <summary>
        /// 删除书籍
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Response> DeleteBook([FromBody]int Id)
        {
            var response = new Response();

            var result = NewDao<BookDao>().DeleteBook(Id);
            response.AddData("result", result);
            return response;
        }

        [HttpGet]
        public void TestMessage()
        {
            // 短信应用 SDK AppID
            int appid = 1400268227;
            // 短信应用 SDK AppKey
            string appkey = "c1d2cbfb30b5df4e8f0f4be3bd905964";
            // 需要发送短信的手机号码
            string[] phoneNumbers = { "21212313123", "12345678902", "12345678903" };
            // 短信模板 ID，需要在短信控制台中申请
            int templateId = 440141; // NOTE: 这里的模板 ID`7839`只是示例，真实的模板 ID 需要在短信控制台中申请
                                   // 签名
            string smsSign = "网医智捷"; // NOTE: 签名参数使用的是`签名内容`，而不是`签名ID`。这里的签名"腾讯云"只是示例，真实的签名需要在短信控制台申请

            SmsSingleSender ssender = new SmsSingleSender(appid, appkey);
            var result = ssender.sendWithParam("86", "18942561085",
                templateId, new[] { "5678","3" }, smsSign, "", "");
            Console.WriteLine(result);

        }
    }
}