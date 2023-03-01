using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Diagnostics;

namespace Utils
{
    public class HttpHelper
    {
        /// <summary>
        /// POST Method
        /// </summary>
        /// <param name="methodUrl">Url</param>
        /// <param name="postStr">Post参数字符串</param>
        /// <returns>应答数据字符串</returns>
        public static string ExecutePost(String methodUrl, String postStr)
        {
            string content = string.Empty;

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(methodUrl);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                //request.ContentType = "application/json";
                request.ReadWriteTimeout = 30 * 1000;

                byte[] data = Encoding.UTF8.GetBytes(postStr);
                request.ContentLength = data.Length;

                Stream myRequestStream = request.GetRequestStream();

                myRequestStream.Write(data, 0, data.Length);
                myRequestStream.Close();

                Debug.WriteLine("【Post】" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Debug.WriteLine(methodUrl);
                Debug.WriteLine(postStr);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                content = myStreamReader.ReadToEnd();
                myStreamReader.Close();
            }
            catch (Exception ex)
            {
                LogHelper.Error("HttpHelper.ExecutePost方法执行失败.", ex);
            }
            Debug.WriteLine("【Response】" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            Debug.WriteLine(content);
            return content;
        }

        /// <summary>
        /// 生成POST指令的参数字符串
        /// </summary>
        /// <param name="dicList">参数表</param>
        /// <returns></returns>
        public static String BuildQueryStr(Dictionary<String, String> dicList)
        {
            String postStr = "";

            foreach (var item in dicList)
            {
                postStr += item.Key + "=" + HttpUtility.UrlEncode(item.Value, Encoding.UTF8) + "&";
            }
            postStr = postStr.Substring(0, postStr.LastIndexOf('&'));
            return postStr;
        }
    }
}
