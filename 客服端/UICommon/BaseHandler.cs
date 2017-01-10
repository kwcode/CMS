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
        public HttpRequest Request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }
        public HttpResponse Response
        {
            get
            {
                return HttpContext.Current.Response;
            }
        }
        public void ProcessRequest(HttpContext context)
        {
            var data = ProcessResponse(context);
            var newData = new { code = data.code, data = data.data, msg = data.msg };
            context.Response.ContentType = "application/json";
            string jsonData = JsonConvert.SerializeObject(newData);
            context.Response.Write(jsonData);
        }
        /// <summary>
        /// 定义输出函数
        /// </summary>
        /// <returns></returns>
        protected virtual ResponseData ProcessResponse(HttpContext context)
        {
            ResponseData data = new ResponseData { code = ReturnCode.error, data = "" };
            return data;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
