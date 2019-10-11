using Core.Common.CoreFrame;
using Core.Common.Data;
using Core.Common.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RestSharp;
using System;

namespace Doctor.Web.Controllers
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginController : WebControllerBase
    {
        [HttpPost]
        public object SSOUserAuth()
        {
            string ssotoken = Request.Form["ssotoken"];
            //string url = Request.Form["url"];
            string appkey = Request.Form["appkey"];
            //string ssoauthurl = Request.Form["ssoauthurl"];
            //string ssologinurl = Request.Form["ssologinurl"];
            string right = Request.Form["right"];

            //SysLoginRight sysLoginRight = new SysLoginRight();
            //sysLoginRight.DeptId = ConvertExtend.ToInt32(response.data["DeptId"], 0);
            //sysLoginRight.DeptName = response.data["DeptName"];
            //sysLoginRight.EmpId = ConvertExtend.ToInt32(response.data["EmpId"], 0);
            //sysLoginRight.EmpName = response.data["EmpName"];
            //sysLoginRight.IsAdmin = ConvertExtend.ToInt32(response.data["IsAdmin"], 0);
            //sysLoginRight.UserId = ConvertExtend.ToInt32(response.data["UserId"], 0);
            //sysLoginRight.WorkId = ConvertExtend.ToInt32(response.data["WorkId"], 0);
            //sysLoginRight.WorkName = response.data["WorkName"];
            //HttpContext.Session.SetString(HttpContext.Session.Id, ssoToken);
            //HttpContext.Session.Set<SysLoginRight>("sysLoginRight", sysLoginRight);

            return new Response(0, "ok");
        }

    }
}