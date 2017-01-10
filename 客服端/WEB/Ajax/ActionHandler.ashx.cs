using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UICommon;

namespace WEB.Ajax
{
    /// <summary>
    /// UserPictureBooHandler 的摘要说明
    /// </summary>
    public class ActionHandler : BaseHandler
    {
        ResponseData responseData = new ResponseData()
        {
            code = ReturnCode.error,
            msg = "未配置",
            data = null
        };
        int UserID = 0;
        protected override ResponseData ProcessResponse(HttpContext context)
        {
            UserID = UICommon.SessionAccess.UserID;
            if (UserID <= 0)
            {
                responseData.msg = "登录异常,请刷新重试！";
                return responseData;
            }
            try
            {
                string action = Request["action"];
                switch (action)
                {
                    case "GetSupportInfo"://获得题库支持信息
                        GetSupportInfo();
                        break;
                    default:
                        responseData.msg = "无效方法";
                        break;
                }
            }
            catch (Exception ex)
            {
                responseData.msg = ex.Message;
                responseData.data = ex.ToString();
            }
            return responseData;
        }
        /// <summary>
        /// 获得题库支持信息
        /// </summary>
        private void GetSupportInfo()
        {
            
        }

    }
}