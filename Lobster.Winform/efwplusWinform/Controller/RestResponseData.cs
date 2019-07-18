using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace efwplusWinform.Controller
{
    public class RestResponseData
    {
        /// <summary>
        /// 状态 0正常 1错误
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 提示内容
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 数据字典
        /// </summary>
        public Dictionary<string, string> data { get; set; }

        public RestResponseData()
        {
            code = 0;
            msg = "";
            data = new Dictionary<string, string>();
        }

        public RestResponseData(int _code, string _msg)
        {
            code = _code;
            msg = _msg;
            data = new Dictionary<string, string>();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="item"></param>
        public void AddData<T>(string key, T item)
        {
            if (data.ContainsKey(key))
                throw new Exception("添加了重复的key");
            if (typeof(T).Equals(typeof(String)) || typeof(T).Equals(typeof(int)) || typeof(T).Equals(typeof(decimal)))
            {
                data.Add(key, item.ToString());
            }
            else
            {
                JsonSerializerSettings set = new JsonSerializerSettings();
                set.NullValueHandling = NullValueHandling.Ignore;
                data.Add(key, JsonConvert.SerializeObject(item, set));
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetData<T>(string key)
        {
            if (!data.ContainsKey(key))
                throw new Exception("不存在key");
            if (typeof(T).Equals(typeof(String)) || typeof(T).Equals(typeof(int)) || typeof(T).Equals(typeof(decimal)))
            {
                return (T)((object)data[key]);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(data[key]);
            }
        }
        /// <summary>
        /// 获取Json数据
        /// </summary>
        /// <returns></returns>
        public string GetJsonData()
        {
            return JsonConvert.SerializeObject(this);
        }
        /// <summary>
        /// 获取指定Json数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetJsonData(string key)
        {
            return data[key];
        }

        /// <summary>
        /// 导入Json数据
        /// </summary>
        /// <param name="jsondata"></param>
        public void SetJsonData(string jsondata)
        {
            if (string.IsNullOrEmpty(jsondata) == false)
            {
                RestResponseData vd = JsonConvert.DeserializeObject<RestResponseData>(jsondata);
                code = vd.code;
                msg = vd.msg;
                data = vd.data;
            }
        }
    }
}
