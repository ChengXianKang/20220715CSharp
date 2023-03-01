using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using Utils.HWDataPush.Entity;

namespace Utils.HWDataPush
{
    public class TXDPushData
    {
        private string _apiURL;
        // 添加https
        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受     
        }

        /// <summary>
        /// 系统配置
        /// </summary>
        private HuaweiPushDataConfig _config;
        /// <summary>
        /// Token
        /// </summary>
        private GetTokenUtil _token;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="config"></param>
        public TXDPushData(HuaweiPushDataConfig config)
        {
            _config = config;
            _apiURL = config.ApiURL;
            _token = new GetTokenUtil(config.TokenURL, config.TokenKey, config.TokenSecur);
        }

        /// <summary>
        /// 发送数据
        /// end添加https
        /// </summary>
        /// <param name="postData"></param>
        /// <returns></returns>
        public string sendPushData(string postData)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream requestStream = null;
            Stream responseStream = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(_apiURL);
                // 添加https
                if (_apiURL.Substring(0, 8) == "https://")
                {
                    request.UserAgent = DefaultUserAgent;
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                // end添加https

                request.Method = "POST";
                request.ContentLength = 0;
                request.ContentType = "application/json;charset=UTF-8";
                string authorization = "Bearer " + _token.getDynamicToken();
                request.Headers.Add("Authorization", authorization);
                if (!string.IsNullOrEmpty(postData))//如果传送的数据不为空，并且方法是post
                {
                    var encoding = new UTF8Encoding();
                    var bytes = Encoding.GetEncoding("UTF-8").GetBytes(postData);//编码方式按自己需求进行更改，我在项目中使用的是UTF-8
                    request.ContentLength = bytes.Length;
                    using (requestStream = request.GetRequestStream())
                    {
                        requestStream.Write(bytes, 0, bytes.Length);
                    }
                }

                using (response = (HttpWebResponse)request.GetResponse())
                {
                    var responseValue = string.Empty;
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        _token.expiresstr = "0";
                        var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                        throw new ApplicationException(message);
                    }
                    // grab the response
                    using (responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }
                    return responseValue;
                }

            }
            catch (Exception e)
            {
                _token.expiresstr = "0";
                //MessageBox.Show("sendRequest:" + e.Message);
                //log4Helper.WriteLog("sendRequest:", e);
                throw e;
            }
            finally
            {
                if (requestStream != null)
                    requestStream.Close();
                if (responseStream != null)
                    responseStream.Close();
                if (response != null)
                {
                    response.Close();
                    //response.Dispose();
                }
                if (request != null)
                    request.Abort();
            }
        }
    }
}
