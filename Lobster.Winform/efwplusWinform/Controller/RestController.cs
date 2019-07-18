/*
 *Wcf客户端控制器基类
 */

using System;
using System.Data;
using efwplusWinform.Controller;
using RestSharp;
using RestSharp.Authenticators;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections;

namespace efwplusWinform.Controller
{
 
    /// <summary>
    /// Winform控制器基类
    /// </summary>
    public class RestController : WinformController
    {
        private string userName = "default";
        private string accessKey = "default";

        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }

        public string AccessKey
        {
            get
            {
                return accessKey;
            }

            set
            {
                accessKey = value;
            }
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        public RestController()
        {
            
        }

        #region Rest通讯

        private string GetBaseUrl(string servicenname)
        {
           return  System.Configuration.ConfigurationSettings.AppSettings[servicenname];
        }

        public RestResponseData InvokeService(string servicenname, RestRequest request)
        {
            var restClient = new RestClient(GetBaseUrl(servicenname))
            {
                Authenticator = new HttpBasicAuthenticator(userName, accessKey)
            };

            //request.Method = Method.GET;

            request.AddHeader("Content-Type", "application/json");

            var response = restClient.Execute<RestResponseData>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var browserStackException = new ApplicationException(message, response.ErrorException);
                throw browserStackException;
            }

            return response.Data;
        }


        public Task<IRestResponse> InvokeServiceAsync(string servicenname, RestRequest request)
        {
            var restClient = new RestClient(GetBaseUrl(servicenname))
            {
                Authenticator = new HttpBasicAuthenticator(userName, accessKey)
            };

            //request.Method = Method.GET;

            request.AddHeader("Content-Type", "application/json");

            var tcs = new TaskCompletionSource<IRestResponse>();
            var rrah = restClient.ExecuteAsync(request, response =>
            {
                tcs.SetResult(response);
            });

            return tcs.Task;
        }

        #endregion

        #region 提供给python调用
        public RestResponseData requestget(string servicenname, string url, string jsonpara)
        {
            //实例化RestRequest
            var request = new RestRequest(url);
            if (string.IsNullOrEmpty(jsonpara) == false)
            {
                var para = JsonConvert.DeserializeObject<Hashtable>(jsonpara);
                foreach (DictionaryEntry de in para)
                {
                    request.AddQueryParameter(de.Key.ToString(), de.Value.ToString());
                }
            }
            request.Method = Method.GET;
            //执行请求
            var responseData = InvokeService(servicenname, request);
            return responseData;
        }

        public RestResponseData requestpost(string servicenname, string url, string jsonpara, string jsonbody)
        {
            //实例化RestRequest
            var request = new RestRequest(url);
            if (string.IsNullOrEmpty(jsonpara) == false)
            {
                var para = JsonConvert.DeserializeObject<Hashtable>(jsonpara);
                foreach (DictionaryEntry de in para)
                {
                    request.AddParameter(de.Key.ToString(), de.Value);
                }
            }

            if (string.IsNullOrEmpty(jsonbody) == false)
            {
                request.AddJsonBody(jsonbody);
            }

            request.Method = Method.POST;
            var responseData = InvokeService(servicenname, request);
            return responseData;
        }

        public string getjson(RestResponseData retdata, string key)
        {
            return retdata.GetJsonData(key);
        }

        public DataTable json2dt(string json)
        {
            return (DataTable)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(DataTable));
        }

        public string dt2json(DataTable dt)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(dt);
        }
        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        public object logininfo()
        {
            return LoginUserInfo;
        }
        #endregion
    }
}
