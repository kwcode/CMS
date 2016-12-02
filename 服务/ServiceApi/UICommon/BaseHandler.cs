using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace UICommon
{
    public class BaseHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// Request
        /// 方便使用
        /// </summary>
        public HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }
        public HttpServerUtility Server
        {
            get
            {
                return HttpContext.Current.Server;
            }
        }
        public HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            try
            {
                var data = ProcessResponse(context);
                string jsonData = JsonConvert.SerializeObject(data);
                context.Response.Write(jsonData);
            }
            catch (Exception ex)
            {
                var data = new ResponseData { code = ReturnCode.error, data = ex.ToString(), msg = ex.Message };
                string jsonData = JsonConvert.SerializeObject(data);
                context.Response.Write(jsonData);
            }

        }
        /// <summary>
        /// 默认的返回值 { res = -1, desc = "无效的请求  " }
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual ResponseData ProcessResponse(HttpContext context)
        {
            ResponseData data = new ResponseData { code = ReturnCode.error, msg = "无效的请求", data = null };
            return data;
        }


        #region IHttpHandler 成员

        /// <summary>
        /// 是否允许重用
        /// 默认False
        /// </summary>
        public virtual bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion

    }
}
