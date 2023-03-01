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

namespace Utils.HWDataPush
{
    public class TXDToken
    {
        public string scope { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }

    public class GetTokenUtil
    {
        //测试环境与生产环境不一样
        //测试环境https://api-beta.huawei.com:443/oauth2/token
        //生产环境https://openapi.huawei.com:443/oauth2/token
        //测试环境与生产环境tokenKey与tokenSecury均不一样
        private string tokenURL;//测试环境
        private string tokenKey;   //key
        private string tokenSecury;//value
        //
        private static volatile TXDToken tokenMap = null;
        public volatile string expiresstr = "0";
        //private GetTokenUtil tokenUtil;
        private Object o = new Object();
        public bool is_expires = true;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="url">tokenURL</param>
        /// <param name="key">tokenKey</param>
        /// <param name="secury">tokenSecury</param>
        public GetTokenUtil(string url, string key, string secury)
        {
            tokenURL = url;
            tokenKey = key;
            tokenSecury = secury;
        }

        //public String getDynamicToken()
        //{
        //    System.DateTime currentTime = new System.DateTime();
        //    long currentTimeMillis = currentTime.Millisecond;
        //    //int currentTimeMillis = Environment.TickCount;
        //    long expires = long.Parse(expiresstr);
        //    if (!is_expires) expires = 0;
        //    if (expires <= currentTimeMillis)
        //    {
        //        // 判断时间轴是否大于
        //        TXDToken mapToken = getAccessToken();
        //        String strExpires = mapToken.expires_in;
        //        long lExpires = long.Parse(strExpires);
        //        bool min = lExpires <= 5;
        //        while (min)
        //        {// 小于5秒的token重新获取,服务端被设置为5秒获取新token
        //            mapToken = getAccessToken();
        //            strExpires = mapToken.expires_in;
        //            lExpires = long.Parse(strExpires);
        //            min = lExpires > 5;
        //        }
        //        // 设置时间轴
        //        System.DateTime endTime = new System.DateTime();
        //        expires = endTime.Millisecond + (lExpires - 5) * 1000;

        //        //expires = Environment.TickCount + (lExpires - 5) * 1000;
        //        expiresstr = expires + "";
        //        // 缓存token
        //        tokenMap = mapToken;
        //    }
        //    return tokenMap.access_token;
        //}

        public String getDynamicToken()
        {
            System.DateTime currentTime = System.DateTime.Now;
            long currentTimeMillis = currentTime.Ticks;
            long expires = long.Parse(expiresstr);
            if (!is_expires) expires = 0;
            if (expires <= currentTimeMillis)
            {
                // 判断时间轴是否大于
                TXDToken mapToken = getAccessToken();
                String strExpires = mapToken.expires_in;
                long lExpires = long.Parse(strExpires);
                bool min = lExpires <= 5;
                while (min)
                {// 小于5秒的token重新获取,服务端被设置为5秒获取新token
                    mapToken = getAccessToken();
                    strExpires = mapToken.expires_in;
                    lExpires = long.Parse(strExpires);
                    min = lExpires > 5;
                }
                // 设置时间轴
                System.DateTime endTime = System.DateTime.Now;
                expires = endTime.Ticks + (lExpires - 5) * 10000000;
                expiresstr = expires + "";
                // 缓存token
                tokenMap = mapToken;
            }
            return tokenMap.access_token;
        }
        // 添加https
        private readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受     
        }
        // end添加https
        public TXDToken getAccessToken()
        {
            string result = sendRequestToken();
            tokenMap = JsonConvert.DeserializeObject<TXDToken>(result);
            LogHelper.Info("成功获取动态令牌信息Token：" + tokenMap.access_token);
            return tokenMap;
        }

        public string sendRequestToken()
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream requestStream = null;
            Stream responseStream = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(tokenURL);
                // 添加https
                if (tokenURL.Substring(0, 8) == "https://")
                {
                    request.UserAgent = DefaultUserAgent;
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                }
                // end添加https

                request.Method = "POST";
                request.ContentLength = 0;
                request.ContentType = "application/x-www-form-urlencoded";
                string authorization = "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(tokenKey + ":" + tokenSecury)).Trim();
                request.Headers.Add("Authorization", authorization);
                string postData = "grant_type=client_credentials";
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
                // MessageBox.Show("sendRequest:" + e.Message);
                //log4Helper.WriteLog("发送错误--152！", e);
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
