using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UICommon
{
    /// <summary>
    ///  返回值
    /// </summary>
    public class ResponseData
    {
        /// <summary>
        /// 返回的状态码
        /// </summary>
        public ReturnCode code;
        /// <summary>
        /// 返回状态码对应的消息
        /// </summary>
        public string msg;
        /// <summary>
        /// 附加内容
        /// </summary>
        public object data;

        public static object GetResponseData(ReturnCode code, string msg, object data)
        {
            object obj = new { code = code, msg = msg, data = data };
            return obj;
        }
         
    }
    /// <summary>
    /// 操作代码 返回给前台JS
    /// </summary>
    public enum ReturnCode
    {
        /// <summary>
        /// 失败 0
        /// </summary>
        error = 0,
        /// <summary>
        /// 成功 1
        /// </summary>
        success = 1,
        /// <summary>
        /// 未登录
        /// </summary>
        nologo = -8,
    }
}
