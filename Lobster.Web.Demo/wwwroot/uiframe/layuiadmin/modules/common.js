/**

 @Name：layuiAdmin 公共业务
 @Author：贤心
 @Site：http://www.layui.com/admin/
 @License：LPPL
    
 */

layui.define(function (exports) {
    var $ = layui.$
    , layer = layui.layer
    , laytpl = layui.laytpl
    , setter = layui.setter
    , view = layui.view
    , admin = layui.admin

    //公共业务的逻辑处理可以写在此处，切换任何页面都会执行
    //……



    //退出
    admin.events.logout = function () {
        //退出报表服务
        $.ajax({
            url: "https://ih.efwplus.cn:8801/WebReport/ReportServer?op=fs_load&cmd=ssout",//单点登录的报表服务器  
            dataType: "jsonp",//跨域采用jsonp方式  
            jsonp: "callback",
            timeout: 5000,//超时时间（单位：毫秒）  
            success: function (data) {
                if (data.status === "logout") {
                   
                }
            },
            error: function () {
                // 登出失败（超时或服务器其他错误）  
            }
        });

        //执行退出应用系统接口
        admin.req({
            //url: layui.setter.base + 'json/user/logout.js'
            url: "/Controller.aspx?controller=LoginController&method=Logout"
          , type: 'get'
          , data: {}
          , done: function (res) { //这里要说明一下：done 是只有 response 的 code 正常才会执行。而 succese 则是只要 http 为 200 就会执行
              //清空本地记录的 token，并跳转到登入页
              admin.exit(function () {
                  location.href = 'login/login.html';
              });
          }
        });
    };


    //对外暴露的接口
    exports('common', {});
});