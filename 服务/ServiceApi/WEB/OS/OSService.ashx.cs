using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace WEB.OS
{
    /// <summary>
    /// OSService 的摘要说明
    /// </summary>
    public class OSService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Uri urlRef = context.Request.UrlReferrer;
            if (urlRef != null)
            {
                string host = urlRef.Host;
                int UserID = DAL.DomainDAL.GetUserID(host);
                SqlParameter[] pramsWhere = { };
                List<OnlineServicesEntity> OSList = DAL.OnlineServicesDAL.GetList<Model.OnlineServicesEntity>("*", pramsWhere, "OrderNum");
                context.Response.ContentType = "text/plain";
                int type = UICommon.Util.ConvertToInt32(context.Request["type"]);
                string osHtml = GetHtml(type);
                string OsContent = string.Empty;
                string osHost = DAL.ServiceApiConfigurationDAL.Get_98(2, "ServiceUrl").ServiceUrl;
                switch (type)
                {
                    case 0:
                        OsContent = GetOSContent0(OSList, osHost);
                        break;
                    default:
                        break;
                }

                osHtml = osHtml.Replace("{{OS:Content}}", OsContent);
                osHtml = osHtml.Replace("{{OS:Host}}", osHost);

                osHtml = osHtml.Replace("\r\n", "").Replace("\n", "").Replace("\"", "'");
                context.Response.Write("document.write(\"" + osHtml + "\"); ");
            }
        }
        #region 获取html代码

        private string GetHtml(int type)
        {
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "OS/" + type + "/OS.html";
            string txt = UICommon.FileHelper.GetFileTxt(path);
            return txt;
        }

        public string GetOSContent0(List<OnlineServicesEntity> OSList, string osHost)
        {
            System.Text.StringBuilder osHtml = new System.Text.StringBuilder();
            foreach (OnlineServicesEntity item in OSList)
            {
                osHtml.Append(" <li><span>" + item.Title + "</span> <a target=\"_blank\" ");
                osHtml.Append("href=\"tencent://message/?uin=" + item.ValueNum + "&Site=客服&Menu=yes\" >");
                osHtml.Append("<img border=\"0\" src=\"http://wpa.qq.com/pa?p=1:" + item.ValueNum + ":1\" alt=\"点击这里给我发消息\" title=\"点击这里给我发消息\" />");
                osHtml.Append("</a> </li> ");
            }
            //osHtml.Append("<li>");
            //osHtml.Append("<div class=\"div_clear\"></div>");
            //osHtml.Append("</li>");
            return osHtml.ToString();
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}