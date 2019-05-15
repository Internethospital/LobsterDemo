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
    [Route("sso/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class AppController : ApiControllerBase
    {
        private readonly IApiHelper _apiHelper;//api辅助

        /// <summary>
        /// 应用管理
        /// </summary>
        /// <param name="apiHelper"></param>
        public AppController(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        /// <summary>
        /// 获取应用信息
        /// </summary>
        /// <returns>应用信息</returns>
        [HttpGet]
        public object GetAppData()
        {
            int workId = Convert.ToInt32(Request.Query["workId"]);
            int page = Convert.ToInt32(Request.Query["page"]);
            int limit = Convert.ToInt32(Request.Query["limit"]);
            var isforbidden = Convert.ToBoolean(Request.Query["isforbidden"]);
            string appName = Request.Query["appName"];
            var response = new Response();
            PageInfo pageinfo = new PageInfo(limit, page);
            var data = _apiHelper.NewDao<AppDao>().GetAppData(workId, isforbidden, appName, ref pageinfo);

            response.AddData("data", data);
            response.AddData("count", pageinfo.totalRecord);
            return response;
        }

        /// <summary>
        /// 获取应用的简化信息
        /// 可用于前台下拉框绑定数据源
        /// </summary>
        /// <returns>应用信息</returns>
        [HttpGet]
        public object GetAppSimpleData()
        {
            int workId = Convert.ToInt32(Request.Query["workId"]);
            var response = new Response();
            var data = _apiHelper.NewDao<AppDao>().GetAppSimpleData(workId);
            response.AddData("data", data);
            return response;
        }

        /// <summary>
        /// 验证用户编码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public object CheckAppKey()
        {
            string AppKey = Request.Query["AppKey"];
            var response = new Response();
            var exist = NewDao<AppDao>().CheckAppKey(AppKey);
            response.AddData("data", exist);
            return response;
        }

        /// <summary>
        /// 保存应用数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object SaveAppData()
        {
            int workID = Convert.ToInt32(Request.Form["workID"]);
            int appID = Convert.ToInt32(Request.Form["appID"]);
            string appKey = Request.Form["appKey"];
            string appName = Request.Form["appName"];
            int appType = Convert.ToInt32(Request.Form["appType"]);
            string appUrl = Request.Form["appUrl"];
            string appImage = Request.Form["appImage"];
            int sortNo = Convert.ToInt32(Request.Form["sortNo"]);
            string username = Request.Form["username"];

            SSO_App app = new SSO_App();
            app.WorkId = workID;
            app.AppId = appID;
            app.AppKey = appKey;
            app.AppName = appName;
            app.AppType = appType;
            app.AppUrl = appUrl;
            app.AppImage = appImage;

            var response = new Response();
            app.CreateUser = username;
            app.CreateDate = DateTime.Now.ToCstTime();
            if (appID == 0)
            {
                app.DelFlag = 1;  //新增默认为停用状态
                app.SortNo = Convert.ToInt32(_apiHelper.NewDao<AppDao>().GetMaxSortNo(workID)) + 1;
                app.AppKey = "XYS" + DateTime.Now.ToCstTime().ToString("yyyyMMddHHmmssfff");
            }
            else
            {
                app.SortNo = sortNo;
                app.AppKey = appKey;
            }
            var result = _apiHelper.NewDao<AppDao>().SaveAppData(app);
            response.AddData("result", result);
            return response;
        }

        /// <summary>
        /// 启用,停用应用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object FlagApp()
        {
            int appID = Convert.ToInt32(Request.Form["appID"]);
            int delFlag = Convert.ToInt32(Request.Form["delFlag"]);

            var response = new Response();
            var result = _apiHelper.NewDao<AppDao>().FlagApp(appID, delFlag);
            response.AddData("result", result);
            return response;
        }

        /// <summary>
        /// 删除应用
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object DeleteApp()
        {
            int appID = Convert.ToInt32(Request.Form["appID"]);

            var response = new Response();
            var result = _apiHelper.NewDao<AppDao>().DeleteApp(appID);
            response.AddData("result", result);
            return response;
        }

        /// <summary>
        /// 排序应用 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public object AlterSortOrder()
        {
            int oldAppID = Convert.ToInt32(Request.Form["oldAppID"]);
            int oldSortNo = Convert.ToInt32(Request.Form["oldSortNo"]);
            int newAppID = Convert.ToInt32(Request.Form["newAppID"]);
            int newSortNo = Convert.ToInt32(Request.Form["newSortNo"]);

            var response = new Response();
            var result = _apiHelper.NewDao<AppDao>().AlterSortOrder(oldAppID, oldSortNo, newAppID, newSortNo);
            response.AddData("result", result);
            return response;
        }

        /// <summary>
        /// 获取SSOApp 
        /// </summary>
        [HttpGet]
        public object GetSSOApp()
        {
            string appKey = Request.Query["appKey"];
            var response = new Response(); 
            var data = _apiHelper.NewDao<AppDao>().GetSSOApp(appKey);
            response.AddData("data", data);
            return response;
        }
    }
}