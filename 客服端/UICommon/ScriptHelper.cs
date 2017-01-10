using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace UICommon
{
    public class ScriptHelper
    {
        /// <summary>
        /// 当前页面对象
        /// </summary>
        private static Page CurrentPage
        {
            get
            {
                return ((Page)HttpContext.Current.Handler);
            }
        }
        /// <summary>
        /// 编码脚本
        /// </summary>
        /// <param name="script">要编码的脚本</param>
        /// <returns></returns>
        private static string EncodeScriptText(string script)
        {
            return script.Replace("\r\n", "").Replace(@"\", @"\\").Replace("\"", "\\\"").Replace("\n", @"\n").Replace("\t", @"\t").Replace("\a", @"\a").Replace("\b", @"\b");
        }
        /// <summary>
        /// 客户端脚本提示
        /// </summary> 
        /// <param name="message">要弹出的内容</param>
        public static void Alert(string message)
        {
            CurrentPage.ClientScript.RegisterStartupScript(CurrentPage.GetType(), "", "<script>alert(\"" + EncodeScriptText(message) + "\");</script>");
        }
        /// <summary>
        /// 显示客户端消息并重定向某个URL
        /// </summary>
        /// <param name="message">要弹出的消息</param>
        /// <param name="url">重定向的URL</param>
        public static void ShowAndRedirect(string message, string url)
        {
            string msg = EncodeScriptText(message);
            StringBuilder builder = new StringBuilder();
            builder.Append("<script language='javascript'>");
            builder.AppendFormat("var a=alert('{0}');", msg);
            builder.Append(@"if(typeof(a)!='undefined') { a.done(function(){location.href='" + url + "'}); }");
            builder.Append("else {  location.href='" + url + "'}");
            builder.Append("</script>");
            CurrentPage.ClientScript.RegisterStartupScript(CurrentPage.GetType(), "", builder.ToString());
        }

    }
}
