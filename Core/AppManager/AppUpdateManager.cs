using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WPFCheatUITemplate.Configuration;
using WPFCheatUITemplate.Core.Net;
using WPFCheatUITemplate.Core.Net.Json;

namespace WPFCheatUITemplate.Core.AppManager
{
    public class AppUpdateManager
    {
        class UpdateConfigAddress
        {
            public string version = "";
        }

        public static bool Update()
        {
            bool islatest = false;

            var serverVersion = GetServerVersion();

            if (serverVersion == Configure.version)
            {
                islatest = true;
            }
            else
            {
                islatest = false;
            }
           
            return islatest;
        }



        private static Version GetServerVersion()
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = Configure.ConfigAddress,//URL     必需项  
                Method = "GET",//URL     可选项 默认为Get  
                Timeout = 100000,//连接超时时间     可选项默认为100000  
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000  
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写  
                Cookie = "",//字符串Cookie     可选项  
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0",//用户的浏览器类型，版本，操作系统     可选项有默认值  
                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值  
                ContentType = "application/json",//返回类型    可选项有默认值  
                ResultType = ResultType.String,//返回数据类型，是Byte还是String  
            };
            var ret = http.GetHtml(item);
            string html = ret.Html;
            var json = JsonHelper.JsonDes<UpdateConfigAddress>(html);
            var  serverVersion = new Version((json as UpdateConfigAddress).version);
            return serverVersion;

        }

    }
}
